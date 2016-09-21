using MetaData.Trade;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class WithdrawRMBShowImageRecordDBProvider
    {
        public bool AddWithdrawRMBShowImageRecord(WithdrawRMBShowImageRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlText = "insert into withdrawrmbshowimagerecord ( `PlayerUserName`, `ShowImageTime`, `ImageSource`, `WithdrawRMBRecordID` )" +
                    " values (@PlayerUserName, @ShowImageTime, @ImageSource, @WithdrawRMBRecordID )";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@PlayerUserName", DESEncrypt.EncryptDES(record.PlayerUserName));
                mycmd.Parameters.AddWithValue("@ShowImageTime", record.ShowImageTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@ImageSource", record.ImageSource);
                mycmd.Parameters.AddWithValue("@WithdrawRMBRecordID", record.WithdrawRMBRecordID);

                myconn.Open();
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

        public bool DeleteWithdrawRMBShowImageRecord
    }
}
