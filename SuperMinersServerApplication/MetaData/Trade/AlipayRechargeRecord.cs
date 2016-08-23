using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    public class AlipayRechargeRecord
    {
        /// <summary>
        /// 商品订单号
        /// </summary>
        public string out_trade_no;

        /// <summary>
        /// 支付宝订单号
        /// </summary>
        public string alipay_trade_no;

        /// <summary>
        /// 支付账户
        /// </summary>
        public string buyer_email;

        public string user_name;

        /// <summary>
        /// 支付金额人民币元
        /// </summary>
        public decimal total_fee;

        /// <summary>
        /// 等值的灵币
        /// </summary>
        public decimal value_rmb;

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime pay_time;
    }
}
