using DataBaseProvider;
using MetaData;
using MetaData.Game.StoneStack;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Stack
{
    //class ListStoneDelegateSellOrderInfoCollection
    //{
    //    private List<StoneDelegateSellOrderInfo> listVIPOrders = new List<StoneDelegateSellOrderInfo>();
    //    private List<StoneDelegateSellOrderInfo> listNormalOrders = new List<StoneDelegateSellOrderInfo>();
    //    private object _lockList = new object();

    //    public void Insert(StoneDelegateSellOrderInfo sellOrder)
    //    {
    //        lock (_lockList)
    //        {
    //            int index = -1;
    //            if (sellOrder.PlayerExpLevel > 0)//VIP
    //            {
    //                for (int i = 0; i < listVIPOrders.Count; i++)
    //                {
    //                    if (listVIPOrders[i].UserID == sellOrder.UserID)
    //                    {
    //                        //VIP集合里，一个VIP玩家只能有一个订单
    //                        index = -1;
    //                        break;
    //                    }


    //                }
    //            }

    //            for (int i = 0; i < listNormalOrders.Count; i++)
    //            {

    //            }
    //        }
    //    }
    //}


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

    enum StackMarketState
    {
        /// <summary>
        /// 开市
        /// </summary>
        Opening,
        /// <summary>
        /// 暂停
        /// </summary>
        Suspend,
        /// <summary>
        /// 闭市
        /// </summary>
        Closed
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
        private StackMarketState _marketState = StackMarketState.Closed;

        private TodayStoneStackTradeRecordInfo _todayTradeInfo = new TodayStoneStackTradeRecordInfo();
        private object _lockTodayInfo = new object();

        ConcurrentDictionary<decimal, ListStoneDelegateSellOrderInfoCollection> _dicWaitingSellInfos = new ConcurrentDictionary<decimal, ListStoneDelegateSellOrderInfoCollection>();

        ConcurrentDictionary<decimal, ListStoneDelegateBuyOrderInfoCollection> _dicWaitingBuyInfos = new ConcurrentDictionary<decimal, ListStoneDelegateBuyOrderInfoCollection>();

        private Thread _thrStoneStackTrade = null;

        #region Init

        public void Init()
        {
            LoadDataFromDatabase();

            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 9, 0, 0),
                Task = MarketOpen
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 12, 0, 0),
                Task = MarketSuspend
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 13, 0, 0),
                Task = MarketResume
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 17, 0, 0),
                Task = MarketSuspend
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 18, 0, 0),
                Task = MarketResume
            });
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 23, 0, 0),
                Task = MarketClose
            });

            this._thrStoneStackTrade = new Thread(ThreadStoneStackTrade);
            this._thrStoneStackTrade.IsBackground = true;
            this._thrStoneStackTrade.Name = "ThreadStoneStackTrade";
            this._thrStoneStackTrade.Start();
        }

        private void MarketOpen()
        {
            _marketState = StackMarketState.Opening;
            InitTodayDailyInfo();
        }

        /// <summary>
        /// 暂停休市
        /// </summary>
        private void MarketSuspend()
        {
            if (_marketState != StackMarketState.Opening)
            {
                return;
            }

            _marketState = StackMarketState.Suspend;

        }

        /// <summary>
        /// 恢复继续开市
        /// </summary>
        private void MarketResume()
        {
            if (_marketState != StackMarketState.Suspend)
            {
                return;
            }

            _marketState = StackMarketState.Opening;

        }

        private void MarketClose()
        {
            if (_marketState != StackMarketState.Closed)
            {
                return;
            }

            _marketState = StackMarketState.Closed;

        }

        private void LoadDataFromDatabase()
        {
            var buyOrderList = DBProvider.StoneStackDBProvider.GetAllWaitingStoneDelegateBuyOrderInfo();
            if (buyOrderList != null)
            {
                foreach (var item in buyOrderList)
                {
                    InsertToBuyQueue(item, false);
                }
            }

            var sellOrderList = DBProvider.StoneStackDBProvider.GetAllWaitingStoneDelegateSellOrderInfo();
            if (sellOrderList != null)
            {
                foreach (var item in sellOrderList)
                {
                    InsertToSellQueue(item, false);
                }
            }
        }

        private void InitTodayDailyInfo()
        {
            var lastDailyInfo = DBProvider.StoneStackDBProvider.GetLastStoneStackDailyRecordInfo();
            decimal initPrice = 1 / GlobalConfig.GameConfig.Stones_RMB * 1000;
            this._todayTradeInfo.InitTodayDailyInfo(lastDailyInfo, initPrice);
        }

        private void InsertToBuyQueue(StoneDelegateBuyOrderInfo item, bool saveToDB)
        {
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

            if (saveToDB)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateBuyOrderInfo(item);
            }
        }

        //private bool UpdateToTodayBuyList(int index, bool IsInsert, StoneDelegateBuyOrderInfo item)
        //{
        //    lock (_lockTodayInfo)
        //    {
        //        this._todayTradeInfo.DailyInfo.DelegateBuyStoneSum += item.BuyUnit.TradeStoneHandCount;

        //        if (IsInsert)
        //        {
        //            _todayTradeInfo.BuyOrderPriceCountList.Insert(index, new StackTradeUnit()
        //            {
        //                Price = item.BuyUnit.Price,
        //                TradeStoneHandCount = item.BuyUnit.TradeStoneHandCount
        //            });
        //        }
        //        else
        //        {
        //            if (_todayTradeInfo.BuyOrderPriceCountList[index] == null)
        //            {
        //                _todayTradeInfo.BuyOrderPriceCountList[index] = new StackTradeUnit()
        //                {
        //                    Price = item.BuyUnit.Price,
        //                    TradeStoneHandCount = item.BuyUnit.TradeStoneHandCount
        //                };
        //            }
        //            else
        //            {
        //                _todayTradeInfo.BuyOrderPriceCountList[index].TradeStoneHandCount += item.BuyUnit.TradeStoneHandCount;
        //            }
        //        }
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="IsInsert">false表示Update</param>
        ///// <returns></returns>
        //private int CheckinBuyListIndex(StoneDelegateBuyOrderInfo item, out bool IsInsert)
        //{
        //    int index = -1;
        //    IsInsert = true;

        //    for (int i = 0; i < _todayTradeInfo.BuyOrderPriceCountList.Count; i++)
        //    {
        //        if (_todayTradeInfo.BuyOrderPriceCountList[i] == null)
        //        {
        //            IsInsert = false;
        //            index = i;
        //            break;
        //        }
        //        //从高到低， 排序
        //        if (item.BuyUnit.Price > _todayTradeInfo.BuyOrderPriceCountList[i].Price)
        //        {
        //            IsInsert = true;
        //            if (i == 0)
        //            {
        //                index = i;
        //                break;
        //            }
        //            else
        //            {
        //                index = i - 1;
        //                break;
        //            }
        //        }
        //        else if (item.BuyUnit.Price == _todayTradeInfo.BuyOrderPriceCountList[i].Price)
        //        {
        //            IsInsert = false;
        //            index = i;
        //            break;
        //        }
        //    }

        //    return index;
        //}

        private void InsertToSellQueue(StoneDelegateSellOrderInfo item, bool saveToDB)
        {
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

            if (saveToDB)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateSellOrderInfo(item);
            }
        }

        //private bool UpdateToTodaySellList(int index, bool IsInsert, StoneDelegateSellOrderInfo item)
        //{
        //    lock (_lockTodayInfo)
        //    {
        //        this._todayTradeInfo.DailyInfo.DelegateSellStoneSum += item.SellUnit.TradeStoneHandCount;

        //        if (IsInsert)
        //        {
        //            _todayTradeInfo.SellOrderPriceCountList.Insert(index,  new StackTradeUnit()
        //            {
        //                Price = item.SellUnit.Price,
        //                TradeStoneHandCount = item.SellUnit.TradeStoneHandCount
        //            });
        //        }
        //        else
        //        {
        //            if (_todayTradeInfo.SellOrderPriceCountList[index] == null)
        //            {
        //                _todayTradeInfo.SellOrderPriceCountList[index] = new StackTradeUnit()
        //                {
        //                    Price = item.SellUnit.Price,
        //                    TradeStoneHandCount = item.SellUnit.TradeStoneHandCount
        //                };
        //            }
        //            else
        //            {
        //                _todayTradeInfo.SellOrderPriceCountList[index].TradeStoneHandCount += item.SellUnit.TradeStoneHandCount;
        //            }
        //        }
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="item"></param>
        ///// <param name="IsInsert">false表示Update</param>
        ///// <returns></returns>
        //private int CheckinSellListIndex(StoneDelegateSellOrderInfo item, out bool IsInsert)
        //{
        //    int index = -1;
        //    IsInsert = true;

        //    for (int i = 0; i < _todayTradeInfo.SellOrderPriceCountList.Count; i++)
        //    {
        //        if (_todayTradeInfo.SellOrderPriceCountList[i] == null)
        //        {
        //            IsInsert = false;
        //            index = i;
        //            break;
        //        }
        //        //从低到高， 排序
        //        if (item.SellUnit.Price < _todayTradeInfo.SellOrderPriceCountList[i].Price)
        //        {
        //            IsInsert = true;
        //            if (i == 0)
        //            {
        //                index = i;
        //                break;
        //            }
        //            else
        //            {
        //                index = i - 1;
        //                break;
        //            }
        //        }
        //        else if (item.SellUnit.Price == _todayTradeInfo.SellOrderPriceCountList[i].Price)
        //        {
        //            IsInsert = false;
        //            index = i;
        //            break;
        //        }
        //    }

        //    return index;
        //}

        #endregion

        public OperResultObject PlayerDelegateSellStone(StoneDelegateSellOrderInfo sellOrder)
        {
            OperResultObject result = new OperResultObject();

            //只有开市期间才可挂单，间休期间暂时也不可挂单
            if (_marketState != StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED;
                return result;
            }

            InsertToSellQueue(sellOrder, true);

            result.OperResultCode = OperResult.RESULTCODE_TRUE;
            return result;
        }

        public OperResultObject PlayerCancelSellStone(StoneDelegateSellOrderInfo sellOrder)
        {
            decimal sellPrice = sellOrder.SellUnit.Price;
            OperResultObject result = new OperResultObject();

            //开市期间不可撤单
            if (_marketState == StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING;
                return result;
            }

            result = this._todayTradeInfo.DeleteSellUnit(sellOrder.SellUnit);
            if (result.OperResultCode != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

            var deletedOrder = this._dicWaitingSellInfos[sellPrice].DeleteOrder(sellOrder.OrderNumber);
            if (deletedOrder != null)
            {
                deletedOrder.SellState = StoneDelegateSellState.Cancel;
                CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    if (SaveStoneSellOrderHandler != null)
                    {
                        SaveStoneSellOrderHandler(new List<StoneDelegateSellOrderInfo>() { deletedOrder }, myTrans);
                    }
                    myTrans.Commit();

                    result.OperResultCode = OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("PlayerCancelSellStone Exception", exc);
                    myTrans.Rollback();
                    deletedOrder.SellState = StoneDelegateSellState.Waiting;
                    this._dicWaitingSellInfos[sellPrice].Enqueue(deletedOrder);
                    result.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                }
                finally
                {
                    myTrans.Dispose();
                }
            }
            else
            {
                result.OperResultCode = OperResult.RESULTCODE_FALSE;
            }

            return result;
        }

        public OperResultObject PlayerDelegateBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            OperResultObject result = new OperResultObject();

            //只有开市期间才可挂单，间休期间暂时也不可挂单
            if (_marketState != StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_DELEGATEORDER_FAILED_MARKETISCLOSED;
                return result;
            }

            InsertToBuyQueue(buyOrder, true);
            result.OperResultCode = OperResult.RESULTCODE_TRUE;
            return result;
        }

        public OperResultObject PlayerCancelBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            OperResultObject result = new OperResultObject();

            //开市期间不可撤单
            if (_marketState == StackMarketState.Opening)
            {
                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_MARKETISOPENING;
                return result;
            }

            result = this._todayTradeInfo.DeleteBuyUnit(buyOrder.BuyUnit);
            if (result.OperResultCode != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

            var deletedOrder = this._dicWaitingBuyInfos[buyOrder.BuyUnit.Price].DeleteOrder(buyOrder.OrderNumber);
            if (deletedOrder != null)
            {
                deletedOrder.BuyState = StoneDelegateBuyState.Cancel;
                CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    if (SaveStoneBuyOrderHandler != null)
                    {
                        SaveStoneBuyOrderHandler(deletedOrder, myTrans);
                    }
                    myTrans.Commit();

                    result.OperResultCode = OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("PlayerCancelBuyStone Exception", exc);
                    myTrans.Rollback();
                    deletedOrder.BuyState = StoneDelegateBuyState.Waiting;
                    this._dicWaitingBuyInfos[buyOrder.BuyUnit.Price].Enqueue(deletedOrder);
                    result.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                }
                finally
                {
                    myTrans.Dispose();
                }
            }
            else
            {
                result.OperResultCode = OperResult.RESULTCODE_FALSE;
            }

            return result;
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
                    if (this._marketState != StackMarketState.Opening)
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
                        UpdateTodayInfo();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ThreadStoneStackTrade Exception", exc);
                }
            }
        }

        private void UpdateTodayInfo()
        {
            //List<StackTradeUnit> newSellPriceList = new List<StackTradeUnit>();
            //List<StackTradeUnit> newBuyPriceList = new List<StackTradeUnit>();

            for (int i = 0; i < this._todayTradeInfo.BuyOrderPriceCountList.Count; i++)
            {
                decimal buyPrice = this._todayTradeInfo.BuyOrderPriceCountList[i].Price;
                if (!this._dicWaitingBuyInfos.ContainsKey(buyPrice))
                {
                    this._todayTradeInfo.BuyOrderPriceCountList.RemoveAt(i);
                    i--;
                    continue;
                }
                else
                {
                    if (this._dicWaitingBuyInfos[buyPrice] == null || this._dicWaitingBuyInfos[buyPrice].Count == 0)
                    {
                        ConcurrentQueue<StoneDelegateBuyOrderInfo> queueSellOrder = null;
                        this._dicWaitingBuyInfos.TryRemove(buyPrice, out queueSellOrder);
                        this._todayTradeInfo.BuyOrderPriceCountList.RemoveAt(i);
                        i--;
                        continue;
                    }

                    int sumHandCount = this._dicWaitingBuyInfos[buyPrice].Sum(stonesellOrder => stonesellOrder.BuyUnit.TradeStoneHandCount);
                    this._todayTradeInfo.BuyOrderPriceCountList[i].TradeStoneHandCount = sumHandCount;
                    if (sumHandCount <= 0)
                    {
                        ConcurrentQueue<StoneDelegateBuyOrderInfo> queueSellOrder = null;
                        this._dicWaitingBuyInfos.TryRemove(buyPrice, out queueSellOrder);
                        this._todayTradeInfo.BuyOrderPriceCountList.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }


            for (int i = 0; i < this._todayTradeInfo.SellOrderPriceCountList.Count; i++)
            {
                decimal sellPrice = this._todayTradeInfo.SellOrderPriceCountList[i].Price;
                if (!this._dicWaitingSellInfos.ContainsKey(sellPrice))
                {
                    this._todayTradeInfo.SellOrderPriceCountList.RemoveAt(i);
                    i--;
                    continue;
                }
                else
                {
                    if (this._dicWaitingSellInfos[sellPrice] == null || this._dicWaitingSellInfos[sellPrice].Count == 0)
                    {
                        ConcurrentQueue<StoneDelegateSellOrderInfo> queueSellOrder = null;
                        this._dicWaitingSellInfos.TryRemove(sellPrice, out queueSellOrder);
                        this._todayTradeInfo.SellOrderPriceCountList.RemoveAt(i);
                        i--;
                        continue;
                    }

                    int sumHandCount = this._dicWaitingSellInfos[sellPrice].Sum(stonesellOrder => stonesellOrder.SellUnit.TradeStoneHandCount);
                    this._todayTradeInfo.SellOrderPriceCountList[i].TradeStoneHandCount = sumHandCount;
                    if (sumHandCount <= 0)
                    {
                        ConcurrentQueue<StoneDelegateSellOrderInfo> queueSellOrder = null;
                        this._dicWaitingSellInfos.TryRemove(sellPrice, out queueSellOrder);
                        this._todayTradeInfo.SellOrderPriceCountList.RemoveAt(i);
                        i--;
                        continue;
                    }
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

            int FinishedStoneHandCount = 0;

            //循环取出买价对应的买单
            StoneDelegateBuyOrderInfo buyOrder = this._dicWaitingBuyInfos[buyPrice].Dequeue();
            while (buyOrder != null)
            {
                if (buyOrder.BuyState == StoneDelegateBuyState.Waiting)
                {
                    var result = TradeOneBuyOrder(buyOrder);
                    if (result.State == StackTradeState.Failed)
                    {
                        //没有找到价格合适的卖单，把买单再加回集合中.
                        this._dicWaitingBuyInfos[buyPrice].Enqueue(buyOrder);
                    }
                    else if (result.State == StackTradeState.Succeed)
                    {
                        FinishedStoneHandCount += buyOrder.FinishedStoneTradeHandCount;
                    }
                    else if (result.State == StackTradeState.Splited)
                    {
                        //此时，卖1订单已经全部被吞掉。而当前买单没有消化完，需要将剩余部分拆分，拆分后的订单不再另存数据库，直接添加到集合中
                        StoneDelegateBuyOrderInfo newBuyOrder = new StoneDelegateBuyOrderInfo()
                        {
                            UserID = buyOrder.UserID,
                            IsSubOrder = true,
                            BuyState = StoneDelegateBuyState.Waiting,
                            OrderNumber = buyOrder.OrderNumber,
                            DelegateTime = buyOrder.DelegateTime,
                            BuyUnit = new StackTradeUnit()
                            {
                                Price = buyOrder.BuyUnit.Price,
                                TradeStoneHandCount = buyOrder.BuyUnit.TradeStoneHandCount - buyOrder.FinishedStoneTradeHandCount
                            },
                        };

                        this._dicWaitingBuyInfos[buyPrice].Enqueue(newBuyOrder);
                        FinishedStoneHandCount += buyOrder.FinishedStoneTradeHandCount;
                    }
                }

                buyOrder = this._dicWaitingBuyInfos[buyPrice].Dequeue();
            }

            var operResult = this._todayTradeInfo.DecreaseBuyUnit(buyPrice, FinishedStoneHandCount);
            if (operResult.OperResultCode != OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddErrorLog("TradeBuy1Orders DecreaseBuyUnit Error. " + operResult.Message, null);
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

            CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
            try
            {
                if (listTradeSucceedSellOrders.Count > 0)
                {
                    var lastSellOrder = listTradeSucceedSellOrders[listTradeSucceedSellOrders.Count - 1];

                    if (SaveStoneSellOrderHandler != null)
                    {
                        SaveStoneSellOrderHandler(listTradeSucceedSellOrders, myTrans);
                    }
                }

                if (SaveStoneBuyOrderHandler != null)
                {
                    SaveStoneBuyOrderHandler(buyOrder, myTrans);
                }

                myTrans.Commit();
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
                buyOrderResult = TradeOneSellOrder(sellUnit.Price, this._dicWaitingSellInfos[sellUnit.Price], listTradeSucceedSellOrders, buyUnit);
                if (buyOrderResult.State == StackTradeState.Failed)
                {
                    continue;
                }
                if (buyOrderResult.State == StackTradeState.Succeed)
                {
                    //买单 被全部消化
                    //暂时不清理卖单集合，被拆分的卖单，剩余部分在外部会重新加回卖单集合
                    buyOrder.BuyState = StoneDelegateBuyState.Succeed;
                    buyOrder.FinishedStoneTradeHandCount = buyOrder.BuyUnit.TradeStoneHandCount;
                    buyOrder.FinishedTime = new MyDateTime(DateTime.Now);
                    break;
                }
                if (buyOrderResult.State == StackTradeState.Splited)
                {
                    //买单没有被全部消化，需要再对比处理上一级卖价。等所有卖价都对比完时，再返回
                    buyUnit.TradeStoneHandCount -= buyOrderResult.SucceedStoneHandCount;
                    buyOrder.BuyState = StoneDelegateBuyState.Splited;
                    buyOrder.FinishedStoneTradeHandCount = buyOrder.FinishedStoneTradeHandCount - buyUnit.TradeStoneHandCount;
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
        private StackTradeResult TradeOneSellOrder(decimal sellPrice, ListStoneDelegateSellOrderInfoCollection queueSellOrders,
            List<StoneDelegateSellOrderInfo> listTradeSucceedSellOrders, StackTradeUnit buyUnit)
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

                var nextSellOrder = GetWaitingSellOrderFromQueue(queueSellOrders);
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

            }
            //卖单量大于买单量，需要将当前卖单拆分
            else
            {
                sellOrder.SellState = StoneDelegateSellState.Splited;
                sellOrder.FinishedStoneTradeHandCount = sellOrder.SellUnit.TradeStoneHandCount + surplusStoneHandCount;
                sellOrder.FinishedTime = new MyDateTime(DateTime.Now);
                listTradeSucceedSellOrders.Add(sellOrder);

                //拆分后的订单，不再另存数据库，直接添加到集合中。
                StoneDelegateSellOrderInfo newSellOrder = new StoneDelegateSellOrderInfo()
                {
                    OrderNumber = sellOrder.OrderNumber,
                    UserID = sellOrder.UserID,
                    SellState = StoneDelegateSellState.Waiting,
                    DelegateTime = new MyDateTime(DateTime.Now),
                    SellUnit = new StackTradeUnit()
                    {
                        Price = sellOrder.SellUnit.Price,
                        TradeStoneHandCount = sellOrder.SellUnit.TradeStoneHandCount - sellOrder.FinishedStoneTradeHandCount
                    },
                    IsSubOrder = true
                };
                queueSellOrders.Enqueue(newSellOrder);
            }

            //买单已全部被消化
            buyOrderResult.State = StackTradeState.Succeed;
            buyOrderResult.SucceedStoneHandCount = buyUnit.TradeStoneHandCount;
            return buyOrderResult;
        }

        private StoneDelegateSellOrderInfo GetWaitingSellOrderFromQueue(ConcurrentQueue<StoneDelegateSellOrderInfo> queueSellOrders)
        {
            StoneDelegateSellOrderInfo sellOrder = null;
            while (queueSellOrders.Count > 0)
            {
                queueSellOrders.TryDequeue(out sellOrder);
                if (sellOrder != null && sellOrder.SellState == StoneDelegateSellState.Waiting)
                {
                    break;
                }
            }

            return sellOrder;
        }

        #endregion

        public event SaveStoneDelegateSellOrderInvoke SaveStoneSellOrderHandler;
        public event SaveStoneDelegateBuyOrderInvoke SaveStoneBuyOrderHandler;


    }

    public delegate void SaveStoneDelegateSellOrderInvoke(List<StoneDelegateSellOrderInfo> listSellOrders, CustomerMySqlTransaction myTrans);
    public delegate void SaveStoneDelegateBuyOrderInvoke(StoneDelegateBuyOrderInfo buyOrder, CustomerMySqlTransaction myTrans);
}
