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
    public class AlipayRecordDBProvider
    {
        public bool SaveAlipayRechargeRecord(AlipayRechargeRecord alipayRecord, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into alipayrechargerecord " +
                    "(`out_trade_no`,`alipay_trade_no`,`user_name`,`buyer_email`,`total_fee`,`value_rmb`,`pay_time`) " +
                    " values (@out_trade_no, @alipay_trade_no,@user_name,@buyer_email,@total_fee,@value_rmb,@pay_time)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@out_trade_no", alipayRecord.out_trade_no);
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipayRecord.alipay_trade_no);
                mycmd.Parameters.AddWithValue("@user_name", DESEncrypt.EncryptDES(alipayRecord.user_name));
                mycmd.Parameters.AddWithValue("@buyer_email", alipayRecord.buyer_email);
                mycmd.Parameters.AddWithValue("@total_fee", alipayRecord.total_fee);
                mycmd.Parameters.AddWithValue("@pay_time", alipayRecord.pay_time);
                mycmd.Parameters.AddWithValue("@value_rmb", alipayRecord.value_rmb);
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

        public AlipayRechargeRecord GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(string orderNumber, string alipay_trade_no)
        {
            AlipayRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from superminers.alipayrechargerecord where out_trade_no = @orderNumber or alipay_trade_no = @alipay_trade_no ";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@orderNumber", orderNumber);
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipay_trade_no);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                records = MetaDBAdapter<AlipayRechargeRecord>.GetAlipayRechargeRecordListFromDataTable(dt);

                mycmd.Dispose();

                if (records == null || records.Length == 0)
                {
                    return null;
                }
                return records[0];
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

        public AlipayRechargeRecord[] GetAllAlipayRechargeRecords(string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            AlipayRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                MySqlCommand mycmd = myconn.CreateCommand();
                DataTable dt = new DataTable();

                string sqlTextA = "select * from superminers.alipayrechargerecord ";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" out_trade_no = @orderNumber ");
                    mycmd.Parameters.AddWithValue("@orderNumber", orderNumber);
                }
                if (!string.IsNullOrEmpty(alipayOrderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" alipay_trade_no = @alipayOrderNumber ");
                    mycmd.Parameters.AddWithValue("@alipayOrderNumber", alipayOrderNumber);
                }
                if (!string.IsNullOrEmpty(payEmail))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" buyer_email = @payEmail ");
                    mycmd.Parameters.AddWithValue("@payEmail", payEmail);
                }
                if (!string.IsNullOrEmpty(playerUserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" user_name = @playerUserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(playerUserName);
                    mycmd.Parameters.AddWithValue("@playerUserName", encryptUserName);
                }

                if (beginPayTime != null && !beginPayTime.IsNull && endPayTime != null && !endPayTime.IsNull)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginTime = beginPayTime.ToDateTime();
                    DateTime endTime = endPayTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    builder.Append(" pay_time >= @beginPayTime and pay_time < @endPayTime ");
                    mycmd.Parameters.AddWithValue("@beginPayTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endPayTime", endTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by pay_time desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

                myconn.Open();
                mycmd.CommandText = sqlAllText;

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                records = MetaDBAdapter<AlipayRechargeRecord>.GetAlipayRechargeRecordListFromDataTable(dt);

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

        public AlipayRechargeRecord SearchExceptionAlipayRechargeRecord(string orderNumber)
        {
            AlipayRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from superminers.alipayrecharge_exception_record where out_trade_no = @out_trade_no ";
                mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@out_trade_no", orderNumber);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                records = MetaDBAdapter<AlipayRechargeRecord>.GetAlipayRechargeRecordListFromDataTable(dt);
                if (records == null || records.Length == 0)
                {
                    return null;
                }
                return records[0];
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public AlipayRechargeRecord[] GetAllExceptionAlipayRechargeRecords()
        {
            AlipayRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from superminers.alipayrecharge_exception_record ";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                records = MetaDBAdapter<AlipayRechargeRecord>.GetAlipayRechargeRecordListFromDataTable(dt);

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

        public bool DeleteExceptionAlipayRecord(string alipayTradeNumber, string orderNumber)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlText = "delete from alipayrecharge_exception_record where alipay_trade_no = @alipay_trade_no and out_trade_no = @out_trade_no;";

                myconn.Open();
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipayTradeNumber);
                mycmd.Parameters.AddWithValue("@out_trade_no", orderNumber);
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
