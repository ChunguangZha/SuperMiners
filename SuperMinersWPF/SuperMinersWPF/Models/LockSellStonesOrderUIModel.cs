﻿using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class LockSellStonesOrderUIModel : BaseModel
    {
        public LockSellStonesOrderUIModel(LockSellStonesOrder parent)
        {
            this.ParentObject = parent;
        }

        private LockSellStonesOrder _parentObject;

        public LockSellStonesOrder ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("SellerUserName");
                NotifyPropertyChange("SellStonesCount");
                NotifyPropertyChange("Expense");
                NotifyPropertyChange("ValueRMB");
                NotifyPropertyChange("SellTime");
                NotifyPropertyChange("OrderState");
                NotifyPropertyChange("OrderStateString");
                NotifyPropertyChange("LockedByUserName");
                NotifyPropertyChange("PayUrl");
                NotifyPropertyChange("AwardGoldCoin");
                NotifyPropertyChange("LockedTime");
                NotifyPropertyChange("ValidTimeSeconds");
                NotifyPropertyChange("ValidTimeSecondsString");
                NotifyPropertyChange("HandleButtonContext");
                NotifyPropertyChange("ValidTimeVisibility");
                NotifyPropertyChange("HandleButtonNotEnable");

            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.StonesOrder.OrderNumber;
            }
        }

        public string SellerUserName
        {
            get
            {
                return this._parentObject.StonesOrder.SellerUserName;
            }
        }

        public int SellStonesCount
        {
            get
            {
                return this._parentObject.StonesOrder.SellStonesCount;
            }
        }

        public decimal Expense
        {
            get
            {
                return this._parentObject.StonesOrder.Expense;
            }
        }

        public decimal ValueRMB
        {
            get
            {
                return this._parentObject.StonesOrder.ValueRMB;
            }
        }

        public DateTime SellTime
        {
            get
            {
                return this._parentObject.StonesOrder.SellTime;
            }
        }

        public SellOrderState OrderState
        {
            get
            {
                return this._parentObject.StonesOrder.OrderState;
            }
        }

        public string OrderStateString
        {
            get
            {
                return "等待付款";
            }
        }

        public string LockedByUserName
        {
            get
            {
                return this._parentObject.LockedByUserName;
            }
        }

        public string PayUrl
        {
            get
            {
                return this._parentObject.PayUrl;
            }
        }

        public decimal AwardGoldCoin
        {
            get
            {
                return this.SellStonesCount * GlobalData.GameConfig.StoneBuyerAwardGoldCoinMultiple;
            }
        }

        public DateTime LockedTime
        {
            get
            {
                return this._parentObject.LockedTime;
            }
        }

        private int BuyOrderLockTimeSeconds
        {
            get
            {
                return GlobalData.GameConfig.BuyOrderLockTimeMinutes * 60;
            }
        }

        public int ValidTimeSeconds
        {
            get
            {
                int value = BuyOrderLockTimeSeconds - this._parentObject.OrderLockedTimeSpan;
                if (value <= 0)
                {
                    return 0;
                }
                return value;
            }
        }

        public int ValidTimeSecondsTickDown()
        {
            if (this._parentObject.StonesOrder.OrderState != SellOrderState.Exception)
            {
                this._parentObject.OrderLockedTimeSpan++;
                NotifyPropertyChange("ValidTimeSecondsString");
            }
            return this.ValidTimeSeconds;
        }

        public string ValidTimeSecondsString
        {
            get
            {
                int mintues = this.ValidTimeSeconds / 60;
                int seconds = this.ValidTimeSeconds % 60;
                return mintues.ToString() + " 分 " + seconds.ToString() + " 秒";
            }
        }

        public string HandleButtonContext
        {
            get
            {
                if (this._parentObject.StonesOrder.OrderState == SellOrderState.Lock)
                {
                    return "申诉";
                }
                if (this._parentObject.StonesOrder.OrderState == SellOrderState.Exception)
                {
                    return "已经申诉";
                }
                return "";
            }
        }

        public System.Windows.Visibility ValidTimeVisibility
        {
            get
            {
                if (this._parentObject.StonesOrder.OrderState == SellOrderState.Lock)
                {
                    return System.Windows.Visibility.Visible;
                }

                return System.Windows.Visibility.Collapsed;
            }
        }

        public bool HandleButtonNotEnable
        {
            get
            {
                if (this._parentObject.StonesOrder.OrderState == SellOrderState.Lock)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
