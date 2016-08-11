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
    class MineOrderController
    {
        private object _lock = new object();
        private Dictionary<string, MinesBuyRecord> _listRecord = new Dictionary<string, MinesBuyRecord>();

        public bool Init()
        {
            //load from DB
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
                    SaveFinalMineTradeRecord(record);
                }
            }
            else if (payType == (int)PayType.Alipay)
            {
                lock (this._lock)
                {
                    this._listRecord.Add(record.OrderNumber, record);
                }

                SaveTempMineTradeRecord(record);
                result.ResultCode = OperResult.RESULTCODE_SUCCEED;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(record.OrderNumber, "迅灵矿石", record.SpendRMB / GlobalConfig.GameConfig.Yuan_RMB, "");
            }

            return result;
        }

        private bool SaveTempMineTradeRecord(MinesBuyRecord record)
        {
            //GlobalConfig.GameConfig.BuyOrderLockTimeMinutes锁定时间
        }

        private bool SaveFinalMineTradeRecord(MinesBuyRecord record)
        {

        }

        private bool DeleteTempMineTradeRecord(string orderNumber)
        {

        }

        private bool SaveAlipayRechargeRecord(AlipayRechargeRecord alipayRecord)
        {

        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            lock (this._lock)
            {
                SaveAlipayRechargeRecord(alipayRecord);

                MinesBuyRecord buyRecord = null;
                if (this._listRecord.TryGetValue(alipayRecord.out_trade_no, out buyRecord) && buyRecord!=null)
                {
                    if (alipayRecord.out_trade_no == buyRecord.OrderNumber &&
                        alipayRecord.total_fee * GlobalConfig.GameConfig.Yuan_RMB >= buyRecord.SpendRMB)
                    {
                        //1.delete from temp DB
                        DeleteTempMineTradeRecord(buyRecord.OrderNumber);

                        int value = PlayerController.Instance.BuyMineByAlipay(buyRecord.UserName, buyRecord.SpendRMB);
                        if (value == OperResult.RESULTCODE_SUCCEED)
                        {
                            SaveFinalMineTradeRecord(buyRecord);
                            return true;
                        }
                    }
                }
            }

            return false;
        }        

    }
}
