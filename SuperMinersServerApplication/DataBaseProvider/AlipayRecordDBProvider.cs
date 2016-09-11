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

        public AlipayRechargeRecord[] GetAllAlipayRechargeRecords()
        {
            AlipayRechargeRecord[] records = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from superminers.alipayrechargerecord ";
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

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipayTradeNumber);
                mycmd.Parameters.AddWithValue("@out_trade_no", orderNumber);
                mycmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception exc)
            {
                return false;
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
