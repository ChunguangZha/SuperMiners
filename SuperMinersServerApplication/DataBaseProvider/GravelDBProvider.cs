using MetaData;
using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GravelDBProvider
    {
        public PlayerGravelRequsetRecordInfo[] GetLastDayPlayerGravelRequsetRecords(MyDateTime date)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();

                string sqlText = "SELECT r.*, s.UserName FROM superminers.playergravelrequsetrecordinfo r left join playersimpleinfo s on r.UserID = s.id where @beginDate <= r.RequestDate and r.RequestDate < @endDate ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@beginDate", new DateTime(date.Year, date.Month, date.Day, 0, 0, 0));
                mycmd.Parameters.AddWithValue("@endDate", new DateTime(date.Year, date.Month, date.Day + 1, 0, 0, 0));

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();

                return MetaDBAdapter<PlayerGravelRequsetRecordInfo>.GetPlayerGravelRequsetRecordInfo(table);
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
