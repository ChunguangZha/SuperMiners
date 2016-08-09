using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    class StoneOrderRunnable
    {
        private SellStonesOrder _sellOrder;
        private object _lock = new object();
        private LockSellStonesOrder _lockOrderObject;

        public string OrderNumber
        {
            get
            {
                return this._sellOrder.OrderNumber;
            }
        }

        public SellOrderState OrderState
        {
            get
            {
                return this._sellOrder.OrderState;
            }
        }

        public int StoneCount
        {
            get
            {
                return this._sellOrder.SellStonesCount;
            }
        }

        public float ValueRMB
        {
            get
            {
                return this._sellOrder.ValueRMB;
            }
        }

        public StoneOrderRunnable(SellStonesOrder sellOrder)
        {
            this._sellOrder = sellOrder;
        }

        public StoneOrderRunnable(LockSellStonesOrder lockInfo)
        {
            _lockOrderObject = lockInfo;
            this._sellOrder = lockInfo.StonesOrder;
        }

        public SellStonesOrder GetSellOrder()
        {
            return _sellOrder;
        }

        public LockSellStonesOrder GetLockedOrder()
        {
            return this._lockOrderObject;
        }
        
        public bool CheckBuyerName(string buyerUserName)
        {
            lock (this._lock)
            {
                if (this._sellOrder.OrderState == SellOrderState.Lock && this._lockOrderObject != null)
                {
                    return this._lockOrderObject.LockedByUserName == buyerUserName;
                }

                return false;
            }
        }
        
        public BuyStonesOrder Pay(float rmb, CustomerMySqlTransaction trans)
        {
            lock (this._lock)
            {
                //此处暂不检查TimeOut
                BuyStonesOrder buyOrder = new BuyStonesOrder()
                {
                    StonesOrder = this._sellOrder,
                    BuyerUserName = this._lockOrderObject.LockedByUserName,
                    BuyTime = this._lockOrderObject.LockedTime,
                    AwardGoldCoin = this._sellOrder.SellStonesCount * GlobalConfig.GameConfig.StoneBuyerAwardGoldCoinMultiple
                };

                this._sellOrder.OrderState = SellOrderState.Finish;
                DBProvider.OrderDBProvider.PayOrder(buyOrder, trans);
                DBProvider.OrderDBProvider.FinishOrderLock(this._sellOrder.OrderNumber, trans);

                return buyOrder;
            }
        }

        public LockSellStonesOrder Lock(string playerUserName)
        {
            lock (this._lock)
            {
                CustomerMySqlTransaction trans = null;
                try
                {
                    if (this._lockOrderObject != null && !CheckOrderLockedIsTimeOut())
                    {
                        return this._lockOrderObject;
                    }

                    if (trans == null)
                    {
                        trans = MyDBHelper.Instance.CreateTrans();
                    }

                    this._lockOrderObject = new LockSellStonesOrder()
                    {
                        StonesOrder = this._sellOrder,
                        PayUrl = this.CreatePayUrl(),
                        LockedByUserName = playerUserName,
                        LockedTime = DateTime.Now,
                        OrderLockedTimeSpan = 0
                    };
                    this._sellOrder.OrderState = SellOrderState.Lock;
                    DBProvider.OrderDBProvider.LockOrder(this._lockOrderObject, trans);

                    trans.Commit();

                    return this._lockOrderObject;
                }
                catch (Exception exc)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    this._sellOrder.OrderState = SellOrderState.Wait;
                    this._lockOrderObject = null;
                    LogHelper.Instance.AddErrorLog("Lock Order[" + this._sellOrder.OrderNumber + "] by User[" + playerUserName + "] Error", exc);
                    return null;
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

        private string CreatePayUrl()
        {
            float money = this._sellOrder.ValueRMB / GlobalConfig.GameConfig.Yuan_RMB;
            float money_1 = (float)Math.Round(money, 1);
            if (money_1 < money)//说明刚才是四舍了，要把他加回来
            {
                money_1 += 0.1f;
            }

            //取到角级
            string suburl = @"Alipay/AlipayDefault.aspx?on=" + this._sellOrder.OrderNumber + "&sn=矿石&mn=" + money_1.ToString();
            return suburl;
        }

        public bool ReleaseLock()
        {
            lock (this._lock)
            {
                if (this._sellOrder.OrderState == SellOrderState.Exception)
                {
                    return false;
                }
                CustomerMySqlTransaction trans = null;
                try
                {
                    trans = MyDBHelper.Instance.CreateTrans();
                    DBProvider.OrderDBProvider.ReleaseOrderLock(this._sellOrder.OrderNumber, trans);
                    trans.Commit();
                    this._sellOrder.OrderState = SellOrderState.Wait;
                    this._lockOrderObject = null;
                    return true;
                }
                catch (Exception exc)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    LogHelper.Instance.AddErrorLog("ReleaseLock Order[" + this._sellOrder.OrderNumber + "] Error", exc);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CheckOrderLockedIsTimeOut()
        {
            if (this._sellOrder.OrderState != SellOrderState.Lock || this._lockOrderObject == null)
            {
                return false;
            }

            TimeSpan span = DateTime.Now - this._lockOrderObject.LockedTime;
            this._lockOrderObject.OrderLockedTimeSpan = (int)span.TotalSeconds;
            if (this._lockOrderObject.OrderLockedTimeSpan >= GlobalConfig.GameConfig.BuyOrderLockTimeMinutes * 60)
            {
                this.ReleaseLock();
                return true;
            }

            return false;
        }
    }
}
