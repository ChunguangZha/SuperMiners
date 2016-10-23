﻿using DataBaseProvider;
using MetaData;
using MetaData.Game.Roulette;
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
            //this.BasePlayer.SimpleInfo.LastLogOutTime = null;
            //this.BasePlayer.FortuneInfo.TempOutputStonesStartTime = null;
            bool isOK = DBProvider.UserDBProvider.UpdatePlayerLockedState(this.BasePlayer.SimpleInfo.UserName, this.BasePlayer.SimpleInfo.LockedLogin, this.BasePlayer.SimpleInfo.LockedLoginTime);
            return isOK;
        }

        public bool UnlockPlayer()
        {
            this.BasePlayer.SimpleInfo.LockedLogin = false;
            this.BasePlayer.SimpleInfo.LockedLoginTime = null;
            this.BasePlayer.FortuneInfo.TempOutputStonesStartTime = DateTime.Now;
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

        public decimal ComputePlayerOfflineStoneOutput()
        {
            //if (BasePlayer.SimpleInfo.LastLogOutTime == null ||
            //    BasePlayer.SimpleInfo.LastLogOutTime.Value == PlayerInfo.INVALIDDATETIME)
            //{
            //    //表示该玩家之前没有登录过，以本次登录时间为起始。
            //    BasePlayer.FortuneInfo.TempOutputStones = 0;
            //    BasePlayer.FortuneInfo.TempOutputStonesStartTime = BasePlayer.SimpleInfo.LastLoginTime;
            //    return;
            //}

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

            lock (this._lockFortuneAction)
            {
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

            LogHelper.Instance.AddInfoLog("玩家 [" + BasePlayer.SimpleInfo.UserName + "] 请求登录矿场, 计算离线产出=" + BasePlayer.FortuneInfo.TempOutputStones + ", startTime=" + startTime);

            return tempOutput;
        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="stones">-1表示清空临时产出</param>
        /// <returns></returns>
        public int GatherStones(decimal stones)
        {
            int IntTempOutput;
            DateTime stopTime = DateTime.Now;
            if (stones <= 0)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo);
                return OperResult.RESULTCODE_TRUE;
            }

            if (BasePlayer.FortuneInfo.TempOutputStonesStartTime == null)
            {
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                return OperResult.RESULTCODE_FALSE;
            }

            TimeSpan span = stopTime - BasePlayer.FortuneInfo.TempOutputStonesStartTime.Value;
            if (span.TotalHours > 0)
            {
                lock (this._lockFortuneAction)
                {
                    decimal tempOutput = (decimal)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                    if (tempOutput > MaxTempStonesOutput)
                    {
                        tempOutput = MaxTempStonesOutput;
                    }
                    if (tempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                    {
                        tempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                    }

                    decimal computeTempOutput = stones < tempOutput ? stones : tempOutput;
                    IntTempOutput = (int)computeTempOutput;

                    BasePlayer.FortuneInfo.TempOutputStones = 0;
                    BasePlayer.FortuneInfo.TempOutputStonesStartTime = stopTime;
                    BasePlayer.FortuneInfo.StockOfStones += IntTempOutput;

                    //将零头从矿石储量中减去！
                    BasePlayer.FortuneInfo.TotalProducedStonesCount += computeTempOutput;
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo);
                }

                PlayerActionController.Instance.AddLog(this.BasePlayer.SimpleInfo.UserName, MetaData.ActionLog.ActionType.GatherStone, IntTempOutput);
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
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
                    if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans))
                    {
                        trans.Rollback();
                        RefreshFortune();
                        return OperResult.RESULTCODE_FALSE;
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

        /// <summary>
        /// 矿山只能用RMB购买。0表示账户余额不足；-1表示操作失败；minesCount表示成功。
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMineByRMB(int minesCount, CustomerMySqlTransaction myTrans)
        {
            if (minesCount <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            lock (_lockFortuneAction)
            {
                decimal needRMB = minesCount * GlobalConfig.GameConfig.RMB_Mine;
                if (needRMB > BasePlayer.FortuneInfo.RMB)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
                }

                decimal newReservers = minesCount * GlobalConfig.GameConfig.StonesReservesPerMines;
                BasePlayer.FortuneInfo.RMB -= needRMB;
                BasePlayer.FortuneInfo.MinesCount += minesCount;
                BasePlayer.FortuneInfo.StonesReserves += newReservers;
                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, myTrans))
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
        public int BuyMineByAlipay(decimal moneyYuan, decimal minesCount, CustomerMySqlTransaction myTrans)
        {
            if (minesCount <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            lock (_lockFortuneAction)
            {
                decimal newReservers = minesCount * GlobalConfig.GameConfig.StonesReservesPerMines;
                BasePlayer.FortuneInfo.MinesCount += minesCount;
                BasePlayer.FortuneInfo.StonesReserves += newReservers;

                BasePlayer.FortuneInfo.Exp += (int)moneyYuan;
                ExpChangeRecord expRecord = new ExpChangeRecord()
                {
                    UserID = this.BasePlayer.SimpleInfo.UserID,
                    UserName = this.BasePlayer.SimpleInfo.UserName,
                    AddExp = (int)moneyYuan,
                    NewExp = BasePlayer.FortuneInfo.Exp,
                    Time = DateTime.Now,
                    OperContent = "玩家支付宝充值金币奖励"
                };

                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, myTrans))
                {
                    RefreshFortune();
                    return OperResult.RESULTCODE_FALSE;
                }
                DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(expRecord, myTrans);

                return OperResult.RESULTCODE_TRUE;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rmbValue"></param>
        /// <param name="goldcoinValue"></param>
        /// <returns></returns>
        public int RechargeGoldCoineByRMB(int rmbValue, int goldcoinValue, CustomerMySqlTransaction myTrans)
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
                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, myTrans))
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
                    Time = DateTime.Now,
                    OperContent = "玩家支付宝充值金币奖励"
                };

                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, myTrans))
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
        public bool PayBuyStonesUpdateBuyerInfo(bool isAlipayPay, BuyStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                if (!isAlipayPay)
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
                if (BasePlayer.FortuneInfo.FreezingStones < order.StonesOrder.SellStonesCount)
                {
                    return false;
                }
                BasePlayer.FortuneInfo.RMB += (order.StonesOrder.ValueRMB - order.StonesOrder.Expense);
                BasePlayer.FortuneInfo.StockOfStones -= order.StonesOrder.SellStonesCount;
                BasePlayer.FortuneInfo.FreezingStones -= order.StonesOrder.SellStonesCount;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
            }

            return true;
        }

        /// <summary>
        /// RESULTCODE_TRUE；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="SellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                decimal sellableStones = BasePlayer.FortuneInfo.StockOfStones - BasePlayer.FortuneInfo.FreezingStones;
                if (order.SellStonesCount > sellableStones)
                {
                    return OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK;
                }

                BasePlayer.FortuneInfo.FreezingStones += order.SellStonesCount;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
            }

            return OperResult.RESULTCODE_TRUE;
        }

        public int CancelSellStones(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            lock (_lockFortuneAction)
            {
                BasePlayer.FortuneInfo.FreezingStones -= order.SellStonesCount;

                DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
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
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.FortuneInfo, myTrans);
                    DBProvider.WithdrawRMBRecordDBProvider.AddWithdrawRMBRecord(record, myTrans);

                    myTrans.Commit();

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
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.FortuneInfo, myTrans);
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

        private string CreateOrderNumber(DateTime time, string userName)
        {
            Random r = new Random();
            return time.ToLongDateString() + time.ToLongTimeString() + BasePlayer.SimpleInfo.UserName.GetHashCode().ToString() + r.Next(1000, 9999).ToString();
        }

        #region 取消充值功能

        //public bool RechargeRMB(decimal yuan)
        //{
        //    lock (_lockFortuneAction)
        //    {
        //        RMBRechargeRecord record = new RMBRechargeRecord()
        //        {
        //            UserName = BasePlayer.SimpleInfo.UserName,
        //            RechargeMoney = yuan,
        //            GainRMB = yuan * GlobalConfig.GameConfig.Yuan_RMB,
        //            Time = DateTime.Now
        //        };
        //        BasePlayer.FortuneInfo.RMB += record.GainRMB;

        //        var trans = MyDBHelper.Instance.CreateTrans();
        //        try
        //        {
        //            DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
        //            DBProvider.RechargeDBProvider.AddRechargeRMBRecord(record, trans);
        //            trans.Commit();
        //            return true;
        //        }
        //        catch (Exception exc)
        //        {
        //            trans.Rollback();
        //            RefreshFortune();
        //            throw exc;
        //        }
        //        finally
        //        {
        //            trans.Dispose();
        //        }
        //    }
        //}
        #endregion

        public bool ReferAward(AwardReferrerConfig awardConfig, CustomerMySqlTransaction trans)
        {
            bool isOK = false;
            lock (_lockFortuneAction)
            {
                PlayerFortuneInfo newFortuneInfo = null;
                //此处不直接修改内存对象，先修改数据库，如果成功，重新从数据库加载，否则不动内存！
                newFortuneInfo = BasePlayer.FortuneInfo.CopyTo();

                //按王忠岩要求，无限涨长 20161013
                //if (newFortuneInfo.Exp < GlobalConfig.GameConfig.CanExchangeMinExp)
                //{
                    newFortuneInfo.Exp += awardConfig.AwardReferrerExp;
                //}
                newFortuneInfo.GoldCoin += awardConfig.AwardReferrerGoldCoin;
                newFortuneInfo.MinersCount += awardConfig.AwardReferrerMiners;
                newFortuneInfo.MinesCount += awardConfig.AwardReferrerMines;
                newFortuneInfo.StonesReserves += awardConfig.AwardReferrerMines * GlobalConfig.GameConfig.StonesReservesPerMines;
                newFortuneInfo.StockOfStones += awardConfig.AwardReferrerStones;

                isOK = DBProvider.UserDBProvider.SavePlayerFortuneInfo(newFortuneInfo, trans);
                BasePlayer.FortuneInfo.CopyFrom(newFortuneInfo);
            }

            DBProvider.ExpChangeRecordDBProvider.AddExpChangeRecord(new ExpChangeRecord()
            {
                UserID = this.BasePlayer.SimpleInfo.UserID,
                UserName = this.BasePlayer.SimpleInfo.UserName,
                AddExp = awardConfig.AwardReferrerExp,
                NewExp = this.BasePlayer.FortuneInfo.Exp,
                OperContent = "邀请玩家获取" + awardConfig.ReferLevel + "级奖励",
                Time = DateTime.Now
            }, trans);

            if (this.BasePlayer.FortuneInfo.Exp >= 50 && (this.BasePlayer.FortuneInfo.Exp - awardConfig.AwardReferrerExp < 50))
            {
                AgentAwardController.Instance.PlayerRechargeRMB(this.BasePlayer, AgentAwardType.PlayerAgentExp, 0, trans);
            }

            return isOK;
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
                bool isOK = DBProvider.UserDBProvider.SavePlayerFortuneInfo(this.BasePlayer.FortuneInfo);
                
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
                                Time = DateTime.Now
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

                    isOK = DBProvider.UserDBProvider.SavePlayerFortuneInfo(newFortuneInfo, trans);
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
    }
}
