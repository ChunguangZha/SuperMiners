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

                var lists = MetaDBAdapter<MinesBuyRecord>.GetMinesBuyRecordFromDataTable(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();

                if (lists == null || lists.Length == 0)
                {
                    return null;
                }

                return lists[0];
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
                string cmdText = "select a.* from minesbuyrecord a ";
                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(userName))
                {
                    builder.Append(" a.UserID = ( select id from  playersimpleinfo where UserName = @UserName ) ");
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

                    builder.Append(" a.CreateTime >= @beginTime and a.CreateTime < @endTime ");
                    mycmd.Parameters.AddWithValue("@beginTime", start);
                    mycmd.Parameters.AddWithValue("@endTime", end);
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
                                    " ( " + cmdText + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join playersimpleinfo s  on ttt.UserID = s.id ";
                
                mycmd.CommandText = sqlAllText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var lists = MetaDBAdapter<MinesBuyRecord>.GetMinesBuyRecordFromDataTable(table);

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
                var lists = MetaDBAdapter<MinesBuyRecord>.GetMinesBuyRecordFromDataTable(table);

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
                MyDBHelper.Instance.DisposeConnection(myconn);
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
                        " (@OrderNumber, @UserID, @SpendRMB, @GainMinesCount, @GainStonesReserves, @CreateTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
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
                        " (@OrderNumber, @UserID, @SpendRMB, @GainMinesCount, @GainStonesReserves, @CreateTime, @PayTime); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", record.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
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
