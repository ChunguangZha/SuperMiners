using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class MinesBuyRecordUIModel : BaseModel
    {
        private MinesBuyRecord _parentObject;

        public MinesBuyRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public MinesBuyRecordUIModel(MinesBuyRecord parent)
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

        public int SpendRMB
        {
            get { return this.ParentObject.SpendRMB; }
        }

        public decimal GainMinesCount
        {
            get { return this.ParentObject.GainMinesCount; }
        }

        public int GainStonesReserves
        {
            get { return this.ParentObject.GainStonesReserves; }
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
