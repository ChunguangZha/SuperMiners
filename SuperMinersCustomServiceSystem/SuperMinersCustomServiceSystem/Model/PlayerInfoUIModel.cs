using SuperMinersServerApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class PlayerInfoUIModel : BaseModel
    {
        public PlayerInfoUIModel(PlayerInfoLoginWrap parent)
        {
            this.ParentObject = parent;
        }

        private PlayerInfoLoginWrap _parentObject;

        public PlayerInfoLoginWrap ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("Online");
                NotifyPropertyChange("LoginIP");
                NotifyPropertyChange("UserName");
                NotifyPropertyChange("NickName");
                NotifyPropertyChange("Alipay");
                NotifyPropertyChange("AlipayRealName");
                NotifyPropertyChange("RegisterTime");
                NotifyPropertyChange("ReferrerUserName");
                NotifyPropertyChange("InvitationCode");
                NotifyPropertyChange("LastLoginTime");
                NotifyPropertyChange("IsLocked");
                NotifyPropertyChange("LockedTime");
                NotifyPropertyChange("Exp");
                NotifyPropertyChange("RMB");
                NotifyPropertyChange("FreezingRMB");
                NotifyPropertyChange("GoldCoin");
                NotifyPropertyChange("MinesCount");
                NotifyPropertyChange("StonesReserves");
                NotifyPropertyChange("WorkableStonesReservers");
                NotifyPropertyChange("TotalProducedStonesCount");
                NotifyPropertyChange("MinersCount");
                NotifyPropertyChange("StockOfStones");
                NotifyPropertyChange("FreezingStones");
                NotifyPropertyChange("SellableStones");
                NotifyPropertyChange("StockOfDiamonds");
                NotifyPropertyChange("FreezingDiamonds");
                NotifyPropertyChange("SellableDiamonds");
            }
        }

        private bool _isChecked;

        public bool Checked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChange("Checked");
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

        public string ReferrerUserName
        {
            get { return this._parentObject.SimpleInfo.ReferrerUserName; }
        }

        public string InvitationCode
        {
            get { return this._parentObject.SimpleInfo.InvitationCode; }
        }

        public DateTime? LastLoginTime
        {
            get { return this._parentObject.SimpleInfo.LastLoginTime; }
        }

        public bool IsLocked
        {
            get { return this._parentObject.SimpleInfo.LockedLogin; }
        }

        public DateTime? LockedTime
        {
            get { return this._parentObject.SimpleInfo.LockedLoginTime; }
        }

        public bool Online
        {
            get { return this.ParentObject.isOnline; }
            set
            {
                this.ParentObject.isOnline = value;
                NotifyPropertyChange("Online");
            }
        }

        public string LoginIP
        {
            get { return this.ParentObject.LoginIP; }
            set
            {
                this.ParentObject.LoginIP = value;
                NotifyPropertyChange("LoginIP");
            }
        }

        public float Exp
        {
            get { return this._parentObject.FortuneInfo.Exp; }
        }

        public float RMB
        {
            get { return this._parentObject.FortuneInfo.RMB; }
        }

        public float FreezingRMB
        {
            get { return this._parentObject.FortuneInfo.FreezingRMB; }
        }

        /// <summary>
        /// 金币数
        /// </summary>
        public float GoldCoin
        {
            get { return this._parentObject.FortuneInfo.GoldCoin; }
        }

        /// <summary>
        /// 矿山数
        /// </summary>
        public float MinesCount
        {
            get { return this._parentObject.FortuneInfo.MinesCount; }
        }

        /// <summary>
        /// 矿石储量
        /// </summary>
        public float StonesReserves
        {
            get { return this._parentObject.FortuneInfo.StonesReserves; }
        }

        public float TotalProducedStonesCount
        {
            get { return this._parentObject.FortuneInfo.TotalProducedStonesCount; }
        }

        /// <summary>
        /// 矿工数
        /// </summary>
        public float MinersCount
        {
            get { return this._parentObject.FortuneInfo.MinersCount; }
        }

        /// <summary>
        /// 所有矿工每小时总产量
        /// </summary>
        public float AllOutputPerHour
        {
            get
            {
                return this.MinersCount * GlobalData.GameConfig.OutputStonesPerHour;
            }
        }

        /// <summary>
        /// 可开采矿石储量
        /// </summary>
        public float WorkableStonesReservers
        {
            get
            {
                float workable = this.StonesReserves - this.TotalProducedStonesCount;
                if (workable < 0)
                {
                    return 0;
                }

                return workable;
            }
        }

        /// <summary>
        /// 库存矿石数
        /// </summary>
        public float StockOfStones
        {
            get { return this._parentObject.FortuneInfo.StockOfStones; }
        }

        public float FreezingStones
        {
            get { return this._parentObject.FortuneInfo.FreezingStones; }
        }

        public float SellableStones
        {
            get { return StockOfStones - FreezingStones; }
        }

        /// <summary>
        /// 库存钻石数
        /// </summary>
        public float StockOfDiamonds
        {
            get { return this._parentObject.FortuneInfo.StockOfDiamonds; }
        }

        public float FreezingDiamonds
        {
            get { return this._parentObject.FortuneInfo.FreezingDiamonds; }
        }

        public float SellableDiamonds
        {
            get { return StockOfDiamonds - FreezingDiamonds; }
        }
    }
}
