using MetaData;
using MetaData.User;
using SuperMinersWPF.StringResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperMinersWPF.Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class UserUIModel : BaseModel
    {
        public UserUIModel(PlayerInfo parent)
        {
            this.ParentObject = parent;
        }

        private PlayerInfo _parentObject;

        public PlayerInfo ParentObject
        {
            get
            {
                return this._parentObject;
            }
            set
            {
                this._parentObject = value;

                NotifyPropertyChange("UserName");
                NotifyPropertyChange("NickName");
                NotifyPropertyChange("Password");
                NotifyPropertyChange("Alipay");
                NotifyPropertyChange("AlipayRealName");
                NotifyPropertyChange("RegisterTime");
                NotifyPropertyChange("InvitationCode");
                NotifyPropertyChange("Exp");
                NotifyPropertyChange("RMB");
                NotifyPropertyChange("EnbleRMB");
                NotifyPropertyChange("FreezingRMB");
                NotifyPropertyChange("GoldCoin");
                NotifyPropertyChange("MinesCount");
                NotifyPropertyChange("StonesReserves");
                NotifyPropertyChange("WorkableStonesReservers");
                NotifyPropertyChange("TotalProducedStonesCount");
                NotifyPropertyChange("MinersCount");
                NotifyPropertyChange("AllOutputPerHour");
                NotifyPropertyChange("AllOutputPerDay");
                NotifyPropertyChange("TempOutputStones");
                NotifyPropertyChange("TempOutputStonesString");
                NotifyPropertyChange("StockOfStones");
                NotifyPropertyChange("FreezingStones");
                NotifyPropertyChange("SellableStones");
                NotifyPropertyChange("StockOfDiamonds");
                NotifyPropertyChange("FreezingDiamonds");
                NotifyPropertyChange("SellableDiamonds");
            }
        }

        public string UserName
        {
            get { return this._parentObject.SimpleInfo.UserName; }
        }

        public string NickName
        {
            get { return this._parentObject.SimpleInfo.NickName; }
        }

        public string Password
        {
            get { return this._parentObject.SimpleInfo.Password; }
        }

        public string Alipay
        {
            get { return this._parentObject.SimpleInfo.Alipay; }
        }

        public string AlipayRealName
        {
            get { return this._parentObject.SimpleInfo.AlipayRealName; }
        }

        public DateTime? RegisterTime
        {
            get { return this._parentObject.SimpleInfo.RegisterTime; }
        }

        public string InvitationCode
        {
            get { return this._parentObject.SimpleInfo.InvitationCode; }
        }

        public decimal Exp
        {
            get { return this._parentObject.FortuneInfo.Exp; }
        }

        private BitmapImage userIcon;

        public BitmapImage UserIcon
        {
            get { return userIcon; }
            set { userIcon = value; }
        }

        public decimal RMB
        {
            get { return this._parentObject.FortuneInfo.RMB; }
        }

        public decimal FreezingRMB
        {
            get { return this._parentObject.FortuneInfo.FreezingRMB; }
        }

        public decimal EnbleRMB
        {
            get { return this.RMB - this.FreezingRMB; }
        }

        /// <summary>
        /// 金币数
        /// </summary>
        public decimal GoldCoin
        {
            get { return this._parentObject.FortuneInfo.GoldCoin; }
        }

        /// <summary>
        /// 矿山数
        /// </summary>
        public decimal MinesCount
        {
            get { return this._parentObject.FortuneInfo.MinesCount; }
        }

        /// <summary>
        /// 矿石储量
        /// </summary>
        public decimal StonesReserves 
        {
            get { return (decimal)this._parentObject.FortuneInfo.StonesReserves; }
        }

        public decimal TotalProducedStonesCount
        {
            get { return (decimal)this._parentObject.FortuneInfo.TotalProducedStonesCount; }
        }

        /// <summary>
        /// 矿工数
        /// </summary>
        public decimal MinersCount
        {
            get { return this._parentObject.FortuneInfo.MinersCount; }
        }

        /// <summary>
        /// 所有矿工每小时总产量
        /// </summary>
        public decimal AllOutputPerHour
        {
            get
            {
                return this.MinersCount * GlobalData.GameConfig.OutputStonesPerHour;
            }
        }

        /// <summary>
        /// 所有矿工每天总产量
        /// </summary>
        public decimal AllOutputPerDay
        {
            get
            {
                return AllOutputPerHour * 24;
            }
        }

        private int _outputCountDown;

        public int OutputCountdown
        {
            get { return this._outputCountDown; }
            set
            {
                this._outputCountDown = value;
                NotifyPropertyChange("OutputCountdownString");
            }
        }

        public string OutputCountdownString
        {
            get { return OutputCountdown.ToString() + "秒"; }
        }

        /// <summary>
        /// 可开采矿石储量
        /// </summary>
        public decimal WorkableStonesReservers
        {
            get
            {
                decimal workable = this.StonesReserves - this.TotalProducedStonesCount;
                if (workable < 0)
                {
                    return 0;
                }

                return workable;
            }
        }

        public DateTime TempOutputStonesStartTime
        {
            get
            {
                if (this._parentObject.FortuneInfo.TempOutputStonesStartTime.HasValue)
                {
                    return this._parentObject.FortuneInfo.TempOutputStonesStartTime.Value;
                }

                return this._parentObject.SimpleInfo.LastLoginTime.Value;
            }
        }

        public decimal MaxTempStonesOutput
        {
            get
            {
                return GlobalData.GameConfig.TempStoneOutputValidHour * this.MinersCount * GlobalData.GameConfig.OutputStonesPerHour;
            }
        }

        public decimal TempOutputStones
        {
            get { return this._parentObject.FortuneInfo.TempOutputStones; }
            set
            {
                if (value > this.WorkableStonesReservers)
                {
                    this._parentObject.FortuneInfo.TempOutputStones = this.WorkableStonesReservers;
                }
                else
                {
                    this._parentObject.FortuneInfo.TempOutputStones = value;
                }
                NotifyPropertyChange("TempOutputStones");
                NotifyPropertyChange("TempOutputStonesString");
                NotifyPropertyChange("WorkableStonesReserves");
            }
        }

        public string TempOutputStonesString
        {
            get
            {
                return TempOutputStones.ToString("0.00") + " " + Strings.Stone;
            }
        }

        /// <summary>
        /// 库存矿石数
        /// </summary>
        public decimal StockOfStones
        {
            get { return this._parentObject.FortuneInfo.StockOfStones; }
        }

        public decimal FreezingStones
        {
            get { return this._parentObject.FortuneInfo.FreezingStones; }
        }

        public decimal SellableStones
        {
            get { return StockOfStones - FreezingStones; }
        }

        /// <summary>
        /// 库存钻石数
        /// </summary>
        public decimal StockOfDiamonds
        {
            get { return this._parentObject.FortuneInfo.StockOfDiamonds; }
        }

        public decimal FreezingDiamonds
        {
            get { return this._parentObject.FortuneInfo.FreezingDiamonds; }
        }

        public decimal SellableDiamonds
        {
            get { return StockOfDiamonds - FreezingDiamonds; }
        }
    }
}
