using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class BuyStonesOrderUIModel : BaseModel
    {
        public BuyStonesOrderUIModel(BuyStonesOrder parent)
        {
            this.ParentObject = parent;
        }

        private BuyStonesOrder _parentObject;

        public BuyStonesOrder ParentObject
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

        public decimal Expense
        {
            get
            {
                return this._parentObject.StonesOrder.Expense;
            }
        }

        public decimal ValueRMB
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

        public string BuyerUserName
        {
            get
            {
                return this._parentObject.BuyerUserName;
            }
        }

        public decimal AwardGoldCoin
        {
            get
            {
                return this._parentObject.AwardGoldCoin;
            }
        }

        public DateTime BuyTime
        {
            get
            {
                return this._parentObject.BuyTime;
            }
        }
    }
}
