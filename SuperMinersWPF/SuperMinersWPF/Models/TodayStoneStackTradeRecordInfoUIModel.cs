using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                NotifyPropertyChange("LimitUpPrice");
                NotifyPropertyChange("LimitDownPrice");
                NotifyPropertyChange("MinTradeSucceedPrice");
                NotifyPropertyChange("MaxTradeSucceedPrice");
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

        //private StackTradeUnitUIModel _sell5Unit = new StackTradeUnitUIModel(5, false, null);

        //public StackTradeUnitUIModel Sell5Unit
        //{
        //    get { return _sell5Unit; }
        //}

        //private StackTradeUnitUIModel _sell4Unit = new StackTradeUnitUIModel(4, false, null);

        //public StackTradeUnitUIModel Sell4Unit
        //{
        //    get { return _sell4Unit; }
        //}

        //private StackTradeUnitUIModel _sell3Unit = new StackTradeUnitUIModel(3, false, null);

        //public StackTradeUnitUIModel Sell3Unit
        //{
        //    get { return _sell3Unit; }
        //}

        //private StackTradeUnitUIModel _sell2Unit = new StackTradeUnitUIModel(2, false, null);

        //public StackTradeUnitUIModel Sell2Unit
        //{
        //    get { return _sell2Unit; }
        //}

        //private StackTradeUnitUIModel _sell1Unit = new StackTradeUnitUIModel(1, false, null);

        //public StackTradeUnitUIModel Sell1Unit
        //{
        //    get { return _sell1Unit; }
        //}

        //private StackTradeUnitUIModel _buy1Unit = new StackTradeUnitUIModel(1, true, null);

        //public StackTradeUnitUIModel Buy1Unit
        //{
        //    get { return _buy1Unit; }
        //}

        //private StackTradeUnitUIModel _buy2Unit = new StackTradeUnitUIModel(2, true, null);

        //public StackTradeUnitUIModel Buy2Unit
        //{
        //    get { return _buy2Unit; }
        //}

        //private StackTradeUnitUIModel _buy3Unit = new StackTradeUnitUIModel(3, true, null);

        //public StackTradeUnitUIModel Buy3Unit
        //{
        //    get { return _buy3Unit; }
        //}

        //private StackTradeUnitUIModel _buy4Unit = new StackTradeUnitUIModel(4, true, null);

        //public StackTradeUnitUIModel Buy4Unit
        //{
        //    get { return _buy4Unit; }
        //}

        //private StackTradeUnitUIModel _buy5Unit = new StackTradeUnitUIModel(5, true, null);

        //public StackTradeUnitUIModel Buy5Unit
        //{
        //    get { return _buy5Unit; }
        //}



        public string DayText
        {
            get
            {
                return this._parentObject.DailyInfo.Day.ToDateTime().ToLongDateString();
            }
        }

        public decimal OpenPrice
        {
            get
            {
                return this._parentObject.DailyInfo.OpenPrice;
            }
        }

        public decimal ClosePrice
        {
            get
            {
                return this._parentObject.DailyInfo.ClosePrice;
            }
        }

        public decimal LimitUpPrice
        {
            get
            {
                return this._parentObject.DailyInfo.LimitUpPrice;
            }
        }

        public decimal LimitDownPrice
        {
            get
            {
                return this._parentObject.DailyInfo.LimitDownPrice;
            }
        }

        public decimal MinTradeSucceedPrice
        {
            get
            {
                return this._parentObject.DailyInfo.MinTradeSucceedPrice;
            }
        }

        public decimal MaxTradeSucceedPrice
        {
            get
            {
                return this._parentObject.DailyInfo.MaxTradeSucceedPrice;
            }
        }

        public int TradeSucceedStoneHandSum
        {
            get
            {
                return this._parentObject.DailyInfo.TradeSucceedStoneHandSum;
            }
        }

        public decimal TradeSucceedRMBSum
        {
            get
            {
                return this._parentObject.DailyInfo.TradeSucceedRMBSum;
            }
        }

        public int DelegateSellStoneSum
        {
            get
            {
                return this._parentObject.DailyInfo.DelegateSellStoneSum;
            }
        }

        public int DelegateBuyStoneSum
        {
            get
            {
                return this._parentObject.DailyInfo.DelegateBuyStoneSum;
            }
        }


    }
}
