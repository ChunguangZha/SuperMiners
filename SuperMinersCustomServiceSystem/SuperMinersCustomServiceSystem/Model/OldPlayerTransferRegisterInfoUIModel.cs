using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class OldPlayerTransferRegisterInfoUIModel : BaseModel
    {
        public OldPlayerTransferRegisterInfoUIModel(OldPlayerTransferRegisterInfo parent)
        {
            ParentObject = parent;
        }

        private OldPlayerTransferRegisterInfo _parentObject;

        public OldPlayerTransferRegisterInfo ParentObject
        {
            get { return _parentObject; }
            set { _parentObject = value; }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public string UserName
        {
            get
            {
                return this._parentObject.UserLoginName;
            }
        }

        public string AlipayAccount
        {
            get
            {
                return this._parentObject.AlipayAccount;
            }
        }

        public string AlipayRealName
        {
            get
            {
                return this._parentObject.AlipayRealName;
            }
        }

        public string Email
        {
            get
            {
                return this._parentObject.Email;
            }
        }

        public string NewServerUserLoginName
        {
            get
            {
                return this._parentObject.NewServerUserLoginName;
            }
        }

        public string SubmitTimeText
        {
            get
            {
                if (this._parentObject.SubmitTime != null)
                {
                    return this._parentObject.SubmitTime.ToString();
                }
                return "";
            }
        }

        public bool isTransfered
        {
            get
            {
                return this._parentObject.isTransfered;
            }
        }

        public string HandledTimeText
        {
            get
            {
                if (this._parentObject.HandledTime != null)
                {
                    return this._parentObject.HandledTime.ToString();
                }
                return "";
            }
        }

        public string HandlerName
        {
            get
            {
                return this._parentObject.HandlerName;
            }
        }

    }
}
