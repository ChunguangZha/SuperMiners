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
    public class PlayerLockedInfoDBProvider
    {
        public void AddPlayerLockedInfo(int userID, int expireDays)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlText = "delete from playerlockedinfo where `UserID` = @UserID; " +
                                "insert into playerlockedinfo (`UserID`, `LockedLogin`, `LockedLoginTime`, `ExpireDays`) " +
                                "values (@UserID, @LockedLogin, @LockedLoginTime, @ExpireDays); ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                mycmd.Parameters.AddWithValue("@LockedLogin", true);
                mycmd.Parameters.AddWithValue("@LockedLoginTime", DateTime.Now);
                mycmd.Parameters.AddWithValue("@ExpireDays", expireDays);

                myconn.Open();
                mycmd.ExecuteNonQuery();

            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public void DeletePlayerLockedInfo(int userID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlText = "delete from playerlockedinfo where `UserID` = @UserID; ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", userID);

                myconn.Open();
                mycmd.ExecuteNonQuery();
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public PlayerLockedInfo GetPlayerLockedInfo(int userID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;

            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlText = "select * from playerlockedinfo where `UserID` = @UserID;";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                myconn.Open();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var items = MetaDBAdapter<PlayerLockedInfo>.GetPlayerLockedInfoFromDataTable(table);
                if (items != null && items.Length > 0)
                {
                    return items[0];
                }

                return null;
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

        public PlayerLockedInfo[] GetAllPlayerLockedInfo()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;

            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                string sqlText = "select * from playerlockedinfo";
                mycmd.CommandText = sqlText;
                myconn.Open();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var items = MetaDBAdapter<PlayerLockedInfo>.GetPlayerLockedInfoFromDataTable(table);

                return items;
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
