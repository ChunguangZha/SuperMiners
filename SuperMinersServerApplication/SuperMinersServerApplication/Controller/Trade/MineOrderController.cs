using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Utility;
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
                BuyMineByRMB(record, result);
            }
            else if (payType == (int)PayType.Alipay)
            {
                lock (this._lock)
                {
                    this._listTempRecord.Add(record.OrderNumber, record);
                }

                DBProvider.MineRecordDBProvider.SaveTempMineTradeRecord(record);
                result.ResultCode = OperResult.RESULTCODE_TRUE;
                result.AlipayLink = OrderController.Instance.CreateAlipayLink(userName, record.OrderNumber, "迅灵矿山", record.SpendRMB, "勘探一座矿山，可增加" + GlobalConfig.GameConfig.StonesReservesPerMines + "矿石储量");
            }

            return result;
        }

        private void BuyMineByRMB(MinesBuyRecord record, TradeOperResult result)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                int value = PlayerController.Instance.BuyMineByRMB(record.UserName, (int)record.GainMinesCount, myTrans);
                result.ResultCode = value;
                if (value == OperResult.RESULTCODE_TRUE)
                {
                    record.PayTime = DateTime.Now;
                    DBProvider.MineRecordDBProvider.SaveFinalMineTradeRecord(record, myTrans);
                    PlayerActionController.Instance.AddLog(record.UserName, MetaData.ActionLog.ActionType.BuyMine, (int)record.GainMinesCount,
                        "增加了 " + record.GainStonesReserves.ToString() + " 的矿石储量");
                }

                myTrans.Commit();
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                LogHelper.Instance.AddErrorLog("玩家[" + record.UserName + "], 用灵币购买矿山异常", exc);
            }
            finally
            {
                if (myTrans != null)
                {
                    myTrans.Dispose();
                }
            }
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

        public bool CheckAlipayOrderBeHandled(string userName, string out_trade_no)
        {
            var mineTradeRecord = DBProvider.MineRecordDBProvider.GetMineTradeRecord(userName, out_trade_no);
            return mineTradeRecord != null;
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            bool isOK = false;

            MinesBuyRecord buyRecord = FindRecordByOrderNumber(alipayRecord.out_trade_no);
            if (buyRecord == null)
            {
                LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 支付宝购买矿山回调，找不到订单。支付宝信息：" + alipayRecord.ToString());
                return false;
            }
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                alipayRecord.user_name = buyRecord.UserName;
                if (alipayRecord.out_trade_no == buyRecord.OrderNumber &&
                    alipayRecord.value_rmb >= buyRecord.SpendRMB)
                {
                    int value = PlayerController.Instance.BuyMineByAlipay(buyRecord.UserName, alipayRecord.total_fee, buyRecord.GainMinesCount, myTrans);
                    if (value == OperResult.RESULTCODE_TRUE)
                    {
                        buyRecord.PayTime = DateTime.Now;
                        DBProvider.MineRecordDBProvider.SaveFinalMineTradeRecord(buyRecord, myTrans);
                        DBProvider.MineRecordDBProvider.DeleteTempMineTradeRecord(buyRecord.OrderNumber, myTrans);
                        this.RemoveRecord(alipayRecord.out_trade_no);

                        string tokenBuyer = ClientManager.GetToken(buyRecord.UserName);

                        if (!string.IsNullOrEmpty(tokenBuyer) && MineOrderPaySucceedNotify != null)
                        {
                            MineOrderPaySucceedNotify(tokenBuyer, buyRecord.OrderNumber);
                        }
                        isOK = true;
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 成功购买" + buyRecord.GainMinesCount + "座矿山。ano: " + alipayRecord.alipay_trade_no);
                    }
                    else
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 购买矿山失败，原因为：" + OperResult.GetMsg(value) + "。ano: " + alipayRecord.alipay_trade_no);
                    }
                }

                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord, myTrans);

                myTrans.Commit();
                PlayerActionController.Instance.AddLog(buyRecord.UserName, MetaData.ActionLog.ActionType.BuyMine, buyRecord.GainMinesCount,
                    "增加了 " + buyRecord.GainStonesReserves.ToString() + " 的矿石储量");
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                PlayerController.Instance.RefreshFortune(alipayRecord.user_name);

                LogHelper.Instance.AddErrorLog("玩家[" + alipayRecord.user_name + "] 支付宝金币充值，回调异常。AlipayInfo : " + alipayRecord.ToString(), exc);
                return false;
            }
            finally
            {
                if (myTrans != null)
                {
                    myTrans.Dispose();
                }
            }
            return isOK;
        }
        
        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> MineOrderPaySucceedNotify;

    }
}
