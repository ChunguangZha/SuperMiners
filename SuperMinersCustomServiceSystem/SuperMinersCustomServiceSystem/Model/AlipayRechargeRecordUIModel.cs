using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class AlipayRechargeRecordUIModel
    {
        private AlipayRechargeRecord _parentObject;

        public AlipayRechargeRecord ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public AlipayRechargeRecordUIModel(AlipayRechargeRecord parent)
        {
            this.ParentObject = parent;

            if (!string.IsNullOrEmpty(parent.out_trade_no) && parent.out_trade_no.Length > 20)
            {
                switch (GetTradeType(parent.out_trade_no))
                {
                    case AlipayTradeInType.BuyStone:
                        _tradeTypeText = "购买矿石";
                        break;
                    case AlipayTradeInType.BuyMine:
                        _tradeTypeText = "购买矿山";
                        break;
                    case AlipayTradeInType.BuyMiner:
                        _tradeTypeText = "购买矿工";
                        break;
                    case AlipayTradeInType.BuyRMB:
                        _tradeTypeText = "充值灵币";
                        break;
                    case AlipayTradeInType.BuyGoldCoin:
                        _tradeTypeText = "充值金币";
                        break;
                    default:
                        break;
                }
            }
        }

        private AlipayTradeInType GetTradeType(string orderNumber)
        {
            string strType = orderNumber.Substring(18, 2);
            int valueType = Convert.ToInt32(strType);
            return (AlipayTradeInType)valueType;
        }

        public string out_trade_no
        {
            get { return this.ParentObject.out_trade_no; }
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
        }

        public string buyer_email
        {
            get { return this.ParentObject.buyer_email; }
        }

        public string user_name
        {
            get { return this.ParentObject.user_name; }
        }

        public decimal total_fee
        {
            get { return this.ParentObject.total_fee; }
        }

        public decimal value_rmb
        {
            get { return this.ParentObject.value_rmb; }
        }

        public DateTime pay_time
        {
            get { return this.ParentObject.pay_time; }
        }
    }
}
