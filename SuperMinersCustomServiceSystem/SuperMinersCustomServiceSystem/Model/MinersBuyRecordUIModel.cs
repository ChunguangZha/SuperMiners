using MetaData;
using System;
#if Client
using SuperMinersWPF.Models;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class MinersBuyRecordUIModel : BaseModel
    {
        private MinersBuyRecord _parentObject;

        public MinersBuyRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public MinersBuyRecordUIModel(MinersBuyRecord parent)
        {
            ParentObject = parent;
        }

        public string UserName
        {
            get { return this.ParentObject.UserName; }
        }

        public decimal SpendGoldCoin
        {
            get { return this.ParentObject.SpendGoldCoin; }
        }

        public int GainMinersCount
        {
            get { return this.ParentObject.GainMinersCount; }
        }

        public DateTime Time
        {
            get { return this.ParentObject.Time; }
        }
    }
}
