using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class AlipayRechargeRecordUIModel : BaseModel
    {
        private AlipayRechargeRecord _parentObject;

        public AlipayRechargeRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("out_trade_no");
                NotifyPropertyChange("TradeTypeText");
                NotifyPropertyChange("alipay_trade_no");
                NotifyPropertyChange("buyer_email");
                NotifyPropertyChange("user_name");
                NotifyPropertyChange("total_fee");
                NotifyPropertyChange("value_rmb");
                NotifyPropertyChange("pay_time");
            }
        }

        public AlipayRechargeRecordUIModel(AlipayRechargeRecord parent)
        {
            this.ParentObject = parent;
            this._tradeTypeText = GetTradeTypeText(parent.out_trade_no);
        }

        //private static AlipayTradeInType GetTradeType(string orderNumber)
        //{
        //    string strType = orderNumber.Substring(18, 2);
        //    int valueType = Convert.ToInt32(strType);
        //    return (AlipayTradeInType)valueType;
        //}

        public static string GetTradeTypeText(string orderNumber)
        {
            string tradeTypeText = "";
            if (string.IsNullOrEmpty(orderNumber) || orderNumber.Length < 20)
            {
                return "";
            }
            string strType = orderNumber.Substring(18, 2);

            int valueType = -1;
            if (int.TryParse(strType, out valueType))
            {
                switch ((AlipayTradeInType)valueType)
                {
                    case AlipayTradeInType.BuyStone:
                        tradeTypeText = "购买矿石";
                        break;
                    case AlipayTradeInType.BuyMine:
                        tradeTypeText = "购买矿山";
                        break;
                    case AlipayTradeInType.BuyMiner:
                        tradeTypeText = "购买矿工";
                        break;
                    case AlipayTradeInType.BuyRMB:
                        tradeTypeText = "充值灵币";
                        break;
                    case AlipayTradeInType.BuyGoldCoin:
                        tradeTypeText = "充值金币";
                        break;
                    default:
                        break;
                }
            }

            return tradeTypeText;
        }

        public string out_trade_no
        {
            get { return this.ParentObject.out_trade_no; }
            set
            {
                this.ParentObject.out_trade_no = value;
            }
        }

        private string _tradeTypeText = "";
        public string TradeTypeText
        {
            get
            {
                return _tradeTypeText;
            }
        }

        public string alipay_trade_no
        {
            get { return this.ParentObject.alipay_trade_no; }
            set
            {
                this.ParentObject.alipay_trade_no = value;
            }
        }

        public string buyer_email
        {
            get { return this.ParentObject.buyer_email; }
            set
            {
                this.ParentObject.buyer_email = value;
            }
        }

        public string user_name
        {
            get { return this.ParentObject.user_name; }
            set
            {
                this.ParentObject.user_name = value;
            }
        }

        public decimal total_fee
        {
            get { return this.ParentObject.total_fee; }
            set
            {
                this.ParentObject.total_fee = value;
            }
        }

        public decimal value_rmb
        {
            get { return this.ParentObject.value_rmb; }
            set
            {
                this.ParentObject.value_rmb = value;
            }
        }

        public DateTime pay_time
        {
            get { return this.ParentObject.pay_time; }
            set
            {
                this.ParentObject.pay_time = value;
            }
        }
    }
}
