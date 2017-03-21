using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class StoneDelegateSellOrderInfoUIModel : BaseModel
    {
        public StoneDelegateSellOrderInfoUIModel(StoneDelegateSellOrderInfo parent)
        {
            this.ParentObject = parent;
        }

        private StoneDelegateSellOrderInfo _parentObject;

        public StoneDelegateSellOrderInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;

                NotifyPropertyChange("SellUserName");
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("Price");
                NotifyPropertyChange("TradeStoneHandCount");
                NotifyPropertyChange("FinishedStoneTradeHandCount");
                NotifyPropertyChange("SellState");
                NotifyPropertyChange("SellStateText");
                NotifyPropertyChange("CreateTimeText");
                NotifyPropertyChange("FinishedTimeText");

            }
        }

        public string SellUserName
        {
            get
            {
                return this.ParentObject.UserName;
            }
        }

        public string OrderNumber
        {
            get
            {
                return this.ParentObject.OrderNumber;
            }
        }

        public decimal Price
        {
            get
            {
                return this.ParentObject.SellUnit.Price;
            }
        }

        public int TradeStoneHandCount
        {
            get
            {
                if (this.ParentObject.SellUnit == null)
                {
                    return 0;
                }
                return this.ParentObject.SellUnit.TradeStoneHandCount;
            }
        }

        public int FinishedStoneTradeHandCount
        {
            get
            {
                return this.ParentObject.FinishedStoneTradeHandCount;
            }
        }

        public StoneDelegateSellState SellState
        {
            get
            {
                return this.ParentObject.SellState;
            }
        }

        public string SellStateText
        {
            get
            {
                string text = "";
                switch (SellState)
                {
                    case StoneDelegateSellState.Waiting:
                        text = "等待匹配";
                        break;
                    case StoneDelegateSellState.Succeed:
                        text = "交易成功";
                        break;
                    case StoneDelegateSellState.Splited:
                        text = "订单被拆分";
                        break;
                    case StoneDelegateSellState.Rejected:
                        text = "被拒绝";
                        break;
                    case StoneDelegateSellState.Cancel:
                        text = "已取消";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public string CreateTimeText
        {
            get
            {
                return this.ParentObject.DelegateTime.ToDateTime().ToString();
            }
        }

        public string FinishedTimeText
        {
            get
            {
                if (this.ParentObject.FinishedTime == null)
                {
                    return "";
                }
                return this.ParentObject.FinishedTime.ToDateTime().ToString();
            }
        }
    }
}
