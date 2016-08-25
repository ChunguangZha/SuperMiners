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

        public MinesBuyRecord[] GetNotFinishRecords(string userName)
        {
            List<MinesBuyRecord> listRecords = new List<MinesBuyRecord>();
            lock (this._lock)
            {
                foreach (var item in this._listTempRecord.Values)
                {
                    listRecords.Add(item);
                }
            }

            return listRecords.ToArray();
        }

        public MinesBuyRecord[] GetFinishRecords(string userName, MyDateTime startDate, MyDateTime endDate)
        {
            return DBProvider.MineRecordDBProvider.GetAllMineTradeRecords(userName, startDate, endDate);
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
                if (value == OperResult.RESULTCODE_TRUE)
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
                result.ResultCode = OperResult.RESULTCODE_TRUE;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(record.OrderNumber, "迅灵矿山", record.SpendRMB, "勘探一座矿山，可增加" + GlobalConfig.GameConfig.StonesReservesPerMines + "矿石储量");
            }

            return result;
        }

        private MinesBuyRecord FindRecordByOrderNumber(string orderNumber)
        {
            lock (this._lock)
            {
                MinesBuyRecord buyRecord = null;
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
            MinesBuyRecord buyRecord = FindRecordByOrderNumber(alipayRecord.out_trade_no);
            if (buyRecord != null)
            {
                alipayRecord.user_name = buyRecord.UserName;
                if (alipayRecord.out_trade_no == buyRecord.OrderNumber &&
                    alipayRecord.value_rmb >= buyRecord.SpendRMB)
                {
                    int value = PlayerController.Instance.BuyMineByAlipay(buyRecord.UserName, buyRecord.GainMinesCount);
                    if (value == OperResult.RESULTCODE_TRUE)
                    {
                        DBProvider.MineRecordDBProvider.SaveFinalMineTradeRecord(buyRecord);
                        DBProvider.MineRecordDBProvider.DeleteTempMineTradeRecord(buyRecord.OrderNumber);
                        this.RemoveRecord(alipayRecord.out_trade_no);

                        string tokenBuyer = ClientManager.GetToken(buyRecord.UserName);

                        if (!string.IsNullOrEmpty(tokenBuyer) && MineOrderPaySucceedNotify != null)
                        {
                            MineOrderPaySucceedNotify(tokenBuyer, buyRecord.OrderNumber);
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
        public event Action<string, string> MineOrderPaySucceedNotify;

    }
}
