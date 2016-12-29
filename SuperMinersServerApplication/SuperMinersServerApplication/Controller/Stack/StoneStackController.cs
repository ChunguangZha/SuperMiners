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


    class ListStoneDelegateSellOrderInfoCollection
    {
        private ConcurrentQueue<StoneDelegateSellOrderInfo> QueueOrders = new ConcurrentQueue<StoneDelegateSellOrderInfo>();

        public void Insert(StoneDelegateSellOrderInfo sellOrder)
        {
            QueueOrders.Enqueue(sellOrder);
        }
    }


    public class StoneStackController
    {
        private bool _marketIsOpened = false;

        private TodayStoneStackTradeRecordInfo _todayTradeInfo = new TodayStoneStackTradeRecordInfo();
        private object _lockTodayInfo = new object();

        ConcurrentDictionary<decimal, ConcurrentQueue<StoneDelegateSellOrderInfo>> _dicWaitingSellInfos = new ConcurrentDictionary<decimal, ConcurrentQueue<StoneDelegateSellOrderInfo>>();

        ConcurrentDictionary<decimal, ConcurrentQueue<StoneDelegateBuyOrderInfo>> _dicWaitingBuyInfos = new ConcurrentDictionary<decimal, ConcurrentQueue<StoneDelegateBuyOrderInfo>>();

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
            _marketIsOpened = true;
            InitTodayDailyInfo();
        }

        /// <summary>
        /// 暂停休市
        /// </summary>
        private void MarketSuspend()
        {
            if (!_marketIsOpened)
            {
                return;
            }

            _marketIsOpened = false;

        }

        /// <summary>
        /// 恢复继续开市
        /// </summary>
        private void MarketResume()
        {
            if (!_marketIsOpened)
            {
                return;
            }

            _marketIsOpened = true;

        }

        private void MarketClose()
        {
            if (!_marketIsOpened)
            {
                return;
            }

            _marketIsOpened = false;


        }

        private void LoadDataFromDatabase()
        {
            var buyOrderList = DBProvider.StoneStackDBProvider.GetAllWaitingStoneDelegateBuyOrderInfo();
            if (buyOrderList != null)
            {
                foreach (var item in buyOrderList)
                {
                    InsertToBuy(item, false);
                }
            }

            var sellOrderList = DBProvider.StoneStackDBProvider.GetAllWaitingStoneDelegateSellOrderInfo();
            if (sellOrderList != null)
            {
                foreach (var item in sellOrderList)
                {
                    InsertToSell(item, false);
                }
            }
        }

        private void InitTodayDailyInfo()
        {
            var lastDailyInfo = DBProvider.StoneStackDBProvider.GetLastStoneStackDailyRecordInfo();
            if (lastDailyInfo == null)
            {
                this._todayTradeInfo.DailyInfo = new StoneStackDailyRecordInfo()
                {
                    Day = new MetaData.MyDateTime(DateTime.Now),
                    //初始等于矿石原价
                    OpenPrice = 1 / GlobalConfig.GameConfig.Stones_RMB * 1000
                };
            }
            else
            {
                DateTime nowTime = DateTime.Now;
                if (lastDailyInfo.Day.Year != nowTime.Year || lastDailyInfo.Day.Month != nowTime.Month || lastDailyInfo.Day.Day != nowTime.Day)
                {
                    //又开始新一天
                    this._todayTradeInfo.DailyInfo = new StoneStackDailyRecordInfo()
                    {
                        Day = new MetaData.MyDateTime(nowTime),
                        OpenPrice = lastDailyInfo.ClosePrice,
                    };
                }
                else
                {
                    this._todayTradeInfo.DailyInfo = lastDailyInfo;
                }
            }

        }

        private void InsertToBuy(StoneDelegateBuyOrderInfo item, bool saveToDB)
        {
            bool isInsert = true;
            int index = CheckinBuyListIndex(item, out isInsert);
            if (index >= 0 && index <= 10)
            {
                UpdateToTodayBuyList(index, isInsert, item);
            }

            ConcurrentQueue<StoneDelegateBuyOrderInfo> queueBuy = null;
            if (_dicWaitingBuyInfos.ContainsKey(item.BuyUnit.Price))
            {
                queueBuy = _dicWaitingBuyInfos[item.BuyUnit.Price];
            }
            else
            {
                queueBuy = new ConcurrentQueue<StoneDelegateBuyOrderInfo>();
                _dicWaitingBuyInfos[item.BuyUnit.Price] = queueBuy;
            }

            queueBuy.Enqueue(item);

            if (saveToDB)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateBuyOrderInfo(item);
            }
        }

        private bool UpdateToTodayBuyList(int index, bool IsInsert, StoneDelegateBuyOrderInfo item)
        {
            lock (_lockTodayInfo)
            {
                this._todayTradeInfo.DailyInfo.DelegateBuyStoneSum += item.BuyUnit.TradeStoneHandCount;

                if (IsInsert)
                {
                    //index以后的项全部后移一位。
                    for (int i = _todayTradeInfo.BuyOrderList.Length - 2; i >= index; i++)
                    {
                        _todayTradeInfo.BuyOrderList[i + 1] = _todayTradeInfo.BuyOrderList[i];
                    }

                    _todayTradeInfo.BuyOrderList[index] = new StackTradeUnit()
                    {
                        Price = item.BuyUnit.Price,
                        TradeStoneHandCount = item.BuyUnit.TradeStoneHandCount
                    };
                }
                else
                {
                    if (_todayTradeInfo.BuyOrderList[index] == null)
                    {
                        _todayTradeInfo.BuyOrderList[index] = new StackTradeUnit()
                        {
                            Price = item.BuyUnit.Price,
                            TradeStoneHandCount = item.BuyUnit.TradeStoneHandCount
                        };
                    }
                    else
                    {
                        _todayTradeInfo.BuyOrderList[index].TradeStoneHandCount += item.BuyUnit.TradeStoneHandCount;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="IsInsert">false表示Update</param>
        /// <returns></returns>
        private int CheckinBuyListIndex(StoneDelegateBuyOrderInfo item, out bool IsInsert)
        {
            int index = -1;
            IsInsert = true;

            for (int i = 0; i < _todayTradeInfo.BuyOrderList.Length; i++)
            {
                if (_todayTradeInfo.BuyOrderList[i] == null)
                {
                    IsInsert = false;
                    index = i;
                    break;
                }
                //从高到低， 排序
                if (item.BuyUnit.Price > _todayTradeInfo.BuyOrderList[i].Price)
                {
                    IsInsert = true;
                    if (i == 0)
                    {
                        index = i;
                        break;
                    }
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }
                else if (item.BuyUnit.Price == _todayTradeInfo.BuyOrderList[i].Price)
                {
                    IsInsert = false;
                    index = i;
                    break;
                }
            }

            return index;
        }

        private void InsertToSell(StoneDelegateSellOrderInfo item, bool saveToDB)
        {
            bool isInsert = true;
            int index = CheckinSellListIndex(item, out isInsert);
            if (index >= 0 && index <= 10)
            {
                UpdateToTodaySellList(index, isInsert, item);
            }

            ConcurrentQueue<StoneDelegateSellOrderInfo> queueSell = null;
            if (_dicWaitingSellInfos.ContainsKey(item.SellUnit.Price))
            {
                queueSell = _dicWaitingSellInfos[item.SellUnit.Price];
            }
            else
            {
                queueSell = new ConcurrentQueue<StoneDelegateSellOrderInfo>();
                _dicWaitingSellInfos[item.SellUnit.Price] = queueSell;
            }

            queueSell.Enqueue(item);

            if (saveToDB)
            {
                DBProvider.StoneStackDBProvider.SaveWaitingStoneDelegateSellOrderInfo(item);
            }
        }

        private bool UpdateToTodaySellList(int index, bool IsInsert, StoneDelegateSellOrderInfo item)
        {
            lock (_lockTodayInfo)
            {
                this._todayTradeInfo.DailyInfo.DelegateSellStoneSum += item.SellUnit.TradeStoneHandCount;

                if (IsInsert)
                {
                    //index以后的项全部后移一位。
                    for (int i = _todayTradeInfo.SellOrderList.Length - 2; i >= index; i++)
                    {
                        _todayTradeInfo.SellOrderList[i + 1] = _todayTradeInfo.SellOrderList[i];
                    }

                    _todayTradeInfo.SellOrderList[index] = new StackTradeUnit()
                    {
                        Price = item.SellUnit.Price,
                        TradeStoneHandCount = item.SellUnit.TradeStoneHandCount
                    };
                }
                else
                {
                    if (_todayTradeInfo.SellOrderList[index] == null)
                    {
                        _todayTradeInfo.SellOrderList[index] = new StackTradeUnit()
                        {
                            Price = item.SellUnit.Price,
                            TradeStoneHandCount = item.SellUnit.TradeStoneHandCount
                        };
                    }
                    else
                    {
                        _todayTradeInfo.SellOrderList[index].TradeStoneHandCount += item.SellUnit.TradeStoneHandCount;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="IsInsert">false表示Update</param>
        /// <returns></returns>
        private int CheckinSellListIndex(StoneDelegateSellOrderInfo item, out bool IsInsert)
        {
            int index = -1;
            IsInsert = true;

            for (int i = 0; i < _todayTradeInfo.SellOrderList.Length; i++)
            {
                if (_todayTradeInfo.SellOrderList[i] == null)
                {
                    IsInsert = false;
                    index = i;
                    break;
                }
                //从低到高， 排序
                if (item.SellUnit.Price < _todayTradeInfo.SellOrderList[i].Price)
                {
                    IsInsert = true;
                    if (i == 0)
                    {
                        index = i;
                        break;
                    }
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }
                else if (item.SellUnit.Price == _todayTradeInfo.SellOrderList[i].Price)
                {
                    IsInsert = false;
                    index = i;
                    break;
                }
            }

            return index;
        }

        #endregion

        public OperResultObject PlayerDelegateSellStone(StoneDelegateSellOrderInfo sellOrder)
        {
            InsertToSell(sellOrder, true);
            return new OperResultObject()
            {
                OperResultCode = OperResult.RESULTCODE_TRUE
            };
        }

        public OperResultObject PlayerWithdrawSellStone(StoneDelegateSellOrderInfo sellOrder)
        {
            return null;
        }

        public OperResultObject PlayerDelegateBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            InsertToBuy(buyOrder, true);
            return new OperResultObject()
            {
                 OperResultCode = OperResult.RESULTCODE_TRUE
            };
        }

        public OperResultObject PlayerWithdrawBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            return null;
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
                    if (!this._marketIsOpened)
                    {
                        continue;
                    }

                    if (this._dicWaitingBuyInfos.Count == 0 || this._dicWaitingSellInfos.Count == 0)
                    {
                        continue;
                    }

                    //从买1 遍历到 买10 ， 进行处理。从卖1 到 卖10 遍历取卖单。
                    lock (_lockTodayInfo)
                    {
                        //循环买价
                        foreach (var buyUnit in this._todayTradeInfo.BuyOrderList)
                        {
                            if (buyUnit == null)
                            {
                                continue;
                            }
                            if (buyUnit.TradeStoneHandCount == 0 || !this._dicWaitingBuyInfos.ContainsKey(buyUnit.Price) || this._dicWaitingBuyInfos[buyUnit.Price] == null || this._dicWaitingBuyInfos[buyUnit.Price].Count == 0)
                            {
                                continue;
                            }

                            decimal buyPrice = buyUnit.Price;
                            //循环卖价
                            foreach (var sellUnit in this._todayTradeInfo.SellOrderList)
                            {
                                if (sellUnit == null)
                                {
                                    continue;
                                }
                                if (sellUnit.TradeStoneHandCount == 0 || !this._dicWaitingSellInfos.ContainsKey(sellUnit.Price) || this._dicWaitingSellInfos[sellUnit.Price] == null || this._dicWaitingSellInfos[sellUnit.Price].Count == 0)
                                {
                                    continue;
                                }

                                if (buyPrice >= sellUnit.Price)
                                {
                                    //成交，按量处理

                                    List<StoneDelegateSellOrderInfo> listTradeSucceedSellOrders = new List<StoneDelegateSellOrderInfo>();
                                    List<StoneDelegateBuyOrderInfo> listTradeSucceedBuyOrders = new List<StoneDelegateBuyOrderInfo>();

                                    if (buyPrice < _todayTradeInfo.DailyInfo.MinTradeSucceedPrice)
                                    {
                                        _todayTradeInfo.DailyInfo.MinTradeSucceedPrice = buyPrice;
                                    }
                                    if (_todayTradeInfo.DailyInfo.MaxTradeSucceedPrice < buyPrice)
                                    {
                                        _todayTradeInfo.DailyInfo.MaxTradeSucceedPrice = buyPrice;
                                    }

                                    StoneDelegateBuyOrderInfo buyOrder = null;
                                    do
                                    {
                                        this._dicWaitingBuyInfos[buyPrice].TryDequeue(out buyOrder);
                                    }
                                    while (buyOrder != null);

                                    StoneDelegateSellOrderInfo sellOrder = null;
                                    do
                                    {
                                        this._dicWaitingSellInfos[sellUnit.Price].TryDequeue(out sellOrder);
                                    }
                                    while (sellOrder != null);

                                    //买单量大于卖单量
                                    if (buyOrder.BuyUnit.TradeStoneHandCount >= sellOrder.SellUnit.TradeStoneHandCount)
                                    {
                                        int surplusStoneHandCount = buyOrder.BuyUnit.TradeStoneHandCount - sellOrder.SellUnit.TradeStoneHandCount;
                                        listTradeSucceedSellOrders.Add(sellOrder);

                                        while (surplusStoneHandCount > 0)
                                        {

                                        }
                                    }
                                    else//卖单量大于买单量
                                    {

                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ThreadStoneStackTrade Exception", exc);
                }
            }
        }

        #endregion

    }
}
