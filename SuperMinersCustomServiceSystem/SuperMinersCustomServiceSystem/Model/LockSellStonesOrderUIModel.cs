using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperMinersCustomServiceSystem.Model
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
            set { _parentObject = value; }
        }
        
        public string OrderNumber
        {
            get { return this.ParentObject.StonesOrder.OrderNumber; }
        }

        public string SellerUserName
        {
            get { return this.ParentObject.StonesOrder.SellerUserName; }
        }

        public int SellStonesCount
        {
            get { return this.ParentObject.StonesOrder.SellStonesCount; }
        }

        public decimal Expense
        {
            get { return this.ParentObject.StonesOrder.Expense; }
        }

        public decimal ValueRMB
        {
            get { return this.ParentObject.StonesOrder.ValueRMB; }
        }

        public DateTime SellTime
        {
            get { return this.ParentObject.StonesOrder.SellTime; }
        }

        public string OrderStateText
        {
            get
            {
                string stateText = "";
                switch (this.ParentObject.StonesOrder.OrderState)
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
                switch (this.ParentObject.StonesOrder.OrderState)
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
            get { return this.ParentObject.LockedByUserName; }
        }

        public string PayUrl
        {
            get { return this.ParentObject.PayUrl; }
        }

        public DateTime LockedTime
        {
            get { return this.ParentObject.LockedTime; }
        }
    }
}
