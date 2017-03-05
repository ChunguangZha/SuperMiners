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
                        " (@UserID, @SpendGoldCoin, @GainMinersCount, @Time); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
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

                string sqlTextA = "select a.* from minersbuyrecord a  ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    builder.Append(" a.UserID = ( select id from  playersimpleinfo where UserName = @UserName )");
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
                    builder.Append(" a.Time >= @beginCreateTime and a.Time < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
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
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";


                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    records = MetaDBAdapter<MinersBuyRecord>.GetMinersBuyRecordListFromDataTable(dt);
                }
                dt.Clear();
                dt.Dispose();
                adapter.Dispose();
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
