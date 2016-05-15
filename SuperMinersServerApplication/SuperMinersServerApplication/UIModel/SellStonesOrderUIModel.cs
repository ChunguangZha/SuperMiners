using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.UIModel
{
    class SellStonesOrderUIModel
    {
        SellStonesOrder _parentObject;
        private object _lock = new object();

        public SellStonesOrder ParentObject
        {
            get { return this._parentObject; }
        }

        public SellStonesOrderUIModel(SellStonesOrder parent)
        {
            this._parentObject = parent;
        }

        ///// <summary>
        ///// 订单编号，以日期+卖方用户名HashCode+4位随机数
        ///// </summary>
        //public string OrderNumber
        //{
        //    get { return _parentObject.OrderNumber; }
        //}

        ///// <summary>
        ///// 卖方用户名
        ///// </summary>
        //public string SellerUserName
        //{
        //    get { return _parentObject.SellerUserName; }
        //}

        ///// <summary>
        ///// 出售矿石数
        ///// </summary>
        //public int SellStonesCount
        //{
        //    get { return _parentObject.SellStonesCount; }
        //}

        ///// <summary>
        ///// 手续费
        ///// </summary>
        //public float Expense
        //{
        //    get { return _parentObject.Expense; }
        //}

        ///// <summary>
        ///// 可获取的RMB数
        ///// </summary>
        //public float GainRMB
        //{
        //    get { return _parentObject.GainRMB; }
        //}

        //public DateTime SellTime
        //{
        //    get { return _parentObject.SellTime; }
        //}

        //public bool IsLocked { get; private set; }

        //public string LockedByUserName { get; private set; }

        //public DateTime? LockedTime { get; private set; }

        public SellOrderState CheckOrderState()
        {
            lock (this._lock)
            {
                return CheckOrderStateUnlock();
            }
        }

        private SellOrderState CheckOrderStateUnlock()
        {
            if (this._parentObject.OrderState == SellOrderState.Lock)
            {
                if (string.IsNullOrEmpty(this._parentObject.LockedByUserName) || this._parentObject.LockedTime == null)
                {
                    this._parentObject.OrderState = SellOrderState.Wait;
                    this._parentObject.LockedByUserName = "";
                    this._parentObject.LockedTime = null;
                }
                else
                {
                    TimeSpan span = DateTime.Now - this._parentObject.LockedTime.Value;
                    if (span.TotalMinutes >= GlobalConfig.GameConfig.BuyOrderLockTimeMinutes)
                    {
                        this._parentObject.OrderState = SellOrderState.Wait;
                        this._parentObject.LockedByUserName = "";
                        this._parentObject.LockedTime = null;
                    }
                }
            }

            return this._parentObject.OrderState;
        }

        public bool LockOrder(string userName)
        {
            lock (this._lock)
            {
                if (CheckOrderStateUnlock() == SellOrderState.Wait)
                {
                    this._parentObject.OrderState = SellOrderState.Lock;
                    this._parentObject.LockedByUserName = userName;
                    this._parentObject.LockedTime = DateTime.Now;
                    
                    return true;
                }

                return false;
            }
        }

        public bool UnlockOrder(string userName)
        {
            lock (this._lock)
            {
                if (this._parentObject.OrderState == SellOrderState.Lock && this._parentObject.LockedByUserName == userName)
                {
                    this._parentObject.OrderState = SellOrderState.Wait;
                    this._parentObject.LockedByUserName = "";
                    this._parentObject.LockedTime = null;

                    return true;
                }

                return false;
            }
        }
    }
}
