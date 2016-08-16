using MetaData.Trade;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class AlipayRecordDBProvider
    {
        public bool SaveAlipayRechargeRecord(AlipayRechargeRecord alipayRecord)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string sqlText = "insert into alipayrechargerecord " +
                    "(`out_trade_no`,`alipay_trade_no`,`buyer_email`,`total_fee`,`pay_time`) " +
                    " values (@out_trade_no, @alipay_trade_no,@buyer_email,@total_fee,@pay_time)";
                mycmd.CommandText = sqlText;

                mycmd.Parameters.AddWithValue("@out_trade_no", alipayRecord.out_trade_no);
                mycmd.Parameters.AddWithValue("@alipay_trade_no", alipayRecord.alipay_trade_no);
                mycmd.Parameters.AddWithValue("@buyer_email", alipayRecord.buyer_email);
                mycmd.Parameters.AddWithValue("@total_fee", alipayRecord.total_fee);
                mycmd.Parameters.AddWithValue("@pay_time", alipayRecord.pay_time);
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
