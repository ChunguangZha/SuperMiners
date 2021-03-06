﻿using MetaData.Trade;
using System;
#if Client
using SuperMinersWPF.Models;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperMinersCustomServiceSystem.Model
{
    public class SellStonesOrderUIModel : BaseModel
    {
        public SellStonesOrderUIModel(SellStonesOrder parent)
        {
            this.ParentObject = parent;
        }

        private SellStonesOrder _parentObject;

        public SellStonesOrder ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
            }
        }

        public string OrderNumber
        {
            get { return this.ParentObject.OrderNumber; }
        }

        public string SellerUserName
        {
            get { return this.ParentObject.SellerUserName; }
        }

        public int SellStonesCount
        {
            get { return this.ParentObject.SellStonesCount; }
        }

        public decimal Expense
        {
            get { return this.ParentObject.Expense; }
        }

        public decimal ValueRMB
        {
            get { return this.ParentObject.ValueRMB; }
        }

        public DateTime SellTime
        {
            get { return this.ParentObject.SellTime; }
        }

        public SellOrderState OrderState
        {
            get { return this.ParentObject.OrderState; }
            set
            {
                this.ParentObject.OrderState = value;
            }
        }

        public string OrderStateText
        {
            get
            {
                string stateText = "";
                switch (this.ParentObject.OrderState)
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

        public Brush OrderStateBackground
        {
            get
            {
                SolidColorBrush brush = null;
                Color backcolor;
                switch (this.ParentObject.OrderState)
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
    }
}
