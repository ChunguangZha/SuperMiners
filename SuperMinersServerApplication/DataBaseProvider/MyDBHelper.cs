using MetaData;
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

        internal static readonly string CONNECTIONSTRING = "server=localhost;port=13344; uid=superminersDBA;pwd=dba!@#123;database=superminers;charset=utf8; pooling=false; Keep Alive=5; Allow User Variables=True;";

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


        public int TransactionDataBaseOper(TransactionDBOperDelegte DBOper, TransactionDBOperFailedDelegate FaileOper)
        {
            CustomerMySqlTransaction myTrans = null;
            try
            {
                myTrans = MyDBHelper.Instance.CreateTrans();
                int result = DBOper(myTrans);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    myTrans.Commit();
                }
                else
                {
                    myTrans.Rollback();
                }
                return result;
            }
            catch (Exception exc)
            {
                myTrans.Rollback();
                FaileOper(exc);
                return OperResult.RESULTCODE_FALSE;
            }
            finally
            {
                myTrans.Dispose();
            }
        }

        public bool ConnectionCommandExecuteNonQuery(ConnectionCommandDBOper DBOper)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = this.CreateConnection();
                mycmd = myconn.CreateCommand();
                myconn.Open();
                DBOper(mycmd);
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

        public bool ConnectionCommandSelect(ConnectionCommandDBOper DBOper)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = this.CreateConnection();
                mycmd = myconn.CreateCommand();
                myconn.Open();
                DBOper(mycmd);
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

    public delegate int TransactionDBOperDelegte(CustomerMySqlTransaction myTrans);

    public delegate void TransactionDBOperFailedDelegate(Exception exc);

    public delegate void ConnectionCommandDBOper(MySqlCommand mycmd);


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
