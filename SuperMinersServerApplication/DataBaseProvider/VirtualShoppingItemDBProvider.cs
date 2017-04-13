using MetaData;
using MetaData.Shopping;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class VirtualShoppingItemDBProvider
    {
        public bool AddVirtualShoppingItem(VirtualShoppingItem item)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into virtualshoppingitem " + 
                    "(`Name`,`Remark`,`SellState`,`PlayerMaxBuyableCount`,`ValueRMB`,`GainExp`,`GainRMB`,`GainGoldCoin`,"+
                    "`GainMine_StoneReserves`,`GainMiner`,`GainStone`,`GainDiamond`,`GainShoppingCredits`,`GainGravel`) " +
                    " values (@Name,@Remark,@SellState,@PlayerMaxBuyableCount,@ValueRMB,@GainExp,@GainRMB,@GainGoldCoin," +
                    "@GainMine_StoneReserves,@GainMiner,@GainStone,@GainDiamond,@GainShoppingCredits,@GainGravel )";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Name", item.Name);
                mycmd.Parameters.AddWithValue("@Remark", item.Remark);
                mycmd.Parameters.AddWithValue("@SellState", item.SellState);
                mycmd.Parameters.AddWithValue("@PlayerMaxBuyableCount", item.PlayerMaxBuyableCount);
                mycmd.Parameters.AddWithValue("@ValueRMB", item.ValueRMB);
                mycmd.Parameters.AddWithValue("@GainExp", item.GainExp);
                mycmd.Parameters.AddWithValue("@GainRMB", item.GainRMB);
                mycmd.Parameters.AddWithValue("@GainGoldCoin", item.GainGoldCoin);
                mycmd.Parameters.AddWithValue("@GainMine_StoneReserves", item.GainMine_StoneReserves);
                mycmd.Parameters.AddWithValue("@GainMiner", item.GainMiner);
                mycmd.Parameters.AddWithValue("@GainStone", item.GainStone);
                mycmd.Parameters.AddWithValue("@GainDiamond", item.GainDiamond);
                mycmd.Parameters.AddWithValue("@GainShoppingCredits", item.GainShoppingCredits);
                mycmd.Parameters.AddWithValue("@GainGravel", item.GainGravel);

                mycmd.ExecuteNonQuery();

            });
        }

        public bool UpdateVirtualShoppingItem(VirtualShoppingItem item)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "update virtualshoppingitem " +
                    " set `Name`=@Name,`Remark`=@Remark,`SellState`=@SellState,`PlayerMaxBuyableCount`=@PlayerMaxBuyableCount,"+
                    "`ValueRMB`=@ValueRMB,`GainExp`=@GainExp,`GainRMB`=@GainRMB,`GainGoldCoin`=@GainGoldCoin," +
                    "`GainMine_StoneReserves`=@GainMine_StoneReserves,`GainMiner`=@GainMiner,`GainStone`=@GainStone,"+
                    "`GainDiamond`=@GainDiamond,`GainShoppingCredits`=@GainShoppingCredits,`GainGravel`=@GainGravel " +
                    " where `ID`=@ID;";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Name", item.Name);
                mycmd.Parameters.AddWithValue("@Remark", item.Remark);
                mycmd.Parameters.AddWithValue("@SellState", item.SellState);
                mycmd.Parameters.AddWithValue("@PlayerMaxBuyableCount", item.PlayerMaxBuyableCount);
                mycmd.Parameters.AddWithValue("@ValueRMB", item.ValueRMB);
                mycmd.Parameters.AddWithValue("@GainExp", item.GainExp);
                mycmd.Parameters.AddWithValue("@GainRMB", item.GainRMB);
                mycmd.Parameters.AddWithValue("@GainGoldCoin", item.GainGoldCoin);
                mycmd.Parameters.AddWithValue("@GainMine_StoneReserves", item.GainMine_StoneReserves);
                mycmd.Parameters.AddWithValue("@GainMiner", item.GainMiner);
                mycmd.Parameters.AddWithValue("@GainStone", item.GainStone);
                mycmd.Parameters.AddWithValue("@GainDiamond", item.GainDiamond);
                mycmd.Parameters.AddWithValue("@GainShoppingCredits", item.GainShoppingCredits);
                mycmd.Parameters.AddWithValue("@GainGravel", item.GainGravel);
                mycmd.Parameters.AddWithValue("@ID", item.ID);

                mycmd.ExecuteNonQuery();

            });
        }

        public VirtualShoppingItem[] GetVirtualShoppingItems(bool getAllItem, SellState state)
        {
            VirtualShoppingItem[] items = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from virtualshoppingitem ";
                if (!getAllItem)
                {
                    sqlText += " where SellState=@SellState ";
                    mycmd.Parameters.AddWithValue("@SellState", (int)state);
                }
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<VirtualShoppingItem>.GetVirtualShoppingItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

        public VirtualShoppingItem GetVirtualShoppingItem(int itemID)
        {
            VirtualShoppingItem[] items = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from virtualshoppingitem where ID=@ID ";

                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@ID", itemID);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<VirtualShoppingItem>.GetVirtualShoppingItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            if (items == null || items.Length == 0)
            {
                return null;
            }
            return items[0];
        }

        public bool AddPlayerBuyVirtualShoppingItemRecord(PlayerBuyVirtualShoppingItemRecord record)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into playerbuyvirtualshoppingitemrecord " +
                    "(`OrderNumber`,`UserID`,`VirtualShoppingItemID`,`BuyTime`) " +
                    " values (@OrderNumber,@UserID,@VirtualShoppingItemID,@BuyTime )";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@VirtualShoppingItemID", record.VirtualShoppingItemID);
                mycmd.Parameters.AddWithValue("@BuyTime", record.BuyTime.ToDateTime());

                mycmd.ExecuteNonQuery();

            });
        }

        public int GetPlayerBuyVirtualShoppingItemCount(int userID, int itemID)
        {
            int count = 0;
            bool isOK = MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlTextA = "SELECT count(ID) FROM  playerbuyvirtualshoppingitemrecord where UserID=@UserID and VirtualShoppingItemID=@VirtualShoppingItemID ";
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                mycmd.Parameters.AddWithValue("@VirtualShoppingItemID", itemID);
                count = Convert.ToInt32(mycmd.ExecuteScalar());
            });

            return count;
        }

        public PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecordByName(string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            PlayerBuyVirtualShoppingItemRecord[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  playerbuyvirtualshoppingitemrecord n ";

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
                    builder.Append(" n.VirtualShoppingItemID = (SELECT `ID` FROM virtualshoppingitem where `Name`=@ItemName) ");
                    mycmd.Parameters.AddWithValue("@ItemName", shoppingItemName);
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

                string sqlAllText = "select ttt.*, s.UserName as UserName, v.Name as VirtualShoppingItemName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join playersimpleinfo s on ttt.UserID = s.id " +
                                    "  left join virtualshoppingitem v on ttt.VirtualShoppingItemID=v.ID";

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<PlayerBuyVirtualShoppingItemRecord>.GetPlayerBuyVirtualShoppingItemRecordFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

        public PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecordByID(int userID, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            PlayerBuyVirtualShoppingItemRecord[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  playerbuyvirtualshoppingitemrecord n ";

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
                    builder.Append(" n.VirtualShoppingItemID = @VirtualShoppingItemID ");
                    mycmd.Parameters.AddWithValue("@VirtualShoppingItemID", itemID);
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

                string sqlAllText = "select ttt.*, s.UserName as UserName, v.Name as VirtualShoppingItemName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join playersimpleinfo s on ttt.UserID = s.id " +
                                    "  left join virtualshoppingitem v on ttt.VirtualShoppingItemID=v.ID";

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<PlayerBuyVirtualShoppingItemRecord>.GetPlayerBuyVirtualShoppingItemRecordFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

    }
}
