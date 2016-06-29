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
    class OrderRunnable
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

        public OrderRunnable(SellStonesOrder sellOrder)
        {
            this._sellOrder = sellOrder;
        }

        public OrderRunnable(LockSellStonesOrder lockInfo)
        {
            _lockOrderObject = lockInfo;
            this._sellOrder = lockInfo.StonesOrder;
        }

        public SellStonesOrder GetSellOrder()
        {
            return _sellOrder;
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

        public bool Pay(float moneyYuan, int awardGoldCoin, CustomerMySqlTransaction trans)
        {
            lock (this._lock)
            {
                //此处暂不检查TimeOut
                BuyStonesOrder buyOrder = new BuyStonesOrder()
                {
                    StonesOrder = this._sellOrder,
                    BuyerUserName = this._lockOrderObject.LockedByUserName,
                    BuyTime = this._lockOrderObject.LockedTime,
                    AwardGoldCoin = awardGoldCoin
                };

                if (DBProvider.OrderDBProvider.PayOrder(buyOrder, trans))
                {
                    this._sellOrder.OrderState = SellOrderState.Finish;
                    DBProvider.OrderDBProvider.FinishOrderLock(this._sellOrder.OrderNumber, trans);

                    return true;
                }

                return false;
            }
        }

        public bool Lock(string playerUserName)
        {
            lock (this._lock)
            {
                CustomerMySqlTransaction trans = null;
                try
                {
                    if (this._lockOrderObject != null)
                    {
                        if (CheckLockTimeOut())
                        {
                            //上一次锁定超时，现将其解锁
                            if (trans == null)
                            {
                                trans = MyDBHelper.Instance.CreateTrans();
                            }
                            if (DBProvider.OrderDBProvider.ReleaseOrderLock(_lockOrderObject.StonesOrder.OrderNumber, trans))
                            {
                                this._lockOrderObject = null;
                                this._sellOrder.OrderState = SellOrderState.Wait;
                            }
                            else
                            {
                                trans.Rollback();
                                trans.Dispose();
                                trans = null;
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (trans == null)
                    {
                        trans = MyDBHelper.Instance.CreateTrans();
                    }

                    this._lockOrderObject = new LockSellStonesOrder()
                    {
                        StonesOrder = this._sellOrder,
                        LockedByUserName = playerUserName,
                        LockedTime = DateTime.Now
                    };
                    this._sellOrder.OrderState = SellOrderState.Lock;
                    DBProvider.OrderDBProvider.LockOrder(this._lockOrderObject, trans);

                    trans.Commit();

                    return true;
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

        private bool CheckLockTimeOut()
        {
            if (this._lockOrderObject == null || this._sellOrder.OrderState == SellOrderState.Exception)
            {
                return false;
            }

            TimeSpan span = DateTime.Now - this._lockOrderObject.LockedTime;
            return (span.TotalMinutes >= GlobalConfig.GameConfig.BuyOrderLockTimeMinutes);
        }
    }
}
