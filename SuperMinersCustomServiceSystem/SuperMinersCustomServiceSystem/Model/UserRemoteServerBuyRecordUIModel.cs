using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class UserRemoteServerBuyRecordUIModel : BaseModel
    {
        public UserRemoteServerBuyRecordUIModel(UserRemoteServerBuyRecord parent)
        {
            this.ParentObject = parent;
        }

        private UserRemoteServerBuyRecord _parentObject;

        public UserRemoteServerBuyRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("UserName");
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("ServerTypeText");
                NotifyPropertyChange("PayMoneyYuan");
                NotifyPropertyChange("GetShoppingCredits");
                NotifyPropertyChange("BuyRemoteServerTimeText");
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

        public string ServerTypeText
        {
            get
            {
                string text = "";
                switch (this._parentObject.ServerType)
                {
                    case RemoteServerType.Once:
                        text = "一次";
                        break;
                    case RemoteServerType.OneMonth:
                        text = "一个月";
                        break;
                    case RemoteServerType.ThreeMonth:
                        text = "三个月";
                        break;
                    case RemoteServerType.OneYear:
                        text = "一年";
                        break;
                    default:
                        break;
                }

                return text;
            }
        }

        public int PayMoneyYuan
        {
            get
            {
                return this._parentObject.PayMoneyYuan;
            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.OrderNumber;
            }
        }

        public int GetShoppingCredits
        {
            get
            {
                return this._parentObject.GetShoppingCredits;
            }
        }

        public string BuyRemoteServerTimeText
        {
            get
            {
                if (this._parentObject.BuyRemoteServerTime == null)
                {
                    return "";
                }
                return this._parentObject.BuyRemoteServerTime.ToString();
            }
        }

    }
}
