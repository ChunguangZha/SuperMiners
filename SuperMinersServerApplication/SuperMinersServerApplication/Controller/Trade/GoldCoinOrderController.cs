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
                result = RechargeGoldCoinByRMB(record);
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

        private TradeOperResult RechargeGoldCoinByRMB(GoldCoinRechargeRecord record)
        {
            TradeOperResult result = new TradeOperResult();
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                int value = PlayerController.Instance.RechargeGoldCoinByRMB(record.UserName, (int)record.SpendRMB, (int)record.GainGoldCoin, myTrans);
                result.ResultCode = value;
                if (value == OperResult.RESULTCODE_TRUE)
                {
                    record.PayTime = DateTime.Now;
                    DBProvider.GoldCoinRecordDBProvider.SaveFinalGoldCoinRechargeRecord(record, myTrans);
                }

                myTrans.Commit();
                PlayerActionController.Instance.AddLog(record.UserName, MetaData.ActionLog.ActionType.GoldCoinRecharge, record.GainGoldCoin,
                    "充值了 " + record.GainGoldCoin.ToString() + " 的金币");
                return result;
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                LogHelper.Instance.AddErrorLog("玩家[" + record.UserName + "] 用灵币购买金币异常", exc);
                result.ResultCode = OperResult.RESULTCODE_EXCEPTION;
                return result;
            }
            finally
            {
                if (myTrans != null)
                {
                    myTrans.Dispose();
                }

            }
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
            if (rechargeRecord == null)
            {
                LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 支付宝购买金币回调，找不到订单。支付宝信息：" + alipayRecord.ToString());
                return false;
            }
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                alipayRecord.user_name = rechargeRecord.UserName;
                if (alipayRecord.out_trade_no == rechargeRecord.OrderNumber &&
                    alipayRecord.value_rmb >= rechargeRecord.SpendRMB)
                {
                    rechargeRecord.PayTime = DateTime.Now;
                    int value = PlayerController.Instance.RechargeGoldCoinByAlipay(rechargeRecord.UserName, alipayRecord.total_fee, (int)rechargeRecord.SpendRMB, (int)(rechargeRecord.SpendRMB * GlobalConfig.GameConfig.RMB_GoldCoin), myTrans);
                    if (value == OperResult.RESULTCODE_TRUE)
                    {
                        DBProvider.GoldCoinRecordDBProvider.SaveFinalGoldCoinRechargeRecord(rechargeRecord, myTrans);
                        DBProvider.GoldCoinRecordDBProvider.DeleteTempGoldCoinRechargeTradeRecord(rechargeRecord.OrderNumber, myTrans);
                        this.RemoveRecord(alipayRecord.out_trade_no);

                        string tokenBuyer = ClientManager.GetToken(rechargeRecord.UserName);
                        if (GoldCoinOrderPaySucceedNotify != null)
                        {
                            GoldCoinOrderPaySucceedNotify(tokenBuyer, rechargeRecord.OrderNumber);
                        }
                        isOK = true;
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 成功充值" + rechargeRecord.GainGoldCoin + "金币。ano: " + alipayRecord.alipay_trade_no);
                    }
                    else
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 金币充值失败，原因为：" + OperResult.GetMsg(value) + "。ano: " + alipayRecord.alipay_trade_no);
                    }
                }

                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord, myTrans);

                myTrans.Commit();
                return isOK;
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
        }

        public bool CheckAlipayOrderBeHandled(string userName, string out_trade_no)
        {
            var goldcoinRecord = DBProvider.GoldCoinRecordDBProvider.GetGoldCoinRechargeRecord(userName, out_trade_no);
            return goldcoinRecord != null;
        }

        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> GoldCoinOrderPaySucceedNotify;
    }
}
