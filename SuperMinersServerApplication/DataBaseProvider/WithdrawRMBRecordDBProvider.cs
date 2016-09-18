using MetaData;
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
    public class WithdrawRMBRecordDBProvider
    {
        public bool AddWithdrawRMBRecord(WithdrawRMBRecord record, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string sqlText = "insert into withdrawrmbrecord " +
                    "(`PlayerUserName`, `AlipayAccount`, `AlipayRealName`, `WidthdrawRMB`, `ValueYuan`,`CreateTime`, `IsPayedSucceed`, `AdminUserName`, `PayTime`) " +
                    " values (@PlayerUserName, @AlipayAccount, @AlipayRealName, @WidthdrawRMB, @ValueYuan, @CreateTime, @IsPayedSucceed, @AdminUserName, @PayTime)";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@PlayerUserName", DESEncrypt.EncryptDES(record.PlayerUserName));
                mycmd.Parameters.AddWithValue("@AlipayAccount", DESEncrypt.EncryptDES(record.AlipayAccount));
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(record.AlipayRealName));
                mycmd.Parameters.AddWithValue("@WidthdrawRMB", record.WidthdrawRMB);
                mycmd.Parameters.AddWithValue("@ValueYuan", record.ValueYuan);
                mycmd.Parameters.AddWithValue("@CreateTime", record.CreateTime);
                mycmd.Parameters.AddWithValue("@IsPayedSucceed", false);
                if (string.IsNullOrEmpty(record.AdminUserName))
                {
                    mycmd.Parameters.AddWithValue("@AdminUserName", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@AdminUserName", DESEncrypt.EncryptDES(record.AdminUserName));
                }
                if (record.PayTime == null || !record.PayTime.HasValue)
                {
                    mycmd.Parameters.AddWithValue("@PayTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
                }

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

        public bool ConfirmWithdrawRMB(WithdrawRMBRecord record, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string sqlText = "update withdrawrmbrecord set " +
                    "`IsPayedSucceed` = @IsPayedSucceed, `AdminUserName` = @AdminUserName, `AlipayOrderNumber` = @AlipayOrderNumber, `PayTime` = @PayTime " +
                    " where id = @id ";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@IsPayedSucceed", true);
                mycmd.Parameters.AddWithValue("@AdminUserName", DESEncrypt.EncryptDES(record.AdminUserName));
                mycmd.Parameters.AddWithValue("@AlipayOrderNumber", record.AlipayOrderNumber);
                mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
                mycmd.Parameters.AddWithValue("@id", record.id);

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

        public WithdrawRMBRecord GetWithdrawRMBRecord(bool isPayed, string playerUserName, int withdrawRMB, DateTime createTime)
        {
            WithdrawRMBRecord record = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select * " +
                                    "from withdrawrmbrecord " +
                                    "where IsPayedSucceed = @IsPayedSucceed and PlayerUserName = @PlayerUserName and WidthdrawRMB = @WidthdrawRMB and CreateTime = @CreateTime ";
                string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                mycmd.Parameters.AddWithValue("@IsPayedSucceed", isPayed);
                mycmd.Parameters.AddWithValue("@PlayerUserName", encryptUserName);
                mycmd.Parameters.AddWithValue("@WidthdrawRMB", withdrawRMB);
                mycmd.Parameters.AddWithValue("@CreateTime", createTime);

                mycmd.CommandText = sqlTextA;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    var records = MetaDBAdapter<WithdrawRMBRecord>.GetWithdrawRMBRecordListFromDataTable(dt);
                    if (records != null && records.Length != 0)
                    {
                        record = records[0];
                    }
                }
                mycmd.Dispose();

                return record;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public WithdrawRMBRecord[] GetWithdrawRMBRecordList(bool isPayed, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            WithdrawRMBRecord[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select * " +
                                    "from withdrawrmbrecord ";
                StringBuilder builder = new StringBuilder();

                builder.Append(" IsPayedSucceed = @IsPayedSucceed  ");
                mycmd.Parameters.AddWithValue("@IsPayedSucceed", isPayed);

                if (!string.IsNullOrEmpty(playerUserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" PlayerUserName = @PlayerUserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@PlayerUserName", encryptUserName);
                }

                if (beginCreateTime != null && !beginCreateTime.IsNull && endCreateTime != null && !endCreateTime.IsNull)
                {
                    DateTime beginTime = beginCreateTime.ToDateTime();
                    DateTime endTime = endCreateTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" CreateTime >= @beginCreateTime and CreateTime < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endTime);
                }

                if (!string.IsNullOrEmpty(adminUserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" AdminUserName = @AdminUserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(adminUserName);
                    mycmd.Parameters.AddWithValue("@AdminUserName", encryptUserName);
                }

                bool orderByPayTime = false;
                if (beginPayTime != null && !beginPayTime.IsNull && endPayTime != null && !endPayTime.IsNull)
                {
                    DateTime beginTime = beginPayTime.ToDateTime();
                    DateTime endTime = endPayTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" PayTime >= @beginPayTime and PayTime < @endPayTime ");
                    mycmd.Parameters.AddWithValue("@beginPayTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endPayTime", endTime);
                    orderByPayTime = true;
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = "";
                if (pageItemCount > 0)
                {
                    if (orderByPayTime)
                    {
                        sqlOrderLimit = " order by PayTime desc ";
                    }
                    else
                    {
                        sqlOrderLimit = " order by CreateTime desc ";
                    }
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;
                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    orders = MetaDBAdapter<WithdrawRMBRecord>.GetWithdrawRMBRecordListFromDataTable(dt);
                }
                mycmd.Dispose();

                return orders;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }
    }
}
