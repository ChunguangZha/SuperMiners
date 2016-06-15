using MetaData;
using MetaData.Trade;
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

        public bool LockOrder(LockSellStonesOrder lockOrder, CustomerMySqlTransaction trans)
        {
            //1.修改订单状态；
            //2.添加锁定信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textA = "update sellstonesorder set OrderState = @OrderState where OrderNumber = @OrderNumber;";
                string textB = "insert into locksellstonesorder " +
                    "(`SellOrderID`, `LockedByUserID`, `LockedTime` ) " +
                    " values " +
                    "((select b.id from sellstonesorder b where b.OrderNumber = @OrderNumber), (select c.id from playersimpleinfo b where c.UserName = @UserName), @LockedTime); ";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textA + textB;
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Lock);
                mycmd.Parameters.AddWithValue("@OrderNumber", lockOrder.OrderNumber);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(lockOrder.LockedByUserName));
                mycmd.Parameters.AddWithValue("@LockedTime", lockOrder.LockedTime);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool ReleaseOrderLock(LockSellStonesOrder lockOrder, CustomerMySqlTransaction trans)
        {
            //1.修改订单状态；
            //2.删除锁定信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textA = "update sellstonesorder set OrderState = @OrderState where OrderNumber = @OrderNumber;";
                string textB = "delete locksellstonesorder b where b.SellOrderID = (select c.id from sellstonesorder c where c.OrderNumber = @OrderNumber);";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textA + textB;
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Lock);
                mycmd.Parameters.AddWithValue("@OrderNumber", lockOrder.OrderNumber);
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
                    + " `OrderState`=@OrderState where OrderNumber=@OrderNumber;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@OrderState", order.OrderState);
                mycmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool PayOrder(BuyStonesOrder buyOrder, SellStonesOrder sellOrder, LockSellStonesOrder lockOrder, CustomerMySqlTransaction trans)
        {
            //1.删除订单状态；
            //2.删除锁定信息记录；
            //3.添加购买信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textA = "delete sellstonesorder where OrderNumber = @OrderNumber;";
                //先尝试会否级联删除。
                //string textB = "delete locksellstonesorder b where b.SellOrderID = (select c.id from sellstonesorder c where c.OrderNumber = @OrderNumber);"

                string textC = "insert into buystonesrecord " +
                    "(`OrderNumber`, `SellerUserID`, `SellStonesCount`, `Expense`, `GainRMB`, `SellTime`, `OrderState` ) " +
                    " values " +
                    "(@OrderNumber, (select b.id from playersimpleinfo b where b.UserName = @SellerUserName), @SellStonesCount, @Expense, @GainRMB, @SellTime, @OrderState); ";


                mycmd = trans.CreateCommand();
                mycmd.CommandText = textA;// +textB;
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Lock);
                mycmd.Parameters.AddWithValue("@OrderNumber", lockOrder.OrderNumber);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public BuyStonesOrder[] GetBuyStonesOrderList()
        {
            return null;
        }

        public SellStonesOrder[] GetSellOrderList()
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

        public LockSellStonesOrder[] GetLockSellStonesOrderList()
        {
            return null;
        }

        public SellStonesOrder GetSellOrder(string orderNumber)
        {
            return null;
        }
    }
}
