using MetaData.Game.StoneStack;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class StoneDelegateBuyOrderInfoUIModel : BaseModel
    {
        public StoneDelegateBuyOrderInfoUIModel(StoneDelegateBuyOrderInfo parent)
        {
            this.ParentObject = parent;
        }

        private StoneDelegateBuyOrderInfo _parentObject;

        public StoneDelegateBuyOrderInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("Price");
                NotifyPropertyChange("TradeStoneHandCount");
                NotifyPropertyChange("PayType");
                NotifyPropertyChange("PayTypeText");
                NotifyPropertyChange("FinishedStoneTradeHandCount");
                NotifyPropertyChange("BuyState");
                NotifyPropertyChange("BuyStateText");
                NotifyPropertyChange("DelegateTimeText");
                NotifyPropertyChange("FinishedTimeText");
                NotifyPropertyChange("AwardGoldCoin");
                NotifyPropertyChange("AlipayLink");
            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.OrderNumber;
            }
        }

        public decimal Price
        {
            get
            {
                return this._parentObject.BuyUnit.Price;
            }
        }

        public int TradeStoneHandCount
        {
            get
            {
                return this._parentObject.BuyUnit.TradeStoneHandCount;
            }
        }

        public PayType PayType
        {
            get
            {
                return this._parentObject.PayType;
            }
        }

        public string PayTypeText
        {
            get
            {
                string text = "";
                switch (this.PayType)
                {
                    case PayType.Alipay:
                        text = "支付宝";
                        break;
                    case PayType.RMB:
                        text = "灵币";
                        break;
                    case PayType.GoldCoin:
                        text = "金币";
                        break;
                    case PayType.Diamand:
                        text = "钻石";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public int FinishedStoneTradeHandCount
        {
            get
            {
                return this._parentObject.FinishedStoneTradeHandCount;
            }
        }

        public StoneDelegateBuyState BuyState
        {
            get
            {
                return this._parentObject.BuyState;
            }
        }

        public string BuyStateText
        {
            get
            {
                string text = "";
                switch (this.BuyState)
                {
                    case StoneDelegateBuyState.Waiting:
                        text = "等待匹配";
                        break;
                    case StoneDelegateBuyState.Succeed:
                        text = "交易成功";
                        break;
                    case StoneDelegateBuyState.Splited:
                        text = "订单被拆分";
                        break;
                    case StoneDelegateBuyState.Rejected:
                        text = "被拒绝";
                        break;
                    case StoneDelegateBuyState.Cancel:
                        text = "已取消";
                        break;
                    case StoneDelegateBuyState.Exception:
                        text = "异常";
                        break;
                    case StoneDelegateBuyState.NotPayed:
                        text = "未支付";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public string DelegateTimeText
        {
            get
            {
                return this._parentObject.DelegateTime.ToDateTime().ToString();
            }
        }

        public string FinishedTimeText
        {
            get
            {
                if (this._parentObject.FinishedTime == null)
                {
                    return "";
                }
                return this._parentObject.FinishedTime.ToDateTime().ToString();
            }
        }

        public decimal AwardGoldCoin
        {
            get
            {
                return this._parentObject.AwardGoldCoin;
            }
        }

        public string AlipayLink
        {
            get
            {
                return this._parentObject.AlipayLink;
            }
        }

    }
}
