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
        public PlayerGravelRequsetRecordInfo[] GetLastDayPlayerGravelRequsetRecords(MyDateTime date, int userID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();

                string sqlInnerSelect = " select * from superminers.playergravelrequsetrecordinfo ";
                string sqlWhere = " where ";
                if (userID > 0)
                {
                    sqlWhere += " UserID=@userID and ";
                }
                sqlWhere += " @beginDate <= RequestDate and RequestDate < @endDate ";
                string sqlText = "SELECT r.*, s.UserName FROM (" + sqlInnerSelect + sqlWhere + ") r left join playersimpleinfo s on r.UserID = s.id ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@beginDate", new DateTime(date.Year, date.Month, date.Day, 0, 0, 0));
                mycmd.Parameters.AddWithValue("@endDate", new DateTime(date.Year, date.Month, date.Day + 1, 0, 0, 0));
                mycmd.Parameters.AddWithValue("@userID", userID);

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();

                return MetaDBAdapter<PlayerGravelRequsetRecordInfo>.GetPlayerGravelRequsetRecordInfoFromDataTable(table);
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

        public PlayerGravelRequsetRecordInfo GetLastDayPlayerGravelRequsetRecord(int userID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();

                string sqlInnerSelect = " select * from superminers.playergravelrequsetrecordinfo where UserID=@userID order by id desc limit 1 ";
                string sqlText = "SELECT r.*, s.UserName FROM (" + sqlInnerSelect + ") r left join playersimpleinfo s on r.UserID = s.id ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@userID", userID);

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();

                var records = MetaDBAdapter<PlayerGravelRequsetRecordInfo>.GetPlayerGravelRequsetRecordInfoFromDataTable(table);
                if (records == null || records.Length == 0)
                {
                    return null;
                }

                return records[0];
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

        public bool UpdatePlayerGravelRequsetRecords(PlayerGravelRequsetRecordInfo[] records, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < records.Length; i++)
                {
                    string sqlText = "UPDATE superminers.playergravelrequsetrecordinfo set `IsResponsed`=@IsResponsed" + i + ",`ResponseDate`=@ResponseDate" + i + ",`Gravel`=@Gravel" + i + " where `id`=@ID" + i + "; ";
                    builder.Append(sqlText);
                    mycmd.Parameters.AddWithValue("@IsResponsed" + i, records[i].IsResponsed);
                    mycmd.Parameters.AddWithValue("@ResponseDate" + i, records[i].ResponseDate);
                    mycmd.Parameters.AddWithValue("@Gravel" + i, records[i].Gravel);
                    mycmd.Parameters.AddWithValue("@ID" + i, records[i].ID);
                }
                mycmd.CommandText = builder.ToString();

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

        public bool SetPlayerGravelRequsetRecordInfoIsGoted(PlayerGravelRequsetRecordInfo record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string sqlText = "update superminers.playergravelrequsetrecordinfo set `IsGoted`=@IsGoted where `id`=@ID; ";

                mycmd.Parameters.AddWithValue("@IsGoted", record.IsGoted);
                mycmd.Parameters.AddWithValue("@ID", record.ID);
                mycmd.CommandText = sqlText;

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

        public bool CreatePlayerGravelRequestRecord(PlayerGravelRequsetRecordInfo record)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into superminers.playergravelrequsetrecordinfo (`UserID`,`RequestDate`,`IsResponsed` ) values (@UserID,@RequestDate,@IsResponsed ); ";

                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@RequestDate", record.RequestDate.ToDateTime());
                mycmd.Parameters.AddWithValue("@IsResponsed", record.IsResponsed);
                mycmd.CommandText = sqlText;

                mycmd.ExecuteNonQuery();
            });
        }

        public bool SaveGravelDistributeRecordInfo(GravelDistributeRecordInfo record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string sqlText = "insert into superminers.graveldistributerecordinfo (`CreateDate`,`AllPlayerCount`,`RequestPlayerCount`,`ResponseGravelCount`) values (@CreateDate,@AllPlayerCount,@RequestPlayerCount,@ResponseGravelCount); ";

                mycmd.Parameters.AddWithValue("@CreateDate", record.CreateDate);
                mycmd.Parameters.AddWithValue("@AllPlayerCount", record.AllPlayerCount);
                mycmd.Parameters.AddWithValue("@RequestPlayerCount", record.RequestPlayerCount);
                mycmd.Parameters.AddWithValue("@ResponseGravelCount", record.ResponseGravelCount);
                mycmd.CommandText = sqlText;

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

    }
}
