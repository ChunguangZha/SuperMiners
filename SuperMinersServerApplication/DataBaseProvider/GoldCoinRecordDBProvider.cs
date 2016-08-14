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

        public bool DeleteTempGoldCoinRechargeTradeRecord(string orderNumber)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();

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
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SaveFinalGoldCoinRechargeRecord(GoldCoinRechargeRecord record)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();

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
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

    }
}
