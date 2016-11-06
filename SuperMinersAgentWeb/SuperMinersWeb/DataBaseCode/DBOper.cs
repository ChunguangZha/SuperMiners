using MySql.Data.MySqlClient;
using SuperMinersServerApplication.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersAgentWeb.DataBaseCode
{
    public class DBOper
    {
        internal static readonly string CONNECTIONSTRING = "server=localhost;port=13344; uid=superminersDBA;pwd=dba!@#123;database=superminers;charset=utf8; pooling=false; Keep Alive=5;";

        public static bool AddExceptionAlipayRechargeRecord(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = new MySqlConnection(CONNECTIONSTRING);
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string sqlText = "insert into alipayrecharge_exception_record " +
                    "(`out_trade_no`,`alipay_trade_no`,`user_name`,`buyer_email`,`total_fee`,`value_rmb`,`pay_time`) " +
                    " values (@out_trade_no, @alipay_trade_no,@user_name,@buyer_email,@total_fee,@value_rmb,@pay_time)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@out_trade_no", out_trade_no);
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipay_trade_no);
                mycmd.Parameters.AddWithValue("@user_name", DESEncrypt.EncryptDES(userName));
                mycmd.Parameters.AddWithValue("@buyer_email", buyer_email);
                mycmd.Parameters.AddWithValue("@total_fee", total_fee);
                mycmd.Parameters.AddWithValue("@pay_time", pay_time);
                mycmd.Parameters.AddWithValue("@value_rmb", 0);
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