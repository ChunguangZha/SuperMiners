using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperMinersWPF.Models
{
    public class TodayStoneStackTradeRecordInfoUIModel : BaseModel
    {
        public TodayStoneStackTradeRecordInfoUIModel(TodayStoneStackTradeRecordInfo parent)
        {
            _top5SellOrderList = new StackTradeUnitUIModel[5];
            for (int i = 0; i < _top5SellOrderList.Length; i++)
			{
                this._top5SellOrderList[4 - i] = new StackTradeUnitUIModel(i + 1, false, null);
			}

            _top5BuyOrderList = new StackTradeUnitUIModel[5];
            for (int i = 0; i < _top5BuyOrderList.Length; i++)
            {
                this._top5BuyOrderList[i] = new StackTradeUnitUIModel(i + 1, true, null);
            }

            this.ParentObject = parent;
        }

        private TodayStoneStackTradeRecordInfo _parentObject;

        public TodayStoneStackTradeRecordInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;

                SetBuyPriceLevels();
                SetSellPriceLevels();
                NotifyPropertyChange("Top5SellOrderList");
                NotifyPropertyChange("Top5BuyOrderList");
                NotifyPropertyChange("DayText");
                NotifyPropertyChange("OpenPrice");
                NotifyPropertyChange("ClosePrice");
                NotifyPropertyChange("ClosePriceColor");
                NotifyPropertyChange("RiseValue");
                NotifyPropertyChange("RisePercent");
                NotifyPropertyChange("RiseValueColor");
                NotifyPropertyChange("LimitUpPrice");
                NotifyPropertyChange("LimitDownPrice");
                NotifyPropertyChange("MinTradeSucceedPrice");
                NotifyPropertyChange("MinTradeSucceedPriceColor");
                NotifyPropertyChange("MaxTradeSucceedPrice");
                NotifyPropertyChange("MaxTradeSucceedPriceColor");
                NotifyPropertyChange("TradeSucceedStoneHandSum");
                NotifyPropertyChange("TradeSucceedRMBSum");
                NotifyPropertyChange("DelegateSellStoneSum");
                NotifyPropertyChange("DelegateBuyStoneSum");

                NotifyPropertyChange("Sell5Unit");
                NotifyPropertyChange("Sell4Unit");
                NotifyPropertyChange("Sell3Unit");
                NotifyPropertyChange("Sell2Unit");
                NotifyPropertyChange("Sell1Unit");
                NotifyPropertyChange("Buy1Unit");
                NotifyPropertyChange("Buy2Unit");
                NotifyPropertyChange("Buy3Unit");
                NotifyPropertyChange("Buy4Unit");
                NotifyPropertyChange("Buy5Unit");

            }
        }

        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _whiteBrush = new SolidColorBrush(Colors.White);

        public StackMarketState MarketState
        {
            get
            {
                if (this.ParentObject == null)
                {
                    return StackMarketState.Closed;
                }
                return this.ParentObject.MarketState;
            }
        }

        private StackTradeUnitUIModel[] _top5SellOrderList = null;

        public StackTradeUnitUIModel[] Top5SellOrderList
        {
            get
            {
                return this._top5SellOrderList;
            }
        }

        private StackTradeUnitUIModel[] _top5BuyOrderList =null;

        public StackTradeUnitUIModel[] Top5BuyOrderList
        {
            get
            {
                return this._top5BuyOrderList;
            }
        }

        private void SetBuyPriceLevels()
        {
            if (ParentObject == null || ParentObject.Top5BuyOrderList == null)
            {
                return;
            }

            int index = 0;
            for (; index < ParentObject.Top5BuyOrderList.Length; index++)
            {
                var buyUnit = ParentObject.Top5BuyOrderList[index];
                if (this._top5BuyOrderList[index] == null)
                {
                    this._top5BuyOrderList[index] = new StackTradeUnitUIModel(index + 1, true, buyUnit);
                }
                else
                {
                    this._top5BuyOrderList[index].ParentObject = buyUnit;
                }
            }

            for (; index < 5; index++)
            {
                if (this._top5BuyOrderList[index] == null)
                {
                    this._top5BuyOrderList[index] = new StackTradeUnitUIModel(index + 1, true, null);
                }
                else
                {
                    this._top5BuyOrderList[index].ParentObject = null;
                }
            }
        }

        private void SetSellPriceLevels()
        {
            if (ParentObject == null || ParentObject.Top5SellOrderList == null)
            {
                return;
            }

            int index = 0;
            for (; index < ParentObject.Top5SellOrderList.Length; index++)
            {
                var sellUnit = ParentObject.Top5SellOrderList[index];
                if (this._top5SellOrderList[4 - index] == null)
                {
                    this._top5SellOrderList[4 - index] = new StackTradeUnitUIModel(index + 1, false, sellUnit);
                }
                else
                {
                    this._top5SellOrderList[4 - index].ParentObject = sellUnit;
                }
            }

            for (; index < 5; index++)
            {
                if (this._top5SellOrderList[4 - index] == null)
                {
                    this._top5SellOrderList[4 - index] = new StackTradeUnitUIModel(index + 1, false, null);
                }
                else
                {
                    this._top5SellOrderList[4 - index].ParentObject = null;
                }
            }
        }
        
        public string DayText
        {
            get
            {
                if (this._parentObject == null)
                {
                    return "";
                }
                return this._parentObject.DailyInfo.Day.ToDateTime().ToLongDateString();
            }
        }

        public decimal OpenPrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.OpenPrice;
            }
        }

        public decimal ClosePrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.ClosePrice;
            }
        }

        public SolidColorBrush ClosePriceColor
        {
            get
            {
                if (ClosePrice == OpenPrice) return _whiteBrush;
                return ClosePrice > OpenPrice ? _redBrush : _greenBrush;
            }
        }

        public decimal RiseValue
        {
            get
            {
                return Math.Round(ClosePrice - OpenPrice, 2);
            }
        }

        public SolidColorBrush RiseValueColor
        {
            get
            {
                if (RiseValue == 0) return _whiteBrush;
                return RiseValue > 0 ? _redBrush : _greenBrush;
            }
        }

        public decimal RisePercent
        {
            get
            {
                if (OpenPrice == 0)
                {
                    return 0;
                }
                return Math.Round((ClosePrice - OpenPrice) / OpenPrice * 100m, 2);
            }
        }

        public decimal LimitUpPrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.LimitUpPrice;
            }
        }

        public decimal LimitDownPrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.LimitDownPrice;
            }
        }

        public decimal MinTradeSucceedPrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                if (this._parentObject.DailyInfo.MinTradeSucceedPrice > this.MaxTradeSucceedPrice)
                {
                    return this.MaxTradeSucceedPrice;
                }
                return this._parentObject.DailyInfo.MinTradeSucceedPrice;
            }
        }

        public SolidColorBrush MinTradeSucceedPriceColor
        {
            get
            {
                if (this.MinTradeSucceedPrice == this.OpenPrice) return _whiteBrush;
                return this.MinTradeSucceedPrice > this.OpenPrice ? _redBrush : _greenBrush;
            }
        }

        public decimal MaxTradeSucceedPrice
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.MaxTradeSucceedPrice;
            }
        }

        public SolidColorBrush MaxTradeSucceedPriceColor
        {
            get
            {
                if (this.MaxTradeSucceedPrice == this.OpenPrice) return _whiteBrush;
                return this.MaxTradeSucceedPrice > this.OpenPrice ? _redBrush : _greenBrush;
            }
        }

        public int TradeSucceedStoneHandSum
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.TradeSucceedStoneHandSum;
            }
        }

        public decimal TradeSucceedRMBSum
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.TradeSucceedRMBSum;
            }
        }

        public int DelegateSellStoneSum
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.DelegateSellStoneSum;
            }
        }

        public int DelegateBuyStoneSum
        {
            get
            {
                if (this._parentObject == null)
                {
                    return 0;
                }
                return this._parentObject.DailyInfo.DelegateBuyStoneSum;
            }
        }


    }
}
