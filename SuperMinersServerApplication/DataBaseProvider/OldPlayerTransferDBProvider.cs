using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class OldPlayerTransferDBProvider
    {
        public OldPlayerTransferRegisterInfo[] GetAllPlayerTransferRecords()
        {
            OldPlayerTransferRegisterInfo[] records = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from oldplayertransferregisterinfo ";
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                records = MetaDBAdapter<OldPlayerTransferRegisterInfo>.GetOldPlayerTransferRegisterInfo(table);
            });

            return records;
        }

        public bool AddOldPlayerTransferRecord(OldPlayerTransferRegisterInfo record)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into oldplayertransferregisterinfo (`UserName`,`AlipayAccount`,`AlipayRealName`,`Email`,`SubmitTime`,`isTransfered`) "+
                    " values (@UserName,@AlipayAccount,@AlipayRealName,@Email,@SubmitTime,@isTransfered) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
                mycmd.Parameters.AddWithValue("@AlipayAccount", DESEncrypt.EncryptDES(record.AlipayAccount));
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(record.AlipayRealName));
                mycmd.Parameters.AddWithValue("@Email", DESEncrypt.EncryptDES(record.Email));
                mycmd.Parameters.AddWithValue("@SubmitTime", record.SubmitTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@isTransfered", false);
                mycmd.ExecuteNonQuery();
            });
        }

        public int GetRegisteredCountByUserName(string userName)
        {
            int count = 0;
            MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "select count(ID) from oldplayertransferregisterinfo where UserName =@UserName ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                count = Convert.ToInt32(mycmd.ExecuteScalar());

            });

            return count;
        }

        public bool TransferPlayer(int recordID, string adminUserName)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "update oldplayertransferregisterinfo set `isTransfered`=@isTransfered,`HandledTime`=@HandledTime,`HandlerName`=@HandlerName where `ID`=@ID ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@isTransfered", true);
                mycmd.Parameters.AddWithValue("@HandledTime", DateTime.Now);
                mycmd.Parameters.AddWithValue("@HandlerName", DESEncrypt.EncryptDES(adminUserName));
                mycmd.Parameters.AddWithValue("@ID", recordID);
                mycmd.ExecuteNonQuery();
            });
        }
    }
}
