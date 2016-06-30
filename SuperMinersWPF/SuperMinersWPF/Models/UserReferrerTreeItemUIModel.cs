using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class UserReferrerTreeItemUIModel : BaseModel
    {
        public UserReferrerTreeItemUIModel(UserReferrerTreeItem parent)
        {
            this.ParentObject = parent;
        }

        private UserReferrerTreeItem _parentObject;

        public UserReferrerTreeItem ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("Level");
                NotifyPropertyChange("UserName");
                NotifyPropertyChange("NickName");
                NotifyPropertyChange("RegisterIP");
                NotifyPropertyChange("RegisterTime");
            }
        }

        public int Level
        {
            get { return this._parentObject.Level; }
        }

        public string UserName
        {
            get { return this._parentObject.UserName; }
        }

        public string NickName
        {
            get { return this._parentObject.NickName; }
        }

        public string RegisterIP
        {
            get { return this._parentObject.RegisterIP; }
        }

        public DateTime RegisterTime
        {
            get
            {
                if (this._parentObject.RegisterTime == null)
                {
                    return DateTime.MinValue;
                }
                return this._parentObject.RegisterTime;
            }
        }

        public string RegisterTimeString
        {
            get
            {
                return this.RegisterTime.ToLongDateString();
            }
        }
    }
}
