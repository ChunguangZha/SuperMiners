﻿using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SuperMinersCustomServiceSystem.Model
{
    public class StoneOrderNotFinishedRecordUIModel : BaseModel
    {
        public StoneOrderNotFinishedRecordUIModel(LockSellStonesOrder parent)
        {
            this.LockSellStonesOrderObject = parent;
        }

        public StoneOrderNotFinishedRecordUIModel(SellStonesOrder parent)
        {
            this.SellStonesOrderObject = parent;
        }

        private SellStonesOrder _SellStonesOrderObject;

        public SellStonesOrder SellStonesOrderObject
        {
            get { return _SellStonesOrderObject; }
            set { _SellStonesOrderObject = value; }
        }


        private LockSellStonesOrder _LockSellStonesOrderObject;

        public LockSellStonesOrder LockSellStonesOrderObject
        {
            get { return _LockSellStonesOrderObject; }
            set { _LockSellStonesOrderObject = value; }
        }
        
        public string OrderNumber
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.OrderNumber; }
        }

        public string SellerUserName
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.SellerUserName; }
        }

        public int SellStonesCount
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.SellStonesCount; }
        }

        public decimal Expense
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.Expense; }
        }

        public decimal ValueRMB
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.ValueRMB; }
        }

        public DateTime SellTime
        {
            get { return this.LockSellStonesOrderObject.StonesOrder.SellTime; }
        }

        public string OrderStateText
        {
            get
            {
                string stateText = "";
                switch (this.LockSellStonesOrderObject.StonesOrder.OrderState)
                {
                    case SellOrderState.Wait:
                        stateText = "等待";
                        break;
                    case SellOrderState.Lock:
                        stateText = "锁定";
                        break;
                    case SellOrderState.Finish:
                        stateText = "完成";
                        break;
                    case SellOrderState.Exception:
                        stateText = "异常";
                        break;
                    default:
                        break;
                }

                return stateText;
            }
        }

        public string HandleButtonContext
        {
            get
            {
                string stateText = "";
                switch (this.LockSellStonesOrderObject.StonesOrder.OrderState)
                {
                    case SellOrderState.Wait:
                        stateText = "无需处理";
                        break;
                    case SellOrderState.Lock:
                        stateText = "无需处理";
                        break;
                    case SellOrderState.Finish:
                        stateText = "无需处理";
                        break;
                    case SellOrderState.Exception:
                        stateText = "处理异常";
                        break;
                    default:
                        break;
                }

                return stateText;
            }
        }

        public Visibility HandleButtonVisibility
        {
            get
            {
                if (this.LockSellStonesOrderObject.StonesOrder.OrderState == SellOrderState.Exception)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }

        public Brush OrderStateBackground
        {
            get
            {
                SolidColorBrush brush = null;
                Color backcolor;
                switch (this.LockSellStonesOrderObject.StonesOrder.OrderState)
                {
                    case SellOrderState.Wait:
                        backcolor = Colors.White;
                        break;
                    case SellOrderState.Lock:
                        backcolor = Colors.Yellow;
                        break;
                    case SellOrderState.Finish:
                        backcolor = Colors.Gray;
                        break;
                    case SellOrderState.Exception:
                        backcolor = Colors.Red;
                        break;
                    default:
                        backcolor = Colors.Gray;
                        break;
                }

                brush = new SolidColorBrush(backcolor);
                return brush;
            }
        }

        public string LockedByUserName
        {
            get { return this.LockSellStonesOrderObject.LockedByUserName; }
        }

        public string PayUrl
        {
            get { return this.LockSellStonesOrderObject.PayUrl; }
        }

        public DateTime LockedTime
        {
            get { return this.LockSellStonesOrderObject.LockedTime; }
        }
    }
}
