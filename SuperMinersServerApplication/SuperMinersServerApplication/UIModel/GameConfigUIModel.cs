using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.UIModel
{
    public class GameConfigUIModel : BaseUIModel
    {
        public GameConfigUIModel()
        {
            IsChanged = false;
        }

        public bool IsChanged { get; set; }

        private decimal _Yuan_RMB = 1;

        /// <summary>
        /// 人民币兑换RMB
        /// </summary>
        public decimal Yuan_RMB
        {
            get { return this._Yuan_RMB; }
            set
            {
                if (this._Yuan_RMB != value)
                {
                    this._Yuan_RMB = value;
                    IsChanged = true;
                    NotifyPropertyChanged("Yuan_RMB");
                }
            }
        }

        private decimal _RMB_GoldCoin = 1000;

        /// <summary>
        /// RMB兑换金币
        /// </summary>
        public decimal RMB_GoldCoin
        {
            get { return this._RMB_GoldCoin; }
            set
            {
                if (value != this._RMB_GoldCoin)
                {
                    this._RMB_GoldCoin = value;
                    IsChanged = true;
                    NotifyPropertyChanged("RMB_GoldCoin");
                }
            }
        }

        private decimal _RMB_Mine = 10;

        /// <summary>
        /// 购买矿山所需要的RMB
        /// </summary>
        public decimal RMB_Mine
        {
            get { return this._RMB_Mine; }
            set
            {
                if (value != this._RMB_Mine)
                {
                    this._RMB_Mine = value;
                    IsChanged = true;
                    NotifyPropertyChanged("RMB_Mine");
                }
            }
        }

        private decimal _GoldCoin_Miner = 1000;

        /// <summary>
        /// 购买矿工所需要的金币
        /// </summary>
        public decimal GoldCoin_Miner
        {
            get { return this._GoldCoin_Miner; }
            set
            {
                if (value != this._GoldCoin_Miner)
                {
                    this._GoldCoin_Miner = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GoldCoin_Miner");
                }
            }
        }

        private decimal _Stones_RMB = 10000;

        /// <summary>
        /// 多少矿石等价于1RMB
        /// </summary>
        public decimal Stones_RMB
        {
            get { return this._Stones_RMB; }
            set
            {
                if (value != this._Stones_RMB)
                {
                    this._Stones_RMB = value;
                    IsChanged = true;
                    NotifyPropertyChanged("Stones_RMB");
                }
            }
        }

        private decimal _Diamonds_RMB = 10;

        /// <summary>
        /// 多少钻石等价于1RMB
        /// </summary>
        public decimal Diamonds_RMB
        {
            get { return this._Diamonds_RMB; }
            set
            {
                if (value != this._Diamonds_RMB)
                {
                    this._Diamonds_RMB = value;
                    IsChanged = true;
                    NotifyPropertyChanged("Diamonds_RMB");
                }
            }
        }

        private decimal _StoneBuyerAwardGoldCoinMultiple = 1;

        /// <summary>
        /// 矿石买家奖励金币数
        /// </summary>
        public decimal StoneBuyerAwardGoldCoinMultiple
        {
            get { return this._StoneBuyerAwardGoldCoinMultiple; }
            set
            {
                if (value != this._StoneBuyerAwardGoldCoinMultiple)
                {
                    this._StoneBuyerAwardGoldCoinMultiple = value;
                    IsChanged = true;
                    NotifyPropertyChanged("StoneBuyerAwardGoldCoinMultiple");
                }
            }
        }

        private decimal _OutputStonesPerHour = 0.003m;

        /// <summary>
        /// 每个矿工每小时生产矿石数
        /// </summary>
        public decimal OutputStonesPerHour
        {
            get { return this._OutputStonesPerHour; }
            set
            {
                if (value != this._OutputStonesPerHour)
                {
                    this._OutputStonesPerHour = value;
                    IsChanged = true;
                    NotifyPropertyChanged("OutputStonesPerHour");
                }
            }
        }

        private decimal _StonesReservesPerMines = 1000;

        /// <summary>
        /// 每座矿山的矿石储量
        /// </summary>
        public decimal StonesReservesPerMines
        {
            get { return this._StonesReservesPerMines; }
            set
            {
                if (value != this._StonesReservesPerMines)
                {
                    this._StonesReservesPerMines = value;
                    IsChanged = true;
                    NotifyPropertyChanged("StonesReservesPerMines");
                }
            }
        }

        private decimal _ExchangeExpensePercent = 5;

        /// <summary>
        /// 提现手续费比例百分数，值为1，表示1%
        /// </summary>
        public decimal ExchangeExpensePercent
        {
            get { return this._ExchangeExpensePercent; }
            set
            {
                if (value != this._ExchangeExpensePercent)
                {
                    this._ExchangeExpensePercent = value;
                    IsChanged = true;
                    NotifyPropertyChanged("ExchangeExpensePercent");
                }
            }
        }

        private decimal _ExchangeExpenseMinNumber = 1;

        /// <summary>
        /// 提现手续费手续费最小金额
        /// </summary>
        public decimal ExchangeExpenseMinNumber
        {
            get { return this._ExchangeExpenseMinNumber; }
            set
            {
                if (value != this._ExchangeExpenseMinNumber)
                {
                    this._ExchangeExpenseMinNumber = value;
                    IsChanged = true;
                    NotifyPropertyChanged("ExchangeExpenseMinNumber");
                }
            }
        }

        private int _UserMaxHaveMinersCount = 50;

        /// <summary>
        /// 
        /// </summary>
        public int UserMaxHaveMinersCount
        {
            get { return this._UserMaxHaveMinersCount; }
            set
            {
                if (value != this._UserMaxHaveMinersCount)
                {
                    this._UserMaxHaveMinersCount = value;
                    IsChanged = true;
                    NotifyPropertyChanged("UserMaxHaveMinersCount");
                }
            }
        }

        private int _BuyOrderLockTimeMinutes = 30;

        /// <summary>
        /// 
        /// </summary>
        public int BuyOrderLockTimeMinutes
        {
            get { return this._BuyOrderLockTimeMinutes; }
            set
            {
                if (value != this._BuyOrderLockTimeMinutes)
                {
                    this._BuyOrderLockTimeMinutes = value;
                    IsChanged = true;
                    NotifyPropertyChanged("BuyOrderLockTimeMinutes");
                }
            }
        }

        private int _CanExchangeMinExp;

        public int CanExchangeMinExp
        {
            get { return _CanExchangeMinExp; }
            set
            {
                if (value != this._CanExchangeMinExp)
                {
                    this._CanExchangeMinExp = value;
                    IsChanged = true;
                    NotifyPropertyChanged("CanExchangeMinExp");
                }
            }
        }

        private int _CanDiscountMinExp;

        public int CanDiscountMinExp
        {
            get { return _CanDiscountMinExp; }
            set
            {
                if (value != this._CanDiscountMinExp)
                {
                    this._CanDiscountMinExp = value;
                    IsChanged = true;
                    NotifyPropertyChanged("CanDiscountMinExp");
                }
            }
        }

        private decimal _Discount;

        public decimal Discount
        {
            get { return _Discount; }
            set
            {
                if (value != this._Discount)
                {
                    this._Discount = value;
                    IsChanged = true;
                    NotifyPropertyChanged("Discount");
                }
            }
        }


        private int _TempStoneOutputValidHour;

        public int TempStoneOutputValidHour
        {
            get { return _TempStoneOutputValidHour; }
            set
            {
                if (value != this._TempStoneOutputValidHour)
                {
                    this._TempStoneOutputValidHour = value;
                    IsChanged = true;
                    NotifyPropertyChanged("TempStoneOutputValidHour");
                }
            }
        }



        public static GameConfigUIModel CreateFromDBObject(GameConfig parent)
        {
            if (parent == null)
            {
                return new GameConfigUIModel();
            }
            GameConfigUIModel uiConfig = new GameConfigUIModel()
            {
                Diamonds_RMB = parent.Diamonds_RMB,
                ExchangeExpenseMinNumber = parent.ExchangeExpenseMinNumber,
                ExchangeExpensePercent = parent.ExchangeExpensePercent,
                RMB_Mine = parent.RMB_Mine,
                GoldCoin_Miner = parent.GoldCoin_Miner,
                OutputStonesPerHour = parent.OutputStonesPerHour,
                RMB_GoldCoin = parent.RMB_GoldCoin,
                StoneBuyerAwardGoldCoinMultiple = parent.StoneBuyerAwardGoldCoinMultiple,
                Stones_RMB = parent.Stones_RMB,
                StonesReservesPerMines = parent.StonesReservesPerMines,
                Yuan_RMB = parent.Yuan_RMB,
                UserMaxHaveMinersCount = parent.UserMaxHaveMinersCount,
                BuyOrderLockTimeMinutes = parent.BuyOrderLockTimeMinutes,
                TempStoneOutputValidHour = parent.TempStoneOutputValidHour,
                CanDiscountMinExp = parent.CanDiscountMinExp,
                CanExchangeMinExp = parent.CanExchangeMinExp,
                Discount = parent.Discount,
            };

            return uiConfig;
        }

        public GameConfig ToDBObject()
        {
            GameConfig dbConfig = new GameConfig()
            {
                Diamonds_RMB = this.Diamonds_RMB,
                ExchangeExpenseMinNumber = this.ExchangeExpenseMinNumber,
                ExchangeExpensePercent = this.ExchangeExpensePercent,
                RMB_Mine = this.RMB_Mine,
                GoldCoin_Miner = this.GoldCoin_Miner,
                OutputStonesPerHour = this.OutputStonesPerHour,
                RMB_GoldCoin = this.RMB_GoldCoin,
                StoneBuyerAwardGoldCoinMultiple = this.StoneBuyerAwardGoldCoinMultiple,
                Stones_RMB = this.Stones_RMB,
                StonesReservesPerMines = this.StonesReservesPerMines,
                Yuan_RMB = this.Yuan_RMB,
                UserMaxHaveMinersCount = this.UserMaxHaveMinersCount,
                BuyOrderLockTimeMinutes = this.BuyOrderLockTimeMinutes,
                TempStoneOutputValidHour = this.TempStoneOutputValidHour,
                CanDiscountMinExp = this.CanDiscountMinExp,
                CanExchangeMinExp = this.CanExchangeMinExp,
                Discount = this.Discount
            };

            return dbConfig;
        }

    }
}
