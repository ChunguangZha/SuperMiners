using MetaData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GoldCoinRecordDBProvider
    {
        public GoldCoinRechargeRecord[] GetAllTempGoldCoinRechargeTradeRecords()
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select a.*, b.UserName from tempgoldcoinrechargerecord a left join playersimpleinfo b on a.UserID=b.id ";
                mycmd.CommandText = cmdText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var lists = MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();

                return lists;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SaveTempGoldCoinRechargeTradeRecord(GoldCoinRechargeRecord record)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string cmdTextA = "insert into tempgoldcoinrechargerecord " +
                        "(`OrderNumber`, `UserID`, `SpendRMB`, `GainGoldCoin`,`CreateTime`) values " +
                        " (@OrderNumber, @UserID, @SpendRMB, @GainGoldCoin, @CreateTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@SpendRMB", record.SpendRMB);
                mycmd.Parameters.AddWithValue("@GainGoldCoin", record.GainGoldCoin);
                mycmd.Parameters.AddWithValue("@CreateTime", record.CreateTime);

                mycmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool DeleteTempGoldCoinRechargeTradeRecord(string orderNumber, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string cmdTextA = "delete from tempgoldcoinrechargerecord where OrderNumber = @OrderNumber;";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

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

        public bool SaveFinalGoldCoinRechargeRecord(GoldCoinRechargeRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string cmdTextA = "insert into goldcoinrechargerecord " +
                        "(`OrderNumber`, `UserID`, `SpendRMB`, `GainGoldCoin`, `CreateTime`, `PayTime`) values " +
                        " (@OrderNumber, @UserID, @SpendRMB, @GainGoldCoin, @CreateTime, @PayTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@SpendRMB", record.SpendRMB);
                mycmd.Parameters.AddWithValue("@GainGoldCoin", record.GainGoldCoin);
                mycmd.Parameters.AddWithValue("@CreateTime", record.CreateTime);
                mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);

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

        public GoldCoinRechargeRecord GetGoldCoinRechargeRecord(string playerUserName, string orderNumber)
        {
            GoldCoinRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.* from goldcoinrechargerecord a  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" a.UserID = ( select id from   playersimpleinfo where UserName = @UserName ) ");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                }

                string whereText = builder.Length > 0 ? " where " : "";

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA + whereText + builder.ToString() +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";

                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(dt);
                }
                dt.Clear();
                dt.Dispose();
                adapter.Dispose();
                mycmd.Dispose();

                if (records.Length > 0)
                {
                    return records[0];
                }
                return null;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }
        
        public GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            GoldCoinRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.* from goldcoinrechargerecord a ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" a.UserID = ( select id from   playersimpleinfo where UserName = @UserName ) ");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" .OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
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
                    builder.Append(" a.CreateTime >= @beginCreateTime and a.CreateTime < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by a.id desc ";
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
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table != null)
                {
                    records = MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(table);
                }
                table.Clear();
                table.Dispose();
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
