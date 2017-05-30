using DataBaseProvider;
using MetaData;
using MetaData.Shopping;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
using System;
using System.Collections.Generic;
using System.IO;
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
        
        public int AddVirtualShoppingItem(VirtualShoppingItem item)
        {
            string filePath = Path.Combine(GlobalData.VirtualShoppingImageFolder, item.Name + ".jpg");
            if (File.Exists(filePath))
            {
                return OperResult.RESULTCODE_SHOPPINGNAME_EXISTED;
            }

            bool isOK = DBProvider.VirtualShoppingItemDBProvider.AddVirtualShoppingItem(item);
            if (isOK)
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(item.IconBuffer, 0, item.IconBuffer.Length);
                }

                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int UpdateVirtualShoppingItem(VirtualShoppingItem item)
        {
            string filePath = Path.Combine(GlobalData.VirtualShoppingImageFolder, item.Name + ".jpg");

            bool isOK = DBProvider.VirtualShoppingItemDBProvider.UpdateVirtualShoppingItem(item);
            if (isOK)
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(item.IconBuffer, 0, item.IconBuffer.Length);
                }

                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public VirtualShoppingItem[] GetVirtualShoppingItems(bool getAllItem, SellState state)
        {
            VirtualShoppingItem[] items = DBProvider.VirtualShoppingItemDBProvider.GetVirtualShoppingItems(getAllItem, state);
            if (items == null)
            {
                return items;
            }

            foreach (var item in items)
            {
                string filePath = Path.Combine(GlobalData.VirtualShoppingImageFolder, item.Name + ".jpg");

                if (File.Exists(filePath))
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        item.IconBuffer = new byte[stream.Length];
                        stream.Read(item.IconBuffer, 0, (int)stream.Length);
                    }
                }
            }

            return items;
        }

        public int BuyVirtualShoppingItem(int userID, string userName, VirtualShoppingItem shoppingItem, CustomerMySqlTransaction myTrans)
        {
            PlayerBuyVirtualShoppingItemRecord record = null;
            DateTime time = DateTime.Now;
            if (shoppingItem.ItemType == VirtualShoppingItemType.FactorySlaveFoods30Days)
            {
                int result = StoneFactoryController.Instance.AddFoods(userID, 30, myTrans);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    return result;
                }

                record = new PlayerBuyVirtualShoppingItemRecord()
                {
                    OrderNumber = OrderController.Instance.CreateOrderNumber(userName, time, AlipayTradeInType.VirtualShopping),
                    UserID = userID,
                    VirtualShoppingItemID = shoppingItem.ID,
                    VirtualShoppingItemName = shoppingItem.Name,
                    BuyTime = new MetaData.MyDateTime(time),
                    UserName = userName,
                };
            }
            else if (shoppingItem.ItemType == VirtualShoppingItemType.FactoryOpenTool)
            {
                int result = StoneFactoryController.Instance.OpenFactory(userID, myTrans);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    return result;
                }

                record = new PlayerBuyVirtualShoppingItemRecord()
                {
                    OrderNumber = OrderController.Instance.CreateOrderNumber(userName, time, AlipayTradeInType.VirtualShopping),
                    UserID = userID,
                    VirtualShoppingItemID = shoppingItem.ID,
                    VirtualShoppingItemName = shoppingItem.Name,
                    BuyTime = new MetaData.MyDateTime(time),
                    UserName = userName,
                };
            }
            else
            {
                record = new PlayerBuyVirtualShoppingItemRecord()
                {
                    OrderNumber = OrderController.Instance.CreateOrderNumber(userName, time, AlipayTradeInType.VirtualShopping),
                    UserID = userID,
                    VirtualShoppingItemID = shoppingItem.ID,
                    BuyTime = new MetaData.MyDateTime(time)
                };
            }

            bool isOK = DBProvider.VirtualShoppingItemDBProvider.AddPlayerBuyVirtualShoppingItemRecord(record, myTrans);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }
            return OperResult.RESULTCODE_FALSE;
        }

        public bool CheckVirtualShoppingItemBuyable(int userID, VirtualShoppingItem item)
        {
            //小于等于0表示不限购
            if (item.PlayerMaxBuyableCount <= 0)
            {
                return true;
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
