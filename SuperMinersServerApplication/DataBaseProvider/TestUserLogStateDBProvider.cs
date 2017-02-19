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
    public class TestUserLogStateDBProvider
    {
        public TestUserLogState GetTestUserLogStateByMac(string mac)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select * from testuserlogstate where Mac = @Mac ";
                mycmd.Parameters.AddWithValue("@Mac", mac);
                mycmd.CommandText = cmdText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                
                var lists = MetaDBAdapter<TestUserLogState>.GetTestUserLogStateFromDataTable(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();

                if (lists == null || lists.Length == 0)
                {
                    return null;
                }

                return lists[0];
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

        public TestUserLogState GetTestUserLogStateByUserName(string userName)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                DataTable table = new DataTable();
                myconn.Open();
                mycmd = myconn.CreateCommand();
                string cmdText = "select * from testuserlogstate where UserName = @UserName ";
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.CommandText = cmdText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var lists = MetaDBAdapter<TestUserLogState>.GetTestUserLogStateFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();

                if (lists == null || lists.Length == 0)
                {
                    return null;
                }
                return lists[0];
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

        public bool AddTestUserLogState(string userName, string mac, string ip)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string cmdTextA = "insert into testuserlogstate " +
                        "(`UserName`, `Mac`, `IP`) values " +
                        " (@UserName, @Mac, @IP); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.Parameters.AddWithValue("@Mac", mac);
                mycmd.Parameters.AddWithValue("@IP", ip);

                mycmd.ExecuteNonQuery();
                return true;
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

        public bool DeleteTestUserLogState(string userName)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string cmdTextA = "delete from testuserlogstate where UserName = @UserName; ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));

                mycmd.ExecuteNonQuery();
                return true;
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
    }
}
