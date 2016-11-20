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
    public class PlayerLoginInfoDBProvider
    {
        //public bool AddPlayerLoginInfo(PlayerLoginInfo loginInfo)
        //{
        //    MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
        //    MySqlCommand mycmd = null;
        //    try
        //    {
        //        myconn.Open();
        //        mycmd = myconn.CreateCommand();

        //        string cmdTextA = "insert into playerlogininfo " +
        //                "(`LoginIP`, `LoginMac`, `LoginTime`, `UserID`) values " +
        //                " (@LoginIP, @LoginMac, @LoginTime, @UserID); ";

        //        mycmd.CommandText = cmdTextA;
        //        mycmd.Parameters.AddWithValue("@LoginIP", loginInfo.LoginIP);
        //        mycmd.Parameters.AddWithValue("@LoginMac", loginInfo.LoginMac);
        //        mycmd.Parameters.AddWithValue("@LoginTime", loginInfo.LoginTime);
        //        mycmd.Parameters.AddWithValue("@UserID", loginInfo.UserID);

        //        mycmd.ExecuteNonQuery();
        //        return true;
        //    }
        //    finally
        //    {
        //        if (mycmd != null)
        //        {
        //            mycmd.Dispose();
        //        }
        //        if (myconn != null)
        //        {
        //            myconn.Close();
        //            myconn.Dispose();
        //        }
        //    }
        //}


        //public PlayerLoginInfo[] GetUserLoginLogs(int userID)
        //{
        //    MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
        //    MySqlCommand mycmd = null;
        //    try
        //    {
        //        myconn.Open();
        //        mycmd = myconn.CreateCommand();

        //        string cmdTextA = "select * from playerlogininfo where `UserID` = @UserID ; ";

        //        mycmd.CommandText = cmdTextA;
        //        mycmd.Parameters.AddWithValue("@UserID", userID);
        //        DataTable table = new DataTable();
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
        //        adapter.Fill(table);
        //        PlayerLoginInfo[] records = MetaDBAdapter<PlayerLoginInfo>.GetPlayerLoginInfoListFromDataTable(table);
        //        return records;
        //    }
        //    finally
        //    {
        //        if (mycmd != null)
        //        {
        //            mycmd.Dispose();
        //        }
        //        if (myconn != null)
        //        {
        //            myconn.Close();
        //            myconn.Dispose();
        //        }
        //    }
        //}
    }
}
