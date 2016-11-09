using MetaData.Trade;
using SuperMinersServerApplication.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperMinersWeb.Utility
{
    public class Tools
    {
        /// <summary>
        /// 相对URL，需要客户端拼接根URL
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="shopName"></param>
        /// <param name="valueRMB"></param>
        /// <param name="shopDescript"></param>
        /// <returns></returns>
        public static string CreateAlipayLink(string userName, AlipayTradeInType tradeType, string shopName, decimal valueRMB, string shopDescript)
        {
            decimal money = valueRMB / GlobalData.GameConfig.Yuan_RMB;
            decimal money_1 = (decimal)Math.Round(money, 1);
            if (money_1 < money)//说明刚才是四舍了，要把他加回来
            {
                money_1 += 0.1m;
            }
            string orderNo = CreateOrderNumber(userName, DateTime.Now, tradeType);
            string srcParameter = userName + "," + orderNo + "," + shopName + "," + money_1.ToString("0.00") + "," + shopDescript;
            string desParameter = DESEncrypt.EncryptDES(srcParameter);

            string p = System.Web.HttpUtility.UrlEncode(desParameter, Encoding.UTF8);

            return "../Alipay/AlipayDefault.aspx?p=" + p;
        }

        public static string CreateOrderNumber(string userName, DateTime time, AlipayTradeInType tradeType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(InnerPayOrderNumberHeader);
            builder.Append(time.Month.ToString("00"));
            builder.Append(time.Day.ToString("00"));
            builder.Append(time.Hour.ToString("00"));
            builder.Append(time.Minute.ToString("00"));
            builder.Append(time.Second.ToString("00"));
            builder.Append(time.Millisecond.ToString("0000"));
            builder.Append((int)tradeType);//第18到20位
            builder.Append(Math.Abs(userName.GetHashCode()));
            builder.Append((new Random()).Next(1000, 9999));
            return builder.ToString();
        }


        public static readonly string InnerPayOrderNumberHeader = "0000";
    }
}