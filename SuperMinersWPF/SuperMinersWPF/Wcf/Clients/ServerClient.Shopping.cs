using MetaData;
using MetaData.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        public event EventHandler<WebInvokeEventArgs<PlayerBuyVirtualShoppingItemRecord[]>> GetPlayerBuyVirtualShoppingItemRecordCompleted;
        public void GetPlayerBuyVirtualShoppingItemRecord(int userID, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<PlayerBuyVirtualShoppingItemRecord[]>(this._context, "GetPlayerBuyVirtualShoppingItemRecord", this.GetPlayerBuyVirtualShoppingItemRecordCompleted, GlobalData.Token, userID, itemID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<VirtualShoppingItem[]>> GetVirtualShoppingItemsCompleted;
        public void GetVirtualShoppingItems()
        {
            this._invoker.Invoke<VirtualShoppingItem[]>(this._context, "GetVirtualShoppingItems", this.GetVirtualShoppingItemsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<int>> BuyVirtualShoppingItemCompleted;
        public void BuyVirtualShoppingItem(VirtualShoppingItem shoppingItem)
        {
            this._invoker.Invoke<int>(this._context, "BuyVirtualShoppingItem", this.BuyVirtualShoppingItemCompleted, GlobalData.Token, shoppingItem);
        }

    }
}
