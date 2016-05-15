using MetaData;
using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    class MetaDBAdapter<T> where T : class
    {
        internal MySqlCommand ConvertToInsertSqlCommand(string tableName, T obj)
        {
            try
            {
                MySqlCommand mycmd = new MySqlCommand();
                string cmdHead = "insert into " + tableName;
                StringBuilder strbuilderColumns = new StringBuilder(" ( ");
                StringBuilder strbuilderParams = new StringBuilder(" ( ");

                Type type = typeof(T);
                FieldInfo[] fields = type.GetFields();
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        Attribute attr = field.GetCustomAttribute(typeof(DatabaseFieldAttribute));
                        if (attr != null)
                        {
                            strbuilderColumns.Append(field.Name + ",");

                            object value = field.GetValue(obj);
                            strbuilderParams.Append("@" + field.Name + ",");
                            mycmd.Parameters.AddWithValue("@" + field.Name, value);
                        }
                    }

                    string cmdText = cmdHead + strbuilderColumns.ToString(0, strbuilderColumns.Length - 1) + ")"
                                     + " values " + strbuilderParams.ToString(0, strbuilderParams.Length - 1) + ")";
                    mycmd.CommandText = cmdText;
                    return mycmd;
                }

                return null;
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        internal static PlayerInfo[] GetPlayerInfoFromDataTable(DataTable dt)
        {
            PlayerInfo[] players = new PlayerInfo[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PlayerInfo player = new PlayerInfo();

                string encryptedUserName = dt.Rows[i]["UserName"].ToString();
                string encryptedUserPassword = dt.Rows[i]["Password"].ToString();
                string encryptedAlipay = dt.Rows[i]["Alipay"] == DBNull.Value ? "" : dt.Rows[i]["Alipay"].ToString();
                string encryptedAlipayRealName = dt.Rows[i]["AlipayRealName"] == DBNull.Value ? "" : dt.Rows[i]["AlipayRealName"].ToString();
                string encryptedInvitationCode = dt.Rows[i]["InvitationCode"].ToString();

                player.SimpleInfo.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                player.SimpleInfo.Password = DESEncrypt.DecryptDES(encryptedUserPassword);
                player.SimpleInfo.Alipay = string.IsNullOrEmpty(encryptedAlipay) ? "" : DESEncrypt.DecryptDES(encryptedAlipay);
                player.SimpleInfo.AlipayRealName = string.IsNullOrEmpty(encryptedAlipayRealName) ? "" : DESEncrypt.DecryptDES(encryptedAlipayRealName);
                player.SimpleInfo.RegisterIP = dt.Rows[i]["RegisterIP"].ToString();
                player.SimpleInfo.InvitationCode = DESEncrypt.DecryptDES(encryptedInvitationCode);
                player.SimpleInfo.RegisterTime = Convert.ToDateTime(dt.Rows[i]["RegisterTime"]);
                if (dt.Rows[i]["LastLoginTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLoginTime = null;
                }
                else
                {
                    player.SimpleInfo.LastLoginTime = Convert.ToDateTime(dt.Rows[i]["LastLoginTime"]);
                }
                if (dt.Rows[i]["LastLogOutTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLogOutTime = null;
                }
                else
                {
                    player.SimpleInfo.LastLogOutTime = Convert.ToDateTime(dt.Rows[i]["LastLogOutTime"]);
                }
                if (dt.Rows[i]["ReferrerUserName"] != DBNull.Value)
                {
                    string encryptedReferrerUserName = dt.Rows[i]["ReferrerUserName"].ToString();
                    player.SimpleInfo.ReferrerUserName = DESEncrypt.DecryptDES(encryptedReferrerUserName);
                }

                player.FortuneInfo.UserName = player.SimpleInfo.UserName;
                player.FortuneInfo.Exp = Convert.ToSingle(dt.Rows[i]["Exp"]);
                player.FortuneInfo.RMB = Convert.ToSingle(dt.Rows[i]["RMB"]);
                player.FortuneInfo.GoldCoin = Convert.ToSingle(dt.Rows[i]["GoldCoin"]);
                player.FortuneInfo.MinesCount = Convert.ToSingle(dt.Rows[i]["MinesCount"]);
                player.FortuneInfo.MinersCount = Convert.ToSingle(dt.Rows[i]["MinersCount"]);
                player.FortuneInfo.StonesReserves = Convert.ToSingle(dt.Rows[i]["StonesReserves"]);
                player.FortuneInfo.TotalProducedStonesCount = Convert.ToSingle(dt.Rows[i]["TotalProducedStonesCount"]);
                player.FortuneInfo.StockOfStones = Convert.ToSingle(dt.Rows[i]["StockOfStones"]);
                if (dt.Rows[i]["TempOutputStonesStartTime"] == DBNull.Value)
                {
                    player.FortuneInfo.TempOutputStonesStartTime = null;
                }
                else
                {
                    player.FortuneInfo.TempOutputStonesStartTime = Convert.ToDateTime(dt.Rows[i]["TempOutputStonesStartTime"]);
                }
                player.FortuneInfo.TempOutputStones = Convert.ToSingle(dt.Rows[i]["TempOutputStones"]);
                player.FortuneInfo.FreezingStones = Convert.ToSingle(dt.Rows[i]["FreezingStones"]);
                player.FortuneInfo.StockOfDiamonds = Convert.ToSingle(dt.Rows[i]["StockOfDiamonds"]);
                player.FortuneInfo.FreezingDiamonds = Convert.ToSingle(dt.Rows[i]["FreezingDiamonds"]);

                players[i] = player;
            }
            dt.Dispose();

            return players;
        }

        internal static SellStonesOrder[] GetSellStonesOrderFromDataTable(DataTable dt)
        {
            SellStonesOrder[] orders = new SellStonesOrder[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SellStonesOrder order = new SellStonesOrder();
                order.OrderNumber = Convert.ToString(dt.Rows[i]["OrderNumber"]);
                string encryptedSellerUserName = dt.Rows[i]["SellerUserName"].ToString();
                order.SellerUserName = DESEncrypt.DecryptDES(encryptedSellerUserName);
                order.SellStonesCount = Convert.ToInt32(dt.Rows[i]["SellStonesCount"]);
                order.Expense = Convert.ToSingle(dt.Rows[i]["Expense"]);
                order.GainRMB = Convert.ToSingle(dt.Rows[i]["GainRMB"]);
                order.SellTime = Convert.ToDateTime(dt.Rows[i]["SellTime"]);
                order.OrderState = (SellOrderState)Convert.ToInt32(dt.Rows[i]["OrderState"]);

                if (order.OrderState != SellOrderState.Wait)
                {
                    if (dt.Rows[i]["BuyerUserName"] != DBNull.Value)
                    {
                        string encryptedBuyerUserName = dt.Rows[i]["BuyerUserName"].ToString();
                        order.LockedByUserName = DESEncrypt.DecryptDES(encryptedBuyerUserName);
                    }
                    if (dt.Rows[i]["LockedTime"] != DBNull.Value)
                    {
                        order.LockedTime = Convert.ToDateTime(dt.Rows[i]["LockedTime"]);
                    }
                }

                orders[i] = order;
            }

            return orders;
        }

    }
}
