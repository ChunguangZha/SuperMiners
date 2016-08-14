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
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public static string CreateAlipayLink(string orderNumber, string shopName, float valueRMB, string clientIP)
        {
            float money = valueRMB / GlobalData.GameConfig.Yuan_RMB;
            float money_1 = (float)Math.Round(money, 1);
            if (money_1 < money)//说明刚才是四舍了，要把他加回来
            {
                money_1 += 0.1f;
            }
            string srcParameter = orderNumber + "," + shopName + "," + money_1.ToString("0.00") + "," + clientIP;
            string desParameter = DESEncrypt.EncryptDES(srcParameter);

            string p = System.Web.HttpUtility.UrlEncode(desParameter, Encoding.UTF8);

            return "~/Alipay/AlipayDefault.aspx?p=" + p;
        }

    }
}