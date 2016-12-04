using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataBaseProvider;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Game;
using MetaData.Game.Roulette;
using MetaData.AgentUser;
using SuperMinersServerApplication.Controller.Trade;

namespace SuperMinersServerApplication.Controller
{
    /// <summary>
    /// 
    /// </summary>
    internal class PlayerController
    {
        #region Single

        private static PlayerController _instance = new PlayerController();

        internal static PlayerController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Fields

        public event Action<PlayerInfo> PlayerInfoChanged;
        public event Action<string> KickOutPlayer;

        public int RegisteredPlayersCount { get; private set; }
        public decimal AllMiners { get; private set; }
        public decimal AllOutputStones { get; private set; }

        private ConcurrentDictionary<string, PlayerRunnable> _dicOnlinePlayerRuns = new ConcurrentDictionary<string, PlayerRunnable>();

        #endregion

        public void Init()
        {
            this.RegisteredPlayersCount = DBProvider.UserDBProvider.GetAllPlayerCount();
            this.AllMiners = DBProvider.UserDBProvider.GetAllMinersCount();
            this.AllOutputStones = DBProvider.UserDBProvider.GetAllOutputStonesCount();
        }

        public bool RouletteWinVirtualAwardPayUpdatePlayer(string userName, RouletteAwardItem awardItem)
        {
            var runnable = this.GetOnlinePlayerRunnable(userName);
            if (runnable == null)
            {
                return false;
            }

            bool isOK = runnable.RouletteWinVirtualAwardPayUpdatePlayer(userName, awardItem);
            if (isOK)
            {
                NotifyPlayerClient(runnable.BasePlayer);
            }

            return isOK;
        }

        public int CheckUserAlipayAccountExist(string alipayAccount)
        {
            if (string.IsNullOrEmpty(alipayAccount))
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }
            int count = DBProvider.UserDBProvider.GetPlayerCountByAlipayAccount(alipayAccount);
            if (count == 0)
            {
                //不存在
                return OperResult.RESULTCODE_FALSE;
            }

            return OperResult.RESULTCODE_TRUE;
        }
        
