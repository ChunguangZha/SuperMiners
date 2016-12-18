using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class AlipayRechargeRecord
    {
        private string _orderNumber;

        /// <summary>
        /// 商品订单号
        /// </summary>
        [DataMember]
        public string out_trade_no
        {
            get { return this._orderNumber; }
            set
            {
                this._orderNumber = value;
                string strType = _orderNumber.Substring(18, 2);
                int valueType = Convert.ToInt32(strType);
                this.trade_type = (AlipayTradeInType)valueType;
            }
        }

        /// <summary>
        /// 将支付类型单独保存到数据库，以便查询，该值不直接付值，只在out_trade_no属性中赋值
        /// </summary>
        [DataMember]
        public AlipayTradeInType trade_type { get; private set; }

        /// <summary>
        /// 支付宝订单号
        /// </summary>
        [DataMember]
        public string alipay_trade_no;

        /// <summary>
        /// 支付账户
        /// </summary>
        [DataMember]
        public string buyer_email;

        [DataMember]
        public string user_name;

        /// <summary>
        /// 支付金额人民币元
        /// </summary>
        [DataMember]
        public decimal total_fee;

        /// <summary>
        /// 等值的灵币
        /// </summary>
        [DataMember]
        public decimal value_rmb;

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime pay_time = Common.INVALIDTIME;
        [DataMember]
        public string pay_timeString
        {
            get
            {
                return this.pay_time.ToString();
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!DateTime.TryParse(value, out pay_time))
                        {
                            pay_time = Common.INVALIDTIME;
                        }
                    }
                }
                catch (Exception)
                {
                    pay_time = Common.INVALIDTIME;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("out_trade_no:" + out_trade_no);
            builder.Append(",alipay_trade_no:" + alipay_trade_no);
            builder.Append(",buyer_email:" + buyer_email);
            builder.Append(",user_name:" + user_name);
            builder.Append(",total_fee:" + total_fee);
            builder.Append(",value_rmb:" + value_rmb);
            builder.Append(",pay_time:" + pay_time);
            return builder.ToString();
        }
    }
}
