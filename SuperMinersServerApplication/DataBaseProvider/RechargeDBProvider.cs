using MetaData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class RechargeDBProvider
    {
        public bool AddRechargeRMBRecord(RMBRechargeRecord record, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "insert into rmbrechargerecord set " +
                            "`UserID` = (select p.id from playersimpleinfo p where p.UserName = @UserName)," +
                            "`RechargeMoney` = @RechargeMoney, " +
                            "`GainRMB` = @GainRMB, " +
                            "`Time` = @Time;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@UserName", record.UserName);
                mycmd.Parameters.AddWithValue("@RechargeMoney", record.RechargeMoney);
                mycmd.Parameters.AddWithValue("@GainRMB", record.GainRMB);
                mycmd.Parameters.AddWithValue("@Time", record.Time);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public bool AddRechargeGoldCoinRecord(GoldCoinRechargeRecord record, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "insert into goldcoinrechargerecord set " +
                            "`UserID` = (select p.id from playersimpleinfo p where p.UserName = @UserName)," +
                            "`RechargeMoney` = @RechargeMoney, " +
                            "`GainGoldCoin` = @GainGoldCoin, " +
                            "`Time` = @Time;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@UserName", record.UserName);
                mycmd.Parameters.AddWithValue("@RechargeMoney", record.RechargeMoney);
                mycmd.Parameters.AddWithValue("@GainGoldCoin", record.GainGoldCoin);
                mycmd.Parameters.AddWithValue("@Time", record.Time);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
