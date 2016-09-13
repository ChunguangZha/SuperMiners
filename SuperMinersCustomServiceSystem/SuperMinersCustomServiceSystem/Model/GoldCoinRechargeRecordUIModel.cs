using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class GoldCoinRechargeRecordUIModel : BaseModel
    {
        private GoldCoinRechargeRecord _parentObject;

        public GoldCoinRechargeRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public GoldCoinRechargeRecordUIModel(GoldCoinRechargeRecord parent)
        {
            this.ParentObject = parent;
        }

        public string UserName
        {
            get { return this.ParentObject.UserName; }
        }

        public string OrderNumber
        {
            get { return this.ParentObject.OrderNumber; }
        }

        public decimal SpendRMB
        {
            get { return this.ParentObject.SpendRMB; }
        }

        public decimal GainGoldCoin
        {
            get { return this.ParentObject.GainGoldCoin; }
        }

        public DateTime CreateTime
        {
            get { return this.ParentObject.CreateTime; }
        }

        public DateTime PayTime
        {
            get { return this.ParentObject.PayTime; }
        }
    }
}