        public int CheckUserAlipayRealNameExist(string alipayRealName)
        {
            if (string.IsNullOrEmpty(alipayRealName))
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }
            int count = DBProvider.UserDBProvider.GetPlayerCountByAlipayRealName(alipayRealName);
            if (count == 0)
            {
                //不存在
                return OperResult.RESULTCODE_FALSE;
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CheckUserIDCardNoExist(string IDCardNo)
        {
            if (string.IsNullOrEmpty(IDCardNo))
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }
            int count = DBProvider.UserDBProvider.GetPlayerCountByIDCardNo(IDCardNo);
            if (count == 0)
            {
                //不存在
                return OperResult.RESULTCODE_FALSE;
            }

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_SUCCEED
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUser(string clientIP, string userName, string nickName, string password, 
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            int userCount = DBProvider.UserDBProvider.GetPlayerCountByUserName(userName);
            if (userCount > 0)
            {
                return OperResult.RESULTCODE_REGISTER_USERNAME_EXIST;
            }

            if (!string.IsNullOrEmpty(alipayAccount))
            {
                if (this.CheckUserAlipayAccountExist(alipayAccount) == OperResult.RESULTCODE_TRUE)
                {
                    return OperResult.RESULTCODE_REGISTER_ALIPAY_EXIST;
                }
            }
            //if (this.CheckUserAlipayRealNameExist(alipayRealName) == OperResult.RESULTCODE_TRUE)
            //{
            //    return OperResult.RESULTCODE_REGISTER_ALIPAY_EXIST;
            //}
            if (!string.IsNullOrEmpty(IDCardNo))
            {
                if (this.CheckUserIDCardNoExist(IDCardNo) == OperResult.RESULTCODE_TRUE)
                {
                    return OperResult.RESULTCODE_REGISTER_IDCARDNO_EXIST;
                }
            }

            //userCount = DBProvider.UserDBProvider.GetPlayerCountByNickName(nickName);
            //if (userCount > 0)
            //{
            //    return OperResult.RESULTCODE_REGISTER_NICKNAME_EXIST;
            //}

            bool Awardable = false;
            userCount = DBProvider.UserDBProvider.GetPlayerCountByRegisterIP(clientIP);
            if (userCount < GlobalConfig.RegisterPlayerConfig.UserCountCreateByOneIP)
            {
                Awardable = true;
            }

            //if (userCount >= GlobalConfig.RegisterPlayerConfig.UserCountCreateByOneIP)
            //{
            //    return 2;
            //}
            //if (userCount == 0)
            //{
            //    Awardable = true;
            //}

            List<WaitToReferAwardRecord> listWaitToAwardExpRecord = new List<WaitToReferAwardRecord>();
            //List<PlayerRunnable> listPlayerRun = new List<PlayerRunnable>();
            var trans = MyDBHelper.Instance.CreateTrans();

            try
            {
                PlayerInfo referrerLevel1player = null;
                if (invitationCode != GlobalData.TestInvitationCode)
                {
                    int awardLevelCount = GlobalConfig.AwardReferrerLevelConfig.AwardLevelCount;
                    if (awardLevelCount > 0)
                    {
                        string previousReferrerUserName = null;
                        referrerLevel1player = DBProvider.UserDBProvider.GetPlayerByInvitationCode(invitationCode);

                        if (Awardable)
                        {
                            if (referrerLevel1player != null)
                            {
                                var playerrun = this.GetOnlinePlayerRunnable(referrerLevel1player.SimpleInfo.UserName);
                                if (playerrun == null)
                                {
                                    playerrun = new PlayerRunnable(referrerLevel1player);
                                    this._dicOnlinePlayerRuns.TryAdd(playerrun.BasePlayer.SimpleInfo.UserName, playerrun);
                                }

                                if (referrerLevel1player.SimpleInfo.GroupType != PlayerGroupType.AgentPlayer)
                                {
                                    //如果玩家上级推荐人不是代理，才给推荐人奖励
                                    var awardRecord = new WaitToReferAwardRecord()
                                    {
                                        AwardLevel = 1,
                                        NewRegisterUserNme = userName,
                                        ReferrerUserName = referrerLevel1player.SimpleInfo.UserName
                                    };
                                    listWaitToAwardExpRecord.Add(awardRecord);
                                    //listPlayerRun.Add(playerrun);
                                }

                                previousReferrerUserName = referrerLevel1player.SimpleInfo.ReferrerUserName;
                            }

                            int indexLevel = 2;
                            while (indexLevel <= awardLevelCount)
                            {
                                if (string.IsNullOrEmpty(previousReferrerUserName))
                                {
                                    break;
                                }

                                var playerrun = this.GetOnlinePlayerRunnable(previousReferrerUserName);
                                if (playerrun == null)
                                {
                                    PlayerInfo previousReferrerPlayer = DBProvider.UserDBProvider.GetPlayer(previousReferrerUserName);
                                    if (previousReferrerPlayer == null)
                                    {
                                        break;
                                    }
                                    playerrun = new PlayerRunnable(previousReferrerPlayer);
                                    this._dicOnlinePlayerRuns.TryAdd(playerrun.BasePlayer.SimpleInfo.UserName, playerrun);
                                }

                                if (playerrun.BasePlayer.SimpleInfo.GroupType != PlayerGroupType.AgentPlayer)
                                {
                                    //如果玩家上级推荐人不是代理，才给推荐人奖励
                                    var awardExpRecord = new WaitToReferAwardRecord()
                                    {
                                        AwardLevel = indexLevel,
                                        NewRegisterUserNme = userName,
                                        ReferrerUserName = previousReferrerUserName
                                    };
                                    listWaitToAwardExpRecord.Add(awardExpRecord);
                                }

                                //listPlayerRun.Add(playerrun);

                                previousReferrerUserName = playerrun.BasePlayer.SimpleInfo.UserName;
                                //PlayerActionController.Instance.AddLog(previousReferrerUserName, MetaData.ActionLog.ActionType.Refer, 1, "收获" + awardConfig.ToString());

                                indexLevel++;
                            }
                        }
                    }
                }
                PlayerInfo newplayer = new PlayerInfo()
                {
                    SimpleInfo = new PlayerSimpleInfo()
                    {
                        UserName = userName,
                        NickName = nickName,
                        Password = password,
                         Alipay = alipayAccount,
                          AlipayRealName = alipayRealName,
                           IDCardNo = IDCardNo,
                        Email = email,
                        QQ = qq,
                        InvitationCode = invitationCode != GlobalData.TestInvitationCode ? CreateInvitationCode(userName) : GlobalData.TestInvitationCode,
                        RegisterTime = DateTime.Now,
                        RegisterIP = clientIP,
                        ReferrerUserName = referrerLevel1player == null ? "" : referrerLevel1player.SimpleInfo.UserName,
                        GroupType = PlayerGroupType.NormalPlayer,
                        AgentReferredLevel = 0,
                    },
                    FortuneInfo = new PlayerFortuneInfo()
                    {
                        UserName = userName,
                        Exp = GlobalConfig.RegisterPlayerConfig.GiveToNewUserExp,
                        GoldCoin = GlobalConfig.RegisterPlayerConfig.GiveToNewUserGoldCoin,
                        MinersCount = GlobalConfig.RegisterPlayerConfig.GiveToNewUserMiners,
                        MinesCount = GlobalConfig.RegisterPlayerConfig.GiveToNewUserMines,
                        StonesReserves = GlobalConfig.RegisterPlayerConfig.GiveToNewUserMines * GlobalConfig.GameConfig.StonesReservesPerMines,
                        TotalProducedStonesCount = 0
                    }
                };
                if (referrerLevel1player == null)
                {
                    newplayer.SimpleInfo.IsAgentReferred = false;
                }
                else
                {
                    newplayer.SimpleInfo.IsAgentReferred = referrerLevel1player.SimpleInfo.GroupType == PlayerGroupType.AgentPlayer || referrerLevel1player.SimpleInfo.IsAgentReferred;
                    if (newplayer.SimpleInfo.IsAgentReferred)
                    {
                        newplayer.SimpleInfo.AgentReferredLevel = referrerLevel1player.SimpleInfo.AgentReferredLevel + 1;

                        if (referrerLevel1player.SimpleInfo.GroupType == PlayerGroupType.AgentPlayer)
                        {
                            newplayer.SimpleInfo.AgentUserID = referrerLevel1player.SimpleInfo.UserID;
                        }
                        else
                        {
                            newplayer.SimpleInfo.AgentUserID = referrerLevel1player.SimpleInfo.AgentUserID;
                        }
                    }
                }


                DBProvider.UserDBProvider.AddPlayer(newplayer, trans);
                foreach (var item in listWaitToAwardExpRecord)
                {
                    DBProvider.WaitToAwardExpRecordDBProvider.SaveWaitToAwardExpRecord(item, trans);
                }
                trans.Commit();

                LogHelper.Instance.AddInfoLog("玩家[" + userName + "]注册成功，推荐人：" + newplayer.SimpleInfo.ReferrerUserName + "。");
                PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.Register, 1, "注册成为新矿主。");

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                trans.Dispose();
            }
        }

