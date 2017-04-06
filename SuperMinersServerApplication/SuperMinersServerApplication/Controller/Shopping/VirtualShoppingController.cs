using MetaData;
using MetaData.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Shopping
{
    public class VirtualShoppingController
    {
        #region Single

        private static VirtualShoppingController _instance = new VirtualShoppingController();

        internal static VirtualShoppingController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion
        
        public bool AddVirtualShoppingItem(VirtualShoppingItem item)
        {
            return DBProvider.VirtualShoppingItemDBProvider.AddVirtualShoppingItem(item);
        }
        
        public bool UpdateVirtualShoppingItem(VirtualShoppingItem item)
        {
            return DBProvider.VirtualShoppingItemDBProvider.UpdateVirtualShoppingItem(item);
        }

        public VirtualShoppingItem[] GetVirtualShoppingItems(bool getAllItem, SellState state)
        {
            return DBProvider.VirtualShoppingItemDBProvider.GetVirtualShoppingItems(getAllItem, state);
        }

        public int BuyVirtualShoppingItem(int userID, int itemID)
        {
            PlayerBuyVirtualShoppingItemRecord record = new PlayerBuyVirtualShoppingItemRecord()
            {
                UserID = userID,
                VirtualShoppingItemID = itemID,
                BuyTime = new MetaData.MyDateTime(DateTime.Now)
            };

            bool isOK = DBProvider.VirtualShoppingItemDBProvider.AddPlayerBuyVirtualShoppingItemRecord(record);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }
            return OperResult.RESULTCODE_FALSE;
        }

        public bool CheckVirtualShoppingItemBuyable(int userID, VirtualShoppingItem item)
        {
            if (item.PlayerMaxBuyableCount < 0)
            {
                return false;
            }
            int buyCount = DBProvider.VirtualShoppingItemDBProvider.GetPlayerBuyVirtualShoppingItemCount(userID, item.ID);

            return buyCount < item.PlayerMaxBuyableCount;
        }

        public PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecordByID(int userID, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.VirtualShoppingItemDBProvider.GetPlayerBuyVirtualShoppingItemRecordByID(userID, itemID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecordByName(string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.VirtualShoppingItemDBProvider.GetPlayerBuyVirtualShoppingItemRecordByName(playerUserName, shoppingItemName, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }
    }
}
