using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class MyDBHelper
    {
        #region Single

        private static MyDBHelper _instance = new MyDBHelper();

        public static MyDBHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        private MyDBHelper()
        {

        }

        #endregion

        internal static readonly string CONNECTIONSTRING = "server=localhost;port=13344; uid=superminersDBA;pwd=dba!@#123;database=superminers;charset=utf8; pooling=false; Keep Alive=5;";

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(CONNECTIONSTRING);
        }


        public void DisposeConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public CustomerMySqlTransaction CreateTrans()
        {
            return new CustomerMySqlTransaction(CreateConnection());
        }

        public bool Execute()
        {
            return false;
        }
    }

    public class CustomerMySqlTransaction
    {
        public MySqlConnection MyConn { get; private set; }
        public MySqlTransaction MyTrans { get; private set; }

        public CustomerMySqlTransaction(MySqlConnection connection)
        {
            this.MyConn = connection;
            this.MyConn.Open();
            this.MyTrans = MyConn.BeginTransaction();
        }

        public MySqlCommand CreateCommand()
        {
            var cmd = this.MyConn.CreateCommand();
            cmd.Transaction = this.MyTrans;
            return cmd;
        }

        public void Commit()
        {
            this.MyTrans.Commit();
        }

        public void Rollback()
        {
            try
            {
                this.MyTrans.Rollback();
            }
            catch { }
        }

        public void Dispose()
        {
            if (MyTrans != null)
            {
                MyTrans.Dispose();
                MyTrans = null;
            }
            if (MyConn != null)
            {
                MyConn.Close();
                MyConn.Dispose();
                MyConn = null;
            }
        }
    }
}
