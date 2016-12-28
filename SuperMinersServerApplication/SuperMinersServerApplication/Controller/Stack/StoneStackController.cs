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
    public class StoneStackController
    {
        private bool _marketIsOpened = false;

        private TodayStoneStackTradeRecordInfo _todayTradeInfo = new TodayStoneStackTradeRecordInfo();
        private object _lockTodayInfo = new object();

        ConcurrentDictionary<decimal, ConcurrentBag<StoneDelegateSellOrderInfo>> _dicWaitingSellInfos = new ConcurrentDictionary<decimal, ConcurrentBag<StoneDelegateSellOrderInfo>>();

        ConcurrentDictionary<decimal, ConcurrentBag<StoneDelegateBuyOrderInfo>> _dicWaitingBuyInfos = new ConcurrentDictionary<decimal, ConcurrentBag<StoneDelegateBuyOrderInfo>>();

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

            ConcurrentBag<StoneDelegateBuyOrderInfo> bagBuy = null;
            if (_dicWaitingBuyInfos.ContainsKey(item.BuyUnit.Price))
            {
                bagBuy = _dicWaitingBuyInfos[item.BuyUnit.Price];
            }
            else
            {
                bagBuy = new ConcurrentBag<StoneDelegateBuyOrderInfo>();
                _dicWaitingBuyInfos[item.BuyUnit.Price] = bagBuy;
            }

            bagBuy.Add(item);

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

            ConcurrentBag<StoneDelegateSellOrderInfo> bagSell = null;
            if (_dicWaitingSellInfos.ContainsKey(item.SellUnit.Price))
            {
                bagSell = _dicWaitingSellInfos[item.SellUnit.Price];
            }
            else
            {
                bagSell = new ConcurrentBag<StoneDelegateSellOrderInfo>();
                _dicWaitingSellInfos[item.SellUnit.Price] = bagSell;
            }

            bagSell.Add(item);

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
            return null;
        }

        public OperResultObject PlayerWithdrawSellStone(StoneDelegateSellOrderInfo sellOrder)
        {
            return null;
        }

        public OperResultObject PlayerWithdrawBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            return null;
        }

        public OperResultObject PlayerDelegateBuyStone(StoneDelegateBuyOrderInfo buyOrder)
        {
            return null;
        }

        #region Trade

        private void ThreadStoneStackTrade(object state)
        {
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    if (!this._marketIsOpened)
                    {
                        continue;
                    }

                    //从买1 遍历到 买10 ， 进行处理。从卖1 到 卖10 遍历取卖单。

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
