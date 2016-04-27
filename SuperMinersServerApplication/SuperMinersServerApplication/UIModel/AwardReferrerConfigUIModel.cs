using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.UIModel
{
    public class AwardReferrerConfigUIModel : BaseUIModel
    {
        public AwardReferrerConfigUIModel()
        {
            IsChanged = false;
        }

        public bool IsChanged { get; set; }

        private int _feferLevel;

        public int ReferLevel
        {
            get { return _feferLevel; }
            set
            {
                if (value != this._feferLevel)
                {
                    this._feferLevel = value;
                    IsChanged = true;
                    NotifyPropertyChanged("ReferLevel");
                }
            }
        }


        private float _awardReferrerExp = 1;

        /// <summary>
        /// 奖励推荐人贡献值
        /// </summary>
        public float AwardReferrerExp
        {
            get { return this._awardReferrerExp; }
            set
            {
                if (value != this._awardReferrerExp)
                {
                    this._awardReferrerExp = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerExp");
                }
            }
        }

        private float _awardReferrerGoldCoin = 5;

        /// <summary>
        /// 奖励推荐人金币数
        /// </summary>
        public float AwardReferrerGoldCoin
        {
            get { return this._awardReferrerGoldCoin; }
            set
            {
                if (value != this._awardReferrerGoldCoin)
                {
                    this._awardReferrerGoldCoin = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerGoldCoin");
                }
            }
        }

        private float _awardReferrerMines = 0;

        /// <summary>
        /// 奖励推荐人矿山数
        /// </summary>
        public float AwardReferrerMines
        {
            get { return this._awardReferrerMines; }
            set
            {
                if (value != this._awardReferrerMines)
                {
                    this._awardReferrerMines = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerMines");
                }
            }
        }

        private int _awardReferrerMiners = 1;

        /// <summary>
        /// 奖励推荐人矿工数
        /// </summary>
        public int AwardReferrerMiners
        {
            get { return this._awardReferrerMiners; }
            set
            {
                if (value != this._awardReferrerMiners)
                {
                    this._awardReferrerMiners = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerMiners");
                }
            }
        }

        private float _awardReferrerStones = 0.05f;

        /// <summary>
        /// 奖励推荐人矿石数
        /// </summary>
        public float AwardReferrerStones
        {
            get { return this._awardReferrerStones; }
            set
            {
                if (value != this._awardReferrerStones)
                {
                    this._awardReferrerStones = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerStones");
                }
            }
        }

        private float _awardReferrerDiamond = 0.05f;

        /// <summary>
        /// 奖励推荐人钻石数
        /// </summary>
        public float AwardReferrerDiamond
        {
            get { return this._awardReferrerDiamond; }
            set
            {
                if (value != this._awardReferrerDiamond)
                {
                    this._awardReferrerDiamond = value;
                    IsChanged = true;
                    NotifyPropertyChanged("AwardReferrerDiamond");
                }
            }
        }

        public static AwardReferrerConfigUIModel CreateFromDBObject(AwardReferrerConfig parent)
        {
            if (parent == null)
            {
                return new AwardReferrerConfigUIModel();
            }
            AwardReferrerConfigUIModel uiConfig = new AwardReferrerConfigUIModel()
            {
                AwardReferrerDiamond = parent.AwardReferrerDiamond,
                AwardReferrerExp = parent.AwardReferrerExp,
                AwardReferrerGoldCoin = parent.AwardReferrerGoldCoin,
                AwardReferrerMiners = parent.AwardReferrerMiners,
                AwardReferrerMines = parent.AwardReferrerMines,
                AwardReferrerStones = parent.AwardReferrerStones,
                ReferLevel = parent.ReferLevel
            };

            return uiConfig;
        }

        public AwardReferrerConfig ToDBObject()
        {
            AwardReferrerConfig dbConfig = new AwardReferrerConfig()
            {
                AwardReferrerDiamond = this.AwardReferrerDiamond,
                AwardReferrerExp = this.AwardReferrerExp,
                AwardReferrerGoldCoin = this.AwardReferrerGoldCoin,
                AwardReferrerMiners = this.AwardReferrerMiners,
                AwardReferrerMines = this.AwardReferrerMines,
                AwardReferrerStones = this.AwardReferrerStones,
                ReferLevel = this.ReferLevel
            };

            return dbConfig;
        }

    }
}