        private string CreateInvitationCode(string userName)
        {
            return (Math.Abs(userName.GetHashCode())).ToString();
        }

        public int BindWeiXinUser(string wxUserOpenID, PlayerInfo player)
        {
            //PlayerRunnable playerrun = new PlayerRunnable(player, wxUserOpenID);
            //this._dicOnlinePlayerRuns[player.SimpleInfo.UserName] = playerrun;
            bool isOK = DBProvider.UserDBProvider.BindWeiXinUser(player.SimpleInfo.UserID, wxUserOpenID);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public bool WeiXinLoginPlayer(string weixinopenid,  PlayerInfo player)
        {
            var onlineRunner = this.GetRunnable(player.SimpleInfo.UserName);
            if (onlineRunner == null)
            {
                this.LoginPlayer(player);
                onlineRunner = this.GetRunnable(player.SimpleInfo.UserName);
            }

            onlineRunner.WeiXinOpenid = weixinopenid;

            return true;
        }

        public bool LoginPlayer(PlayerInfo player)
        {
            //说明是第一次登录
            if (player.SimpleInfo.LastLoginTime == null)
            {
                LogHelper.Instance.AddInfoLog("玩家[" + player.SimpleInfo.UserName + "]第一次登录矿场。");
                var awardRecords = DBProvider.WaitToAwardExpRecordDBProvider.GetWaitToAwardExpRecord(player.SimpleInfo.UserName);
                if (awardRecords != null)
                {
                    var myTrans = MyDBHelper.Instance.CreateTrans();
                    try
                    {
                        foreach (var awardrecord in awardRecords)
                        {
                            var referrerPlayerRunnable = this.GetRunnable(awardrecord.ReferrerUserName);

                            var award = GlobalConfig.AwardReferrerLevelConfig.GetAwardByLevel(awardrecord.AwardLevel);
                            referrerPlayerRunnable.ReferAward(award, player.SimpleInfo.UserName, myTrans);
                            LogHelper.Instance.AddInfoLog("玩家[" + player.SimpleInfo.UserName + "]，的 " + awardrecord.AwardLevel + " 级推荐人[" + awardrecord.ReferrerUserName + "]收获: " + award.ToString());
                            PlayerActionController.Instance.AddLog(referrerPlayerRunnable.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.Refer, awardrecord.AwardLevel, "收获" + award.ToString());
                            DBProvider.WaitToAwardExpRecordDBProvider.DeleteWaitToAwardExpRecord(awardrecord.ID, myTrans);
                        }

                        myTrans.Commit();
                    }
                    catch (Exception exc)
                    {
                        LogHelper.Instance.AddErrorLog("LoginPlayer Award Referrer Exception", exc);
                        myTrans.Rollback();
                    }
                    finally
                    {
                        myTrans.Dispose();
                    }
                }
            }
            player.SimpleInfo.LastLoginTime = DateTime.Now;

            DBProvider.UserDBProvider.SavePlayerLoginTime(player.SimpleInfo);
            PlayerRunnable playerrun = new PlayerRunnable(player);

            //计算玩家上一次退出，到本次登录时，累计矿工产量。
            decimal tempOutputStone = playerrun.ComputePlayerOfflineStoneOutput();

            this._dicOnlinePlayerRuns[player.SimpleInfo.UserName] = playerrun;
            //LogHelper.Instance.AddInfoLog("玩家[" + player.SimpleInfo.UserName + "] 冻结灵币为：" + player.FortuneInfo.FreezingRMB);

            return true;
        }

        public void LogoutPlayer(string userName)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return;
            }

