using MetaData;
using MetaData.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {
        public event EventHandler<WebInvokeEventArgs<PlayerBuyVirtualShoppingItemRecord[]>> GetPlayerBuyVirtualShoppingItemRecordCompleted;
        public void GetPlayerBuyVirtualShoppingItemRecord(string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<PlayerBuyVirtualShoppingItemRecord[]>(this._context, "GetPlayerBuyVirtualShoppingItemRecord", this.GetPlayerBuyVirtualShoppingItemRecordCompleted, GlobalData.Token, playerUserName, shoppingItemName, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<VirtualShoppingItem[]>> GetVirtualShoppingItemsCompleted;
        public void GetVirtualShoppingItems(bool getAllItem, MetaData.Shopping.SellState state)
        {
            this._invoker.Invoke<VirtualShoppingItem[]>(this._context, "GetVirtualShoppingItems", this.GetVirtualShoppingItemsCompleted, GlobalData.Token, getAllItem, state);
        }

        public event EventHandler<WebInvokeEventArgs<int>> UpdateVirtualShoppingItemCompleted;
        public void UpdateVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            this._invoker.Invoke<int>(this._context, "UpdateVirtualShoppingItem", this.UpdateVirtualShoppingItemCompleted, GlobalData.Token, actionPassword, item);
        }

        public event EventHandler<WebInvokeEventArgs<int>> AddVirtualShoppingItemCompleted;
        public void AddVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            this._invoker.Invoke<int>(this._context, "AddVirtualShoppingItem", this.AddVirtualShoppingItemCompleted, GlobalData.Token, actionPassword, item);
        }

    }
}
