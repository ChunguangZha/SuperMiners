using MetaData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class OrderDBProvider
    {
        public bool AddSellOrder(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA = "insert into sellstonesorder " +
                    "(`OrderNumber`, `SellerUserID`, `SellStonesCount`, `Expense`, `GainRMB`, `SellTime`, `OrderState` ) " +
                    " values " +
                    "(@OrderNumber, (select b.id from playersimpleinfo b where b.UserName = @SellerUserName), @SellStonesCount, @Expense, @GainRMB, @SellTime, @OrderState); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                mycmd.Parameters.AddWithValue("@SellerUserName", DESEncrypt.EncryptDES(order.SellerUserName));
                mycmd.Parameters.AddWithValue("@SellStonesCount", order.SellStonesCount);
                mycmd.Parameters.AddWithValue("@Expense", order.Expense);
                mycmd.Parameters.AddWithValue("@GainRMB", order.GainRMB);
                mycmd.Parameters.AddWithValue("@SellTime", order.SellTime);
                mycmd.Parameters.AddWithValue("@OrderState", order.OrderState);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool UpdateSellOrderState(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "UPDATE `sellstonesorder` SET "
                    + " `OrderState`=@OrderState, `LockedByUserID`(select b.id from playersimpleinfo b where b.UserName = @LockedByUserName), `LockedTime`=@LockedTime;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@OrderState", order.OrderState);
                mycmd.Parameters.AddWithValue("@LockedByUserName", DESEncrypt.EncryptDES(order.LockedByUserName));
                mycmd.Parameters.AddWithValue("@LockedTime", order.LockedTime);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public SellStonesOrder[] GetAllFinishedSellOrders()
        {
            SellStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, b.UserName as SellerUserName, c.UserName as BuyerUserName " +
                                "from sellstonesorder a " +
                                "left join playersimpleinfo b on a.SellerUserID = b.id " +
                                "left join playersimpleinfo c on a.LockedByUserID = c.id " +
                                "where a.OrderState = @OrderState;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Finish);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                orders = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(dt);

                mycmd.Dispose();

                return orders;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public SellStonesOrder[] GetAllNotFinishedSellOrders()
        {
            SellStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, b.UserName as SellerUserName, c.UserName as BuyerUserName " + 
                                "from sellstonesorder a " +
                                "left join playersimpleinfo b on a.SellerUserID = b.id " +
                                "left join playersimpleinfo c on a.LockedByUserID = c.id " +
                                "where a.OrderState != @OrderState;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Finish);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                orders = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(dt);

                mycmd.Dispose();

                return orders;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public SellStonesOrder GetSellOrder(string orderNumber)
        {
            return null;
        }
    }
}
