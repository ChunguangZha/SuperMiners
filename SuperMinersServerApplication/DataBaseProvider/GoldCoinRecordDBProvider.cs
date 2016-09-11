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
                if (table.Rows.Count > 0)
                {
                    return MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(table);
                }

                return null;
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
                        " (@OrderNumber, (select c.id from playersimpleinfo c where c.UserName = @UserName), @SpendRMB, @GainGoldCoin, @CreateTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
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
                        " (@OrderNumber, (select c.id from playersimpleinfo c where c.UserName = @UserName), @SpendRMB, @GainGoldCoin, @CreateTime, @PayTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
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

                string sqlTextA = "select a.*, b.UserName from goldcoinrechargerecord a left join playersimpleinfo b on a.UserID=b.id  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" UserName = @UserName ");
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

                string cmdText = sqlTextA + whereText + builder.ToString();
                mycmd.CommandText = cmdText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(dt);
                }
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
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.*, b.UserName from goldcoinrechargerecord a left join playersimpleinfo b on a.UserID=b.id  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" UserName = @UserName ");
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
                    builder.Append(" and CreateTime >= @beginCreateTime and CreateTime < @endCreateTime ;");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }

                string whereText = builder.Length > 0 ? " where " : "";
                string orderByText = " order by CreateTime desc ";
                int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                string limitText = " limit " + start.ToString() + ", " + pageItemCount;

                string cmdText = sqlTextA + whereText + builder.ToString() + orderByText + limitText;
                mycmd.CommandText = cmdText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<GoldCoinRechargeRecord>.GetGoldCoinRechargeRecordFromDataTable(dt);
                }
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
