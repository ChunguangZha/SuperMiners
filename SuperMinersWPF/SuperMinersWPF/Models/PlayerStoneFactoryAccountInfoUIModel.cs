using MetaData;
using MetaData.StoneFactory;
using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class PlayerStoneFactoryAccountInfoUIModel : BaseModel
    {
        public PlayerStoneFactoryAccountInfoUIModel(PlayerStoneFactoryAccountInfo parent)
        {
            this.ParentObject = parent;
        }

        private PlayerStoneFactoryAccountInfo _parentObject;

        public PlayerStoneFactoryAccountInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("ID");
                NotifyPropertyChange("UserID");
                NotifyPropertyChange("FactoryIsOpening");
                NotifyPropertyChange("FactoryLiveDays");
                NotifyPropertyChange("FreezingSlavesCount");
                NotifyPropertyChange("FreezingSlaveGroupCount");
                NotifyPropertyChange("EnableSlavesGroupCount");
                NotifyPropertyChange("EnableSlavesCount");
                NotifyPropertyChange("Food");
                NotifyPropertyChange("FoodUsableDays");
                NotifyPropertyChange("LastDayValidStoneStack");
                NotifyPropertyChange("TotalStackCount");
                NotifyPropertyChange("TotalStoneCount");
                NotifyPropertyChange("FreezingStackCount");
                NotifyPropertyChange("FreezingStoneCount");
                NotifyPropertyChange("WithdrawableStackCount");
                NotifyPropertyChange("WithdrawableStoneEnable");
                NotifyPropertyChange("TotalProfitRMB");
                NotifyPropertyChange("WithdrawableProfitRMB");
                NotifyPropertyChange("WithdrawableStoneCount");
                NotifyPropertyChange("WithdrawProfitRMBEnable");
                NotifyPropertyChange("YesterdayTotalProfitRMB");
                NotifyPropertyChange("YesterdayFactoryProfitRate");
            }
        }


        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public int UserID
        {
            get
            {
                return this._parentObject.UserID;
            }
        }

        /// <summary>
        /// 工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
        /// </summary>
        public bool FactoryIsOpening
        {
            get
            {
                return this._parentObject.FactoryIsOpening;
            }
        }

        /// <summary>
        /// 工厂剩余可用天数，默认值为3天，当0点检查时，如果工厂状态开启，又没有可用的矿石或奴隶，则该值减一天。直到减为0，工厂关闭。
        /// </summary>
        public int FactoryLiveDays
        {
            get
            {
                return this._parentObject.FactoryLiveDays;
            }
        }

        /// <summary>
        /// 冻结奴隶组数（100个奴隶为一组）。当天存入的奴隶先保存到冻结奴隶中。第二天0时结算时再加入到可用奴隶中。
        /// </summary>
        public int FreezingSlaveGroupCount
        {
            get
            {
                return this._parentObject.FreezingSlaveGroupCount;
            }
        }

        public int FreezingSlavesCount
        {
            get
            {
                return this.FreezingSlaveGroupCount * StoneFactoryConfig.OneGroupSlaveHasMiners;
            }
        }

        /// <summary>
        /// 可用奴隶组数（100个奴隶为一组）。凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
        /// </summary>
        public int EnableSlavesGroupCount
        {
            get
            {
                return this._parentObject.EnableSlavesGroupCount;
            }
        }

        public int EnableSlavesCount
        {
            get
            {
                return this.EnableSlavesGroupCount * StoneFactoryConfig.OneGroupSlaveHasMiners;
            }
        }

        /// <summary>
        /// 食物（以一组奴隶（100矿工）一天口粮为单位），该值每天0时需按奴隶值检查减值，充值时相应增加，每次投入奴隶时，会自动增加本次奴隶两天的食物。
        /// </summary>
        public int Food
        {
            get
            {
                return this._parentObject.Food;
            }
        }

        public int FoodUsableDays
        {
            get
            {
                if (this.EnableSlavesGroupCount == 0 && this.FreezingSlaveGroupCount == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling((float)this.Food / (this.EnableSlavesGroupCount + this.FreezingSlaveGroupCount));
            }
        }

        public MyDateTime LastFeedSlaveTime
        {
            get
            {
                return this._parentObject.LastFeedSlaveTime;
            }
        }

        /// <summary>
        /// 奴隶寿命倒计时（秒）
        /// </summary>
        public int SlaveLiveDiscountms
        {
            get
            {
                return this._parentObject.SlaveLiveDiscountms;
            }
            set
            {
                this._parentObject.SlaveLiveDiscountms = value;
                NotifyPropertyChange("SlaveLiveDiscountms");
                NotifyPropertyChange("SlaveLiveDiscountmsText");
            }
        }

        public string SlaveLiveDiscountmsText
        {
            get
            {
                if (SlaveLiveDiscountms <= 0)
                {
                    return "已超时";
                }

                float total = SlaveLiveDiscountms;
                float day_s = 24f * 60 * 60;
                float hour_s = 60f * 60;
                float minute_s = 60f;
                int day = (int)(SlaveLiveDiscountms / day_s);
                total -= day * day_s;
                int hour = (int)(total / hour_s);//23
                total -= hour * hour_s;
                int minute = (int)(total / minute_s);//13
                total -= minute * minute_s;
                int second = (int)total;

                return day.ToString() + "天" + hour.ToString() + "时" + minute.ToString() + "分" + second.ToString() + "秒";
            }
        }

        /// <summary>
        /// 前一天有效矿石，每天0点计算前一天有效矿石（即前一天之前存入的矿石和前一天前存入的存活的奴隶），每天14点，会开出前一天盈利点，按盈利点*LastDayValidStone，得出前一天收益。
        /// </summary>
        public int LastDayValidStoneStack
        {
            get
            {
                return this._parentObject.LastDayValidStoneStack;
            }
        }

        /// <summary>
        /// 总可用股数，不包含冻结矿石（该值不存入数据库，每次查询数据库中所有股权变更记录累计得出）。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        public int TotalStackCount
        {
            get
            {
                return this._parentObject.TotalStackCount;
            }
        }

        public int TotalStoneCount
        {
            get { return this.TotalStackCount * StoneFactoryConfig.StoneFactoryStone_Stack; }
        }
        
        /// <summary>
        /// 冻结的矿石股数（即当天存入的矿石为冻结状态，第二天0时检查时将期算为可用）
        /// </summary>
        public int FreezingStackCount
        {
            get
            {
                return this._parentObject.FreezingStackCount;
            }
        }

        public int FreezingStoneCount
        {
            get { return this.FreezingStackCount * StoneFactoryConfig.StoneFactoryStone_Stack; }
        }
        
        /// <summary>
        /// 可以提取的股数（该值不存入数据库，每次查询数据库中所有股权变更记录累计得出）。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        public int WithdrawableStackCount
        {
            get
            {
                return this._parentObject.WithdrawableStackCount;
            }
        }

        public int WithdrawableStoneCount
        {
            get { return this.WithdrawableStackCount * StoneFactoryConfig.StoneFactoryStone_Stack; }
        }

        public bool WithdrawableStoneEnable
        {
            get
            {
                return WithdrawableStackCount > 0;
            }
        }

        /// <summary>
        /// 总计生产出的灵币值（该值不存入数据库，每次查询数据库中所有灵币收益变更记录累计得出）
        /// </summary>
        public decimal TotalProfitRMB
        {
            get
            {
                return this._parentObject.TotalProfitRMB;
            }
        }

        /// <summary>
        /// 当前可提取的灵币值（18天前生产出的灵币）（该值不存入数据库，每次查询数据库中所有灵币收益变更记录累计得出）
        /// </summary>
        public decimal WithdrawableProfitRMB
        {
            get
            {
                return this._parentObject.WithdrawableProfitRMB;
            }
        }

        public bool WithdrawProfitRMBEnable
        {
            get
            {
                return WithdrawableProfitRMB > 0;
            }
        }

        public decimal YesterdayTotalProfitRMB
        {
            get
            {
                return this._parentObject.YesterdayTotalProfitRMB;
            }
        }

        public decimal YesterdayFactoryProfitRate
        {
            get
            {
                return GlobalData.CurrentUser.YesterdayFactoryProfitRate;
            }
        }


    }
}
