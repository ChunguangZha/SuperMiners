using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SuperMinersWPF.Models
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
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("SellerUserName");
                NotifyPropertyChange("SellStonesCount");
                NotifyPropertyChange("Expense");
                NotifyPropertyChange("ValueRMB");
                NotifyPropertyChange("SellTime");
                NotifyPropertyChange("OrderState");
                NotifyPropertyChange("OrderStateString");
            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.OrderNumber;
            }
        }

        public string SellerUserName
        {
            get
            {
                return this._parentObject.SellerUserName;
            }
        }

        public int SellStonesCount
        {
            get
            {
                return this._parentObject.SellStonesCount;
            }
        }

        public float Expense
        {
            get
            {
                return this._parentObject.Expense;
            }
        }

        public float ValueRMB
        {
            get
            {
                return this._parentObject.ValueRMB;
            }
        }

        public DateTime SellTime
        {
            get
            {
                return this._parentObject.SellTime;
            }
        }

        public SellOrderState OrderState
        {
            get
            {
                return this._parentObject.OrderState;
            }
            set
            {
                this._parentObject.OrderState = value;
                NotifyPropertyChange("OrderState");
                NotifyPropertyChange("OrderStateString");
            }
        }

        public SolidColorBrush OrderStateBrush
        {
            get
            {
                switch (this.OrderState)
                {
                    case SellOrderState.Wait:
                        return new SolidColorBrush(Colors.Black);

                    case SellOrderState.Lock:
                        return new SolidColorBrush(Colors.Gray);

                    case SellOrderState.Finish:
                        return new SolidColorBrush(Colors.Green);

                    case SellOrderState.Exception:
                        return new SolidColorBrush(Colors.Red);

                    default:
                        return new SolidColorBrush(Colors.Black);
                }
            }
        }

        public string OrderStateString
        {
            get
            {
                switch (this.OrderState)
                {
                    case SellOrderState.Wait:
                        return "等待交易...";

                    case SellOrderState.Lock:
                        return "已被锁定";

                    case SellOrderState.Finish:
                        return "交易完成";

                    case SellOrderState.Exception:
                        return "交易异常";

                    default:
                        return "";
                }
            }
        }
    }
}
