using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
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

        public TradeOperResult RechargeGoldCoin(string userName, int rmbValue, int payType)
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
                GainGoldCoin = rmbValue * (int)GlobalConfig.GameConfig.RMB_GoldCoin
            };

            if (payType == (int)PayType.RMB)
            {
                int value = PlayerController.Instance.BuyMineByRMB(userName, rmbValue);
                result.ResultCode = value;
                if (value == OperResult.RESULTCODE_SUCCEED)
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
                result.ResultCode = OperResult.RESULTCODE_SUCCEED;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(record.OrderNumber, "迅灵金币", record.SpendRMB / GlobalConfig.GameConfig.Yuan_RMB, "");
            }

            return result;
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            lock (this._lock)
            {
                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord);

                GoldCoinRechargeRecord rechargeRecord = null;
                if (this._listTempRecord.TryGetValue(alipayRecord.out_trade_no, out rechargeRecord) && rechargeRecord != null)
                {
                    if (alipayRecord.out_trade_no == rechargeRecord.OrderNumber &&
                        alipayRecord.total_fee * GlobalConfig.GameConfig.Yuan_RMB >= rechargeRecord.SpendRMB)
                    {
                        //1.delete from temp DB
                        DBProvider.GoldCoinRecordDBProvider.DeleteTempGoldCoinRechargeTradeRecord(rechargeRecord.OrderNumber);

                        int value = PlayerController.Instance.RechargeGoldCoinByAlipay(rechargeRecord.UserName, (int)rechargeRecord.SpendRMB, (int)(rechargeRecord.SpendRMB * GlobalConfig.GameConfig.RMB_GoldCoin));
                        if (value == OperResult.RESULTCODE_SUCCEED)
                        {
                            DBProvider.GoldCoinRecordDBProvider.SaveFinalGoldCoinRechargeRecord(rechargeRecord);
                            return true;
                        }
                    }
                }
            }

            return false;
        }        

    }
}
