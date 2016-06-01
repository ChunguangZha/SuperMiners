using MetaData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class BuyMinerRecordDBProvider
    {
        public bool AddBuyMinerRecord(MinersBuyRecord record, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA = "insert into minersbuyrecord " +
                        "(`UserID`, `SpendGoldCoin`, `GainMinersCount`, `Time`) values " +
                        " ((select c.id from playersimpleinfo c where c.UserName = @UserName), @SpendGoldCoin, @GainMinersCount, @Time); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(record.UserName));
                mycmd.Parameters.AddWithValue("@SpendGoldCoin", record.SpendGoldCoin);
                mycmd.Parameters.AddWithValue("@GainMinersCount", record.GainMinersCount);
                mycmd.Parameters.AddWithValue("@Time", record.Time);

                mycmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }
    }
}
