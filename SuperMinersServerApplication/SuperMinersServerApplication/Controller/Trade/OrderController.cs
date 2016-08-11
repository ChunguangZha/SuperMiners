using MetaData.Trade;
using SuperMinersServerApplication.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Trade
{
    public class OrderController
    {
        #region Single Stone

        private static OrderController _instance = new OrderController();

        public static OrderController Instance
        {
            get { return _instance; }
        }

        private OrderController()
        {

        }

        #endregion

        public StoneOrderController StoneOrderController = new StoneOrderController();
        public GoldCoinOrderController GoldCoinOrderController = new GoldCoinOrderController();
        public MineOrderController MineOrderController = new MineOrderController();


        public string CreateOrderNumber(string userName, DateTime time, AlipayTradeInType tradeType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(time.Year.ToString("0000"));
            builder.Append(time.Month.ToString("00"));
            builder.Append(time.Day.ToString("00"));
            builder.Append(time.Hour.ToString("00"));
            builder.Append(time.Minute.ToString("00"));
            builder.Append(time.Second.ToString("00"));
            builder.Append(time.Millisecond.ToString("0000"));
            builder.Append((int)tradeType);//表示此为矿石交易，对应还有矿山交易等
            builder.Append(Math.Abs(userName.GetHashCode()));
            builder.Append((new Random()).Next(1000, 9999));
            return builder.ToString();
        }

        public string CreateAlipayLink(string orderNumber, string shopName, float money, string clientIP)
        {
            string srcParameter = orderNumber + "," + shopName + "," + money.ToString("0.00") + "," + clientIP;
            string desParameter = DESEncrypt.EncryptDES(srcParameter);

            string baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
            return baseuri + "AlipayDefault.aspx?p=" + desParameter;
        }
    }
}
