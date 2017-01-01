using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class StackTradeUnitUIModel :BaseModel
    {
        public StackTradeUnitUIModel(int index, bool isbuy, StackTradeUnit parent)
        {
            this.Index = index;
            this.IsBuy = isbuy;
            this.ParentObject = parent;
        }

        private StackTradeUnit _parentObject;

        public StackTradeUnit ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("Title");
                NotifyPropertyChange("Price");
                NotifyPropertyChange("Count");
            }
        }

        private int Index;

        private bool IsBuy;

        public string Title
        {
            get
            {
                return IsBuy ? "买" + Index.ToString() : "卖" + Index.ToString();
            }
        }

        public string Price
        {
            get
            {
                if (this._parentObject == null)
                {
                    return "";
                }

                return this.ParentObject.Price.ToString("0.00");
            }
        }

        public string Count
        {
            get
            {
                if (this._parentObject == null)
                {
                    return "";
                }

                return this.ParentObject.TradeStoneHandCount.ToString();
            }
        }

    }
}
