using MetaData;
using MetaData.Shopping;
using MetaData.User;
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

        public event EventHandler<WebInvokeEventArgs<DiamondShoppingItem[]>> GetDiamondShoppingItemsCompleted;
        public void GetDiamondShoppingItems()
        {
            this._invoker.Invoke<DiamondShoppingItem[]>(this._context, "GetDiamondShoppingItems", this.GetDiamondShoppingItemsCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<byte[][]>> GetDiamondShoppingItemDetailImageBufferCompleted;
        public void GetDiamondShoppingItemDetailImageBuffer(string diamondShoppingItemName)
        {
            this._invoker.Invoke<byte[][]>(this._context, "GetDiamondShoppingItemDetailImageBuffer", this.GetDiamondShoppingItemDetailImageBufferCompleted, GlobalData.Token, diamondShoppingItemName);
        }

        public event EventHandler<WebInvokeEventArgs<int>> BuyDiamondShoppingItemCompleted;
        public void BuyDiamondShoppingItem(DiamondShoppingItem shoppingItem, PostAddress address)
        {
            this._invoker.Invoke<int>(this._context, "BuyDiamondShoppingItem", this.BuyDiamondShoppingItemCompleted, GlobalData.Token, shoppingItem, address);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerBuyDiamondShoppingItemRecord[]>> GetPlayerBuyDiamondShoppingItemRecordCompleted;
        public void GetPlayerBuyDiamondShoppingItemRecord(int userID, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<PlayerBuyDiamondShoppingItemRecord[]>(this._context, "GetPlayerBuyDiamondShoppingItemRecord", this.GetPlayerBuyDiamondShoppingItemRecordCompleted, GlobalData.Token, userID, itemID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

    }
}
