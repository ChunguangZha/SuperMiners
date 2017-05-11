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

        public event EventHandler<WebInvokeEventArgs<int>> AddDiamondShoppingItemCompleted;
        public void AddDiamondShoppingItem(string actionPassword, DiamondShoppingItem item, byte[][] detailImagesBuffer)
        {
            this._invoker.Invoke<int>(this._context, "AddDiamondShoppingItem", this.AddDiamondShoppingItemCompleted, GlobalData.Token, actionPassword, item, detailImagesBuffer);
        }

        public event EventHandler<WebInvokeEventArgs<int>> UpdateDiamondShoppingItemCompleted;
        public void UpdateDiamondShoppingItem(string actionPassword, DiamondShoppingItem item, byte[][] detailImagesBuffer)
        {
            this._invoker.Invoke<int>(this._context, "UpdateDiamondShoppingItem", this.UpdateDiamondShoppingItemCompleted, GlobalData.Token, actionPassword, item, detailImagesBuffer);
        }

        public event EventHandler<WebInvokeEventArgs<DiamondShoppingItem[]>> GetDiamondShoppingItemsCompleted;
        public void GetDiamondShoppingItems(bool getAllSellState, MetaData.Shopping.SellState state, DiamondsShoppingItemType itemType)
        {
            this._invoker.Invoke<DiamondShoppingItem[]>(this._context, "GetDiamondShoppingItems", this.GetDiamondShoppingItemsCompleted, GlobalData.Token, getAllSellState, state, itemType);
        }

        public event EventHandler<WebInvokeEventArgs<byte[][]>> GetDiamondShoppingItemDetailImageBufferCompleted;
        public void GetDiamondShoppingItemDetailImageBuffer(int diamondShoppingItemID)
        {
            this._invoker.Invoke<byte[][]>(this._context, "GetDiamondShoppingItemDetailImageBuffer", this.GetDiamondShoppingItemDetailImageBufferCompleted, GlobalData.Token, diamondShoppingItemID);
        }

        public event EventHandler<WebInvokeEventArgs<int>> HandleBuyDiamondShoppingCompleted;
        public void HandleBuyDiamondShopping(string actionPassword, PlayerBuyDiamondShoppingItemRecord record)
        {
            this._invoker.Invoke<int>(this._context, "HandleBuyDiamondShopping", this.HandleBuyDiamondShoppingCompleted, GlobalData.Token, actionPassword, record);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerBuyDiamondShoppingItemRecord[]>> GetPlayerBuyDiamondShoppingItemRecordByNameCompleted;
        public void GetPlayerBuyDiamondShoppingItemRecordByName(string playerUserName, string shoppingItemName, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<PlayerBuyDiamondShoppingItemRecord[]>(this._context, "GetPlayerBuyDiamondShoppingItemRecordByName", this.GetPlayerBuyDiamondShoppingItemRecordByNameCompleted, GlobalData.Token, playerUserName, shoppingItemName, shoppingStateInt, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

    }
}
