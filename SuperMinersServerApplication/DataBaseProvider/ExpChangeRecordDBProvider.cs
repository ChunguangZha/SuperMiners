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
    public class ExpChangeRecordDBProvider
    {
        public bool AddExpChangeRecord(ExpChangeRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string cmdTextA = "insert into expchangerecord " +
                    "(`UserID`, `AddExp`, `NewExp`, `Time`, `OperContent` ) " +
                    " values " +
                    "(@UserID, @AddExp, @NewExp, @Time, @OperContent); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@AddExp", record.AddExp);
                mycmd.Parameters.AddWithValue("@NewExp", record.NewExp);
                mycmd.Parameters.AddWithValue("@Time", record.Time);
                mycmd.Parameters.AddWithValue("@OperContent", record.OperContent);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public ExpChangeRecord[] GetExpChangeRecord(int userID)
        {
            ExpChangeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.*, b.UserName from expchangerecord a left join playersimpleinfo b on a.UserID = b.id where UserID = @UserID; ";

                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@UserID", userID);

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<ExpChangeRecord>.GetExpChangeRecordListFromDataTable(dt);
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
