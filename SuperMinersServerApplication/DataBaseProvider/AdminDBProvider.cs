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
    public class AdminDBProvider
    {
        public bool AddAdmin(string userName, string loginPassword, string actionPassword, string mac)
        {
            var myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string cmdTextA = "insert into admininfo " +
                        "(`UserName`, `LoginPassword`, `ActionPassword`, `Mac`) values " +
                        " (@UserName, @LoginPassword, @ActionPassword, @Mac) ; ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.Parameters.AddWithValue("@LoginPassword", DESEncrypt.EncryptDES(loginPassword));
                mycmd.Parameters.AddWithValue("@ActionPassword", DESEncrypt.EncryptDES(actionPassword));
                mycmd.Parameters.AddWithValue("@Mac", mac);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public bool EditAdmin(string userName, string loginPassword, string actionPassword, string mac)
        {
            var myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string cmdTextA = "update admininfo set `UserName` = @UserName, `LoginPassword` = @LoginPassword, `Mac` = @Mac where `UserName` = @UserName ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.Parameters.AddWithValue("@LoginPassword", DESEncrypt.EncryptDES(loginPassword));
                mycmd.Parameters.AddWithValue("@ActionPassword", DESEncrypt.EncryptDES(actionPassword));
                mycmd.Parameters.AddWithValue("@Mac", mac);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public AdminInfo[] GetAllAdmin()
        {
            AdminInfo[] listAdmin = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from admininfo";
                mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                listAdmin = MetaDBAdapter<AdminInfo>.GetAdminInfoListFromDataTable(dt);

                return listAdmin;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public AdminInfo GetAdmin(string userName)
        {
            AdminInfo admin = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select * from AdminInfo where `UserName` = @UserName";
                mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                admin = MetaDBAdapter<AdminInfo>.GetAdminInfoListFromDataTable(dt)[0];

                return admin;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public bool DeleteAdmin(string userName)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                string textB = "delete from admininfo where UserName = @UserName;";

                myconn.Open();
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = textB;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }
    }
}
