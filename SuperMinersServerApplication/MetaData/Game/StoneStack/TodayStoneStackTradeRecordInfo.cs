using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    /// <summary>
    /// 向客户端展示当天交易信息
    /// </summary>
    [DataContract]
    public class TodayStoneStackTradeRecordInfo
    {
        private object _lock = new object();

        [DataMember]
        public StackMarketState MarketState = StackMarketState.Closed;


        [DataMember]
        public StoneStackDailyRecordInfo DailyInfo = new StoneStackDailyRecordInfo();

        /// <summary>
        /// 卖1到卖10的信息(价格从低到高排序)，服务器上保存所有，客户端去显示前10项
        /// </summary>
        private List<StackTradeUnit> SellOrderPriceCountList = new List<StackTradeUnit>();

        [DataMember]
        public StackTradeUnit[] Top5SellOrderList
        {
            get
            {
                StackTradeUnit[] list = SellOrderPriceCountList.ToArray();
                if (list.Length > 5)
                {
                    StackTradeUnit[] list5 = new StackTradeUnit[5];
                    for (int i = 0; i < 5; i++)
                    {
                        list5[i] = list[i];
                    }
                    return list5;
                }


                return list;
            }
            set
            {
                this.SellOrderPriceCountList = new List<StackTradeUnit>(value);
            }
        }

        /// <summary>
        /// 买一到买10的信息(价格从高到低排序)，服务器上保存所有，客户端去显示前10项
        /// </summary>
        private List<StackTradeUnit> BuyOrderPriceCountList = new List<StackTradeUnit>();

        [DataMember]
        public StackTradeUnit[] Top5BuyOrderList
        {
            get
            {
                StackTradeUnit[] list = BuyOrderPriceCountList.ToArray();
                if (list.Length > 5)
                {
                    StackTradeUnit[] list5 = new StackTradeUnit[5];
                    for (int i = 0; i < 5; i++)
                    {
                        list5[i] = list[i];
                    }
                    return list5;
                }


                return list;
            }
            set
            {
                this.BuyOrderPriceCountList = new List<StackTradeUnit>(value);
            }
        }

        #region DailyInfo

        public void InitTodayDailyInfo(StoneStackDailyRecordInfo lastDailyInfo, decimal initPrice)
        {
            if (lastDailyInfo == null)
            {
                this.DailyInfo = new StoneStackDailyRecordInfo()
                {
                    Day = new MetaData.MyDateTime(DateTime.Now),
                    //初始等于矿石原价
                    OpenPrice = initPrice,
                    ClosePrice = initPrice,
                    MaxTradeSucceedPrice = initPrice,
                    MinTradeSucceedPrice = initPrice,
                };
            }
            else
            {
                DateTime nowTime = DateTime.Now;
                if (lastDailyInfo.Day.Year != nowTime.Year || lastDailyInfo.Day.Month != nowTime.Month || lastDailyInfo.Day.Day != nowTime.Day)
                {
                    //又开始新一天
                    this.DailyInfo = new StoneStackDailyRecordInfo()
                    {
                        Day = new MetaData.MyDateTime(nowTime),
                        OpenPrice = lastDailyInfo.ClosePrice,
                        ClosePrice = lastDailyInfo.ClosePrice,
                    };
                    this.DailyInfo.MaxTradeSucceedPrice = this.DailyInfo.LimitDownPrice;
                    this.DailyInfo.MinTradeSucceedPrice = this.DailyInfo.LimitUpPrice;
                }
                else
                {
                    this.DailyInfo = lastDailyInfo;
                }
            }

        }
        
        #endregion

        #region Sell List

        public OperResultObject DeleteSellUnit(StackTradeUnit sellUnit)
        {
            OperResultObject result = new OperResultObject();
            lock (_lock)
            {
                for (int i = 0; i < this.SellOrderPriceCountList.Count; i++)
                {
                    var priceUnit = this.SellOrderPriceCountList[i];
                    if (priceUnit != null)
                    {
                        if (priceUnit.Price == sellUnit.Price)
                        {
                            if (priceUnit.TradeStoneHandCount < sellUnit.TradeStoneHandCount)
                            {
                                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_TOTALHANDCOUNTERROR;
                            }
                            else
                            {
                                priceUnit.TradeStoneHandCount -= sellUnit.TradeStoneHandCount;
                                result.OperResultCode = OperResult.RESULTCODE_TRUE;
                            }

                            if (priceUnit.TradeStoneHandCount == 0)
                            {
                                this.SellOrderPriceCountList.RemoveAt(i);
                            }
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public int InsertSellUnit(StackTradeUnit sellUnit)
        {
            bool isInsert = false;
            int index = CheckinSellListIndex(sellUnit.Price, out isInsert);
            if (index >= 0)
            {
                UpdateToTodaySellList(index, isInsert, sellUnit);
            }

            return index;
        }

        private bool UpdateToTodaySellList(int index, bool IsInsert, StackTradeUnit sellUnit)
        {
            lock (_lock)
            {
                this.DailyInfo.DelegateSellStoneSum += sellUnit.TradeStoneHandCount;

                if (IsInsert)
                {
                    SellOrderPriceCountList.Insert(index, new StackTradeUnit()
                    {
                        Price = sellUnit.Price,
                        TradeStoneHandCount = sellUnit.TradeStoneHandCount
                    });
                }
                else
                {
                    if (SellOrderPriceCountList[index] == null)
                    {
                        SellOrderPriceCountList[index] = new StackTradeUnit()
                        {
                            Price = sellUnit.Price,
                            TradeStoneHandCount = sellUnit.TradeStoneHandCount
                        };
                    }
                    else
                    {
                        SellOrderPriceCountList[index].TradeStoneHandCount += sellUnit.TradeStoneHandCount;
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
        private int CheckinSellListIndex(decimal sellPrice, out bool IsInsert)
        {
            int index = 0;
            IsInsert = true;

            for (; index < SellOrderPriceCountList.Count; index++)
            {
                if (SellOrderPriceCountList[index] == null)
                {
                    IsInsert = false;
                    break;
                }
                //从低到高， 排序
                if (sellPrice < SellOrderPriceCountList[index].Price)
                {
                    IsInsert = true;
                    break;
                }
                else if (sellPrice == SellOrderPriceCountList[index].Price)
                {
                    IsInsert = false;
                    break;
                }
            }

            return index;
        }

        public StackTradeUnit GetSell1Unit()
        {
            lock (_lock)
            {
                while (this.SellOrderPriceCountList.Count > 0)
                {
                    if (SellOrderPriceCountList[0].TradeStoneHandCount <= 0)
                    {
                        this.SellOrderPriceCountList.RemoveAt(0);
                    }
                    else
                    {
                        return SellOrderPriceCountList[0];
                    }
                }

                return null;
            }
        }

        public OperResultObject DecreaseSellUnit(decimal price, int finishedStoneHandCount)
        {
            OperResultObject result = new OperResultObject();

            lock (_lock)
            {
                for (int i = 0; i < SellOrderPriceCountList.Count; i++)
                {
                    var unit = SellOrderPriceCountList[i];
                    if (unit.Price == price)
                    {
                        result.OperResultCode = OperResult.RESULTCODE_TRUE;

                        unit.TradeStoneHandCount -= finishedStoneHandCount;
                        if (unit.TradeStoneHandCount < 0)
                        {
                            result.OperResultCode = OperResult.RESULTCODE_FALSE;
                            result.Message = "DecreasetBuyUnit unit.TradeStoneHandCount < 0. Price: " + price + ",  unit.TradeStoneHandCount: " + unit.TradeStoneHandCount;
                            unit.TradeStoneHandCount = 0;
                        }
                        if (unit.TradeStoneHandCount == 0)
                        {
                            SellOrderPriceCountList.RemoveAt(i);
                        }
                        break;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Buy List

        public OperResultObject DeleteBuyUnit(StackTradeUnit buyUnit)
        {
            OperResultObject result = new OperResultObject();
            lock (_lock)
            {
                for (int i = 0; i < this.BuyOrderPriceCountList.Count; i++)
                {
                    var priceUnit = this.BuyOrderPriceCountList[i];
                    if (priceUnit != null)
                    {
                        if (priceUnit.Price == buyUnit.Price)
                        {
                            if (priceUnit.TradeStoneHandCount < buyUnit.TradeStoneHandCount)
                            {
                                result.OperResultCode = OperResult.RESULTCODE_STACK_CANCELORDER_FAILED_TOTALHANDCOUNTERROR;
                            }
                            else
                            {
                                priceUnit.TradeStoneHandCount -= buyUnit.TradeStoneHandCount;
                                result.OperResultCode = OperResult.RESULTCODE_TRUE;
                            }

                            if (priceUnit.TradeStoneHandCount == 0)
                            {
                                this.BuyOrderPriceCountList.RemoveAt(i);
                            }
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public int InsertBuyUnit(StackTradeUnit buyUnit)
        {
            bool isInsert = false;
            int index = CheckinBuyListIndex(buyUnit.Price, out isInsert);
            if (index >= 0)
            {
                UpdateToTodayBuyList(index, isInsert, buyUnit);
            }

            return index;
        }

        private bool UpdateToTodayBuyList(int index, bool IsInsert, StackTradeUnit buyUnit)
        {
            lock (_lock)
            {
                this.DailyInfo.DelegateBuyStoneSum += buyUnit.TradeStoneHandCount;

                if (IsInsert)
                {
                    BuyOrderPriceCountList.Insert(index, new StackTradeUnit()
                    {
                        Price = buyUnit.Price,
                        TradeStoneHandCount = buyUnit.TradeStoneHandCount
                    });
                }
                else
                {
                    if (BuyOrderPriceCountList[index] == null)
                    {
                        BuyOrderPriceCountList[index] = new StackTradeUnit()
                        {
                            Price = buyUnit.Price,
                            TradeStoneHandCount = buyUnit.TradeStoneHandCount
                        };
                    }
                    else
                    {
                        BuyOrderPriceCountList[index].TradeStoneHandCount += buyUnit.TradeStoneHandCount;
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
        private int CheckinBuyListIndex(decimal buyPrice, out bool IsInsert)
        {
            //买价： 从高到低
            int index = 0;
            IsInsert = true;

            for (; index < BuyOrderPriceCountList.Count; index++)
            {
                if (BuyOrderPriceCountList[index] == null)
                {
                    IsInsert = false;
                    break;
                }

                if (buyPrice > BuyOrderPriceCountList[index].Price)
                {
                    IsInsert = true;
                    break;
                    //if (i == 0)
                    //{
                    //    index = i;
                    //    break;
                    //}
                    //else
                    //{
                    //    index = i - 1;
                    //    break;
                    //}
                }
                else if (buyPrice == BuyOrderPriceCountList[index].Price)
                {
                    IsInsert = false;
                    break;
                }
            }

            return index;
        }

        public StackTradeUnit GetBuy1Unit()
        {
            lock (_lock)
            {
                while (this.BuyOrderPriceCountList.Count > 0)
                {
                    if (BuyOrderPriceCountList[0].TradeStoneHandCount <= 0)
                    {
                        this.BuyOrderPriceCountList.RemoveAt(0);
                    }
                    else
                    {
                        return BuyOrderPriceCountList[0];
                    }
                }
                
                return null;
            }
        }

        public decimal ComputeTradePrice(decimal buyPrice, decimal sellPrice, int handCount)
        {
            lock (_lock)
            {
                decimal tradePrice = buyPrice;

                bool buyPriceLargethenSell2Price = false;
                bool sellPriceSmallthenBuy2Price = false;
                if (this.SellOrderPriceCountList.Count > 1)
                {
                    //买价大于卖2价，则按卖1价计成交价。
                    if (buyPrice >= this.SellOrderPriceCountList[1].Price)
                    {
                        tradePrice = sellPrice;
                        buyPriceLargethenSell2Price = true;
                    }
                }
                if (this.BuyOrderPriceCountList.Count > 1)
                {
                    //卖价大于买2价，则按买1价计成交价。
                    if (sellPrice <= this.BuyOrderPriceCountList[1].Price)
                    {
                        tradePrice = buyPrice;
                        sellPriceSmallthenBuy2Price = true;
                    }
                }
                if (buyPriceLargethenSell2Price && sellPriceSmallthenBuy2Price)
                {
                    //则取买2价为成交价。
                    tradePrice = this.BuyOrderPriceCountList[1].Price;
                }

                this.DailyInfo.ClosePrice = tradePrice;
                if (tradePrice < this.DailyInfo.MinTradeSucceedPrice)
                {
                    this.DailyInfo.MinTradeSucceedPrice = tradePrice;
                }
                if (tradePrice > this.DailyInfo.MaxTradeSucceedPrice)
                {
                    this.DailyInfo.MaxTradeSucceedPrice = tradePrice;
                }

                this.DailyInfo.TradeSucceedStoneHandSum += handCount;

                //成交金额，还是按买价算，表示玩家实际支付金额。
                this.DailyInfo.TradeSucceedRMBSum += (handCount * buyPrice);
                return tradePrice;
            }
        }

        public OperResultObject DecreaseBuyUnit(decimal price, int finishedStoneHandCount)
        {
            OperResultObject result = new OperResultObject();

            lock (_lock)
            {
                for (int i = 0; i < BuyOrderPriceCountList.Count; i++)
                {
                    var unit = BuyOrderPriceCountList[i];
                    if (unit.Price == price)
                    {
                        result.OperResultCode = OperResult.RESULTCODE_TRUE;

                        unit.TradeStoneHandCount -= finishedStoneHandCount;
                        if (unit.TradeStoneHandCount < 0)
                        {
                            result.OperResultCode = OperResult.RESULTCODE_FALSE;
                            result.Message = "DecreasetBuyUnit unit.TradeStoneHandCount < 0. Price: " + price + ",  unit.TradeStoneHandCount: " + unit.TradeStoneHandCount;
                            unit.TradeStoneHandCount = 0;
                        }
                        if (unit.TradeStoneHandCount == 0)
                        {
                            BuyOrderPriceCountList.RemoveAt(i);
                        }
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }

    [DataContract]
    public class StackTradeUnit
    {
        /// <summary>
        /// 一手矿石的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// 交易的矿石手数。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public int TradeStoneHandCount { get; set; }
    }

    public enum StackMarketState
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


}
