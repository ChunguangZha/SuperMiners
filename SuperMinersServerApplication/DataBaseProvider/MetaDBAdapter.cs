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
                            mycmd.Parameters.Add("@" + field.Name, value);
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

        internal static PlayerInfo GetPlayerInfoFromDataTable(DataTable dt)
        {
            PlayerInfo player = null;
            if (dt.Rows.Count != 0)
            {
                player = new PlayerInfo();
                string encryptedUserName = dt.Rows[0]["UserName"].ToString();
                string encryptedUserPassword = dt.Rows[0]["Password"].ToString();
                string encryptedAlipay = dt.Rows[0]["Alipay"] == DBNull.Value ? "" : dt.Rows[0]["Alipay"].ToString();
                string encryptedAlipayRealName = dt.Rows[0]["AlipayRealName"] == DBNull.Value ? "" : dt.Rows[0]["AlipayRealName"].ToString();
                string encryptedInvitationCode = dt.Rows[0]["InvitationCode"].ToString();

                player.SimpleInfo.UserName = DESEncrypt.DecryptDES(encryptedUserName);
                player.SimpleInfo.Password = DESEncrypt.DecryptDES(encryptedUserPassword);
                player.SimpleInfo.Alipay = string.IsNullOrEmpty(encryptedAlipay) ? "" : DESEncrypt.DecryptDES(encryptedAlipay);
                player.SimpleInfo.AlipayRealName = string.IsNullOrEmpty(encryptedAlipayRealName) ? "" : DESEncrypt.DecryptDES(encryptedAlipayRealName);
                player.SimpleInfo.RegisterIP = dt.Rows[0]["RegisterIP"].ToString();
                player.SimpleInfo.InvitationCode = DESEncrypt.DecryptDES(encryptedInvitationCode);
                player.SimpleInfo.RegisterTime = Convert.ToDateTime(dt.Rows[0]["RegisterTime"]);
                if (dt.Rows[0]["LastLoginTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLoginTime = PlayerSimpleInfo.INVALIDDATETIME;
                }
                else
                {
                    player.SimpleInfo.LastLoginTime = Convert.ToDateTime(dt.Rows[0]["LastLoginTime"]);
                }
                if (dt.Rows[0]["LastLogOutTime"] == DBNull.Value)
                {
                    player.SimpleInfo.LastLogOutTime = PlayerSimpleInfo.INVALIDDATETIME;
                }
                else
                {
                    player.SimpleInfo.LastLogOutTime = Convert.ToDateTime(dt.Rows[0]["LastLogOutTime"]);
                }
                if (dt.Rows[0]["ReferrerUserName"] != DBNull.Value)
                {
                    player.SimpleInfo.ReferrerUserName = dt.Rows[0]["ReferrerUserName"].ToString();
                }

                player.FortuneInfo.UserName = player.SimpleInfo.UserName;
                player.FortuneInfo.Exp = Convert.ToSingle(dt.Rows[0]["Exp"]);
                player.FortuneInfo.RMB = Convert.ToSingle(dt.Rows[0]["RMB"]);
                player.FortuneInfo.GoldCoin = Convert.ToSingle(dt.Rows[0]["GoldCoin"]);
                player.FortuneInfo.MinesCount = Convert.ToSingle(dt.Rows[0]["MinesCount"]);
                player.FortuneInfo.MinersCount = Convert.ToSingle(dt.Rows[0]["MinersCount"]);
                player.FortuneInfo.StonesReserves = Convert.ToSingle(dt.Rows[0]["StonesReserves"]);
                player.FortuneInfo.TotalProducedStonesCount = Convert.ToSingle(dt.Rows[0]["TotalProducedStonesCount"]);
                player.FortuneInfo.StockOfStones = Convert.ToSingle(dt.Rows[0]["StockOfStones"]);
                player.FortuneInfo.TempOutputStones = Convert.ToSingle(dt.Rows[0]["TempOutputStones"]);
                player.FortuneInfo.FreezingStones = Convert.ToSingle(dt.Rows[0]["FreezingStones"]);
                player.FortuneInfo.StockOfDiamonds = Convert.ToSingle(dt.Rows[0]["StockOfDiamonds"]);
                player.FortuneInfo.FreezingDiamonds = Convert.ToSingle(dt.Rows[0]["FreezingDiamonds"]);

                dt.Dispose();
            }

            return player;
        }
    }
}
