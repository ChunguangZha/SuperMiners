using DataBaseProvider;
using MetaData;
using MetaData.Game.StoneStack;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Stack
{
    class StackTradeResult
    {
        public StackTradeState State = StackTradeState.Failed;

        public int SucceedStoneHandCount;
    }

    enum StackTradeState
    {
        Succeed,
        Splited,
        Failed
    }

    class ListStoneDelegateSellOrderInfoCollection
    {
        private List<StoneDelegateSellOrderInfo> ListOrders = new List<StoneDelegateSellOrderInfo>();
        private object _lockList = new object();

        public int ItemsCount
        {
            get { return this.ListOrders.Count; }
        }

        public void Enqueue(StoneDelegateSellOrderInfo sellOrder)
        {
            lock (_lockList)
            {
                ListOrders.Add(sellOrder);
            }
        }

        public StoneDelegateSellOrderInfo Dequeue()
        {
            StoneDelegateSellOrderInfo sellOrder = null;
            lock (_lockList)
            {
                if (ListOrders.Count > 0)
                {
                    sellOrder = ListOrders[0];
                    ListOrders.RemoveAt(0);
                }
            }
            return sellOrder;
        }

        public StoneDelegateSellOrderInfo DeleteOrder(string orderNumber)
        {
            StoneDelegateSellOrderInfo sellOrder = null;
            lock (_lockList)
            {
                if (ListOrders.Count > 0)
                {
                    int index = -1;
                    for (int i = 0; i < ListOrders.Count; i++)
                    {
                        if (ListOrders[i].OrderNumber == orderNumber)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        sellOrder = ListOrders[index];
                        ListOrders.RemoveAt(index);
                    }
                }
            }

            return sellOrder;
        }

    }

    class ListStoneDelegateBuyOrderInfoCollection
    {
        private List<StoneDelegateBuyOrderInfo> ListOrders = new List<StoneDelegateBuyOrderInfo>();
        private object _lockList = new object();

        public int ItemsCount
        {
            get { return this.ListOrders.Count; }
        }

        public void Enqueue(StoneDelegateBuyOrderInfo sellOrder)
        {
            lock (_lockList)
            {
                ListOrders.Add(sellOrder);
            }
        }

        public StoneDelegateBuyOrderInfo Dequeue()
        {
            StoneDelegateBuyOrderInfo buyOrder = null;
            lock (_lockList)
            {
                if (ListOrders.Count > 0)
                {
                    buyOrder = ListOrders[0];
                    ListOrders.RemoveAt(0);
                }
            }
            return buyOrder;
        }

        public StoneDelegateBuyOrderInfo DeleteOrder(string orderNumber)
        {
            StoneDelegateBuyOrderInfo sellOrder = null;
            lock (_lockList)
            {
                if (ListOrders.Count > 0)
                {
                    int index = -1;
                    for (int i = 0; i < ListOrders.Count; i++)
                    {
                        if (ListOrders[i].OrderNumber == orderNumber)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        sellOrder = ListOrders[index];
                        ListOrders.RemoveAt(index);
                    }
                }
            }

            return sellOrder;
        }

    }


    public class StoneStackController
    {
        private List<StoneStackDailyRecordInfo> _listTodayRealTimeTradeRecords = new List<StoneStackDailyRecordInfo>();
        private TodayStoneStackTradeRecordInfo _todayTradeInfo = new TodayStoneStackTradeRecordInfo();
        private object _lockTodayInfo = new object();

        private object _lockUpdateDB = new object();
        ConcurrentDictionary<decimal, ListStoneDelegateSellOrderInfoCollection> _dicWaitingSellInfos = new ConcurrentDictionary<decimal, ListStoneDelegateSellOrderInfoCollection>();

        ConcurrentDictionary<decimal, ListStoneDelegateBuyOrderInfoCollection> _dicWaitingBuyInfos = new ConcurrentDictionary<decimal, ListStoneDelegateBuyOrderInfoCollection>();

        private List<StoneDelegateBuyOrderInfo> _listTempAlipayBuyOrders = new List<StoneDelegateBuyOrderInfo>();
        private object _lockTempAlipayList = new object();

        private Thread _thrStoneStackTrade = null;

        #region Init

        public void Init()
        {
            LoadDataFromDatabase();

            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(2),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketMorningOpenTime, 0, 0),
                Task = MarketOpen
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(22),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketMorningCloseTime, 0, 0),
                Task = MarketSuspend
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(32),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketAfternoonOpenTime, 0, 0),
                Task = MarketResume
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(42),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketAfternoonCloseTime, 0, 0),
                Task = MarketSuspend
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(52),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketNightOpenTime, 0, 0),
                Task = MarketResume
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(62),
                DailyTime = new DateTime(2000, 1, 1, GlobalConfig.GameConfig.StackMarketNightCloseTime, 0, 0),
                Task = MarketClose
            });

            this._thrStoneStackTrade = new Thread(ThreadStoneStackTrade);
            this._thrStoneStackTrade.IsBackground = true;
            this._thrStoneStackTrade.Name = "ThreadStoneStackTrade";
            this._thrStoneStackTrade.Start();
        }

        public TodayStoneStackTradeRecordInfo GetTodayStackInfo()
        {
            if (this._todayTradeInfo.MarketState != StackMarketState.Opening && DateTime.Now.Hour < GlobalConfig.GameConfig.StackMarketMorningOpenTime)
            {
                return null;
            }
            return this._todayTradeInfo;
        }

        public void MarketOpen()
        {
            if (this._todayTradeInfo.MarketState == StackMarketState.Opening)
            {
                return;
            }

            this._todayTradeInfo.MarketState = StackMarketState.Opening;

            InitTodayDailyInfo();

            LogHelper.Instance.AddInfoLog("MarketOpen");
        }

        /// <summary>
        /// 暂停休市
        /// </summary>
        public void MarketSuspend()
        {
            if (this._todayTradeInfo.MarketState != StackMarketState.Opening)
            {
                return;
            }

            this._todayTradeInfo.MarketState = StackMarketState.Suspend;

            LogHelper.Instance.AddInfoLog("MarketSuspend");
        }

        /// <summary>
        /// 恢复继续开市
        /// </summary>
        public void MarketResume()
        {
            if (this._todayTradeInfo.MarketState != StackMarketState.Suspend)
            {
                return;
            }

            this._todayTradeInfo.MarketState = StackMarketState.Opening;

            LogHelper.Instance.AddInfoLog("MarketResume");
        }

        public void MarketClose()
        {
            if (this._todayTradeInfo.MarketState == StackMarketState.Closed)
            {
                return;
            }

            this._todayTradeInfo.MarketState = StackMarketState.Closed;

            LogHelper.Instance.AddInfoLog("MarketClose");

            //清理内存
            lock (_lockUpdateDB)
            {
                _dicWaitingSellInfos.Clear();
                _dicWaitingBuyInfos.Clear();
                this._listTodayRealTimeTradeRecords.Clear();
                //_listTempAlipayBuyOrders.Clear();
            }

            try
            {
                if (this._todayTradeInfo.DailyInfo.ClosePrice < this._todayTradeInfo.DailyInfo.MinTradeSucceedPrice || this._todayTradeInfo.DailyInfo.ClosePrice > this._todayTradeInfo.DailyInfo.MaxTradeSucceedPrice)
                {
                    this._todayTradeInfo.DailyInfo.ClosePrice = this._todayTradeInfo.DailyInfo.OpenPrice;
                }

                //有可能存在支付成功但服务器处理失败的情况，所以先不清除，先手动清理未支付的订单
                //DBProvider.StoneStackDBProvider.ClearNotPayedNotFinishedStoneDelegateBuyOrder();
                DBProvider.StoneStackDBProvider.SaveStoneStackDailyRecordInfo(this._todayTradeInfo.DailyInfo);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("SaveStoneStackDailyRecordInfo Exception", exc);
            }

            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            var buyOrderNotFinishedList = DBProvider.StoneStackDBProvider.GetAllNotFinishedStoneDelegateBuyOrderInfo();
            if (buyOrderNotFinishedList != null)
            {
                foreach (var item in buyOrderNotFinishedList)
                {
                    //需要分别处理Waiting和Splited两种情况
                    if (item.BuyState == StoneDelegateBuyState.Splited)
                    {
                        item.BuyUnit.TradeStoneHandCount = item.BuyUnit.TradeStoneHandCount - item.FinishedStoneTradeHandCount;
                        item.FinishedStoneTradeHandCount = 0;
                        InsertToBuyQueue(item, null);
                    }
                    else if (item.BuyState == StoneDelegateBuyState.Waiting)
                    {
                        InsertToBuyQueue(item, null);
                    }
                    else
                    {
                        this._listTempAlipayBuyOrders.Add(item);
                    }
                }
            }

            var sellOrderNotFinishedList = DBProvider.StoneStackDBProvider.GetAllNotFinishedStoneDelegateSellOrderInfo();
            if (sellOrderNotFinishedList != null)
            {
                foreach (var item in sellOrderNotFinishedList)
                {
                    if (item.SellState == StoneDelegateSellState.Splited)
                    {
                        item.SellUnit.TradeStoneHandCount = item.SellUnit.TradeStoneHandCount - item.FinishedStoneTradeHandCount;
                        item.FinishedStoneTradeHandCount = 0;
                    }
                    InsertToSellQueue(item, null);
                }
            }
        }

        private void InitTodayDailyInfo()
        {
            var lastDailyInfo = DBProvider.StoneStackDBProvider.GetLastStoneStackDailyRecordInfo();
            decimal initPrice = 1 / GlobalConfig.GameConfig.Stones_RMB * 1000;
            this._todayTradeInfo.InitTodayDailyInfo(lastDailyInfo, initPrice);

        }

        private void InsertToBuyQueue(StoneDelegateBuyOrderInfo item, CustomerMySqlTransaction myTrans)
        {
            //需要分别处理Waiting和Splited两种情况
            if (item.BuyState != StoneDelegateBuyState.Waiting && item.BuyState != StoneDelegateBuyState.Splited)
            {
                return;
            }
            
            int index = this._todayTradeInfo.InsertBuyUnit(item.BuyUnit);

            ListStoneDelegateBuyOrderInfoCollection queueBuy = null;
            if (_dicWaitingBuyInfos.ContainsKey(item.BuyUnit.Price))
            {
                queueBuy = _dicWaitingBuyInfos[item.BuyUnit.Price];
            }
            else
            {
                queueBuy = new ListStoneDelegateBuyOrderInfoCollection();
                _dicWaitingBuyInfos[item.BuyUnit.Price] = queueBuy;
            }

            queueBuy.Enqueue(item);

            if (myTrans != null)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateBuyOrderInfo(item, myTrans);
            }
        }

        private void InsertToSellQueue(StoneDelegateSellOrderInfo item, CustomerMySqlTransaction myTrans)
        {
            //需要分别处理Waiting和Splited两种情况
            if (item.SellState != StoneDelegateSellState.Waiting && item.SellState != StoneDelegateSellState.Splited)
            {
                return;
            }

            int index = this._todayTradeInfo.InsertSellUnit(item.SellUnit);

            ListStoneDelegateSellOrderInfoCollection queueSell = null;
            if (_dicWaitingSellInfos.ContainsKey(item.SellUnit.Price))
            {
                queueSell = _dicWaitingSellInfos[item.SellUnit.Price];
            }
            else
            {
                queueSell = new ListStoneDelegateSellOrderInfoCollection();
                _dicWaitingSellInfos[item.SellUnit.Price] = queueSell;
            }

            queueSell.Enqueue(item);

            if (myTrans!= null)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateSellOrderInfo(item, myTrans);
            }
        }

        #endregion

        public int AlipayCallback(AlipayRechargeRecord alipayRecord)
        {
            int result = OperResult.RESULTCODE_FALSE;

            var alipayRecordFromDB = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(alipayRecord.out_trade_no, alipayRecord.alipay_trade_no);
            if (alipayRecordFromDB != null)
            {
                return OperResult.RESULTCODE_ORDER_BUY_SUCCEED;
            }

            StoneDelegateBuyOrderInfo buyOrder = null;
            lock (_lockTempAlipayList)
            {
                for (int i = 0; i < _listTempAlipayBuyOrders.Count; i++)
                {
                    if (_listTempAlipayBuyOrders[i].OrderNumber == alipayRecord.out_trade_no)
                    {
                        buyOrder = _listTempAlipayBuyOrders[i];
                        decimal valueRMB = buyOrder.BuyUnit.Price * buyOrder.BuyUnit.TradeStoneHandCount;
                        if (alipayRecord.value_rmb < valueRMB)
                        {
                            buyOrder.BuyState = StoneDelegateBuyState.Exception;
                            LogHelper.Instance.AddErrorLog(alipayRecord.ToString() + "  充值的灵币小于需要的灵币：" + valueRMB, null);
                        }
                        else
                        {
                            buyOrder.BuyState = StoneDelegateBuyState.Waiting;
                        }
                        
                        this._listTempAlipayBuyOrders.RemoveAt(i);
                        break;
                    }
                }
            }

            if (buyOrder == null)
            {
                LogHelper.Instance.AddErrorLog("委托挂单购买矿石，支付宝回调，没有找到订单。" + alipayRecord.ToString(), null);
            }
            CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
            try
            {
                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord, myTrans);
                if (buyOrder != null)
                {
                    DBProvider.StoneStackDBProvider.UpdateWaitingStoneDelegateBuyOrderState(buyOrder.OrderNumber, buyOrder.BuyState, myTrans);
                }

                myTrans.Commit();

                result = OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                LogHelper.Instance.AddErrorLog("StoneStackControl.AlipayCallback.SaveAlipayRechargeRecord Exception. " + alipayRecord.ToString()
                    + ". buyOrder: " + (buyOrder == null ? "NULL" : buyOrder.ToString()), exc);
            }
            finally
            {
                myTrans.Dispose();
            }

            if (result == OperResult.RESULTCODE_TRUE)
            {
                if (buyOrder != null)
                {
                    this.InsertToBuyQueue(buyOrder, null);
                }
                BuyOrderAlipayPaySucceedNotify(alipayRecord.user_name, alipayRecord.out_trade_no);
            }

            return result;
        }

        private void BuyOrderAlipayPaySucceedNotify(string buyerUserName, string orderNumber)
        {
            string tokenBuyer = ClientManager.GetToken(buyerUserName);
            if (!string.IsNullOrEmpty(tokenBuyer))
            {
                if (DelegateBuyStoneOrderAlipayPaySucceedNotify != null)
                {
                    DelegateBuyStoneOrderAlipayPaySucceedNotify(tokenBuyer, orderNumber);
                    LogHelper.Instance.AddInfoLog("订单号： " + orderNumber + " 委托订单，支付宝充值成功，已经通知买家：" + buyerUserName);
                }
            }
        }

        public OperResultObject PlayerDelegateSellStone(StoneDelegateSellOrderInfo sellOrder, CustomerMySqlTransaction myTrans)
        {
            OperResultObject result = new OperResultObject();

            //只有开市期间才可挂单，间休期间暂时也不可挂单
            if (this._todayTradeInfo.MarketState != StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED;
                return result;
            }
            if (sellOrder.SellUnit.Price < this._todayTradeInfo.DailyInfo.LimitDownPrice || sellOrder.SellUnit.Price > this._todayTradeInfo.DailyInfo.LimitUpPrice)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_PRICE_OUTOFRANGE;
                return result;
            }

            InsertToSellQueue(sellOrder, myTrans);

            result.OperResultCode = OperResult.RESULTCODE_TRUE;
            return result;
        }

        public OperResultObject PlayerCancelSellStone(string orderNumber, decimal sellPrice, CustomerMySqlTransaction myTrans, out StoneDelegateSellOrderInfo canceledSellOrder)
        {
            canceledSellOrder = null;
            OperResultObject result = new OperResultObject();

            //开市期间不可撤单
            if (this._todayTradeInfo.MarketState == StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING;
                return result;
            }

            canceledSellOrder = this._dicWaitingSellInfos[sellPrice].DeleteOrder(orderNumber);
            if (canceledSellOrder != null)
            {
                result = this._todayTradeInfo.DeleteSellUnit(canceledSellOrder.SellUnit);
                if (result.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return result;
                }

                DBProvider.StoneStackDBProvider.CancelSellStoneOrder(canceledSellOrder, myTrans);

                result.OperResultCode = OperResult.RESULTCODE_TRUE;
            }
            else
            {
                result.OperResultCode = OperResult.RESULTCODE_FALSE;
            }

            return result;
        }

        /// <summary>
        /// 付款方式为支付宝的订单，添加到临时集合中
        /// </summary>
        /// <param name="buyOrder"></param>
        /// <returns></returns>
        public OperResultObject PlayerDelegateBuyStone(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction myTrans)
        {
            OperResultObject result = new OperResultObject();

            //只有开市期间才可挂单，间休期间暂时也不可挂单
            if (this._todayTradeInfo.MarketState != StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED;
                return result;
            }

            if (buyOrder.PayType == PayType.Alipay)
            {
                //也要保存到数据库里
                buyOrder.BuyState = StoneDelegateBuyState.NotPayed;
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateBuyOrderInfo(buyOrder, myTrans);
                lock (_lockTempAlipayList)
                {
                    this._listTempAlipayBuyOrders.Add(buyOrder);
                }
            }
            else
            {
                InsertToBuyQueue(buyOrder, myTrans);
            }
            result.OperResultCode = OperResult.RESULTCODE_TRUE;
            return result;
        }

        public OperResultObject PlayerCancelBuyStone(string orderNumber, decimal sellPrice, CustomerMySqlTransaction myTrans, out StoneDelegateBuyOrderInfo canceledBuyOrder)
        {
            canceledBuyOrder = null;
            OperResultObject result = new OperResultObject();

            //开市期间不可撤单
            if (this._todayTradeInfo.MarketState == StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING;
                return result;
            }


            canceledBuyOrder = this._dicWaitingBuyInfos[sellPrice].DeleteOrder(orderNumber);
            if (canceledBuyOrder != null)
            {
                result = this._todayTradeInfo.DeleteBuyUnit(canceledBuyOrder.BuyUnit);
                if (result.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return result;
                }

                DBProvider.StoneStackDBProvider.CancelBuyStoneOrder(canceledBuyOrder, myTrans);
                result.OperResultCode = OperResult.RESULTCODE_TRUE;
            }
            else
            {
                canceledBuyOrder = this._listTempAlipayBuyOrders.FirstOrDefault(s => s.OrderNumber == orderNumber);
                if (canceledBuyOrder != null)
                {
                    DBProvider.StoneStackDBProvider.CancelBuyStoneOrder(canceledBuyOrder, myTrans);
                    this._listTempAlipayBuyOrders.Remove(canceledBuyOrder);
                    result.OperResultCode = OperResult.RESULTCODE_TRUE;
                }
                else
                {
                    result.OperResultCode = OperResult.RESULTCODE_FALSE;
                }
            }

            return result;
        }

        public StoneStackDailyRecordInfo[] GetTodayRealTimeTradeRecords()
        {
            return this._listTodayRealTimeTradeRecords.ToArray();
        }

        #region Trade

        private void ThreadStoneStackTrade(object state)
        {
            while (true)
            {
                //10秒处理一次
                Thread.Sleep(10 * 1000);
                try
                {
                    if (this._todayTradeInfo.MarketState != StackMarketState.Opening)
                    {
                        continue;
                    }

                    if (this._dicWaitingBuyInfos.Count == 0 || this._dicWaitingSellInfos.Count == 0)
                    {
                        continue;
                    }

                    //只处理 买1 和 卖1
                    lock (_lockTodayInfo)
                    {
                        TradeBuy1Orders();

                        this._todayTradeInfo.DailyInfo.Day = new MyDateTime(DateTime.Now);
                        var newDailyRecord = this._todayTradeInfo.DailyInfo.Copy();
                        this._listTodayRealTimeTradeRecords.Add(newDailyRecord);
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ThreadStoneStackTrade Exception", exc);
                }
            }
        }

        private void TradeBuy1Orders()
        {
            var buy1Unit = this._todayTradeInfo.GetBuy1Unit();
            if (buy1Unit == null)
            {
                return;
            }
            decimal buyPrice = buy1Unit.Price;

            if (buy1Unit.TradeStoneHandCount == 0 || !this._dicWaitingBuyInfos.ContainsKey(buyPrice) || this._dicWaitingBuyInfos[buyPrice] == null)
            {
                return;
            }

            //循环取出买价对应的买单
            StoneDelegateBuyOrderInfo buyOrder = this._dicWaitingBuyInfos[buyPrice].Dequeue();
            while (buyOrder != null)
            {
                int FinishedStoneHandCount = 0;

                if (buyOrder.BuyState == StoneDelegateBuyState.Waiting)
                {
                    var result = TradeOneBuyOrder(buyOrder);
                    if (result.State == StackTradeState.Failed)
                    {
                        //没有找到价格合适的卖单，把买单再加回集合中.
                        this._dicWaitingBuyInfos[buyPrice].Enqueue(buyOrder);
                        break;
                    }
                    else if (result.State == StackTradeState.Succeed)
                    {
                        FinishedStoneHandCount += buyOrder.FinishedStoneTradeHandCount;
                    }
                    else if (result.State == StackTradeState.Splited)
                    {
                        DateTime nowtime = DateTime.Now;
                        //此时，卖1订单已经全部被吞掉。而当前买单没有消化完，需要将剩余部分拆分，拆分后的订单保存到数据库，并添加到集合中
                        StoneDelegateBuyOrderInfo newBuyOrder = new StoneDelegateBuyOrderInfo()
                        {
                            UserID = buyOrder.UserID,
                            IsSubOrder = true,
                            BuyState = StoneDelegateBuyState.Waiting,
                            OrderNumber = OrderController.Instance.CreateOrderNumber(buyOrder.UserName, nowtime, AlipayTradeInType.StackStoneBuy),
                            ParentOrderNumber = buyOrder.OrderNumber,
                            UserName = buyOrder.UserName,
                            PayType = buyOrder.PayType,
                            DelegateTime = new MyDateTime(nowtime),
                            BuyUnit = new StackTradeUnit()
                            {
                                Price = buyOrder.BuyUnit.Price,
                                TradeStoneHandCount = buyOrder.BuyUnit.TradeStoneHandCount - buyOrder.FinishedStoneTradeHandCount
                            },
                        };

                        try
                        {
                            DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateBuyOrderInfo(newBuyOrder);
                            this._dicWaitingBuyInfos[buyPrice].Enqueue(newBuyOrder);
                        }
                        catch (Exception exc)
                        {
                            LogHelper.Instance.AddErrorLog("Save Sub Buy Order Exception. UserName:" + buyOrder.UserName + ", ParentOrderNumber: " + buyOrder.OrderNumber, exc);
                        }
                        FinishedStoneHandCount += buyOrder.FinishedStoneTradeHandCount;
                    }

                    if (FinishedStoneHandCount > 0)
                    {
                        var operResult = this._todayTradeInfo.DecreaseBuyUnit(buyPrice, FinishedStoneHandCount);
                        if (operResult.OperResultCode != OperResult.RESULTCODE_TRUE)
                        {
                            LogHelper.Instance.AddErrorLog("TradeBuy1Orders DecreaseBuyUnit Error. " + operResult.Message, null);
                        }
                    }
                }

                buyOrder = this._dicWaitingBuyInfos[buyPrice].Dequeue();
            }
        }

        private StackTradeResult TradeOneBuyOrder(StoneDelegateBuyOrderInfo buyOrder)
        {
            List<StoneDelegateSellOrderInfo> listTradeSucceedSellOrders = new List<StoneDelegateSellOrderInfo>();
            StackTradeResult buyOrderResult = TradeSell1Orders(buyOrder, listTradeSucceedSellOrders);

            if (buyOrderResult.State == StackTradeState.Failed)
            {
                return buyOrderResult;
            }

            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();
                foreach (var item in listTradeSucceedSellOrders)
                {
                    DBProvider.StoneStackDBProvider.SaveFinishedStoneDelegateSellOrderInfo(item);
                    PlayerController.Instance.PayDelegateBuyStonesUpdateSellerInfo(item, myTrans);
                }

                int selfSellHandCount = 0;
                foreach (var item in listTradeSucceedSellOrders)
                {
                    if (item.UserID == buyOrder.UserID)
                    {
                        selfSellHandCount += item.FinishedStoneTradeHandCount;
                    }
                }
                decimal allNeedRMB = (buyOrder.FinishedStoneTradeHandCount - selfSellHandCount) * buyOrder.BuyUnit.Price;
                buyOrder.AwardGoldCoin = (int)((allNeedRMB * GlobalConfig.GameConfig.StoneBuyerAwardGoldCoinMultiple) * GlobalConfig.GameConfig.RMB_GoldCoin);
                DBProvider.StoneStackDBProvider.SaveFinishedStoneDelegateBuyOrderInfo(buyOrder);
                PlayerController.Instance.PayDelegateBuyStonesUpdateBuyerInfo(buyOrder, myTrans);

                myTrans.Commit();

                //计算成交价
                this._todayTradeInfo.ComputeTradePrice(buyOrder.BuyUnit.Price, listTradeSucceedSellOrders[0].SellUnit.Price, buyOrder.FinishedStoneTradeHandCount);
                foreach (var item in listTradeSucceedSellOrders)
                {
                    AddLogNotifySeller(item);
                    LogHelper.Instance.AddInfoLog("委卖矿石交易成功：" + item.ToString());

                }
                AddLogNotifyBuyer(buyOrder);

                LogHelper.Instance.AddInfoLog("委买矿石交易成功：" + buyOrder.ToString());

            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("ThreadStoneStackTrade.SaveStoneSellOrderHandler Exception", exc);
                myTrans.Rollback();
            }
            finally
            {
                myTrans.Dispose();
            }

            return buyOrderResult;
        }

        private StackTradeResult TradeSell1Orders(StoneDelegateBuyOrderInfo buyOrder, List<StoneDelegateSellOrderInfo> listTradeSucceedSellOrders)
        {
            StackTradeResult buyOrderResult = new StackTradeResult();

            decimal buyPrice = buyOrder.BuyUnit.Price;

            StackTradeUnit buyUnit = new StackTradeUnit()
            {
                Price = buyOrder.BuyUnit.Price,
                TradeStoneHandCount = buyOrder.BuyUnit.TradeStoneHandCount
            };

            //只处理卖1
            var sellUnit = this._todayTradeInfo.GetSell1Unit();
            if (sellUnit == null)
            {
                return buyOrderResult;
            }
            if (sellUnit.TradeStoneHandCount == 0 || !this._dicWaitingSellInfos.ContainsKey(sellUnit.Price) || this._dicWaitingSellInfos[sellUnit.Price] == null)
            {
                return buyOrderResult;
            }

            if (buyPrice >= sellUnit.Price)
            {
                //成交，按量处理
                buyOrderResult = TradeSamePriceSellOrder(sellUnit.Price, this._dicWaitingSellInfos[sellUnit.Price], listTradeSucceedSellOrders, buyUnit, buyOrder.UserName);
                
                if (buyOrderResult.State == StackTradeState.Succeed)
                {
                    //买单 被全部消化
                    //暂时不清理卖单集合，被拆分的卖单，剩余部分在外部会重新加回卖单集合
                    buyOrder.BuyState = StoneDelegateBuyState.Succeed;
                    buyOrder.FinishedStoneTradeHandCount = buyOrder.BuyUnit.TradeStoneHandCount;
                    buyOrder.FinishedTime = new MyDateTime(DateTime.Now);
                }
                else if (buyOrderResult.State == StackTradeState.Splited)
                {
                    //买单没有被全部消化，需要再对比处理上一级卖价。等所有卖价都对比完时，再返回
                    buyUnit.TradeStoneHandCount -= buyOrderResult.SucceedStoneHandCount;
                    buyOrder.BuyState = StoneDelegateBuyState.Splited;
                    buyOrder.FinishedStoneTradeHandCount = buyOrder.BuyUnit.TradeStoneHandCount - buyUnit.TradeStoneHandCount;
                    buyOrder.FinishedTime = new MyDateTime(DateTime.Now);
                }
            }

            return buyOrderResult;
        }

        /// <summary>
        /// 返回买单处理结果
        /// </summary>
        /// <param name="sellPrice"></param>
        /// <param name="queueSellOrders"></param>
        /// <param name="listTradeSucceedSellOrders"></param>
        /// <param name="buyOrder"></param>
        /// <returns></returns>
        private StackTradeResult TradeSamePriceSellOrder(decimal sellPrice, ListStoneDelegateSellOrderInfoCollection queueSellOrders,
            List<StoneDelegateSellOrderInfo> listTradeSucceedSellOrders, StackTradeUnit buyUnit, string buyerUserName)
        {
            StackTradeResult buyOrderResult = new StackTradeResult();

            //取出卖价对应的卖单
            StoneDelegateSellOrderInfo sellOrder = queueSellOrders.Dequeue();
            if (sellOrder == null)
            {
                return buyOrderResult;
            }

            int surplusStoneHandCount = buyUnit.TradeStoneHandCount - sellOrder.SellUnit.TradeStoneHandCount;

            //买单量大于或等于卖单量
            while (surplusStoneHandCount > 0)//继续找其他卖单凑量
            {
                sellOrder.SellState = StoneDelegateSellState.Succeed;
                sellOrder.FinishedStoneTradeHandCount = sellOrder.SellUnit.TradeStoneHandCount;
                sellOrder.FinishedTime = new MyDateTime(DateTime.Now);
                listTradeSucceedSellOrders.Add(sellOrder);

                var operResult = this._todayTradeInfo.DecreaseSellUnit(sellOrder.SellUnit.Price, sellOrder.SellUnit.TradeStoneHandCount);
                if (operResult.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddErrorLog("TradeBuy1Orders DecreaseSellUnit Error1. " + operResult.Message, null);
                }

                var nextSellOrder = queueSellOrders.Dequeue();
                if (nextSellOrder == null)
                {
                    //说明已经没有当前价格的卖单了。
                    //买单只消化一部，需要被拆分
                    buyOrderResult.State = StackTradeState.Splited;
                    buyOrderResult.SucceedStoneHandCount = buyUnit.TradeStoneHandCount - surplusStoneHandCount;
                    return buyOrderResult;
                }
                else
                {
                    //判断下一卖单
                    sellOrder = nextSellOrder;
                    surplusStoneHandCount = surplusStoneHandCount - sellOrder.SellUnit.TradeStoneHandCount;
                }
            }
            //卖单量等于买单量
            if (surplusStoneHandCount == 0)
            {
                sellOrder.SellState = StoneDelegateSellState.Succeed;
                sellOrder.FinishedStoneTradeHandCount = sellOrder.SellUnit.TradeStoneHandCount;
                sellOrder.FinishedTime = new MyDateTime(DateTime.Now);
                listTradeSucceedSellOrders.Add(sellOrder);

                var operResult = this._todayTradeInfo.DecreaseSellUnit(sellOrder.SellUnit.Price, sellOrder.SellUnit.TradeStoneHandCount);
                if (operResult.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddErrorLog("TradeBuy1Orders DecreaseSellUnit Error2. " + operResult.Message, null);
                }

            }
            else
            {
                //卖单量大于买单量，需要将当前卖单拆分。将已成功的部分拆成一新订单添加到数据库中；将老订单缩减到剩余部分，继续留在市场中。
                sellOrder.SellState = StoneDelegateSellState.Splited;
                sellOrder.FinishedStoneTradeHandCount = sellOrder.SellUnit.TradeStoneHandCount + surplusStoneHandCount;
                sellOrder.FinishedTime = new MyDateTime(DateTime.Now);
                listTradeSucceedSellOrders.Add(sellOrder);

                var operResult = this._todayTradeInfo.DecreaseSellUnit(sellOrder.SellUnit.Price, sellOrder.FinishedStoneTradeHandCount);
                if (operResult.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddErrorLog("TradeBuy1Orders DecreaseSellUnit Error3. " + operResult.Message, null);
                }


                DateTime nowTime = DateTime.Now;

                //拆分后的订单，再保存到数据库，并添加到集合中。
                StoneDelegateSellOrderInfo newSellOrder = new StoneDelegateSellOrderInfo()
                {
                    OrderNumber = OrderController.Instance.CreateOrderNumber(sellOrder.UserName, nowTime, AlipayTradeInType.StackStoneSell),
                    UserID = sellOrder.UserID,
                    UserName = sellOrder.UserName,
                    SellState = StoneDelegateSellState.Waiting,
                    DelegateTime = new MyDateTime(nowTime),
                    SellUnit = new StackTradeUnit()
                    {
                        Price = sellOrder.SellUnit.Price,
                        TradeStoneHandCount = sellOrder.SellUnit.TradeStoneHandCount - sellOrder.FinishedStoneTradeHandCount
                    },
                    IsSubOrder = true
                };

                try
                {
                    DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateSellOrderInfo(newSellOrder);
                    queueSellOrders.Enqueue(newSellOrder);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("股票操作。TradeSamePriceSellOrder.SaveWaitingStoneDelegateSellOrderInfo 异常。信息为：" + newSellOrder.ToString(), exc);
                }
            }

            //买单已全部被消化
            buyOrderResult.State = StackTradeState.Succeed;
            buyOrderResult.SucceedStoneHandCount = buyUnit.TradeStoneHandCount;
            return buyOrderResult;
        }

        #endregion


        private void AddLogNotifyBuyer(StoneDelegateBuyOrderInfo buyOrder)
        {
            PlayerActionController.Instance.AddLog(buyOrder.UserName, MetaData.ActionLog.ActionType.DelegateBuyStoneSucceed, buyOrder.FinishedStoneTradeHandCount * GlobalConfig.GameConfig.HandStoneCount, buyOrder.AwardGoldCoin.ToString());

            string tokenBuyer = ClientManager.GetToken(buyOrder.UserName);
            if (!string.IsNullOrEmpty(tokenBuyer))
            {
                if (DelegateStoneOrderTradeSucceedNotifyPlayer != null)
                {
                    DelegateStoneOrderTradeSucceedNotifyPlayer(tokenBuyer, buyOrder.OrderNumber, AlipayTradeInType.StackStoneBuy);
                    LogHelper.Instance.AddInfoLog("委托买单。订单号： " + buyOrder.OrderNumber + " 矿石交易成功，Order：" + buyOrder.ToString());
                }
            }
        }

        private void AddLogNotifySeller(StoneDelegateSellOrderInfo sellOrder)
        {
            PlayerActionController.Instance.AddLog(sellOrder.UserName, MetaData.ActionLog.ActionType.DelegateSellStoneSucceed, sellOrder.FinishedStoneTradeHandCount * GlobalConfig.GameConfig.HandStoneCount);

            string tokenBuyer = ClientManager.GetToken(sellOrder.UserName);
            if (!string.IsNullOrEmpty(tokenBuyer))
            {
                if (DelegateStoneOrderTradeSucceedNotifyPlayer != null)
                {
                    DelegateStoneOrderTradeSucceedNotifyPlayer(tokenBuyer, sellOrder.OrderNumber, AlipayTradeInType.StackStoneSell);
                    LogHelper.Instance.AddInfoLog("委托卖单。订单号： " + sellOrder.OrderNumber + " 矿石交易成功，Order：" + sellOrder.ToString());
                }
            }
        }


        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string, AlipayTradeInType> DelegateStoneOrderTradeSucceedNotifyPlayer;

        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> DelegateBuyStoneOrderAlipayPaySucceedNotify;


    }

    public delegate void SaveStoneDelegateSellOrderInvoke(List<StoneDelegateSellOrderInfo> listSellOrders, CustomerMySqlTransaction myTrans);
    public delegate void SaveStoneDelegateBuyOrderInvoke(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction myTrans);
}
