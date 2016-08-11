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
    public class MineRecordDBProvider
    {
        public MinesBuyRecord[] GetAllTempMineTradeRecords()
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                mycmd = myconn.CreateCommand();
                string cmdText = "select * from tempminesbuyrecord";
                mycmd.CommandText = cmdText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    return MetaDBAdapter<MinesBuyRecord>.GetMinesBuyRecordFromDataTable(table);
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

        public bool SaveTempMineTradeRecord(MinesBuyRecord record)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myconn.CreateCommand();

                string cmdTextA = "insert into tempminesbuyrecord " +
                        "(`OrderNumber`, `UserID`, `SpendRMB`, `GainMinesCount`,`GainStonesReserves`, `CreateTime`) values " +
                        " (@OrderNumber, (select c.id from playersimpleinfo c where c.UserName = @UserName), @SpendRMB, @GainMinesCount, @GainStonesReserves, @CreateTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
                mycmd.Parameters.AddWithValue("@SpendRMB", record.SpendRMB);
                mycmd.Parameters.AddWithValue("@GainMinesCount", record.GainMinesCount);
                mycmd.Parameters.AddWithValue("@GainStonesReserves", record.GainStonesReserves);
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

        public bool DeleteTempMineTradeRecord(string orderNumber)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myconn.CreateCommand();

                string cmdTextA = "delete from tempminesbuyrecord where OrderNumber = @OrderNumber;";

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

        public bool SaveFinalMineTradeRecord(MinesBuyRecord record)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myconn.CreateCommand();

                string cmdTextA = "insert into minesbuyrecord " +
                        "(`OrderNumber`, `UserID`, `SpendRMB`, `GainMinesCount`,`GainStonesReserves`, `CreateTime`, `PayTime`) values " +
                        " (@OrderNumber, (select c.id from playersimpleinfo c where c.UserName = @UserName), @SpendRMB, @GainMinesCount, @GainStonesReserves, @CreateTime, @PayTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
                mycmd.Parameters.AddWithValue("@SpendRMB", record.SpendRMB);
                mycmd.Parameters.AddWithValue("@GainMinesCount", record.GainMinesCount);
                mycmd.Parameters.AddWithValue("@GainStonesReserves", record.GainStonesReserves);
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
