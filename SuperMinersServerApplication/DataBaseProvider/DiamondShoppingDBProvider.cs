using MetaData;
using MetaData.Shopping;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class DiamondShoppingDBProvider
    {
        public bool AddDiamondShoppingItem(DiamondShoppingItem item)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into diamondshoppingitem " +
                    "(`Name`,`Type`,`Remark`,`SellState`,`ValueDiamonds`,`DetailText`,`DetailImageNames` ) " +
                    " values (@Name,@Type,@Remark,@SellState,@ValueDiamonds,@DetailText,@DetailImageNames )";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Name", item.Name);
                mycmd.Parameters.AddWithValue("@Type", (int)item.Type);
                mycmd.Parameters.AddWithValue("@Remark", item.Remark);
                mycmd.Parameters.AddWithValue("@SellState", (int)item.SellState);
                mycmd.Parameters.AddWithValue("@ValueDiamonds", item.ValueDiamonds);
                mycmd.Parameters.AddWithValue("@DetailText", item.DetailText);

                StringBuilder builderDetailImageNames = new StringBuilder();
                if (item.DetailImageNames != null)
                {
                    for (int i = 0; i < item.DetailImageNames.Length; i++)
                    {
                        builderDetailImageNames.Append(item.DetailImageNames[i]);
                        builderDetailImageNames.Append(";");
                    }
                }
                string detailImageNames = "";
                if (builderDetailImageNames.Length > 0)
                {
                    detailImageNames = builderDetailImageNames.ToString(0, builderDetailImageNames.Length - 1);
                }
                mycmd.Parameters.AddWithValue("@DetailImageNames", detailImageNames);

                mycmd.ExecuteNonQuery();

            });
        }

        public bool UpdateDiamondShoppingItem(DiamondShoppingItem item)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "update diamondshoppingitem " +
                    "set `Name`=@Name,`Type`=@Type,`Remark`=@Remark,`SellState`=@SellState,`ValueDiamonds`=@ValueDiamonds,`DetailText`=@DetailText,`DetailImageNames`=@DetailImageNames where `ID`=@ID;";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Name", item.Name);
                mycmd.Parameters.AddWithValue("@Type", (int)item.Type);
                mycmd.Parameters.AddWithValue("@Remark", item.Remark);
                mycmd.Parameters.AddWithValue("@SellState", (int)item.SellState);
                mycmd.Parameters.AddWithValue("@ValueDiamonds", item.ValueDiamonds);
                mycmd.Parameters.AddWithValue("@DetailText", item.DetailText);

                StringBuilder builderDetailImageNames = new StringBuilder();
                if (item.DetailImageNames != null)
                {
                    for (int i = 0; i < item.DetailImageNames.Length; i++)
                    {
                        builderDetailImageNames.Append(item.DetailImageNames[i]);
                        builderDetailImageNames.Append(";");
                    }
                }
                string detailImageNames = "";
                if (builderDetailImageNames.Length > 0)
                {
                    detailImageNames = builderDetailImageNames.ToString(0, builderDetailImageNames.Length - 1);
                }
                mycmd.Parameters.AddWithValue("@DetailImageNames", detailImageNames);

                mycmd.Parameters.AddWithValue("@ID", item.ID);

                mycmd.ExecuteNonQuery();

            });
        }

        public DiamondShoppingItem[] GetDiamondShoppingItems(bool getAllSellState, SellState state, DiamondsShoppingItemType itemType)
        {
            DiamondShoppingItem[] items = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from diamondshoppingitem where `Type`=@Type ";
                mycmd.Parameters.AddWithValue("@Type", (int)itemType);
                if (!getAllSellState)
                {
                    sqlText += " and SellState=@SellState ";
                    mycmd.Parameters.AddWithValue("@SellState", (int)state);
                }
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<DiamondShoppingItem>.GetDiamondShoppingItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

        public DiamondShoppingItem GetDiamondShoppingItem(int itemID)
        {
            DiamondShoppingItem[] items = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from diamondshoppingitem where `ID`=@ID ";
                mycmd.Parameters.AddWithValue("@ID", itemID);
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<DiamondShoppingItem>.GetDiamondShoppingItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            if (items == null || items.Length == 0)
            {
                return null;
            }
            return items[0];
        }

        public bool AddPlayerBuyDiamondShoppingItemRecord(PlayerBuyDiamondShoppingItemRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into playerbuydiamondshoppingitemrecord " +
                    "(`OrderNumber`,`UserID`,`DiamondShoppingItemID`,`SendAddress`,`BuyTime`,`ShoppingState`) " +
                    " values (@OrderNumber,@UserID,@DiamondShoppingItemID,@SendAddress,@BuyTime,@ShoppingState )";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@DiamondShoppingItemID", record.DiamondShoppingItemID);
                mycmd.Parameters.AddWithValue("@SendAddress", DESEncrypt.EncryptDES(record.SendAddress));
                mycmd.Parameters.AddWithValue("@BuyTime", record.BuyTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@ShoppingState", (int)record.ShoppingState);

                mycmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public bool UpdatePlayerBuyDiamondShoppingItemRecord(PlayerBuyDiamondShoppingItemRecord record)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "update playerbuydiamondshoppingitemrecord " +
                    " set `ShoppingState`=@ShoppingState,`ExpressCompany`=@ExpressCompany,`ExpressNumber`=@ExpressNumber,`OperAdmin`=@OperAdmin,`OperTime`=@OperTime,`ShoppingState`=@ShoppingState " +
                    " where `ID`=@ID ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@ShoppingState", (int)record.ShoppingState);
                mycmd.Parameters.AddWithValue("@ShoppingState", (int)record.ShoppingState);
                mycmd.Parameters.AddWithValue("@ExpressCompany", record.ExpressCompany);
                mycmd.Parameters.AddWithValue("@ExpressNumber", record.ExpressNumber);
                mycmd.Parameters.AddWithValue("@OperAdmin", record.OperAdmin);
                mycmd.Parameters.AddWithValue("@OperTime", record.OperTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@ID", record.ID);

                mycmd.ExecuteNonQuery();

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerUserName"></param>
        /// <param name="shoppingItemName"></param>
        /// <param name="shoppingStateInt">-1表示全部</param>
        /// <param name="beginBuyTime"></param>
        /// <param name="endBuyTime"></param>
        /// <param name="pageItemCount"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByName(string playerUserName, string shoppingItemName, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            PlayerBuyDiamondShoppingItemRecord[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  playerbuydiamondshoppingitemrecord n ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" n.UserID = (select `id` from playersimpleinfo where `UserName` = @UserName) ");
                    mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(playerUserName));
                }
                if (!string.IsNullOrEmpty(shoppingItemName))
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" n.DiamondShoppingItemID = (SELECT `ID` FROM diamondshoppingitem where `Name`=@ItemName) ");
                    mycmd.Parameters.AddWithValue("@ItemName", shoppingItemName);
                }
                if (shoppingStateInt >= 0)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" n.ShoppingState = @ShoppingState ");
                    mycmd.Parameters.AddWithValue("@ShoppingState", shoppingStateInt);
                }
                if (beginBuyTime != null && !beginBuyTime.IsNull && endBuyTime != null && !endBuyTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginBuyTime.ToDateTime();
                    DateTime endTime = endBuyTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return;
                    }
                    builder.Append(" n.BuyTime >= @beginTime and n.BuyTime < @endTime ");
                    mycmd.Parameters.AddWithValue("@beginTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by n.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName, v.Name as DiamondShoppingItemName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join playersimpleinfo s on ttt.UserID = s.id " +
                                    "  left join diamondshoppingitem v on ttt.DiamondShoppingItemID=v.ID";

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<PlayerBuyDiamondShoppingItemRecord>.GetPlayerBuyDiamondShoppingItemRecordFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

        public PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByID(int userID, int itemID, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            PlayerBuyDiamondShoppingItemRecord[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  playerbuydiamondshoppingitemrecord n ";

                StringBuilder builder = new StringBuilder();
                if (userID > 0)
                {
                    builder.Append(" n.UserID = @UserID ");
                    mycmd.Parameters.AddWithValue("@UserID", userID);
                }
                if (itemID > 0)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" n.DiamondShoppingItemID = @DiamondShoppingItemID ");
                    mycmd.Parameters.AddWithValue("@DiamondShoppingItemID", itemID);
                }
                if (shoppingStateInt >= 0)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" n.ShoppingState = @ShoppingState ");
                    mycmd.Parameters.AddWithValue("@ShoppingState", shoppingStateInt);
                }
                if (beginBuyTime != null && !beginBuyTime.IsNull && endBuyTime != null && !endBuyTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginBuyTime.ToDateTime();
                    DateTime endTime = endBuyTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return;
                    }
                    builder.Append(" n.BuyTime >= @beginTime and n.BuyTime < @endTime ");
                    mycmd.Parameters.AddWithValue("@beginTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by n.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName, v.Name as DiamondShoppingItemName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join playersimpleinfo s on ttt.UserID = s.id " +
                                    "  left join diamondshoppingitem v on ttt.DiamondShoppingItemID=v.ID";

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<PlayerBuyDiamondShoppingItemRecord>.GetPlayerBuyDiamondShoppingItemRecordFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }


    }
}
