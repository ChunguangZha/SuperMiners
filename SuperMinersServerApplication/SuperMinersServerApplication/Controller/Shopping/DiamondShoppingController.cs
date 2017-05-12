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

        public int AddDiamondShoppingItem(DiamondShoppingItem item, byte[][] detailImagesBuffer)
        {
            string dirPath = GetShoppingItemDirPath(item.Name);
            if (Directory.Exists(dirPath))
            {
                return OperResult.RESULTCODE_SHOPPINGNAME_EXISTED;
            }

            //创建商品文件夹
            Directory.CreateDirectory(dirPath);

            bool isOK = DBProvider.DiamondShoppingDBProvider.AddDiamondShoppingItem(item);
            if (isOK)
            {
                //保存首页
                string titleImgFilePath = Path.Combine(dirPath, item.Name.GetHashCode().ToString() + ".jpg");
                using (FileStream stream = new FileStream(titleImgFilePath, FileMode.Create))
                {
                    stream.Write(item.IconBuffer, 0, item.IconBuffer.Length);
                }

                //保存详情图
                for (int i = 0; i < item.DetailImageNames.Length; i++)
                {
                    string detailImgFilePath = Path.Combine(dirPath, item.DetailImageNames[i] + ".jpg");
                    using (FileStream stream = new FileStream(detailImgFilePath, FileMode.Create))
                    {
                        stream.Write(detailImagesBuffer[i], 0, detailImagesBuffer[i].Length);
                    }
                }

                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int UpdateDiamondShoppingItem(DiamondShoppingItem item, byte[][] detailImagesBuffer)
        {
            string dirPath = GetShoppingItemDirPath(item.Name);
            //删除所有图片，重新保存
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
            Directory.CreateDirectory(dirPath);

            bool isOK = DBProvider.DiamondShoppingDBProvider.UpdateDiamondShoppingItem(item);
            if (isOK)
            {
                string titleImgFilePath = Path.Combine(dirPath, item.Name.GetHashCode().ToString() + ".jpg");
                using (FileStream stream = new FileStream(titleImgFilePath, FileMode.Create))
                {
                    stream.Write(item.IconBuffer, 0, item.IconBuffer.Length);
                }

                for (int i = 0; i < item.DetailImageNames.Length; i++)
                {
                    string detailImgFilePath = Path.Combine(dirPath, item.DetailImageNames[i] + ".jpg");
                    using (FileStream stream = new FileStream(detailImgFilePath, FileMode.Create))
                    {
                        stream.Write(detailImagesBuffer[i], 0, detailImagesBuffer[i].Length);
                    }
                }

                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public string GetShoppingItemDirPath(string itemName)
        {
            string dirPath = Path.Combine(GlobalData.DiamondShoppingImageFolder, "item" + itemName.GetHashCode().ToString());
            return dirPath;
        }

        public byte[][] GetDiamondShoppingItemDetailImageBuffer(int diamondShoppingItemID)
        {
            DiamondShoppingItem item = GetDiamondShoppingItem(diamondShoppingItemID);
            if (item == null || item.DetailImageNames == null)
            {
                return null;
            }

            string dirPath = GetShoppingItemDirPath(item.Name);
            if (!Directory.Exists(dirPath))
            {
                return null;
            }

            string[] files = Directory.GetFiles(dirPath);
            if (files == null || files.Length == 0)
            {
                return null;
            }

            List<byte[]> listImageBuffers = new List<byte[]>();

            //返回ImageBuffer的序列和item.DetailImageNames中名称的顺序完全一致！！
            foreach (var imageFileName in item.DetailImageNames)
            {
                foreach (var fileName in files)
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    if (fileInfo.Name == imageFileName + ".jpg")
                    {
                        using (FileStream stream = File.OpenRead(fileName))
                        {
                            byte[] buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            listImageBuffers.Add(buffer);
                        }
                        break;
                    }
                }
            }

            return listImageBuffers.ToArray();
        }

        public DiamondShoppingItem GetDiamondShoppingItem(int itemID)
        {
            return DBProvider.DiamondShoppingDBProvider.GetDiamondShoppingItem(itemID);
        }

        public DiamondShoppingItem[] GetDiamondShoppingItems(bool getAllSellState, SellState state, DiamondsShoppingItemType itemType)
        {
            DiamondShoppingItem[] items = DBProvider.DiamondShoppingDBProvider.GetDiamondShoppingItems(getAllSellState, state, itemType);
            if (items == null)
            {
                return items;
            }

            foreach (var item in items)
            {
                string filePath = Path.Combine(GetShoppingItemDirPath(item.Name), item.Name.GetHashCode().ToString() + ".jpg");

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
