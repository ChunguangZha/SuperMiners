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
    public class MineOrderController
    {
        private object _lock = new object();

        /// <summary>
        /// Key:OrderNumber
        /// </summary>
        private Dictionary<string, MinesBuyRecord> _listTempRecord = new Dictionary<string, MinesBuyRecord>();

        public void Init()
        {
            //load from DB
            _listTempRecord.Clear();
            var list = DBProvider.MineRecordDBProvider.GetAllTempMineTradeRecords();
            if (list != null)
            {
                foreach (var item in list)
                {
                    _listTempRecord.Add(item.OrderNumber, item);
                }
            }
        }

        public TradeOperResult BuyMine(string userName, int minesCount, int payType)
        {
            TradeOperResult result = new TradeOperResult();
            result.PayType = payType;
            DateTime timenow = DateTime.Now;
                
            string orderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, AlipayTradeInType.BuyMine);
            MinesBuyRecord record = new MinesBuyRecord()
            {
                OrderNumber = orderNumber,
                CreateTime = timenow,
                UserName = userName,
                GainMinesCount = minesCount,
                GainStonesReserves = minesCount * (int)GlobalConfig.GameConfig.StonesReservesPerMines,
                SpendRMB = (int)Math.Ceiling(minesCount * GlobalConfig.GameConfig.RMB_Mine)
            };

            if (payType == (int)PayType.RMB)
            {
                int value = PlayerController.Instance.BuyMineByRMB(userName, minesCount);
                result.ResultCode = value;
                if (value == OperResult.RESULTCODE_SUCCEED)
                {
                    record.PayTime = DateTime.Now;
                    DBProvider.MineRecordDBProvider.SaveFinalMineTradeRecord(record);
                }
            }
            else if (payType == (int)PayType.Alipay)
            {
                lock (this._lock)
                {
                    this._listTempRecord.Add(record.OrderNumber, record);
                }

                DBProvider.MineRecordDBProvider.SaveTempMineTradeRecord(record);
                result.ResultCode = OperResult.RESULTCODE_SUCCEED;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(record.OrderNumber, "迅灵矿石", record.SpendRMB / GlobalConfig.GameConfig.Yuan_RMB, "");
            }

            return result;
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            lock (this._lock)
            {
                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord);

                MinesBuyRecord buyRecord = null;
                if (this._listTempRecord.TryGetValue(alipayRecord.out_trade_no, out buyRecord) && buyRecord!=null)
                {
                    if (alipayRecord.out_trade_no == buyRecord.OrderNumber &&
                        alipayRecord.total_fee * GlobalConfig.GameConfig.Yuan_RMB >= buyRecord.SpendRMB)
                    {
                        //1.delete from temp DB
                        DBProvider.MineRecordDBProvider.DeleteTempMineTradeRecord(buyRecord.OrderNumber);

                        int value = PlayerController.Instance.BuyMineByAlipay(buyRecord.UserName, buyRecord.SpendRMB);
                        if (value == OperResult.RESULTCODE_SUCCEED)
                        {
                            DBProvider.MineRecordDBProvider.SaveFinalMineTradeRecord(buyRecord);
                            return true;
                        }
                    }
                }
            }

            return false;
        }        

    }
}
