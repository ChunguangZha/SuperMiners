using DataBaseProvider;
using MetaData;
using MetaData.Shopping;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller.Trade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Shopping
{
    public class DiamondShoppingController
    {
        #region Single

        private static DiamondShoppingController _instance = new DiamondShoppingController();

        internal static DiamondShoppingController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        public int AddDiamondShoppingItem(DiamondShoppingItem item)
        {
            string filePath = Path.Combine(GlobalData.DiamondShoppingImageFolder, item.Name + ".jpg");
            if (File.Exists(filePath))
            {
                return OperResult.RESULTCODE_SHOPPINGNAME_EXISTED;
            }

            bool isOK = DBProvider.DiamondShoppingDBProvider.AddDiamondShoppingItem(item);
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

        public int UpdateDiamondShoppingItem(DiamondShoppingItem item)
        {
            string filePath = Path.Combine(GlobalData.DiamondShoppingImageFolder, item.Name + ".jpg");

            bool isOK = DBProvider.DiamondShoppingDBProvider.UpdateDiamondShoppingItem(item);
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

        public DiamondShoppingItem[] GetDiamondShoppingItems(bool getAllItem, SellState state)
        {
            DiamondShoppingItem[] items = DBProvider.DiamondShoppingDBProvider.GetDiamondShoppingItems(getAllItem, state);
            if (items == null)
            {
                return items;
            }

            foreach (var item in items)
            {
                string filePath = Path.Combine(GlobalData.DiamondShoppingImageFolder, item.Name + ".jpg");

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

        public int BuyDiamondShoppingItem(int userID, string userName, int itemID, PostAddress address, CustomerMySqlTransaction myTrans)
        {
            DateTime time = DateTime.Now;
            PlayerBuyDiamondShoppingItemRecord record = new PlayerBuyDiamondShoppingItemRecord()
            {
                OrderNumber = OrderController.Instance.CreateOrderNumber(userName, time, AlipayTradeInType.DiamondShopping),
                UserID = userID,
                DiamondShoppingItemID = itemID,
                BuyTime = new MetaData.MyDateTime(time),
                SendAddress = address.ToString(),
                ShoppingState = DiamondShoppingState.Payed
            };

            bool isOK = DBProvider.DiamondShoppingDBProvider.AddPlayerBuyDiamondShoppingItemRecord(record, myTrans);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }
            return OperResult.RESULTCODE_FALSE;
        }

        public int HandleBuyDiamondShopping(PlayerBuyDiamondShoppingItemRecord record)
        {
            DBProvider.DiamondShoppingDBProvider.UpdatePlayerBuyDiamondShoppingItemRecord(record);
            return OperResult.RESULTCODE_TRUE;
        }

        public PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByID(int userID, int itemID, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.DiamondShoppingDBProvider.GetPlayerBuyDiamondShoppingItemRecordByID(userID, itemID, shoppingStateInt, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByName(string playerUserName, string shoppingItemName, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.DiamondShoppingDBProvider.GetPlayerBuyDiamondShoppingItemRecordByName(playerUserName, shoppingItemName, shoppingStateInt, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }
    }
}
