using MetaData.Trade;
using System;
#if Client
using SuperMinersWPF.Models;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.Model
{
    public class WithdrawRMBRecordUIModel : BaseModel
    {
        public WithdrawRMBRecordUIModel(WithdrawRMBRecord parent)
        {
            this.ParentObject = parent;
        }

        private WithdrawRMBRecord _parentObject;

        public WithdrawRMBRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("IsPayedSucceed");
                NotifyPropertyChange("PayButtonVisibility");
                NotifyPropertyChange("AdminUserName");
                NotifyPropertyChange("PayTimeString");
            }
        }

        public int ID
        {
            get { return this._parentObject.id; }
        }

        public decimal WidthdrawRMB
        {
            get
            {
                return this._parentObject.WidthdrawRMB;
            }
        }

        public int ValueYuan
        {
            get
            {
                return this._parentObject.ValueYuan;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return this._parentObject.CreateTime;
            }
        }

        public bool IsPayedSucceed
        {
            get
            {
                return this._parentObject.IsPayedSucceed;
            }
        }

        public Visibility PayButtonVisibility
        {
            get
            {
                return this.IsPayedSucceed ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public string AdminUserName
        {
            get
            {
                return this._parentObject.AdminUserName;
            }
        }

        public string AlipayOrderNumber
        {
            get
            {
                return this._parentObject.AlipayOrderNumber;
            }
        }

        public string PayTimeString
        {
            get
            {
                return this._parentObject.PayTime.ToString();
            }
        }

    }
}
