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
        public MinesBuyRecord GetMineTradeRecord(string userName, string orderNumber)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select a.*, b.UserName from minesbuyrecord a left join playersimpleinfo b on a.UserID=b.id ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    builder.Append(" a.OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    builder.Append(" b.UserName = @UserName ");
                    mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                }

                mycmd.CommandText = cmdText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    return MetaDBAdapter<MinesBuyRecord>.GetMinesBuyRecordFromDataTable(table)[0];
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


        public MinesBuyRecord[] GetAllMineTradeRecords(string userName, MyDateTime startDate, MyDateTime endDate, int pageItemCount, int pageIndex)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select a.*, b.UserName from minesbuyrecord a left join playersimpleinfo b on a.UserID=b.id ";
                string whereText = " where ";
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(userName))
                {
                    builder.Append(" b.UserName = @UserName ");
                    mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                }
                if (startDate != null && !startDate.IsNull && endDate != null && !endDate.IsNull)
                {
                    DateTime start = startDate.ToDateTime();
                    DateTime end = endDate.ToDateTime();
                    if (start >= end)
                    {
                        return null;
                    }

                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }

                    builder.Append(" b.CreateTime >= @beginTime and b.CreateTime < @endTime ;");
                    mycmd.Parameters.AddWithValue("@beginTime", start);
                    mycmd.Parameters.AddWithValue("@endTime", end);
                }

                if (builder.Length > 0)
                {
                    cmdText = cmdText + whereText + builder.ToString();
                }
                string orderByText = " order by CreateTime desc ";
                int startIndex = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                string limitText = " limit " + startIndex.ToString() + ", " + pageItemCount;
                cmdText = cmdText + orderByText + limitText;

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

        public MinesBuyRecord[] GetAllTempMineTradeRecords()
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select a.*, b.UserName from tempminesbuyrecord a left join playersimpleinfo b on a.UserID=b.id ";
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
                myconn.Open();
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

        public bool DeleteTempMineTradeRecord(string orderNumber, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

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
            }
        }

        public bool SaveFinalMineTradeRecord(MinesBuyRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

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
            }
        }

    }
}
