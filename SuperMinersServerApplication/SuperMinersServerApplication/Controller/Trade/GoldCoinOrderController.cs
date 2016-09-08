using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class GoldCoinOrderController
    {
        private object _lock = new object();

        /// <summary>
        /// Key:OrderNumber
        /// </summary>
        private Dictionary<string, GoldCoinRechargeRecord> _listTempRecord = new Dictionary<string, GoldCoinRechargeRecord>();

        public void Init()
        {
            //load from DB
            _listTempRecord.Clear();
            var list = DBProvider.GoldCoinRecordDBProvider.GetAllTempGoldCoinRechargeTradeRecords();
            if (list != null)
            {
                foreach (var item in list)
                {
                    _listTempRecord.Add(item.OrderNumber, item);
                }
            }
        }

        public TradeOperResult RechargeGoldCoin(string userName, int rmbValue, int gainGoldCoin, int payType)
        {
            TradeOperResult result = new TradeOperResult();
            result.PayType = payType;
            DateTime timenow = DateTime.Now;

            string orderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, AlipayTradeInType.BuyGoldCoin);
            GoldCoinRechargeRecord record = new GoldCoinRechargeRecord()
            {
                OrderNumber = orderNumber,
                CreateTime = timenow,
                UserName = userName,
                SpendRMB = rmbValue,
                GainGoldCoin = gainGoldCoin
            };

            if (payType == (int)PayType.RMB)
            {
                int value = PlayerController.Instance.RechargeGoldCoinByRMB(userName, rmbValue, gainGoldCoin);
                result.ResultCode = value;
                if (value == OperResult.RESULTCODE_TRUE)
                {
                    record.PayTime = DateTime.Now;
                    DBProvider.GoldCoinRecordDBProvider.SaveFinalGoldCoinRechargeRecord(record);
                }
            }
            else if (payType == (int)PayType.Alipay)
            {
                lock (this._lock)
                {
                    this._listTempRecord.Add(record.OrderNumber, record);
                }

                DBProvider.GoldCoinRecordDBProvider.SaveTempGoldCoinRechargeTradeRecord(record);
                result.ResultCode = OperResult.RESULTCODE_TRUE;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(userName, record.OrderNumber, "迅灵金币", record.SpendRMB, "金币可用于购买矿工");
            }

            return result;
        }

        private GoldCoinRechargeRecord FindRecordByOrderNumber(string orderNumber)
        {
            lock (this._lock)
            {
                GoldCoinRechargeRecord buyRecord = null;
                this._listTempRecord.TryGetValue(orderNumber, out buyRecord);
                return buyRecord;
            }
        }

        private void RemoveRecord(string orderNumber)
        {
            lock (this._lock)
            {
                this._listTempRecord.Remove(orderNumber);
            }
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            bool isOK = false;

            GoldCoinRechargeRecord rechargeRecord = FindRecordByOrderNumber(alipayRecord.out_trade_no);
            if (rechargeRecord != null)
            {
                alipayRecord.user_name = rechargeRecord.UserName;
                if (alipayRecord.out_trade_no == rechargeRecord.OrderNumber &&
                    alipayRecord.value_rmb >= rechargeRecord.SpendRMB)
                {
                    int value = PlayerController.Instance.RechargeGoldCoinByAlipay(rechargeRecord.UserName, alipayRecord.total_fee, (int)rechargeRecord.SpendRMB, (int)(rechargeRecord.SpendRMB * GlobalConfig.GameConfig.RMB_GoldCoin));
                    if (value == OperResult.RESULTCODE_TRUE)
                    {
                        DBProvider.GoldCoinRecordDBProvider.SaveFinalGoldCoinRechargeRecord(rechargeRecord);
                        DBProvider.GoldCoinRecordDBProvider.DeleteTempGoldCoinRechargeTradeRecord(rechargeRecord.OrderNumber);
                        this.RemoveRecord(alipayRecord.out_trade_no);

                        string tokenBuyer = ClientManager.GetToken(rechargeRecord.UserName);
                        if (GoldCoinOrderPaySucceedNotify != null)
                        {
                            GoldCoinOrderPaySucceedNotify(tokenBuyer, rechargeRecord.OrderNumber);
                        }
                        isOK = true;
                    }
                }
            }

            DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord);

            return isOK;
        }

        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> GoldCoinOrderPaySucceedNotify;
    }
}
