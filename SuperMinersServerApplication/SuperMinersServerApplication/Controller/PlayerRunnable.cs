using DataBaseProvider;
using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
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
        public PlayerInfo BasePlayer { get; private set; }
        private object _lockSimpleAction = new object();
        private object _lockFortuneAction = new object();

        public PlayerRunnable(PlayerInfo player)
        {
            BasePlayer = player;
        }

        public bool SetFortuneInfo(PlayerFortuneInfo fortuneInfo)
        {
            lock (this._lockFortuneAction)
            {
                bool isOK = DBProvider.UserDBProvider.SavePlayerFortuneInfo(fortuneInfo);
                if (isOK)
                {
                    this.BasePlayer.FortuneInfo = fortuneInfo;
                }

                return isOK;
            }
        }

        public float MaxTempStonesOutput
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

        public bool ChangePlayerSimpleInfo(string nickName, string alipayAccount, string alipayRealName, string email, string qq)
        {
            if (!string.IsNullOrEmpty(this.BasePlayer.SimpleInfo.Alipay) && !string.IsNullOrEmpty(this.BasePlayer.SimpleInfo.AlipayRealName))
            {
                //先做验证，如果玩家之前已经绑定过支付信息，而本次又修改了支付宝信息，则返回false.
                if (this.BasePlayer.SimpleInfo.Alipay != alipayAccount || this.BasePlayer.SimpleInfo.AlipayRealName != alipayRealName)
                {
                    return false;
                }
            }
            DBProvider.UserDBProvider.UpdatePlayerSimpleInfo(BasePlayer.SimpleInfo.UserName, nickName, alipayAccount, alipayRealName, email, qq);
            this.BasePlayer.SimpleInfo.Alipay = alipayAccount;
            this.BasePlayer.SimpleInfo.AlipayRealName = alipayRealName;
            return true;
        }

        public bool LockPlayer()
        {
            this.BasePlayer.SimpleInfo.LockedLogin = true;
            this.BasePlayer.SimpleInfo.LockedLoginTime = DateTime.Now;
            this.BasePlayer.SimpleInfo.LastLogOutTime = null;
            this.BasePlayer.FortuneInfo.TempOutputStonesStartTime = null;
            bool isOK = DBProvider.UserDBProvider.UpdatePlayerLockedState(this.BasePlayer.SimpleInfo.UserName, this.BasePlayer.SimpleInfo.LockedLogin, this.BasePlayer.SimpleInfo.LockedLoginTime);
            return isOK;
        }

        public bool UnlockPlayer()
        {
            this.BasePlayer.SimpleInfo.LockedLogin = false;
            this.BasePlayer.SimpleInfo.LockedLoginTime = null;
            bool isOK = DBProvider.UserDBProvider.UpdatePlayerLockedState(this.BasePlayer.SimpleInfo.UserName, this.BasePlayer.SimpleInfo.LockedLogin, null);
            return isOK;
        }

        public bool LogoutPlayer()
        {
            BasePlayer.SimpleInfo.LastLogOutTime = DateTime.Now;
            return DBProvider.UserDBProvider.LogoutPlayer(BasePlayer.SimpleInfo, BasePlayer.FortuneInfo);
        }

        public void RefreshFortune()
        {
            BasePlayer.FortuneInfo = DBProvider.UserDBProvider.GetPlayerFortuneInfo(BasePlayer.SimpleInfo.UserName);
            ComputePlayerOfflineStoneOutput();
        }

        public void ComputePlayerOfflineStoneOutput()
        {
            if (BasePlayer.SimpleInfo.LastLogOutTime == null ||
                BasePlayer.SimpleInfo.LastLogOutTime.Value == PlayerInfo.INVALIDDATETIME)
            {
                //表示该玩家之前没有登录过，以本次登录时间为起始。
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = BasePlayer.SimpleInfo.LastLoginTime;
                return;
            }

            DateTime startTime;
            if (BasePlayer.FortuneInfo.TempOutputStonesStartTime == null)
            {
                //如果没有保存起始时间，则以上一次退出时间为起始。
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = BasePlayer.SimpleInfo.LastLogOutTime.Value;
            }

            startTime = BasePlayer.FortuneInfo.TempOutputStonesStartTime.Value;

            TimeSpan span = BasePlayer.SimpleInfo.LastLoginTime.Value - startTime;
            if (span.TotalHours < 0)
            {
                return;
            }

            lock (this._lockFortuneAction)
            {
                float tempOutput = (float)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                if (tempOutput > MaxTempStonesOutput)
                {
                    tempOutput = MaxTempStonesOutput;
                }
                if (tempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                {
                    tempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                }
                BasePlayer.FortuneInfo.TempOutputStones = tempOutput;
            }
        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="stones"></param>
        /// <returns></returns>
        public int GatherStones(float stones)
        {
            int IntTempOutput;
            DateTime stopTime = DateTime.Now;
            if (stones <= 0)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo);
                return 0;
            }

            if (BasePlayer.FortuneInfo.TempOutputStonesStartTime == null)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                return 0;
            }

            TimeSpan span = stopTime - BasePlayer.FortuneInfo.TempOutputStonesStartTime.Value;
            if (span.TotalHours > 0)
            {
                lock (this._lockFortuneAction)
                {
                    float tempOutput = (float)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                    if (tempOutput > MaxTempStonesOutput)
                    {
                        tempOutput = MaxTempStonesOutput;
                    }
                    if (tempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                    {
                        tempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                    }

                    float computeTempOutput = stones < tempOutput ? stones : tempOutput;
                    IntTempOutput = (int)computeTempOutput;

                    BasePlayer.FortuneInfo.TempOutputStones = 0;
                    BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                    BasePlayer.FortuneInfo.StockOfStones += IntTempOutput;

                    //将零头从矿石储量中减去！
                    BasePlayer.FortuneInfo.TotalProducedStonesCount += computeTempOutput;
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo);
                }

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.GatherStone, IntTempOutput);
                return IntTempOutput;
            }

            return 0;
        }

        public int BuyMiner(int minersCount)
        {
            if (minersCount <= 0)
            {
                return 0;
            }

            lock (_lockFortuneAction)
            {
                float allGoldCoin = BasePlayer.FortuneInfo.GoldCoin + BasePlayer.FortuneInfo.RMB * GlobalConfig.GameConfig.RMB_GoldCoin;
                float allNeedGoldCoin = minersCount * GlobalConfig.GameConfig.GoldCoin_Miner;
                if (allNeedGoldCoin > allGoldCoin)
                {
                    return 0;
                }

                if (allNeedGoldCoin < BasePlayer.FortuneInfo.GoldCoin)
                {
                    BasePlayer.FortuneInfo.GoldCoin -= allNeedGoldCoin;
                }
                else
                {
                    float gc = allNeedGoldCoin - BasePlayer.FortuneInfo.GoldCoin;
                    int needRMB = (int)Math.Ceiling(gc / GlobalConfig.GameConfig.RMB_GoldCoin);
                    if (needRMB > BasePlayer.FortuneInfo.RMB)
                    {
                        return 0;
                    }

                    BasePlayer.FortuneInfo.RMB -= needRMB;
                    BasePlayer.FortuneInfo.GoldCoin = needRMB * GlobalConfig.GameConfig.RMB_GoldCoin - gc;
                }
                BasePlayer.FortuneInfo.MinersCount += minersCount;

                CustomerMySqlTransaction trans = null;
                try
                {
                    trans = MyDBHelper.Instance.CreateTrans();
                    if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans))
                    {
                        trans.Rollback();
                        RefreshFortune();
                        return -1;
                    }
                    MinersBuyRecord record = new MinersBuyRecord()
                    {
                        UserName = BasePlayer.SimpleInfo.UserName,
                        SpendGoldCoin = allNeedGoldCoin,
                        GainMinersCount = minersCount,
                        Time = DateTime.Now
                    };
                    DBProvider.BuyMinerRecordDBProvider.AddBuyMinerRecord(record, trans);

                    trans.Commit();
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    LogHelper.Instance.AddErrorLog("Buy Miners Error!", exc);
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }
                }

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.BuyMiner, minersCount);
                return minersCount;
            }
        }

        /// <summary>
        /// 矿山只能用RMB购买
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMineByRMB(int minesCount)
        {
            if (minesCount <= 0)
            {
                return 0;
            }

            lock (_lockFortuneAction)
            {
                float needRMB = minesCount * GlobalConfig.GameConfig.RMB_Mine;
                if (needRMB > BasePlayer.FortuneInfo.RMB)
                {
                    return 0;
                }

                float newReservers = minesCount * GlobalConfig.GameConfig.StonesReservesPerMines;
                BasePlayer.FortuneInfo.RMB -= needRMB;
                BasePlayer.FortuneInfo.MinesCount += minesCount;
                BasePlayer.FortuneInfo.StonesReserves += newReservers;
                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo))
                {
                    RefreshFortune();
                    return -1;
                }

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.BuyMine, minesCount,
                    "增加了 " + newReservers.ToString() + " 的矿石储量");
                return minesCount;
            }
        }

        /// <summary>
        /// 支付购买矿石订单后，更新买家信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="rmbPay">true为灵币支付；false为支付宝支付</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool PayBuyStonesUpdateBuyerInfo(BuyStonesOrder order, bool rmbPay, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                if (rmbPay)
                {
                    if (BasePlayer.FortuneInfo.RMB < order.StonesOrder.ValueRMB)
                    {
                        return false;
                    }
                    BasePlayer.FortuneInfo.RMB -= order.StonesOrder.ValueRMB;
                }
                BasePlayer.FortuneInfo.StockOfStones += order.StonesOrder.SellStonesCount;
                BasePlayer.FortuneInfo.GoldCoin += order.AwardGoldCoin;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
            }

            return true;
        }

        /// <summary>
        /// 支付购买矿石订单后，更新卖家信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool PayBuyStonesUpdateSellerInfo(BuyStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                BasePlayer.FortuneInfo.RMB += (order.StonesOrder.ValueRMB - order.StonesOrder.Expense);
                BasePlayer.FortuneInfo.StockOfStones -= order.StonesOrder.SellStonesCount;
                BasePlayer.FortuneInfo.FreezingStones -= order.StonesOrder.SellStonesCount;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
            }

            return true;
        }

        /// <summary>
        /// 0表示成功；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="SellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                float sellableStones = BasePlayer.FortuneInfo.StockOfStones - BasePlayer.FortuneInfo.FreezingStones;
                if (order.SellStonesCount > sellableStones)
                {
                    return 1;
                }

                BasePlayer.FortuneInfo.FreezingStones += order.SellStonesCount;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
            }

            return 0;
        }

        private string CreateOrderNumber(DateTime time, string userName)
        {
            Random r = new Random();
            return time.ToLongDateString() + time.ToLongTimeString() + BasePlayer.SimpleInfo.UserName.GetHashCode().ToString() + r.Next(1000, 9999).ToString();
        }

        #region 取消充值功能
        public bool RechargeGoldCoin(float yuan)
        {
            lock (_lockFortuneAction)
            {
                GoldCoinRechargeRecord record = new GoldCoinRechargeRecord()
                {
                    UserName = BasePlayer.SimpleInfo.UserName,
                    RechargeMoney = yuan,
                    GainGoldCoin = yuan * GlobalConfig.GameConfig.Yuan_RMB * GlobalConfig.GameConfig.RMB_GoldCoin,
                    Time = DateTime.Now
                };
                BasePlayer.FortuneInfo.GoldCoin += record.GainGoldCoin;

                var trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
                    DBProvider.RechargeDBProvider.AddRechargeGoldCoinRecord(record, trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    RefreshFortune();
                    throw exc;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public bool RechargeRMB(float yuan)
        {
            lock (_lockFortuneAction)
            {
                RMBRechargeRecord record = new RMBRechargeRecord()
                {
                    UserName = BasePlayer.SimpleInfo.UserName,
                    RechargeMoney = yuan,
                    GainRMB = yuan * GlobalConfig.GameConfig.Yuan_RMB,
                    Time = DateTime.Now
                };
                BasePlayer.FortuneInfo.RMB += record.GainRMB;

                var trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
                    DBProvider.RechargeDBProvider.AddRechargeRMBRecord(record, trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    RefreshFortune();
                    throw exc;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
        #endregion

        public bool ReferAward(AwardReferrerConfig awardConfig, CustomerMySqlTransaction trans)
        {
            PlayerFortuneInfo newFortuneInfo = null;
            lock (_lockFortuneAction)
            {
                //此处不直接修改内存对象，先修改数据库，如果成功，重新从数据库加载，否则不动内存！
                newFortuneInfo = BasePlayer.FortuneInfo.CopyTo();
            }

            if (newFortuneInfo.Exp < GlobalConfig.GameConfig.CanExchangeMinExp)
            {
                newFortuneInfo.Exp += awardConfig.AwardReferrerExp;
            }
            newFortuneInfo.GoldCoin += awardConfig.AwardReferrerGoldCoin;
            newFortuneInfo.MinersCount += awardConfig.AwardReferrerMiners;
            newFortuneInfo.MinesCount += awardConfig.AwardReferrerMines;
            newFortuneInfo.StonesReserves += awardConfig.AwardReferrerMines * GlobalConfig.GameConfig.StonesReservesPerMines;
            newFortuneInfo.StockOfStones += awardConfig.AwardReferrerStones;
            return DBProvider.UserDBProvider.SavePlayerFortuneInfo(newFortuneInfo, trans);
        }
    }
}
