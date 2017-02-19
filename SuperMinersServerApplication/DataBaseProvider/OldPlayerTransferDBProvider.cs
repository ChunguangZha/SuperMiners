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
    }
}
