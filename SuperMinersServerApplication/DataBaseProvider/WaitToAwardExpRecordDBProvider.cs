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
        public WaitToReferAwardRecord[] GetWaitToAwardExpRecord(string newRegisterUserName)
        {
            WaitToReferAwardRecord[] records = null;
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
                records = MetaDBAdapter<WaitToReferAwardRecord>.GetWaitToAwardExpRecordListFromDataTable(dt);

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

        public bool SaveWaitToAwardExpRecord(WaitToReferAwardRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into waittoawardexprecord " +
                    "(`ReferrerUserName`,`NewRegisterUserNme`,`AwardLevel`) " +
                    " values (@ReferrerUserName, @NewRegisterUserNme,@AwardLevel)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@ReferrerUserName", DESEncrypt.EncryptDES(record.ReferrerUserName));
                mycmd.Parameters.AddWithValue("@NewRegisterUserNme", DESEncrypt.EncryptDES(record.NewRegisterUserNme));
                mycmd.Parameters.AddWithValue("@AwardLevel", record.AwardLevel);
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

        public bool DeleteWaitToAwardExpRecord(int id, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();
                string sqlText = "delete from waittoawardexprecord where id = @id ";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@id", id);
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
