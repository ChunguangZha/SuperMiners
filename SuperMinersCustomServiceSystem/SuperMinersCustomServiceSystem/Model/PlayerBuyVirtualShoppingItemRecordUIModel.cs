using MetaData.Shopping;
#if Client
using SuperMinersWPF.Models;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Model
{
    public class PlayerBuyVirtualShoppingItemRecordUIModel : BaseModel
    {
        public PlayerBuyVirtualShoppingItemRecordUIModel(PlayerBuyVirtualShoppingItemRecord parent)
        {
            this.ParentObject = parent;
        }

        private PlayerBuyVirtualShoppingItemRecord _parentObject;

        public PlayerBuyVirtualShoppingItemRecord ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                NotifyPropertyChange("ID");
                NotifyPropertyChange("OrderNumber");
                NotifyPropertyChange("UserName");
                NotifyPropertyChange("VirtualShoppingItemName");
                NotifyPropertyChange("BuyTimeText");
            }
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public string OrderNumber
        {
            get
            {
                return this._parentObject.OrderNumber;
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

        public int VirtualShoppingItemID
        {
            get
            {
                return this._parentObject.VirtualShoppingItemID;
            }
        }

        public string VirtualShoppingItemName
        {
            get
            {
                return this._parentObject.VirtualShoppingItemName;
            }
        }

        public string BuyTimeText
        {
            get
            {
                return this._parentObject.BuyTime.ToString();
            }
        }

    }
}
