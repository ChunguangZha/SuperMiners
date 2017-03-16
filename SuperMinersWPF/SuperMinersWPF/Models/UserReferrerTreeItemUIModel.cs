using MetaData.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int UserID
        {
            get { return this._parentObject.UserID; }
        }

        public string UserName
        {
            get { return this._parentObject.UserName; }
        }

        public int ParentUserID
        {
            get { return this._parentObject.ParentUserID; }
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

        private ObservableCollection<UserReferrerTreeItemUIModel> _listDownRefrerrerTree = new ObservableCollection<UserReferrerTreeItemUIModel>();

        /// <summary>
        /// 下线
        /// </summary>
        public ObservableCollection<UserReferrerTreeItemUIModel> ListDownRefrerrerTree
        {
            get { return _listDownRefrerrerTree; }
            set { _listDownRefrerrerTree = value; }
        }

    }
}
