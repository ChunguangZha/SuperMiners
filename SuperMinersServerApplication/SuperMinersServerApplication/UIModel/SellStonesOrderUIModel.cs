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

        public SellStonesOrderUIModel(SellStonesOrder parent)
        {
            this._parentObject = parent;

            IsLocked = false;
            LockedByUserName = null;
            LockedTime = null;
        }

        /// <summary>
        /// 订单编号，以日期+卖方用户名HashCode+4位随机数
        /// </summary>
        public string OrderNumber
        {
            get { return _parentObject.OrderNumber; }
        }

        /// <summary>
        /// 卖方用户名
        /// </summary>
        public string SellerUserName
        {
            get { return _parentObject.SellerUserName; }
        }

        /// <summary>
        /// 出售矿石数
        /// </summary>
        public int SellStonesCount
        {
            get { return _parentObject.SellStonesCount; }
        }

        /// <summary>
        /// 手续费
        /// </summary>
        public float Expense
        {
            get { return _parentObject.Expense; }
        }

        /// <summary>
        /// 可获取的RMB数
        /// </summary>
        public float GainRMB
        {
            get { return _parentObject.GainRMB; }
        }

        public DateTime SellTime
        {
            get { return _parentObject.SellTime; }
        }

        public bool IsLocked { get; private set; }

        public string LockedByUserName { get; private set; }

        public DateTime? LockedTime { get; private set; }

        public bool LockOrder(string userName)
        {
            lock (this._lock)
            {
                if (this.IsLocked)
                {
                    return false;
                }

                this.IsLocked = true;
                this.LockedByUserName = userName;
                this.LockedTime = DateTime.Now;
                return true;
            }
        }
    }
}
