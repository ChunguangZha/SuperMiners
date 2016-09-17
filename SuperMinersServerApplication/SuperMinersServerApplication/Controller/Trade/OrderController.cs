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

        public void Init()
        {
            StoneOrderController.Init();
            MineOrderController.Init();
            GoldCoinOrderController.Init();
        }

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
            builder.Append((int)tradeType);//第18到20位
            builder.Append(Math.Abs(userName.GetHashCode()));
            builder.Append((new Random()).Next(1000, 9999));
            return builder.ToString();
        }

        /// <summary>
        /// 相对URL，需要客户端拼接根URL
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="shopName"></param>
        /// <param name="valueRMB"></param>
        /// <param name="shopDescript"></param>
        /// <returns></returns>
        public string CreateAlipayLink(string userName, string orderNumber, string shopName, decimal valueRMB, string shopDescript)
        {
            decimal money = valueRMB / GlobalConfig.GameConfig.Yuan_RMB;
            decimal money_1 = (decimal)Math.Round(money, 1);
            if (money_1 < money)//说明刚才是四舍了，要把他加回来
            {
                money_1 += 0.1m;
            }
            string srcParameter = userName + "," + orderNumber + "," + shopName + "," + money_1.ToString("0.00") + "," + shopDescript;
            string desParameter = DESEncrypt.EncryptDES(srcParameter);

            string p = System.Web.HttpUtility.UrlEncode(desParameter, Encoding.UTF8);

            return "Alipay/AlipayDefault.aspx?p=" + p;
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            bool isOK = false;
            AlipayTradeInType type = GetTradeType(alipayRecord.out_trade_no);
            switch (type)
            {
                case AlipayTradeInType.BuyGoldCoin:
                    isOK = this.GoldCoinOrderController.AlipayCallback(alipayRecord);
                    break;
                case AlipayTradeInType.BuyMine:
                    isOK = this.MineOrderController.AlipayCallback(alipayRecord);
                    break;
                case AlipayTradeInType.BuyMiner:
                    break;
                case AlipayTradeInType.BuyRMB:
                    break;
                case AlipayTradeInType.BuyStone:
                    isOK = this.StoneOrderController.AlipayCallback(alipayRecord);
                    break;

                default:
                    break;
            }

            return isOK;
        }

        public bool CheckAlipayOrderBeHandled(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            bool isOK = false;
            var alipayRecord = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(out_trade_no, alipay_trade_no);
            if (alipayRecord == null)
            {
                return false;
            }
            
            AlipayTradeInType type = GetTradeType(out_trade_no);
            switch (type)
            {
                case AlipayTradeInType.BuyGoldCoin:
                    isOK = this.GoldCoinOrderController.CheckAlipayOrderBeHandled(userName, out_trade_no);
                    break;
                case AlipayTradeInType.BuyMine:
                    isOK = this.MineOrderController.CheckAlipayOrderBeHandled(userName, out_trade_no);
                    break;
                case AlipayTradeInType.BuyMiner:
                case AlipayTradeInType.BuyRMB:
                    break;
                case AlipayTradeInType.BuyStone:
                    isOK = this.StoneOrderController.CheckAlipayOrderBeHandled(userName, out_trade_no);
                    break;
                default:
                    isOK = false;
                    break;
            }

            return isOK;
        }

        private AlipayTradeInType GetTradeType(string orderNumber)
        {
            string strType = orderNumber.Substring(18, 2);
            int valueType = Convert.ToInt32(strType);
            return (AlipayTradeInType)valueType;
        }
    }
}
