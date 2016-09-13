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

        public MinersBuyRecord[] GetFinishedBuyMinerRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            MinersBuyRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select a.*, b.UserName from minersbuyrecord a left join playersimpleinfo b on a.UserID=b.id  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" UserName = @UserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@UserName", encryptUserName);
                }

                if (beginCreateTime != null && !beginCreateTime.IsNull && endCreateTime != null && !endCreateTime.IsNull)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginCreateTime.ToDateTime();
                    DateTime endTime = endCreateTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    builder.Append(" and Time >= @beginCreateTime and Time < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    builder.Append(" order by CreateTime desc limit " + start.ToString() + ", " + pageItemCount);
                }

                string whereText = builder.Length > 0 ? " where " : "";

                string cmdText = sqlTextA + whereText + builder.ToString();
                mycmd.CommandText = cmdText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<MinersBuyRecord>.GetMinersBuyRecordListFromDataTable(dt);
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
