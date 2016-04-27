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

        public float _Yuan_RMB = 1;

        /// <summary>
        /// 人民币兑换RMB
        /// </summary>
        public float Yuan_RMB
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

        public float _RMB_GoldCoin = 1000;

        /// <summary>
        /// RMB兑换金币
        /// </summary>
        public float RMB_GoldCoin
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

        public float _GoldCoin_Mine = 10000;

        /// <summary>
        /// 购买矿山所需要的金币
        /// </summary>
        public float GoldCoin_Mine
        {
            get { return this._GoldCoin_Mine; }
            set
            {
                if (value != this._GoldCoin_Mine)
                {
                    this._GoldCoin_Mine = value;
                    IsChanged = true;
                    NotifyPropertyChanged("GoldCoin_Mine");
                }
            }
        }

        public float _GoldCoin_Miner = 1000;

        /// <summary>
        /// 购买矿工所需要的金币
        /// </summary>
        public float GoldCoin_Miner
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

        public float _Stones_RMB = 10000;

        /// <summary>
        /// 多少矿石等价于1RMB
        /// </summary>
        public float Stones_RMB
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

        public float _Diamonds_RMB = 10;

        /// <summary>
        /// 多少钻石等价于1RMB
        /// </summary>
        public float Diamonds_RMB
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

        public float _StoneBuyerAwardGoldCoinMultiple = 1;

        /// <summary>
        /// 矿石买家奖励金币数
        /// </summary>
        public float StoneBuyerAwardGoldCoinMultiple
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

        public float _OutputStonesPerHour = 0.003f;

        /// <summary>
        /// 每个矿工每小时生产矿石数
        /// </summary>
        public float OutputStonesPerHour
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

        public float _StonesReservesPerMines = 1000;

        /// <summary>
        /// 每座矿山的矿石储量
        /// </summary>
        public float StonesReservesPerMines
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

        public float _ExchangeExpensePercent = 5;

        /// <summary>
        /// 提现手续费比例百分数，值为1，表示1%
        /// </summary>
        public float ExchangeExpensePercent
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
        
        public float _ExchangeExpenseMinNumber = 1;

        /// <summary>
        /// 提现手续费手续费最小金额
        /// </summary>
        public float ExchangeExpenseMinNumber
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
                GoldCoin_Mine = parent.GoldCoin_Mine,
                GoldCoin_Miner = parent.GoldCoin_Miner,
                OutputStonesPerHour = parent.OutputStonesPerHour,
                RMB_GoldCoin = parent.RMB_GoldCoin,
                StoneBuyerAwardGoldCoinMultiple = parent.StoneBuyerAwardGoldCoinMultiple,
                Stones_RMB = parent.Stones_RMB,
                StonesReservesPerMines = parent.StonesReservesPerMines,
                Yuan_RMB = parent.Yuan_RMB
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
                GoldCoin_Mine = this.GoldCoin_Mine,
                GoldCoin_Miner = this.GoldCoin_Miner,
                OutputStonesPerHour = this.OutputStonesPerHour,
                RMB_GoldCoin = this.RMB_GoldCoin,
                StoneBuyerAwardGoldCoinMultiple = this.StoneBuyerAwardGoldCoinMultiple,
                Stones_RMB = this.Stones_RMB,
                StonesReservesPerMines = this.StonesReservesPerMines,
                Yuan_RMB = this.Yuan_RMB
            };

            return dbConfig;
        }

    }
}
