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

        private Random _ran = new Random();

        public decimal GetStoneCount_BuyMine(int minesCount, bool isVIPPlayer, int surplus)
        {
            int reservers;
            if (GlobalConfig.GameConfig.MineReservesIsRandom)
            {
                if (isVIPPlayer)
                {
                    int value = _ran.Next(GlobalConfig.GameConfig.MinStonesReservesPerMine_VIPPlayer, GlobalConfig.GameConfig.MaxStonesReservesPerMine_VIPPlayer);
                    reservers = minesCount * value;
                }
                else
                {
                    int value = _ran.Next(GlobalConfig.GameConfig.MinStonesReservesPerMine_NormalPlayer, GlobalConfig.GameConfig.MaxStonesReservesPerMine_NormalPlayer);
                    reservers = minesCount * value;
                }
            }
            else
            {
                reservers = minesCount * (int)GlobalConfig.GameConfig.StonesReservesPerMines;
            }

            if (reservers > surplus)
            {
                reservers = surplus;
            }

            return reservers;
        }

        public TradeOperResult BuyMine(string userName, int minesCount, int payType)
        {
            TradeOperResult result = new TradeOperResult();
            var systemState = DBProvider.UserDBProvider.GetAllXunLingMineFortuneState();
            int surplus = GlobalConfig.GameConfig.LimitStoneCount - (int)systemState.AllStonesCount;
            if (surplus <= 0)
            {
                result.ResultCode = OperResult.RESULTCODE_BUYMINE_MINEISFULL;
                return result;
            }

            result.PayType = payType;
            DateTime timenow = DateTime.Now;

            var playerInfo = PlayerController.Instance.GetPlayerInfo(userName);
            bool isVIP = playerInfo.FortuneInfo.Exp > GlobalConfig.GameConfig.PlayerVIPInterval;
            string orderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, AlipayTradeInType.BuyMine);
            result.OperNumber = GetStoneCount_BuyMine(minesCount, isVIP, surplus);
            MinesBuyRecord record = new MinesBuyRecord()
            {
                OrderNumber = orderNumber,
                CreateTime = timenow,
                UserName = userName,
                GainMinesCount = minesCount,
                GainStonesReserves = (int)result.OperNumber,
                SpendRMB = (int)Math.Ceiling(minesCount * GlobalConfig.GameConfig.RMB_Mine)
            };

            switch ((PayType)payType)
            {
                case PayType.Alipay:
                    lock (this._lock)
                    {
                        this._listTempRecord.Add(record.OrderNumber, record);
                    }

                    DBProvider.MineRecordDBProvider.SaveTempMineTradeRecord(record);
                    result.ResultCode = OperResult.RESULTCODE_TRUE;
                    result.AlipayLink = OrderController.Instance.CreateAlipayLink(userName, record.OrderNumber, "迅灵矿山", record.SpendRMB, "勘探一座矿山，可增加" + result.OperNumber + "矿石储量");
                    break;
                case PayType.RMB:
                    BuyMineByRMB(record, result);
                    break;
                case PayType.GoldCoin:
                    break;
                case PayType.Diamand:
                    BuyMineByDiamond(record, result);
                    break;
                case PayType.Credits:
                    BuyMineByShoppingCredits(record, result);
                    break;
                default:
                    break;
            }

            return result;
        }

        private void BuyMineByShoppingCredits(MinesBuyRecord record, TradeOperResult result)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                int value = PlayerController.Instance.BuyMineByShoppingCredits(record, myTrans);
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

        private void BuyMineByDiamond(MinesBuyRecord record, TradeOperResult result)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                int value = PlayerController.Instance.BuyMineByDiamond(record, myTrans);
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

        private void BuyMineByRMB(MinesBuyRecord record, TradeOperResult result)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();

                int value = PlayerController.Instance.BuyMineByRMB(record, myTrans);
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

        public int AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            MinesBuyRecord buyRecord = FindRecordByOrderNumber(alipayRecord.out_trade_no);
            if (buyRecord == null)
            {
                buyRecord = DBProvider.MineRecordDBProvider.GetMineTradeRecord(alipayRecord.user_name, alipayRecord.out_trade_no);
                if (buyRecord != null)
                {
                    return OperResult.RESULTCODE_ORDER_BUY_SUCCEED;
                }
                LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 支付宝购买矿山回调，找不到订单。支付宝信息：" + alipayRecord.ToString());
                return OperResult.RESULTCODE_ORDER_NOT_EXIST;
            }
            CustomerMySqlTransaction myTrans = null;
            try
            {
                int result = OperResult.RESULTCODE_FALSE;
                myTrans = MyDBHelper.Instance.CreateTrans();

                //alipayRecord.user_name = buyRecord.UserName;
                if (alipayRecord.value_rmb >= buyRecord.SpendRMB)
                {
                    result = PlayerController.Instance.BuyMineByAlipay(buyRecord, alipayRecord.total_fee, myTrans);
                    if (result == OperResult.RESULTCODE_TRUE)
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
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 成功购买" + buyRecord.GainMinesCount + "座矿山。ano: " + alipayRecord.alipay_trade_no);
                    }
                    else
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + alipayRecord.user_name + "] 购买矿山失败，原因为：" + OperResult.GetMsg(result) + "。ano: " + alipayRecord.alipay_trade_no);
                    }
                }

                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord, myTrans);

                myTrans.Commit();
                PlayerActionController.Instance.AddLog(buyRecord.UserName, MetaData.ActionLog.ActionType.BuyMine, buyRecord.GainMinesCount,
                    "增加了 " + buyRecord.GainStonesReserves.ToString() + " 的矿石储量");

                return result;
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                PlayerController.Instance.RefreshFortune(alipayRecord.user_name);

                LogHelper.Instance.AddErrorLog("玩家[" + alipayRecord.user_name + "] 支付宝金币充值，回调异常。AlipayInfo : " + alipayRecord.ToString(), exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
            finally
            {
                if (myTrans != null)
                {
                    myTrans.Dispose();
                }
            }
        }
        
        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> MineOrderPaySucceedNotify;

    }
}
