using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class LockSellStonesOrderUIModel : BaseModel
    {
        public LockSellStonesOrderUIModel(LockSellStonesOrder parent)
        {
            this.ParentObject = parent;
        }

        private LockSellStonesOrder _parentObject;

        public LockSellStonesOrder ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("SellerUserName");
                NotifyPropertyChange("SellStonesCount");
                NotifyPropertyChange("Expense");
                NotifyPropertyChange("ValueRMB");
                NotifyPropertyChange("SellTime");
                NotifyPropertyChange("OrderState");
                NotifyPropertyChange("OrderStateString");

            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.StonesOrder.OrderNumber;
            }
        }

        public string SellerUserName
        {
            get
            {
                return this._parentObject.StonesOrder.SellerUserName;
            }
        }

        public int SellStonesCount
        {
            get
            {
                return this._parentObject.StonesOrder.SellStonesCount;
            }
        }

        public float Expense
        {
            get
            {
                return this._parentObject.StonesOrder.Expense;
            }
        }

        public float ValueRMB
        {
            get
            {
                return this._parentObject.StonesOrder.ValueRMB;
            }
        }

        public DateTime SellTime
        {
            get
            {
                return this._parentObject.StonesOrder.SellTime;
            }
        }

        public SellOrderState OrderState
        {
            get
            {
                return this._parentObject.StonesOrder.OrderState;
            }
        }

        public string OrderStateString
        {
            get
            {
                return "等待付款";
            }
        }

        public string LockedByUserName
        {
            get
            {
                return this._parentObject.LockedByUserName;
            }
        }

        public string PayUrl
        {
            get
            {
                return this._parentObject.PayUrl;
            }
        }

        public DateTime LockedTime
        {
            get
            {
                return this._parentObject.LockedTime;
            }
        }
    }
}
