using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.UIModel
{
    public class AdminUIModel : BaseUIModel
    {
        public AdminUIModel(AdminInfo parent)
        {
            this.ParentObject = parent;
        }

        private AdminInfo _parentObject;

        public AdminInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChanged("IsChecked");
                NotifyPropertyChanged("UserName");
                NotifyPropertyChanged("LoginPassword");
                NotifyPropertyChanged("ActionPassword");
                NotifyPropertyChanged("Mac");
            }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }


        public string UserName
        {
            get { return this.ParentObject.UserName; }
        }

        public string LoginPassword
        {
            get { return this.ParentObject.LoginPassword; }
            set
            {
                this.ParentObject.LoginPassword = value;
            }
        }

        public string ActionPassword
        {
            get { return this.ParentObject.ActionPassword; }
            set
            {
                this.ParentObject.ActionPassword = value;
            }
        }

        public string Mac
        {
            get
            {
                string m = "";
                foreach (var item in this.ParentObject.Macs)
                {
                    m += item;
                    m += ",";
                }

                if (m.Length > 0)
                {
                    return m.Substring(0, m.Length - 1);
                }

                return "";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.ParentObject.Macs = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
        }
    }
}
