using DataBaseProvider;
using MetaData;
using MetaData.Game.GambleStone;
using MetaData.Game.Roulette;
using MetaData.Game.StoneStack;
using MetaData.Shopping;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class PlayerRunnable
    {
        private PlayerInfo _basePlayer = null;
        public PlayerInfo BasePlayer
        {
            get { return _basePlayer; }
            set
            {
                lock (this._lockFortuneAction)
                {
                    this._basePlayer = value;
                }
            }
        }

        public string WeiXinOpenid { get; set; }
        private object _lockSimpleAction = new object();
        private object _lockFortuneAction = new object();

        public PlayerRunnable(PlayerInfo player)
        {
            BasePlayer = player;
        }

        public PlayerRunnable(PlayerInfo player, string openid)
        {
            BasePlayer = player;
            WeiXinOpenid = openid;
        }

        public bool SetFortuneInfo(PlayerFortuneInfo fortuneInfo, string operMessage)
        {
            lock (this._lockFortuneAction)
            {
                bool isOK = this.SaveUserFortuneInfoToDB(fortuneInfo, operMessage, null);
                if (isOK)
                {
                    this.BasePlayer.FortuneInfo = fortuneInfo;
                }

                return isOK;
            }
        }

        public decimal MaxTempStonesOutput
        {
            get
            {
                return GlobalConfig.GameConfig.TempStoneOutputValidHour * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;
            }
        }

        public bool ChangePassword(string newPassword)
        {
            DBProvider.UserDBProvider.UpdatePlayerPassword(BasePlayer.SimpleInfo.UserName, newPassword);
            this.BasePlayer.SimpleInfo.Password = newPassword;
            return true;
        }

        public bool ChangePlayerSimpleInfo(string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq)
        {
            DBProvider.UserDBProvider.UpdatePlayerSimpleInfo(BasePlayer.SimpleInfo.UserName, alipayAccount, alipayRealName, IDCardNo, email, qq);
            this.BasePlayer.SimpleInfo.Alipay = alipayAccount;
            this.BasePlayer.SimpleInfo.AlipayRealName = alipayRealName;
            this.BasePlayer.SimpleInfo.Email = email;
            this.BasePlayer.SimpleInfo.QQ = qq;
            return true;
        }

        public bool LockPlayer(int expireDays)
        {
            DBProvider.PlayerLockedInfoDBProvider.AddPlayerLockedInfo(this.BasePlayer.SimpleInfo.UserID, expireDays);
            return true;


            //this.BasePlayer.SimpleInfo.LockedLogin = true;
            //this.BasePlayer.SimpleInfo.LockedLoginTime = DateTime.Now;
            ////this.BasePlayer.SimpleInfo.LastLogOutTime = null;
            ////this.BasePlayer.FortuneInfo.TempOutputStonesStartTime = null;
            //bool isOK = DBProvider.UserDBProvider.UpdatePlayerLockedState(this.BasePlayer.SimpleInfo.UserName, this.BasePlayer.SimpleInfo.LockedLogin, this.BasePlayer.SimpleInfo.LockedLoginTime);
            //return isOK;
        }

        public bool UnlockPlayer()
        {
            DBProvider.PlayerLockedInfoDBProvider.DeletePlayerLockedInfo(this.BasePlayer.SimpleInfo.UserID);
            return true;

            //this.BasePlayer.SimpleInfo.LockedLogin = false;
            //this.BasePlayer.SimpleInfo.LockedLoginTime = null;
            //this.BasePlayer.FortuneInfo.TempOutputStonesStartTime = DateTime.Now;
            //bool isOK = DBProvider.UserDBProvider.UpdatePlayerLockedState(this.BasePlayer.SimpleInfo.UserName, this.BasePlayer.SimpleInfo.LockedLogin, null);
            //return isOK;
        }

        public int LockPlayerToTransfer()
        {
            lock (this._lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.StockOfStones < 100000)
                {
                    return OperResult.RESULTCODE_TRANSFEROLDPLAYER_FAILED_STONEOUTOFLANCE;
                }
                this.BasePlayer.FortuneInfo.StockOfStones -= 100000;
                DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.SimpleInfo.UserID, this.BasePlayer.FortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public bool LogoutPlayer()
        {
            BasePlayer.SimpleInfo.LastLogOutTime = DateTime.Now;
            return DBProvider.UserDBProvider.LogoutPlayer(BasePlayer.SimpleInfo, BasePlayer.FortuneInfo);
        }

        public void RefreshFortune()
        {
            BasePlayer.FortuneInfo = DBProvider.UserDBProvider.GetPlayerFortuneInfo(BasePlayer.SimpleInfo.UserName);
            LogHelper.Instance.AddInfoLog("重新加载玩家财富信息：" + BasePlayer.FortuneInfo.ToString());
            ComputePlayerOfflineStoneOutput();
        }

        public decimal ComputePlayerOfflineStoneOutput()
        {
            lock (this._lockFortuneAction)
            {
                DateTime startTime;
                if (BasePlayer.FortuneInfo.TempOutputStonesStartTime == null)//如果玩家没有收取过，则以注册时间为开始。
                {
                    BasePlayer.FortuneInfo.TempOutputStonesStartTime = BasePlayer.SimpleInfo.RegisterTime;
                    try
                    {
                        DBProvider.UserDBProvider.SavePlayerLastGatherTime(BasePlayer.SimpleInfo.UserID, BasePlayer.FortuneInfo.TempOutputStonesStartTime);
                    }
                    catch (Exception exc)
                    {
                        LogHelper.Instance.AddErrorLog("玩家[" + BasePlayer.SimpleInfo.UserName + "]登录，计算离线产出，保存收取时间异常。", exc);
                    }
                }

                startTime = BasePlayer.FortuneInfo.TempOutputStonesStartTime.Value;

                TimeSpan span = BasePlayer.SimpleInfo.LastLoginTime.Value - startTime;
                if (span.TotalHours < 0)
                {
                    return 0;
                }

                decimal tempOutput = (decimal)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                if (tempOutput > MaxTempStonesOutput)
                {
                    tempOutput = MaxTempStonesOutput;
                }
                if (tempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                {
                    tempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                }
                BasePlayer.FortuneInfo.TempOutputStones = tempOutput;

                return tempOutput;
            }

            //LogHelper.Instance.AddInfoLog("玩家 [" + BasePlayer.SimpleInfo.UserName + "] 请求登录矿场, 计算离线产出=" + BasePlayer.FortuneInfo.TempOutputStones + ", startTime=" + startTime);

        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="stones">-1表示清空临时产出</param>
        /// <returns></returns>
        public GatherTempOutputStoneResult GatherStones(decimal stones)
        {
            GatherTempOutputStoneResult result = new GatherTempOutputStoneResult();
            int IntTempOutput;
            DateTime stopTime = DateTime.Now;
            if (stones <= 0)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家收取临时产出的矿石：" + stones.ToString(), null);
                result.OperResult = OperResult.RESULTCODE_GATHERSTONE_NOSTONES;
                return result;
            }

            if (BasePlayer.FortuneInfo.TempOutputStonesStartTime == null)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                result.OperResult = OperResult.RESULTCODE_FALSE;
                return result;
            }

            TimeSpan span = stopTime - BasePlayer.FortuneInfo.TempOutputStonesStartTime.Value;
            if (span.TotalHours > 0)
            {
                lock (this._lockFortuneAction)
                {
                    decimal serverComputeTempOutput = (decimal)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                    if (serverComputeTempOutput > MaxTempStonesOutput)
                    {
                        serverComputeTempOutput = MaxTempStonesOutput;
                    }
                    if (serverComputeTempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                    {
                        serverComputeTempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                    }

                    decimal computeTempOutput = serverComputeTempOutput;
                    if (Math.Abs(stones - serverComputeTempOutput) <= 1)
                    {
                        computeTempOutput = stones;
                    }
                    //decimal computeTempOutput = stones < tempOutput ? stones : tempOutput;
                    IntTempOutput = (int)computeTempOutput;

                    if (IntTempOutput <= 0)
                    {
                        result.OperResult = OperResult.RESULTCODE_GATHERSTONE_NOSTONES;
                        return result;
                    }
                    BasePlayer.FortuneInfo.TempOutputStones = 0;
                    BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                    BasePlayer.FortuneInfo.StockOfStones += IntTempOutput;

                    //将零头从矿石储量中减去！
                    BasePlayer.FortuneInfo.TotalProducedStonesCount += computeTempOutput;
                    this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家收取临时产出的矿石：" + computeTempOutput.ToString(), null);

                    result.GatherStoneCount = IntTempOutput;
                }

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.GatherStone, IntTempOutput);
                result.OperResult = OperResult.RESULTCODE_TRUE;

                return result;
            }

            result.OperResult = OperResult.RESULTCODE_FALSE;
            return result;

        }

        /// <summary>
        /// RESULTCODE_LACK_OF_BALANCE; RESULTCODE_TRUE; 
        /// </summary>
        /// <param name="minersCount"></param>
        /// <returns></returns>
        public int BuyMiner(int minersCount)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.MinersCount + minersCount > GlobalConfig.GameConfig.UserMaxHaveMinersCount)
                {
                    return OperResult.RESULTCODE_USER_MINERSCOUNT_OUT;
                }

                decimal allNeedGoldCoin = minersCount * GlobalConfig.GameConfig.GoldCoin_Miner;
                if (allNeedGoldCoin > BasePlayer.FortuneInfo.GoldCoin)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.GoldCoin -= allNeedGoldCoin;
                BasePlayer.FortuneInfo.MinersCount += minersCount;

                CustomerMySqlTransaction trans = null;
                try
                {
                    trans = MyDBHelper.Instance.CreateTrans();
                    if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家购买矿工：" + minersCount.ToString(), trans))
                    {
                        trans.Rollback();
                        RefreshFortune();
                        return OperResult.RESULTCODE_FALSE;
                    }
                    MinersBuyRecord record = new MinersBuyRecord()
                    {
                        UserID = BasePlayer.SimpleInfo.UserID,
                        UserName = BasePlayer.SimpleInfo.UserName,
                        SpendGoldCoin = allNeedGoldCoin,
                        GainMinersCount = minersCount,
                        Time = DateTime.Now
                    };
                    DBProvider.BuyMinerRecordDBProvider.AddBuyMinerRecord(record, trans);

                    trans.Commit();
                    PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.BuyMiner, minersCount);
                    return OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    LogHelper.Instance.AddErrorLog("Buy Miners Error!", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }
                }

            }
        }

        public MakeAVowToGodResult MakeAVowToGod(int gravelValue)
        {
            MakeAVowToGodResult result = new MakeAVowToGodResult();

            lock (_lockFortuneAction)
            {
                int todayOfYear = DateTime.Now.DayOfYear;
                if (this.BasePlayer.FortuneInfo.MakeAVowToGodTime_DayofYear == todayOfYear)
                {
                    if (this.BasePlayer.FortuneInfo.MakeAVowToGodTimesLastDay > GlobalConfig.GameConfig.MaxMakeAVowTimesOfOneDay)
                    {
                        result.OperResultCode = OperResult.RESULTCODE_MAKEAVOWTIMESOUT;
                        return result;
                    }
                }

                this.BasePlayer.FortuneInfo.MakeAVowToGodTime_DayofYear = todayOfYear;
                this.BasePlayer.FortuneInfo.MakeAVowToGodTimesLastDay += 1;
                this.BasePlayer.GravelInfo.Gravel += gravelValue;

                result.OperResultCode = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                {
                    this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家神灵许愿，奖励碎片：" + gravelValue.ToString(), myTrans);
                    DBProvider.UserDBProvider.SavePlayerGravelInfo(this.BasePlayer.GravelInfo, myTrans);
                    return OperResult.RESULTCODE_TRUE;
                },
                exc =>
                {
                    this.RefreshFortune();
                    LogHelper.Instance.AddErrorLog("玩家[ " + this.BasePlayer.SimpleInfo.UserName + "] Save MakeAVowToGod Failed ", exc);
                });

                result.GravelResult = gravelValue;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyRecord"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public int BuyMineByShoppingCredits(MinesBuyRecord buyRecord, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                int valueCredits = buyRecord.SpendRMB * GlobalConfig.GameConfig.ShoppingCredits_RMB;
                if (valueCredits > BasePlayer.FortuneInfo.ShoppingCreditsEnabled)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.ShoppingCreditsEnabled -= valueCredits;
                BasePlayer.FortuneInfo.MinesCount += buyRecord.GainMinesCount;
                BasePlayer.FortuneInfo.StonesReserves += buyRecord.GainStonesReserves;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + valueCredits.ToString() + "积分购买矿山，获取矿山：" + buyRecord.GainMinesCount.ToString() + "，矿石储量：" + buyRecord.GainStonesReserves.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 矿山只能用RMB购买。0表示账户余额不足；-1表示操作失败；minesCount表示成功。
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMineByDiamond(MinesBuyRecord buyRecord, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                decimal valueDiamond = buyRecord.SpendRMB * GlobalConfig.GameConfig.Diamonds_RMB;
                if (valueDiamond > BasePlayer.FortuneInfo.StockOfDiamonds)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.StockOfDiamonds -= valueDiamond;
                BasePlayer.FortuneInfo.MinesCount += buyRecord.GainMinesCount;
                BasePlayer.FortuneInfo.StonesReserves += buyRecord.GainStonesReserves;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + valueDiamond.ToString() + "钻石购买矿山，获取矿山：" + buyRecord.GainMinesCount.ToString() + "，矿石储量：" + buyRecord.GainStonesReserves.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 矿山只能用RMB购买。0表示账户余额不足；-1表示操作失败；minesCount表示成功。
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMineByRMB(MinesBuyRecord buyRecord, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                if (buyRecord.SpendRMB > BasePlayer.FortuneInfo.RMB)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.RMB -= buyRecord.SpendRMB;
                BasePlayer.FortuneInfo.MinesCount += buyRecord.GainMinesCount;
                BasePlayer.FortuneInfo.StonesReserves += buyRecord.GainStonesReserves;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + buyRecord.SpendRMB.ToString() + "灵币购买矿山，获取矿山：" + buyRecord.GainMinesCount.ToString() + "，矿石储量：" + buyRecord.GainStonesReserves.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 用Alipay购买矿山。-1表示操作失败；minesCount表示成功。
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMineByAlipay(MinesBuyRecord buyRecord, decimal moneyYuan, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                BasePlayer.FortuneInfo.MinesCount += buyRecord.GainMinesCount;
                BasePlayer.FortuneInfo.StonesReserves += buyRecord.GainStonesReserves;

                BasePlayer.FortuneInfo.Exp += (int)moneyYuan;
                ExpChangeRecord expRecord = new ExpChangeRecord()
                {
                    UserID = this.BasePlayer.SimpleInfo.UserID,
                    UserName = this.BasePlayer.SimpleInfo.UserName,
                    AddExp = (int)moneyYuan,
                    NewExp = BasePlayer.FortuneInfo.Exp,
                    Time = MyDateTime.FromDateTime(DateTime.Now),
                    OperContent = "玩家支付宝充值金币奖励"
                };

                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用支付宝充值" + buyRecord.SpendRMB.ToString() + "灵币购买矿山，获取矿山：" + buyRecord.GainMinesCount.ToString() + "，矿石储量：" + buyRecord.GainStonesReserves.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }
                DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(expRecord, myTrans);

                return OperResult.RESULTCODE_TRUE;
            }
        }

        public int BuyRemoteServer(AlipayRechargeRecord alipay, RemoteServerType serverType, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                this.BasePlayer.FortuneInfo.ShoppingCreditsEnabled += (int)alipay.total_fee * GlobalConfig.GameConfig.RemoteServerRechargeReturnShoppingCreditsTimes;

                DateTime lastStopTime = DateTime.Now;
                if (this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime != null)
                {
                    DateTime time = this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime.ToDateTime();
                    if (time > lastStopTime)
                    {
                        lastStopTime = time;
                    }
                }

                switch (serverType)
                {
                    case RemoteServerType.Once:
                        this.BasePlayer.FortuneInfo.UserRemoteServiceValidTimes += 1;
                        if (this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime == null)
                        {
                            this.BasePlayer.FortuneInfo.IsLongTermRemoteServiceUser = false;
                        }
                        break;
                    case RemoteServerType.OneMonth:
                        this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime = new MyDateTime(lastStopTime.AddMonths(1));
                        this.BasePlayer.FortuneInfo.IsLongTermRemoteServiceUser = true;
                        break;
                    case RemoteServerType.ThreeMonth:
                        this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime = new MyDateTime(lastStopTime.AddMonths(3));
                        this.BasePlayer.FortuneInfo.IsLongTermRemoteServiceUser = true;
                        break;
                    case RemoteServerType.OneYear:
                        this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime = new MyDateTime(lastStopTime.AddYears(1));
                        this.BasePlayer.FortuneInfo.IsLongTermRemoteServiceUser = true;
                        break;
                    default:
                        break;
                }

                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家购买远程协助服务：" + serverType.ToString() + "支付宝订单号：" + alipay.alipay_trade_no, myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        public int RechargeGoldCoinByShoppingCredits(decimal rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            if (rmbValue <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            lock (_lockFortuneAction)
            {
                int valueCredits = (int)Math.Ceiling(rmbValue * GlobalConfig.GameConfig.ShoppingCredits_RMB);
                if (valueCredits > BasePlayer.FortuneInfo.ShoppingCreditsEnabled)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.ShoppingCreditsEnabled -= valueCredits;
                BasePlayer.FortuneInfo.GoldCoin += goldcoinValue;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + valueCredits.ToString() + "积分充值金币" + goldcoinValue.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmbValue"></param>
        /// <param name="goldcoinValue"></param>
        /// <returns></returns>
        public int RechargeGoldCoinByDiamond(decimal rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            if (rmbValue <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            lock (_lockFortuneAction)
            {
                int valueDiamond = (int)Math.Ceiling(rmbValue * GlobalConfig.GameConfig.Diamonds_RMB);
                if (valueDiamond > BasePlayer.FortuneInfo.StockOfDiamonds)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.StockOfDiamonds -= valueDiamond;
                BasePlayer.FortuneInfo.GoldCoin += goldcoinValue;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + valueDiamond.ToString() + "钻石充值金币" + goldcoinValue.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmbValue"></param>
        /// <param name="goldcoinValue"></param>
        /// <returns></returns>
        public int RechargeGoldCoinByRMB(int rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            if (rmbValue <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            lock (_lockFortuneAction)
            {
                if (rmbValue > BasePlayer.FortuneInfo.RMB)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                BasePlayer.FortuneInfo.RMB -= rmbValue;
                BasePlayer.FortuneInfo.GoldCoin += goldcoinValue;
                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用" + rmbValue.ToString() + "灵币充值金币" + goldcoinValue.ToString(), myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmbValue"></param>
        /// <param name="goldcoinValue"></param>
        /// <returns></returns>
        public int RechargeGoldCoinByAlipay(decimal moneyYuan, int rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                int awardGoldCoin = 0;
                BasePlayer.FortuneInfo.GoldCoin += goldcoinValue;
                if (!BasePlayer.FortuneInfo.FirstRechargeGoldCoinAward)
                {
                    awardGoldCoin = (int)(goldcoinValue * GlobalConfig.RegisterPlayerConfig.FirstAlipayRechargeGoldCoinAwardMultiple);
                    BasePlayer.FortuneInfo.FirstRechargeGoldCoinAward = true;
                    BasePlayer.FortuneInfo.GoldCoin += awardGoldCoin;
                }

                BasePlayer.FortuneInfo.Exp += (int)moneyYuan;
                ExpChangeRecord expRecord = new ExpChangeRecord()
                {
                    UserID = this.BasePlayer.SimpleInfo.UserID,
                    UserName = this.BasePlayer.SimpleInfo.UserName,
                    AddExp = (int)moneyYuan,
                    NewExp = BasePlayer.FortuneInfo.Exp,
                    Time = MyDateTime.FromDateTime( DateTime.Now),
                    OperContent = "玩家支付宝充值金币奖励"
                };

                if (!this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "玩家使用支付宝充值金币" + goldcoinValue.ToString(), myTrans))
                {
                    LogHelper.Instance.AddInfoLog(this.BasePlayer.SimpleInfo.UserName + " ---支付宝充值金币，保存玩家信息失败");
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }
                DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(expRecord, myTrans);

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.GoldCoinRecharge, goldcoinValue,
                    "充值了 " + goldcoinValue.ToString() + " 金币" + (awardGoldCoin == 0 ? "" : "，同时第一次支付宝充值金币，获取" + awardGoldCoin.ToString() + "金币的额外奖励"));
                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 支付购买矿石订单后，更新买家信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int PayBuyStonesUpdateBuyerInfo(bool isAlipayPay, BuyStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                if (!isAlipayPay)
                {
                    if (fortuneInfo.RMB < order.StonesOrder.ValueRMB)
                    {
                        return OperResult.RESULTCODE_LACK_OF_BALANCE;
                    }

                    fortuneInfo.RMB -= order.StonesOrder.ValueRMB;
                }
                fortuneInfo.StockOfStones += order.StonesOrder.SellStonesCount;
                fortuneInfo.GoldCoin += order.AwardGoldCoin;
                fortuneInfo.CreditValue += order.StonesOrder.SellStonesCount;
                fortuneInfo.StoneSellQuan++;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家成功购买矿石，订单号：" + order.StonesOrder.OrderNumber, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// 支付购买矿石订单后，更新卖家信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int PayBuyStonesUpdateSellerInfo(BuyStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                if (fortuneInfo.FreezingStones < order.StonesOrder.SellStonesCount)
                {
                    return OperResult.RESULTCODE_ORDER_SELLER_FREEZINGSTONECOUNT_ERROR;
                }
                fortuneInfo.RMB += (order.StonesOrder.ValueRMB - order.StonesOrder.Expense);
                fortuneInfo.StockOfStones -= order.StonesOrder.SellStonesCount;
                fortuneInfo.FreezingStones -= order.StonesOrder.SellStonesCount;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家成功出售矿石，订单号：" + order.StonesOrder.OrderNumber, trans);

                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int PayDelegateBuyStonesUpdateSellerInfo(StoneDelegateSellOrderInfo order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                //if (fortuneInfo.FreezingStones < order.SellUnit.TradeStoneHandCount * GlobalConfig.GameConfig.HandStoneCount)
                //{
                //    return OperResult.RESULTCODE_ORDER_SELLER_FREEZINGSTONECOUNT_ERROR;
                //}
                fortuneInfo.RMB += (order.FinishedStoneTradeHandCount * order.SellUnit.Price);

                int allStones = order.FinishedStoneTradeHandCount * GlobalConfig.GameConfig.HandStoneCount;
                fortuneInfo.StockOfStones -= allStones;
                fortuneInfo.FreezingStones -= allStones;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家通过股市完成出售矿石，订单号：" + order.OrderNumber, trans);

                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int PayDelegateBuyStonesUpdateBuyerInfo(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                decimal allNeedRMB = buyOrder.FinishedStoneTradeHandCount * buyOrder.BuyUnit.Price;

                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                if (buyOrder.PayType == PayType.Diamand)
                {
                    decimal allNeedDiamand = allNeedRMB * GlobalConfig.GameConfig.Diamonds_RMB;

                    if (fortuneInfo.FreezingDiamonds < allNeedDiamand)
                    {
                        return OperResult.RESULTCODE_LACK_OF_BALANCE;
                    }

                    fortuneInfo.FreezingDiamonds -= allNeedDiamand;
                }
                else if (buyOrder.PayType == PayType.RMB)
                {
                    if (fortuneInfo.FreezingRMB < allNeedRMB)
                    {
                        return OperResult.RESULTCODE_LACK_OF_BALANCE;
                    }

                    fortuneInfo.FreezingRMB -= allNeedRMB;
                }
                fortuneInfo.StockOfStones += buyOrder.FinishedStoneTradeHandCount * GlobalConfig.GameConfig.HandStoneCount;
                fortuneInfo.GoldCoin += buyOrder.AwardGoldCoin;
                //fortuneInfo.CreditValue += buyOrder.StonesOrder.SellStonesCount;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家通过股市完成购买矿石，订单号：" + buyOrder.OrderNumber, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public decimal SellableStonesCount
        {
            get
            {
                return BasePlayer.FortuneInfo.StockOfStones - BasePlayer.FortuneInfo.FreezingStones;
            }
        }

        public int AddNewBuyStonesByDelegate(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                decimal allNeedRMB = buyOrder.BuyUnit.TradeStoneHandCount * buyOrder.BuyUnit.Price;
                if (buyOrder.PayType == PayType.Diamand)
                {
                    decimal allNeedDiamand = allNeedRMB * GlobalConfig.GameConfig.Diamonds_RMB;

                    var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                    fortuneInfo.StockOfDiamonds -= allNeedDiamand;
                    fortuneInfo.FreezingDiamonds += allNeedDiamand;
                    this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家通过股市申请购买矿石，使用钻石支付，订单号：" + buyOrder.OrderNumber, trans);
                    BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
                }
                else if (buyOrder.PayType == PayType.RMB)
                {
                    var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                    fortuneInfo.RMB -= allNeedRMB;
                    fortuneInfo.FreezingRMB += allNeedRMB;
                    this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家通过股市申请购买矿石，使用灵币支付，订单号：" + buyOrder.OrderNumber, trans);
                    BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
                }
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CancelDelegateBuyStoneOrder(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction trans)
        {
            if (buyOrder.PayType == PayType.Alipay && buyOrder.BuyState == StoneDelegateBuyState.NotPayed)
            {
                return OperResult.RESULTCODE_TRUE;
            }
            lock (_lockFortuneAction)
            {
                decimal allNeedRMB = buyOrder.BuyUnit.TradeStoneHandCount * buyOrder.BuyUnit.Price;
                if (buyOrder.PayType == PayType.Diamand)
                {
                    decimal allNeedDiamand = allNeedRMB * GlobalConfig.GameConfig.Diamonds_RMB;

                    var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                    fortuneInfo.StockOfDiamonds += allNeedDiamand;
                    fortuneInfo.FreezingDiamonds -= allNeedDiamand;
                    this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家取消股市申请购买矿石，使用钻石支付，订单号：" + buyOrder.OrderNumber, trans);
                    BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
                }
                else if (buyOrder.PayType == PayType.RMB)
                {
                    //否则（包括灵币支付和支付宝支付）都退回成灵币
                    var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                    fortuneInfo.RMB += allNeedRMB;
                    fortuneInfo.FreezingRMB -= allNeedRMB;
                    this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家取消股市申请购买矿石，使用灵币支付，订单号：" + buyOrder.OrderNumber, trans);
                    BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
                }
                else if (buyOrder.PayType == PayType.Alipay)
                {
                    AlipayRechargeRecord alipayPayRecord = null;

                    if (buyOrder.IsSubOrder && !string.IsNullOrEmpty(buyOrder.ParentOrderNumber))
                    {
                        alipayPayRecord = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(buyOrder.ParentOrderNumber, "");
                    }
                    else
                    {
                        alipayPayRecord = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(buyOrder.OrderNumber, "");
                    }

                    if (alipayPayRecord != null && alipayPayRecord.value_rmb >= allNeedRMB)
                    {
                        //确实已充值，将其退回到灵币账户，没有冻结项。
                        var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                        fortuneInfo.RMB += allNeedRMB;
                        this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家取消股市申请购买矿石，使用支付宝支付，订单号：" + buyOrder.OrderNumber, trans);
                        BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
                    }
                }
            }

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// 出售矿石所需的手续费（矿石）当时立减，撤消订单不再返回.
        /// RESULTCODE_TRUE；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="sellStoneCount"></param>
        /// <param name="feeStoneCount">出售矿石的手续费（矿石）</param>
        /// <returns></returns>
        public int AddNewSellStonesByDelegate(int sellStoneCount, int feeStoneCount, CustomerMySqlTransaction trans)
        {
            //出售矿石所需的手续费（矿石）当时立减，撤消订单不再返回
            lock (_lockFortuneAction)
            {
                decimal sellableStones = BasePlayer.FortuneInfo.StockOfStones - BasePlayer.FortuneInfo.FreezingStones;
                if (sellStoneCount + feeStoneCount > sellableStones)
                {
                    return OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK;
                }

                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                fortuneInfo.FreezingStones += sellStoneCount;
                fortuneInfo.StockOfStones -= feeStoneCount;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家通过股市申请出售矿石" + sellStoneCount + "，手续费：" + feeStoneCount, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CancelDelegateSellStoneOrder(StoneDelegateSellOrderInfo sellOrder, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();

                decimal sellStoneCount = sellOrder.SellUnit.TradeStoneHandCount * GlobalConfig.GameConfig.HandStoneCount;
                if (sellOrder.SellState == StoneDelegateSellState.Splited)
                {
                    sellStoneCount = (sellOrder.SellUnit.TradeStoneHandCount - sellOrder.FinishedStoneTradeHandCount) * GlobalConfig.GameConfig.HandStoneCount;
                }
                fortuneInfo.FreezingStones -= sellStoneCount;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "玩家取消股市申请出售矿石，订单号：" + sellOrder.OrderNumber, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// RESULTCODE_TRUE；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="SellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(SellStonesOrder order, bool needUseQuan, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                if (needUseQuan && BasePlayer.FortuneInfo.StoneSellQuan < 1)
                {
                    return OperResult.RESULTCODE_ORDER_SELLSTONEQUAN_LACK;
                }
                decimal sellableStones = BasePlayer.FortuneInfo.StockOfStones - BasePlayer.FortuneInfo.FreezingStones;
                if (order.SellStonesCount > sellableStones)
                {
                    return OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK;
                }

                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                fortuneInfo.FreezingStones += order.SellStonesCount;
                if (needUseQuan)
                {
                    fortuneInfo.StoneSellQuan--;
                }
                this.SaveUserFortuneInfoToDB(fortuneInfo, "旧版出售矿石，订单号：" + order.OrderNumber, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CancelSellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                var fortuneInfo = BasePlayer.FortuneInfo.CopyTo();
                fortuneInfo.FreezingStones -= order.SellStonesCount;

                this.SaveUserFortuneInfoToDB(fortuneInfo, "旧版取消出售矿石，订单号：" + order.OrderNumber, trans);
                BasePlayer.FortuneInfo.CopyFrom(fortuneInfo);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CreateWithdrawRMB(int getRMBCount, DateTime createTime)
        {
            if (getRMBCount < 0)
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }
            //服务器限制，贡献值低于50，不予提现。
            if (this.BasePlayer.FortuneInfo.Exp < 50)
            {
                return OperResult.RESULTCODE_CANOT_WITHDRAWRMB;
            }
            //服务器限制，低于5元，不予提现。
            int ValueYuan = (int)Math.Ceiling(getRMBCount / GlobalConfig.GameConfig.Yuan_RMB);
            if (ValueYuan < 5)
            {
                return OperResult.RESULTCODE_CANOT_WITHDRAWRMB;
            }
            if (this.BasePlayer.FortuneInfo.RMB < getRMBCount)
            {
                return OperResult.RESULTCODE_LACK_OF_BALANCE;
            }

            lock (_lockFortuneAction)
            {
                CustomerMySqlTransaction myTrans = null;
                try
                {
                    WithdrawRMBRecord record = new WithdrawRMBRecord()
                    {
                        PlayerUserID = this.BasePlayer.SimpleInfo.UserID,
                        PlayerUserName = this.BasePlayer.SimpleInfo.UserName,
                        AlipayAccount = this.BasePlayer.SimpleInfo.Alipay,
                        AlipayRealName = this.BasePlayer.SimpleInfo.AlipayRealName,
                        WidthdrawRMB = getRMBCount,
                        ValueYuan = (int)Math.Ceiling(getRMBCount / GlobalConfig.GameConfig.Yuan_RMB),
                        CreateTime = createTime,
                        State = RMBWithdrawState.Waiting
                    };

                    this.BasePlayer.FortuneInfo.RMB -= getRMBCount;
                    this.BasePlayer.FortuneInfo.FreezingRMB += getRMBCount;

                    myTrans = MyDBHelper.Instance.CreateTrans();
                    this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家申请提现" + getRMBCount + " 灵币", myTrans);
                    DBProvider.WithdrawRMBRecordDBProvider.AddWithdrawRMBRecord(record, myTrans);

                    myTrans.Commit();

                    LogHelper.Instance.AddInfoLog("玩家[" + this.BasePlayer.SimpleInfo.UserName + "] 申请提现" + getRMBCount + " 灵币，当前冻结灵币为：" + this.BasePlayer.FortuneInfo.FreezingRMB);

                    return OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("CreateWithdrawRMB Exception", exc);
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }

                    this.RefreshFortune();

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

        }

        public int PayWithdrawRMB(WithdrawRMBRecord record)
        {
            record.PayTime = DateTime.Now;
            lock (_lockFortuneAction)
            {
                CustomerMySqlTransaction myTrans = null;
                try
                {
                    decimal valueRMB = this.BasePlayer.FortuneInfo.FreezingRMB - record.WidthdrawRMB;
                    if (valueRMB < 0)
                    {
                        LogHelper.Instance.AddErrorLog("玩家 [" + record.PlayerUserName + "] 灵币提现操作异常，提现灵币为：" + record.WidthdrawRMB.ToString() + "，冻结灵币为：" + this.BasePlayer.FortuneInfo.FreezingRMB.ToString(), null);
                        return OperResult.RESULTCODE_WITHDRAW_FREEZING_RMB_ERROR;
                    }

                    if (record.State == RMBWithdrawState.Payed)
                    {
                        this.BasePlayer.FortuneInfo.FreezingRMB = valueRMB;
                    }
                    else if (record.State == RMBWithdrawState.Rejected)
                    {
                        this.BasePlayer.FortuneInfo.RMB += record.WidthdrawRMB;
                        this.BasePlayer.FortuneInfo.FreezingRMB = valueRMB;
                    }
                    else
                    {
                        return OperResult.RESULTCODE_WITHDRAW_RECORD_STATE_ERROR;
                    }

                    myTrans = MyDBHelper.Instance.CreateTrans();
                    this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家灵币提现申请通过", myTrans);
                    DBProvider.WithdrawRMBRecordDBProvider.ConfirmWithdrawRMB(record, myTrans);

                    myTrans.Commit();

                    return OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("PayWithdrawRMB Exception", exc);
                    if (myTrans != null)
                    {
                        myTrans.Rollback();
                    }

                    this.RefreshFortune();
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

        }

        private bool SaveUserFortuneInfoToDB(PlayerFortuneInfo fortuneInfo, string operMessage, CustomerMySqlTransaction myTrans)
        {
            LogHelper.Instance.AddInfoLog(operMessage +"。玩家财富信息变动为：" + fortuneInfo.ToString());
            if (myTrans == null)
            {
                return DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.SimpleInfo.UserID, fortuneInfo);
            }
            else
            {
                return DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.SimpleInfo.UserID, fortuneInfo, myTrans);
            }
        }

        public bool BuyShoppingCreditAwardParent(decimal awardRMBValue, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                //20170326 改成返灵币
                BasePlayer.FortuneInfo.RMB += (int)awardRMBValue;
                this.SaveUserFortuneInfoToDB(BasePlayer.FortuneInfo, "子玩家充值积分奖励父玩家" + (int)awardRMBValue + "灵币", trans);
            }

            return true;
        }

        public bool ReferAward(AwardReferrerConfig awardConfig, string newUserName, CustomerMySqlTransaction trans)
        {
            //王老板修改，推荐注册不再给奖励
            return true;

            //bool isOK = false;
            //lock (_lockFortuneAction)
            //{
            //    PlayerFortuneInfo newFortuneInfo = null;
            //    //此处不直接修改内存对象，先修改数据库，如果成功，重新从数据库加载，否则不动内存！
            //    newFortuneInfo = BasePlayer.FortuneInfo.CopyTo();

            //    //按王忠岩要求，无限涨长 20161013
            //    //if (newFortuneInfo.Exp < GlobalConfig.GameConfig.CanExchangeMinExp)
            //    //{
            //        newFortuneInfo.Exp += awardConfig.AwardReferrerExp;
            //    //}
            //    newFortuneInfo.GoldCoin += awardConfig.AwardReferrerGoldCoin;
            //    newFortuneInfo.MinersCount += awardConfig.AwardReferrerMiners;
            //    newFortuneInfo.MinesCount += awardConfig.AwardReferrerMines;
            //    newFortuneInfo.StonesReserves += awardConfig.AwardReferrerMines * GlobalConfig.GameConfig.StonesReservesPerMines;
            //    newFortuneInfo.StockOfStones += awardConfig.AwardReferrerStones;

            //    isOK = this.SaveUserFortuneInfoToDB(newFortuneInfo, trans);
            //    BasePlayer.FortuneInfo.CopyFrom(newFortuneInfo);
            //}

            //DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(new ExpChangeRecord()
            //{
            //    UserID = this.BasePlayer.SimpleInfo.UserID,
            //    UserName = this.BasePlayer.SimpleInfo.UserName,
            //    AddExp = awardConfig.AwardReferrerExp,
            //    NewExp = this.BasePlayer.FortuneInfo.Exp,
            //    OperContent = "邀请玩家[" + newUserName + "]注册，并登录成功。获取" + awardConfig.ReferLevel + "级奖励",
            //    Time = MyDateTime.FromDateTime(DateTime.Now)
            //}, trans);

            //if (this.BasePlayer.FortuneInfo.Exp >= 50 && (this.BasePlayer.FortuneInfo.Exp - awardConfig.AwardReferrerExp < 50))
            //{
            //    AgentAwardController.Instance.PlayerRechargeRMB(this.BasePlayer, AgentAwardType.PlayerAgentExp, 0, trans);
            //}

            //return isOK;
        }

        public int RouletteSpendStone(int stoneCount)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.StockOfStones < stoneCount)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                this.BasePlayer.FortuneInfo.StockOfStones -= stoneCount;
                bool isOK = this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家参与幸运大转盘花费" + stoneCount + "矿石", null);

                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
        }

        public bool RouletteWinVirtualAwardPayUpdatePlayer(string userName, RouletteAwardItem awardItem)
        {
            CustomerMySqlTransaction trans = null;
            bool isOK = false;
            try
            {
                trans = MyDBHelper.Instance.CreateTrans();
                lock (_lockFortuneAction)
                {
                    PlayerFortuneInfo newFortuneInfo = null;
                    //此处不直接修改内存对象，先修改数据库，如果成功，重新从数据库加载，否则不动内存！
                    newFortuneInfo = BasePlayer.FortuneInfo.CopyTo();

                    switch (awardItem.RouletteAwardType)
                    {
                        case RouletteAwardType.None:
                            return false;
                        case RouletteAwardType.Stone:
                            newFortuneInfo.StockOfStones += awardItem.AwardNumber;
                            break;
                        case RouletteAwardType.GoldCoin:
                            newFortuneInfo.GoldCoin += awardItem.AwardNumber;
                            break;
                        case RouletteAwardType.Exp:
                            newFortuneInfo.Exp += awardItem.AwardNumber;
                            DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(new ExpChangeRecord()
                            {
                                UserID = this.BasePlayer.SimpleInfo.UserID,
                                UserName = this.BasePlayer.SimpleInfo.UserName,
                                AddExp = awardItem.AwardNumber,
                                NewExp = newFortuneInfo.Exp,
                                OperContent = "幸运大转盘中奖",
                                Time = MyDateTime.FromDateTime( DateTime.Now)
                            }, trans);

                            if (this.BasePlayer.FortuneInfo.Exp >= 50 && (this.BasePlayer.FortuneInfo.Exp - awardItem.AwardNumber < 50))
                            {
                                AgentAwardController.Instance.PlayerRechargeRMB(this.BasePlayer, AgentAwardType.PlayerAgentExp, 0, trans);
                            }

                            break;
                        case RouletteAwardType.StoneReserve:
                            newFortuneInfo.StonesReserves += awardItem.AwardNumber;
                            break;
                        default:
                            return false;

                    }

                    isOK = this.SaveUserFortuneInfoToDB(newFortuneInfo, "玩家幸运大转盘中奖：" + awardItem.ID.ToString() + ":" + awardItem.AwardName, trans);
                    trans.Commit();

                    BasePlayer.FortuneInfo.CopyFrom(newFortuneInfo);

                }

                return true;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                LogHelper.Instance.AddErrorLog("PlayerRunnable PayRouletteWinVirtualAward Exception", exc);
                return false;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        public int JoinRaider(int betStoneCount, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.StockOfStones < betStoneCount)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                this.BasePlayer.FortuneInfo.StockOfStones -= betStoneCount;
                bool isOK = this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家参与夺宝奇兵，下注" + betStoneCount + "矿石", myTrans);

                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
        }

        public int WinRaiderGetAward(int winStoneCount, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                //if ((this.BasePlayer.FortuneInfo.StockOfStones - this.BasePlayer.FortuneInfo.FreezingStones) < winStoneCount)
                //{
                //    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                //}

                this.BasePlayer.FortuneInfo.StockOfStones += winStoneCount;
                bool isOK = this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家赢得夺宝奇兵" + winStoneCount + "矿石", myTrans);

                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
        }

        public int RequestGravel()
        {
            if (this.BasePlayer.GravelInfo == null || this.BasePlayer.GravelInfo.GravelState == PlayerGravelState.Disable)
            {
                return OperResult.RESULTCODE_GRAVEL_CANOTREQUEST;
            }
            if (this.BasePlayer.GravelInfo.GravelState == PlayerGravelState.Requested)
            {
                return OperResult.RESULTCODE_GRAVEL_REQUESTFAILED_TODAYREQUIED;
            }
            if (this.BasePlayer.GravelInfo.GravelState == PlayerGravelState.Requestable)
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int GetGravel(PlayerGravelRequsetRecordInfo record, CustomerMySqlTransaction myTrans)
        {
            if (this.BasePlayer.GravelInfo == null || this.BasePlayer.GravelInfo.GravelState != PlayerGravelState.Getable)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            this.BasePlayer.GravelInfo.Gravel += record.Gravel;
            if (this.BasePlayer.GravelInfo.FirstGetGravelTime == null)
            {
                this.BasePlayer.GravelInfo.FirstGetGravelTime = new MyDateTime(DateTime.Now);
            }
            this.BasePlayer.GravelInfo.GravelState = PlayerGravelState.Requestable;
            DBProvider.UserDBProvider.SavePlayerGravelInfo(this.BasePlayer.GravelInfo, myTrans);
            return OperResult.RESULTCODE_TRUE;
        }

        public void RefreshGravel()
        {
            this.BasePlayer.GravelInfo = DBProvider.UserDBProvider.GetPlayerGravelInfo(this.BasePlayer.SimpleInfo.UserID);
        }

        public int BetInGambleStone(GambleStoneItemColor color, int stoneCount, int gravelCount, CustomerMySqlTransaction myTrans)
        {
            if (gravelCount > 0)
            {
                if (this.BasePlayer.GravelInfo.Gravel < gravelCount)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                this.BasePlayer.GravelInfo.Gravel -= gravelCount;
                DBProvider.UserDBProvider.SavePlayerGravelInfo(this.BasePlayer.GravelInfo, myTrans);
                LogHelper.Instance.AddInfoLog("玩家参与赌石，下注：" + color.ToString() + stoneCount + "矿石+" + gravelCount + "碎片");
            }
            if (stoneCount > 0)
            {
                if (this.BasePlayer.FortuneInfo.StockOfStones < stoneCount)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                this.BasePlayer.FortuneInfo.StockOfStones -= stoneCount;
                bool isOK = this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家参与赌石，下注：" + color.ToString() + stoneCount.ToString() + "矿石+" + gravelCount.ToString() + "碎片", myTrans);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int WinGambleStone(int winnedStone, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                this.BasePlayer.FortuneInfo.StockOfStones += winnedStone;
                bool isOK = this.SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家参与赌石，赢得：" + winnedStone.ToString() + "矿石", myTrans);

                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
        }
        //
        public int HandlePlayerRemoteService(MyDateTime serviceTime, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.IsLongTermRemoteServiceUser)
                {
                    if (this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime == null)
                    {
                        return OperResult.RESULTCODE_REMOTESERVICE_HANDLEFAILED_TIMEOUT;
                    }
                    if (this.BasePlayer.FortuneInfo.UserRemoteServerValidStopTime.ToDateTime() < serviceTime.ToDateTime())
                    {
                        return OperResult.RESULTCODE_REMOTESERVICE_HANDLEFAILED_TIMEOUT;
                    }
                }
                else
                {
                    if (this.BasePlayer.FortuneInfo.UserRemoteServiceValidTimes <= 0)
                    {
                        return OperResult.RESULTCODE_REMOTESERVICE_HANDLEFAILED_TIMEOUT;
                    }

                    this.BasePlayer.FortuneInfo.UserRemoteServiceValidTimes--;
                    SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家使用一次远程服务", myTrans);
                }

                return OperResult.RESULTCODE_TRUE;
            }
        }

        public int BuyVirtualShoppingItem(VirtualShoppingItem shoppingItem, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.ShoppingCreditsEnabled < shoppingItem.ValueShoppingCredits)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                //每消费10积分增长一点贡献值
                this.BasePlayer.FortuneInfo.Exp += (int)(shoppingItem.ValueShoppingCredits / 10);

                this.BasePlayer.FortuneInfo.ShoppingCreditsEnabled -= shoppingItem.ValueShoppingCredits;
                this.BasePlayer.FortuneInfo.StockOfDiamonds += shoppingItem.GainDiamond;
                this.BasePlayer.FortuneInfo.Exp += shoppingItem.GainExp;
                this.BasePlayer.FortuneInfo.GoldCoin += shoppingItem.GainGoldCoin;
                this.BasePlayer.GravelInfo.Gravel += (int)shoppingItem.GainGravel;
                this.BasePlayer.FortuneInfo.StonesReserves += shoppingItem.GainMine_StoneReserves;
                this.BasePlayer.FortuneInfo.MinersCount += shoppingItem.GainMiner;
                this.BasePlayer.FortuneInfo.RMB += shoppingItem.GainRMB;
                this.BasePlayer.FortuneInfo.ShoppingCreditsEnabled += shoppingItem.GainShoppingCredits;
                this.BasePlayer.FortuneInfo.StockOfStones += shoppingItem.GainStone;
                SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家购买虚拟商品：" + shoppingItem.Name, myTrans);
                DBProvider.UserDBProvider.SavePlayerGravelInfo(this.BasePlayer.GravelInfo, myTrans);

                return OperResult.RESULTCODE_TRUE;
            }
        }
        
        public int BuyDiamondShoppingItem(DiamondShoppingItem shoppingItem, CustomerMySqlTransaction myTrans)
        {
            lock (_lockFortuneAction)
            {
                if (this.BasePlayer.FortuneInfo.StockOfDiamonds < (decimal)shoppingItem.ValueDiamonds)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                this.BasePlayer.FortuneInfo.StockOfDiamonds -= (decimal)shoppingItem.ValueDiamonds;
                SaveUserFortuneInfoToDB(this.BasePlayer.FortuneInfo, "玩家购买钻石商品：" + shoppingItem.Name, myTrans);

                return OperResult.RESULTCODE_TRUE;
            }
        }
    }
}
