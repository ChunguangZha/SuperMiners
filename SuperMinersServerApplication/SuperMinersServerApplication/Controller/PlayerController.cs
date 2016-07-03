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

        public int RegisteredPlayersCount { get; private set; }
        public float AllMiners { get; private set; }
        public float AllOutputStones { get; private set; }

        private ConcurrentDictionary<string, PlayerRunnable> _dicOnlinePlayerRuns = new ConcurrentDictionary<string, PlayerRunnable>();

        #endregion

        public void Init()
        {
            this.RegisteredPlayersCount = DBProvider.UserDBProvider.GetAllPlayerCount();
            this.AllMiners = DBProvider.UserDBProvider.GetAllMinersCount();
            this.AllOutputStones = DBProvider.UserDBProvider.GetAllOutputStonesCount();
        }

        /// <summary>
        /// 0：成功；1：用户名已经存在；2：同一IP注册用户数超限；3：注册失败; 4: 用户名长度不够
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUser(string clientIP, string userName, string nickName, string password, string email, string qq, string invitationCode)
        {
            int userCount = DBProvider.UserDBProvider.GetPlayerCountByUserName(userName);
            if (userCount > 0)
            {
                return 1;
            }

            bool Awardable = false;
            userCount = DBProvider.UserDBProvider.GetPlayerCountByRegisterIP(clientIP);
            if (userCount >= GlobalConfig.RegisterPlayerConfig.UserCountCreateByOneIP)
            {
                return 2;
            }
            if (userCount == 0)
            {
                Awardable = true;
            }

            List<PlayerRunnable> listPlayerRun = new List<PlayerRunnable>();
            var trans = MyDBHelper.Instance.CreateTrans();

            try
            {
                PlayerInfo referrerLevel1player = null;
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
                            }
                            var awardConfig = GlobalConfig.AwardReferrerLevelConfig.GetAwardByLevel(1);
                            playerrun.ReferAward(awardConfig, trans);
                            listPlayerRun.Add(playerrun);

                            PlayerActionController.Instance.AddLog(playerrun.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.Refer, 1, "收获" + awardConfig.ToString());

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
                            }

                            var awardConfig = GlobalConfig.AwardReferrerLevelConfig.GetAwardByLevel(indexLevel);
                            playerrun.ReferAward(awardConfig, trans);
                            listPlayerRun.Add(playerrun);

                            previousReferrerUserName = playerrun.BasePlayer.SimpleInfo.UserName;
                            PlayerActionController.Instance.AddLog(previousReferrerUserName, MetaData.ActionLog.ActionType.Refer, 1, "收获" + awardConfig.ToString());

                            indexLevel++;
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
                        Email = email,
                        QQ = qq,
                        InvitationCode = CreateInvitationCode(userName),
                        RegisterTime = DateTime.Now,
                        RegisterIP = clientIP,
                        ReferrerUserName = referrerLevel1player == null ? "" : referrerLevel1player.SimpleInfo.UserName
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

                DBProvider.UserDBProvider.AddPlayer(newplayer, trans);
                PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.Register, 1, "注册成为新矿主。");

                trans.Commit();

                foreach (var playerrun in listPlayerRun)
                {
                    playerrun.RefreshFortune();
                    if (PlayerInfoChanged != null)
                    {
                        PlayerInfoChanged(playerrun.BasePlayer);
                    }
                }

                return 0;
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
            return userName.GetHashCode().ToString();
        }

        public PlayerInfo LoginPlayer(string userName, string password)
        {
            PlayerInfo player = DBProvider.UserDBProvider.GetPlayer(userName);
            if (null != player && password == player.SimpleInfo.Password)
            {
                player.SimpleInfo.LastLoginTime = DateTime.Now;
                PlayerRunnable playerrun = new PlayerRunnable(player);

                //计算玩家上一次退出，到本次登录时，累计矿工产量。
                playerrun.ComputePlayerOfflineStoneOutput();

                this._dicOnlinePlayerRuns[userName] = playerrun;

                return player;
            }

            return null;
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

        public PlayerInfo GetOnlinePlayerInfo(string userName)
        {
            PlayerRunnable playerrun = null;
            if (this._dicOnlinePlayerRuns.TryGetValue(userName, out playerrun))
            {
                return playerrun.BasePlayer;
            }

            return null;
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
            var playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                var player = DBProvider.UserDBProvider.GetPlayer(userName);
                playerrun = new PlayerRunnable(player);
            }

            if (playerrun.BasePlayer.SimpleInfo.Password == oldPassword)
            {
                return playerrun.ChangePassword(newPassword);
            }

            return false;
        }

        public bool ChangePlayerSimpleInfo(string userName, string nickName, string alipayAccount, string alipayRealName, string email, string qq)
        {
            var playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                var player = DBProvider.UserDBProvider.GetPlayer(userName);
                playerrun = new PlayerRunnable(player);
            }
            
            return playerrun.ChangePlayerSimpleInfo(nickName, alipayAccount, alipayRealName, email, qq);
        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="stonesCount">0表示清空临时产出</param>
        /// <returns></returns>
        public int GatherStones(string userName, float stones)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return -1;
            }

            return playerrun.GatherStones(stones);
        }

        public int BuyMiner(string userName, int minersCount)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return -1;
            }

            return playerrun.BuyMiner(minersCount);
        }

        public int BuyMine(string userName, int minesCount)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return -1;
            }

            return playerrun.BuyMine(minesCount);
        }

        public bool PayStoneOrder(PlayerInfo playerBuyer, BuyStonesOrder order, bool rmbPay, CustomerMySqlTransaction trans)
        {
            PlayerRunnable playerBuyerRun = this.GetOnlinePlayerRunnable(playerBuyer.SimpleInfo.UserName);
            if (playerBuyerRun == null)
            {
                playerBuyerRun = new PlayerRunnable(playerBuyer);
            }
            bool isOK = playerBuyerRun.PayBuyStonesUpdateBuyerInfo(order, rmbPay, trans);
            if (!isOK)
            {
                LogHelper.Instance.AddInfoLog("支付订单时，更新买方信息失败。 rmbPay: " + rmbPay + "。 " + order.ToString());
                return false;
            }

            PlayerRunnable playerSellerRun = this.GetOnlinePlayerRunnable(order.StonesOrder.SellerUserName);
            if (playerSellerRun == null)
            {
                var seller = DBProvider.UserDBProvider.GetPlayer(order.StonesOrder.SellerUserName);
                if (seller == null)
                {
                    LogHelper.Instance.AddInfoLog("支付订单时，更新卖方信息失败（数据库中没有卖方玩家信息）。 rmbPay: " + rmbPay + "。 " + order.ToString());
                    return false;
                }

                playerSellerRun = new PlayerRunnable(seller);
            }

            return playerSellerRun.PayBuyStonesUpdateSellerInfo(order, trans);
        }

        /// <summary>
        /// 0表示成功；-2表示该用户不在线；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// 如果事务提交失败，则调用RollbackUserFromDB恢复状态
        /// </summary>
        /// <param name="SellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(order.SellerUserName);
            if (playerrun == null)
            {
                return -2;
            }

            return playerrun.SellStones(order, trans);
        }

        public void RollbackUserFromDB(string userName)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun != null)
            {
                playerrun.RefreshFortune();
            }
        }

        public bool RechargeRMB(string alipay, string alipayRealName, float yuan)
        {
            try
            {
                //PlayerInfo player = DBProvider.UserDBProvider.GetPlayerByAlipay(alipay, alipayRealName);
                //if (player == null)
                //{
                //    return false;
                //}

                //var playerrun = this.GetOnlinePlayerRunnable(player.SimpleInfo.UserName);
                //if (playerrun == null)
                //{
                //    playerrun = new PlayerRunnable(player);
                //}

                //playerrun.RechargeRMB(yuan);
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RechargeRMB exception. alipay = " + alipay + ", reamname=" + alipayRealName + ", yuan=" + yuan.ToString(), exc);
                return false;
            }
        }

        public bool RechargeGoldCoin(string alipay, string alipayRealName, float yuan)
        {
            try
            {
                //PlayerInfo player = DBProvider.UserDBProvider.GetPlayerByAlipay(alipay, alipayRealName);
                //if (player == null)
                //{
                //    return false;
                //}

                //var playerrun = this.GetOnlinePlayerRunnable(player.SimpleInfo.UserName);
                //if (playerrun == null)
                //{
                //    playerrun = new PlayerRunnable(player);
                //}

                //playerrun.RechargeRMB(yuan);
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RechargeGoldCoin exception. alipay = " + alipay + ", realname=" + alipayRealName + ", yuan=" + yuan.ToString(), exc);
                return false;
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
    }
}
