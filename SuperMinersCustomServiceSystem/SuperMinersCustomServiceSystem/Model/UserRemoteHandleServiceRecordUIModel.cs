using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class UserRemoteHandleServiceRecordUIModel : BaseModel
    {
        public UserRemoteHandleServiceRecordUIModel(UserRemoteHandleServiceRecord parent)
        {
            this.ParentObject = parent;
        }

        private UserRemoteHandleServiceRecord _parentObject;

        public UserRemoteHandleServiceRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("ID");
                NotifyPropertyChange("UserName");
                NotifyPropertyChange("ServiceTimeText");
                NotifyPropertyChange("WorkerName");
                NotifyPropertyChange("ServiceContent");
                NotifyPropertyChange("AdminUserName");
            }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public int UserID
        {
            get
            {
                return this._parentObject.UserID;
            }
        }

        public string UserName
        {
            get
            {
                return this._parentObject.UserName;
            }
        }

        public string ServiceTimeText
        {
            get
            {
                if (this._parentObject.ServiceTime == null)
                {
                    return "未服务";
                }
                return this._parentObject.ServiceTime.ToString();
            }
        }

        public string WorkerName
        {
            get
            {
                return this._parentObject.WorkerName;
            }
        }

        public string ServiceContent
        {
            get
            {
                return this._parentObject.ServiceContent;
            }
        }

        public string AdminUserName
        {
            get
            {
                return this._parentObject.AdminUserName;
            }
        }

    }
}
