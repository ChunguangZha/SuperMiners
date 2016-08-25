using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
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
            set { _parentObject = value; }
        }

        public string OrderNumber
        {
            get { return this.ParentObject.StonesOrder.OrderNumber; }
        }

        public string SellerUserName
        {
            get { return this.ParentObject.StonesOrder.SellerUserName; }
        }

        public int SellStonesCount
        {
            get { return this.ParentObject.StonesOrder.SellStonesCount; }
        }

        public decimal Expense
        {
            get { return this.ParentObject.StonesOrder.Expense; }
        }

        public decimal ValueRMB
        {
            get { return this.ParentObject.StonesOrder.ValueRMB; }
        }

        public DateTime SellTime
        {
            get { return this.ParentObject.StonesOrder.SellTime; }
        }

        public string BuyerUserName
        {
            get { return this.ParentObject.BuyerUserName; }
        }

        public DateTime BuyTime
        {
            get { return this.ParentObject.BuyTime; }
        }

        public decimal AwardGoldCoin
        {
            get { return this.ParentObject.AwardGoldCoin; }
        }
    }
}
