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
    public class WaitToAwardExpRecordDBProvider
    {
        public WaitToAwardExpRecord[] GetWaitToAwardExpRecord(string newRegisterUserName)
        {
            WaitToAwardExpRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from superminers.waittoawardexprecord where NewRegisterUserNme = @NewRegisterUserNme ";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@NewRegisterUserNme", DESEncrypt.EncryptDES(newRegisterUserName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                records = MetaDBAdapter<WaitToAwardExpRecord>.GetWaitToAwardExpRecordListFromDataTable(dt);

                mycmd.Dispose();

                return records;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public bool SaveWaitToAwardExpRecord(WaitToAwardExpRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into waittoawardexprecord " +
                    "(`ReferrerUserName`,`NewRegisterUserNme`,`AwardGoldCoin`) " +
                    " values (@ReferrerUserName, @NewRegisterUserNme,@AwardGoldCoin)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@ReferrerUserName", DESEncrypt.EncryptDES(record.ReferrerUserName));
                mycmd.Parameters.AddWithValue("@NewRegisterUserNme", DESEncrypt.EncryptDES(record.NewRegisterUserNme));
                mycmd.Parameters.AddWithValue("@AwardGoldCoin", record.AwardGoldCoin);
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

        public bool DeleteWaitToAwardExpRecord(string newRegisterUserName)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string sqlText = "delete from waittoawardexprecord where NewRegisterUserNme = @NewRegisterUserNme ";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@NewRegisterUserNme", DESEncrypt.EncryptDES(newRegisterUserName));
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
