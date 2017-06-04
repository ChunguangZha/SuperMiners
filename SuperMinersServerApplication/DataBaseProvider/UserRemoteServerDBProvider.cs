using MetaData;
using MetaData.Trade;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class UserRemoteServerDBProvider
    {
        public UserRemoteServerItem[] GetUserRemoteServerItems()
        {
            UserRemoteServerItem[] items = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from userremoteserveritem ";
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                items = MetaDBAdapter<UserRemoteServerItem>.GetUserRemoteServerItemFromDataTable(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();


                if (items == null || items.Length == 0)
                {
                    string insertText = "insert into userremoteserveritem (`ServerType`,`PayMoneyYuan`,`ShopName`,`Description`) values (@Type1,@Money1,@ShopName1,@Description1), (@Type2,@Money2,@ShopName2,@Description2), (@Type3,@Money3,@ShopName3,@Description3), (@Type4,@Money4,@ShopName4,@Description4)";

                    mycmd.CommandText = insertText;
                    mycmd.Parameters.AddWithValue("@Type1", (int)RemoteServerType.Once);
                    mycmd.Parameters.AddWithValue("@Type2", (int)RemoteServerType.OneMonth);
                    mycmd.Parameters.AddWithValue("@Type3", (int)RemoteServerType.ThreeMonth);
                    mycmd.Parameters.AddWithValue("@Type4", (int)RemoteServerType.OneYear);

                    mycmd.Parameters.AddWithValue("@Money1", 50);
                    mycmd.Parameters.AddWithValue("@Money2", 300);
                    mycmd.Parameters.AddWithValue("@Money3", 2000);
                    mycmd.Parameters.AddWithValue("@Money4", 5000);

                    mycmd.Parameters.AddWithValue("@ShopName1", "远程协助服务一次");
                    mycmd.Parameters.AddWithValue("@ShopName2", "远程协助服务一月");
                    mycmd.Parameters.AddWithValue("@ShopName3", "远程协助服务一季度");
                    mycmd.Parameters.AddWithValue("@ShopName4", "远程协助服务一年");

                    mycmd.Parameters.AddWithValue("@Description1", "远程协助服务一次");
                    mycmd.Parameters.AddWithValue("@Description2", "远程协助服务一月");
                    mycmd.Parameters.AddWithValue("@Description3", "远程协助服务一季度");
                    mycmd.Parameters.AddWithValue("@Description4", "远程协助服务一年");
                    mycmd.ExecuteNonQuery();

                    mycmd.CommandText = sqlText;
                    adapter = new MySqlDataAdapter(mycmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    items = MetaDBAdapter<UserRemoteServerItem>.GetUserRemoteServerItemFromDataTable(table);
                    table.Clear();
                    table.Dispose();
                    adapter.Dispose();


                }

            });

            return items;
        }

        public bool SaveUserRemoteServerBuyRecord(UserRemoteServerBuyRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into userremoteserverbuyrecord " +
                    "(`UserID`, `ServerType`,`PayMoneyYuan`,`OrderNumber`,`GetShoppingCredits`,`BuyRemoteServerTime`) " +
                    " values (@UserID, @ServerType, @PayMoneyYuan,@OrderNumber,@GetShoppingCredits,@BuyRemoteServerTime)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@ServerType", (int)record.ServerType);
                mycmd.Parameters.AddWithValue("@PayMoneyYuan", record.PayMoneyYuan);
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@GetShoppingCredits", record.GetShoppingCredits);
                mycmd.Parameters.AddWithValue("@BuyRemoteServerTime", record.BuyRemoteServerTime);
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

        public UserRemoteServerBuyRecord GetUserLastBuyOnceRemoteServiceRecord(string playerUserName)
        {
            UserRemoteServerBuyRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.* from userremoteserverbuyrecord a  " +
                                    " where a.UserID = ( select id from  playersimpleinfo where UserName = @UserName ) " +
                                    " and a.`ServerType`=@ServerType order by a.`ID` desc limit 1 ";
                string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                mycmd.Parameters.AddWithValue("@ServerType", (int)RemoteServerType.Once);

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";


                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null || dt.Rows.Count != 0)
                {
                    records = MetaDBAdapter<UserRemoteServerBuyRecord>.GetUserRemoteServerBuyRecordListFromDataTable(dt);
                }
                dt.Clear();
                dt.Dispose();
                adapter.Dispose();

                mycmd.Dispose();

                if (records == null || records.Length == 0)
                {
                    return null;
                }

                return records[0];
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public UserRemoteServerBuyRecord[] GetUserRemoteServerBuyRecords(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            UserRemoteServerBuyRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.* from userremoteserverbuyrecord a  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" a.UserID = ( select id from  playersimpleinfo where UserName = @UserName )");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (beginCreateTime != null && !beginCreateTime.IsNull && endCreateTime != null && !endCreateTime.IsNull)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginCreateTime.ToDateTime();
                    DateTime endTime = endCreateTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    builder.Append(" a.BuyRemoteServerTime >= @beginCreateTime and a.BuyRemoteServerTime < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by a.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";


                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<UserRemoteServerBuyRecord>.GetUserRemoteServerBuyRecordListFromDataTable(dt);
                }
                dt.Clear();
                dt.Dispose();
                adapter.Dispose();

                mycmd.Dispose();

                return records;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public bool AddUserRemoteHandleServiceRecord(UserRemoteHandleServiceRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into userremotehandleservicerecord " +
                    "(`UserID`, `ServiceTime`,`WorkerName`,`ServiceContent`,`AdminUserName`) " +
                    " values (@UserID, @ServiceTime, @WorkerName,@ServiceContent,@AdminUserName)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@ServiceTime", record.ServiceTime);
                mycmd.Parameters.AddWithValue("@WorkerName", DESEncrypt.EncryptDES(record.WorkerName));
                mycmd.Parameters.AddWithValue("@ServiceContent", DESEncrypt.EncryptDES(record.ServiceContent));
                mycmd.Parameters.AddWithValue("@AdminUserName", DESEncrypt.EncryptDES(record.AdminUserName));
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

        public UserRemoteHandleServiceRecord[] GetUserRemoteHandleServiceRecords(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            UserRemoteHandleServiceRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.* from userremotehandleservicerecord a  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" a.UserID = ( select id from  playersimpleinfo where UserName = @UserName )");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (beginCreateTime != null && !beginCreateTime.IsNull && endCreateTime != null && !endCreateTime.IsNull)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginCreateTime.ToDateTime();
                    DateTime endTime = endCreateTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    builder.Append(" a.ServiceTime >= @beginCreateTime and a.ServiceTime < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by a.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";


                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<UserRemoteHandleServiceRecord>.GetUserRemoteHandleServiceRecordListFromDataTable(dt);
                }
                dt.Clear();
                dt.Dispose();
                adapter.Dispose();
                mycmd.Dispose();

                return records;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

    }
}