            playerrun.LogoutPlayer();
            this._dicOnlinePlayerRuns.TryRemove(userName, out playerrun);
        }

        public MetaData.User.PlayerInfo GetPlayerByWeiXinOpenID(string openid)
        {
            var runnable = this._dicOnlinePlayerRuns.Values.FirstOrDefault(p => p.WeiXinOpenid == openid);
            if (runnable!= null)
            {
                return runnable.BasePlayer;
            }

            return null;
        }

        public PlayerInfo GetOnlinePlayerInfo(string userName)
        {
            PlayerRunnable playerrun = null;
            if (this._dicOnlinePlayerRuns.TryGetValue(userName, out playerrun))
            {
                return playerrun.BasePlayer;
            }

            return null;
        }

        /// <summary>
        /// 不论在线还是离线
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public PlayerInfo GetPlayerInfo(string userName)
        {
            PlayerInfo player = null;

            PlayerRunnable playerrun = null;
            if (this._dicOnlinePlayerRuns.TryGetValue(userName, out playerrun))
            {
                player = playerrun.BasePlayer;
            }

            if (player == null)
            {
                player = DBProvider.UserDBProvider.GetPlayer(userName);
            }

            return player;
        }

        public PlayerRunnable GetRunnable(string userName)
        {
            PlayerRunnable playerSellerRun = this.GetOnlinePlayerRunnable(userName);
            if (playerSellerRun == null)
            {
                var seller = DBProvider.UserDBProvider.GetPlayer(userName);
                if (seller != null)
                {
                    playerSellerRun = new PlayerRunnable(seller);
                }
            }

            return playerSellerRun;
        }

        private PlayerRunnable GetOnlinePlayerRunnable(string userName)
        {
            PlayerRunnable playerrun = null;
            if (this._dicOnlinePlayerRuns.TryGetValue(userName, out playerrun))
            {
                return playerrun;
            }

            return null;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return false;
            }

            if (playerrun.BasePlayer.SimpleInfo.Password == oldPassword)
            {
                return playerrun.ChangePassword(newPassword);
            }

            return false;
        }

        public bool ChangePasswordByAdmin(string userName, string newPassword)
        {
            var playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                var player = DBProvider.UserDBProvider.GetPlayer(userName);
                playerrun = new PlayerRunnable(player);
            }

            return playerrun.ChangePassword(newPassword);
        }

        public int ChangePlayerSimpleInfo(string userName, string nickName, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq)
        {
            var playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                var player = DBProvider.UserDBProvider.GetPlayer(userName);
                playerrun = new PlayerRunnable(player);
            }

            var playerFromDB = DBProvider.UserDBProvider.GetPlayerByAlipay(alipayAccount);
            if (playerFromDB != null && playerFromDB.SimpleInfo.UserName != userName)
            {
                return OperResult.RESULTCODE_REGISTER_ALIPAY_EXIST;
            }
            //playerFromDB = DBProvider.UserDBProvider.GetPlayerByAlipayRealName(alipayRealName);
            //if (playerFromDB != null && playerFromDB.SimpleInfo.UserName != userName)
            //{
            //    return OperResult.RESULTCODE_REGISTER_ALIPAYREALNAME_EXIST;
            //}

            if (IDCardNo != playerrun.BasePlayer.SimpleInfo.IDCardNo)
            {
                if (this.CheckUserIDCardNoExist(IDCardNo) == OperResult.RESULTCODE_TRUE)
                {
                    return OperResult.RESULTCODE_REGISTER_IDCARDNO_EXIST;
                }
            }

            //if (!string.IsNullOrEmpty(playerrun.BasePlayer.SimpleInfo.Alipay) && !string.IsNullOrEmpty(playerrun.BasePlayer.SimpleInfo.AlipayRealName))
            //{
            //    //先做验证，如果玩家之前已经绑定过支付信息，而本次又修改了支付宝信息，则返回false.
            //    if (playerrun.BasePlayer.SimpleInfo.Alipay != alipayAccount || playerrun.BasePlayer.SimpleInfo.AlipayRealName != alipayRealName)
            //    {
            //        return OperResult.RESULTCODE_USER_CANNOT_UPDATEALIPAY;
            //    }
            //}
            var isOK = playerrun.ChangePlayerSimpleInfo(nickName, alipayAccount, alipayRealName, IDCardNo, email, qq);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public bool ChangePlayerFortuneInfo(PlayerFortuneInfo fortuneinfo)
        {
            var playerrun = this.GetRunnable(fortuneinfo.UserName);
            if (playerrun == null)
            {
                return false;
            }

            bool isOK = playerrun.SetFortuneInfo(fortuneinfo);
            if (isOK)
            {
                NotifyPlayerClient(playerrun.BasePlayer);
            }

            return isOK;
        }

        public bool DeletePlayer(string userName)
        {
            PlayerRunnable runnable = null;
            this._dicOnlinePlayerRuns.TryRemove(userName, out runnable);
            DBProvider.TestUserLogStateDBProvider.DeleteTestUserLogState(userName);
            return DBProvider.UserDBProvider.DeletePlayer(userName);
        }

        public bool LockPlayer(string userName)
        {
            var playerrun = this.GetRunnable(userName);
            if (playerrun != null)
            {
                var token = ClientManager.GetToken(playerrun.BasePlayer.SimpleInfo.UserName);
                if (!string.IsNullOrEmpty(token) && this.KickOutPlayer != null)
                {
                    this.KickOutPlayer(token);
                }

                return playerrun.LockPlayer();
            }

            return false;
        }

        public bool UnlockPlayer(string userName)
        {
            var playerrun = this.GetRunnable(userName);
            if (playerrun != null)
            {
                return playerrun.UnlockPlayer();
            }

            return false;
        }

        public int RouletteSpendStone(string userName, int stone)
        {
            var playerRun = this.GetRunnable(userName);
            if (playerRun == null)
            {
                return OperResult.RESULTCODE_USER_OFFLINE;
            }

            int value = playerRun.RouletteSpendStone(stone);
            if (value == OperResult.RESULTCODE_TRUE)
            {
                NotifyPlayerClient(playerRun.BasePlayer);
            }

            return value;
        }

        public int SetPlayerAsAgent(int userID, string userName, string agentReferURL)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                var player = this.GetPlayerInfo(userName);
                if (player == null)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }
                myTrans = MyDBHelper.Instance.CreateTrans();
                AgentUserInfo agent = new AgentUserInfo()
                {
                    Player = player,
                    InvitationURL = agentReferURL,
                    TotalAwardRMB = 0
                };

                DBProvider.UserDBProvider.UpdatePlayerGroupType(userID, PlayerGroupType.AgentPlayer, myTrans);
                DBProvider.AgentUserInfoDBProvider.AddAgentUser(agent, myTrans);
                myTrans.Commit();
                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                LogHelper.Instance.AddErrorLog("设置玩家[" + userName + "]为代理时异常。", exc);
                return OperResult.RESULTCODE_FALSE;
            }
            finally
            {
                if (myTrans != null)
                {
                    myTrans.Dispose();
                }
            }
        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="stonesCount">-1表示清空临时产出</param>
        /// <returns></returns>
        public int GatherStones(string userName, decimal stones)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_USER_OFFLINE;
            }

            return playerrun.GatherStones(stones);
        }

        /// <summary>
        /// RESULTCODE_USER_NOT_EXIST; 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="minersCount"></param>
        /// <returns></returns>
        public int BuyMiner(string userName, int minersCount)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            return playerrun.BuyMiner(minersCount);
        }

        public int BuyMineByRMB(string userName, int minesCount, CustomerMySqlTransaction myTrans)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            return playerrun.BuyMineByRMB(minesCount, myTrans);
        }

        public int BuyMineByAlipay(string userName, decimal moneyYuan, decimal minesCount, CustomerMySqlTransaction myTrans)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            //先给代理计收益，后计算玩家
            AgentAwardController.Instance.PlayerRechargeRMB(playerrun.BasePlayer, AgentAwardType.PlayerInchargeMine, moneyYuan * GlobalConfig.GameConfig.Yuan_RMB, myTrans);

            int value = playerrun.BuyMineByAlipay(moneyYuan, minesCount, myTrans);
            if (value == OperResult.RESULTCODE_TRUE)
            {
                NotifyPlayerClient(playerrun.BasePlayer);
            }

            return value;
        }

        public int RechargeGoldCoinByRMB(string userName, int rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            return playerrun.RechargeGoldCoineByRMB(rmbValue, goldcoinValue, myTrans);
        }

        public int RechargeGoldCoinByAlipay(string userName, decimal moneyYuan, int rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            PlayerRunnable playerrun = this.GetRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            //先给代理计收益，后计算玩家
            AgentAwardController.Instance.PlayerRechargeRMB(playerrun.BasePlayer, AgentAwardType.PlayerInchargeMine, moneyYuan * GlobalConfig.GameConfig.Yuan_RMB, myTrans);

            int value = playerrun.RechargeGoldCoinByAlipay(moneyYuan, rmbValue, goldcoinValue, myTrans);
            if (value == OperResult.RESULTCODE_TRUE)
            {
                NotifyPlayerClient(playerrun.BasePlayer);
            }

            return value;
        }

        public int RefreshFortune(string userName)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            playerrun.RefreshFortune();
            return OperResult.RESULTCODE_TRUE;
        }

        public bool PayStoneOrder(bool isAlipayPay, string buyerUserName, BuyStonesOrder order, CustomerMySqlTransaction trans)
        {
            PlayerRunnable playerBuyerRun = this.GetRunnable(buyerUserName);
            if (playerBuyerRun == null)
            {
                LogHelper.Instance.AddInfoLog("支付订单时，更新买方信息失败（数据库中没有买方玩家信息）。 Order: " + order.ToString());
                return false;
            }
            bool isOK = playerBuyerRun.PayBuyStonesUpdateBuyerInfo(isAlipayPay, order, trans);
            if (!isOK)
            {
                LogHelper.Instance.AddInfoLog("支付订单时，更新买方信息失败。 Order: " + order.ToString());
                return false;
            }

            PlayerRunnable playerSellerRun = this.GetRunnable(order.StonesOrder.SellerUserName);
            if (playerSellerRun == null)
            {
                LogHelper.Instance.AddInfoLog("支付订单时，更新卖方信息失败（数据库中没有卖方玩家信息）。 Order: " + order.ToString());
                return false;
            }

            return playerSellerRun.PayBuyStonesUpdateSellerInfo(order, trans);
        }

        /// <summary>
        /// 0表示成功；RESULTCODE_USER_OFFLINE；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// 如果事务提交失败，则调用RollbackUserFromDB恢复状态
        /// </summary>
        /// <param name="SellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(order.SellerUserName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_USER_OFFLINE;
            }

            return playerrun.SellStones(order, trans);
        }

        public int CancelSellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            PlayerRunnable playerrun = this.GetRunnable(order.SellerUserName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_USER_OFFLINE;
            }

            return playerrun.CancelSellStones(order, trans);
        }

        public void RollbackUserFromDB(string userName)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun != null)
            {
                playerrun.RefreshFortune();
            }
        }
        
        public PlayerInfo GetPlayerByAlipayAccount(string alipayAccount)
        {
            foreach (var item in this._dicOnlinePlayerRuns.Values)
            {
                if (item.BasePlayer.SimpleInfo.Alipay == alipayAccount)
                {
                    return item.BasePlayer;
                }
            }

            return DBProvider.UserDBProvider.GetPlayerByAlipay(alipayAccount);
        }

        public int CreateWithdrawRMB(string userName, int getRMBCount)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_USER_OFFLINE;
            }

            DateTime createTime = DateTime.Now;
            int result = playerrun.CreateWithdrawRMB(getRMBCount, createTime);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                var record = DBProvider.WithdrawRMBRecordDBProvider.GetWithdrawRMBRecord((int)RMBWithdrawState.Waiting, userName, getRMBCount, createTime);
                if (record != null && SomebodyWithdrawRMB != null)
                {
                    SomebodyWithdrawRMB(record);
                }
            }

            return result;
        }

        public int RejectPlayerWithdrawRMB(WithdrawRMBRecord record)
        {
            return 0;
        }

        public int PayWithdrawRMB(WithdrawRMBRecord record)
        {
            PlayerRunnable playerrun = this.GetRunnable(record.PlayerUserName);
            if (playerrun == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }

            var finishedRecord = DBProvider.WithdrawRMBRecordDBProvider.GetWithdrawRMBRecordByID(record.id);
            if (finishedRecord != null && finishedRecord.State != RMBWithdrawState.Waiting)
            {
                return OperResult.RESULTCODE_WITHDRAW_ORDER_BEHANDLED;
            }

            int result = playerrun.PayWithdrawRMB(record);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                string outputMessage = "";
                if (record.State == RMBWithdrawState.Payed)
                {
                    outputMessage = "玩家[" + record.PlayerUserName + "] 提现 " + record.WidthdrawRMB + " 灵币成功，" + record.ValueYuan + "元人民币已到账。当前冻结灵币为：" + playerrun.BasePlayer.FortuneInfo.FreezingRMB;
                }
                else
                {
                    outputMessage = "玩家[" + record.PlayerUserName + "] 提现 " + record.WidthdrawRMB + " 灵币被拒绝，原因为：" + record.Message + "。当前冻结灵币为：" + playerrun.BasePlayer.FortuneInfo.FreezingRMB;
                }
                LogHelper.Instance.AddInfoLog(outputMessage);

                //PlayerActionController.Instance.AddLog(record.PlayerUserName, MetaData.ActionLog.ActionType.WithdrawRMB, record.WidthdrawRMB, outputMessage.ToString());

                string tokenBuyer = ClientManager.GetToken(record.PlayerUserName);
                if (!string.IsNullOrEmpty(tokenBuyer))
                {
                    NotifyPlayerClient(playerrun.BasePlayer);
                }
            }

            return result;
        }

        public void NotifyPlayerClient(PlayerInfo player)
        {
            if (this.PlayerInfoChanged != null)
            {
                this.PlayerInfoChanged(player);
            }
        }

        //public PlayerLoginInfo[] GetUserLoginLog(int userID)
        //{
        //    return DBProvider.PlayerLoginInfoDBProvider.GetUserLoginLogs(userID);
        //}

        public event Action<WithdrawRMBRecord> SomebodyWithdrawRMB;
    }
}
