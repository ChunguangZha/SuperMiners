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
        /// <summary>
        /// 商品订单号
        /// </summary>
        [DataMember]
        public string out_trade_no;

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
        public DateTime pay_time;
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
                    pay_time = DateTime.Parse(value);
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
