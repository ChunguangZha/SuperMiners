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
    public class StoneOrderDBProvider
    {
        public bool AddSellOrder(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA = "insert into sellstonesorder " +
                    "(`OrderNumber`, `SellerUserName`, `SellStonesCount`, `Expense`, `ValueRMB`, `SellTime`, `OrderState` ) " +
                    " values " +
                    "(@OrderNumber, @SellerUserName, @SellStonesCount, @Expense, @ValueRMB, @SellTime, @OrderState); ";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                mycmd.Parameters.AddWithValue("@SellerUserName", DESEncrypt.EncryptDES(order.SellerUserName));
                mycmd.Parameters.AddWithValue("@SellStonesCount", order.SellStonesCount);
                mycmd.Parameters.AddWithValue("@Expense", order.Expense);
                mycmd.Parameters.AddWithValue("@ValueRMB", order.ValueRMB);
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

        public bool CancelSellOrder(SellStonesOrder order)
        {
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myconn.CreateCommand();

                string cmdTextA = "delete from sellstonesorder where OrderNumber = @OrderNumber;";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool LockOrder(LockSellStonesOrder lockOrder, CustomerMySqlTransaction trans)
        {
            //1.修改订单状态；
            //2.添加锁定信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textDel = "delete from locksellstonesorder where OrderNumber = @OrderNumber;";
                string textA = "update sellstonesorder set OrderState = @OrderState where OrderNumber = @OrderNumber;";
                string textB = "insert into locksellstonesorder " +
                    "(`OrderNumber`, `PayUrl`, `LockedByUserName`, `LockedTime` ) " +
                    " values " +
                    "(@OrderNumber, @PayUrl, @LockedByUserName, @LockedTime); ";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textDel + textA + textB;
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Lock);
                mycmd.Parameters.AddWithValue("@OrderNumber", lockOrder.StonesOrder.OrderNumber);
                mycmd.Parameters.AddWithValue("@PayUrl", lockOrder.PayUrl);
                mycmd.Parameters.AddWithValue("@LockedByUserName", DESEncrypt.EncryptDES(lockOrder.LockedByUserName));
                mycmd.Parameters.AddWithValue("@LockedTime", lockOrder.LockedTime);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool ReleaseOrderLock(string orderNumber, CustomerMySqlTransaction trans)
        {
            return UpdateSellOrderState(orderNumber, SellOrderState.Wait, trans);
        }

        public bool FinishOrderLock(string orderNumber, CustomerMySqlTransaction trans)
        {
            return UpdateSellOrderState(orderNumber, SellOrderState.Finish, trans);
        }

        private bool UpdateSellOrderState(string orderNumber, SellOrderState state, CustomerMySqlTransaction trans)
        {
            //1.修改订单状态；
            //2.删除锁定信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textA = "update sellstonesorder set OrderState = @OrderState where OrderNumber = @OrderNumber;";
                string textB = "delete from locksellstonesorder where OrderNumber = @OrderNumber;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textA + textB;
                mycmd.Parameters.AddWithValue("@OrderState", (int)state);
                mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool PayOrder(BuyStonesOrder buyOrder, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string textC = "insert into buystonesrecord " +
                    "(`OrderNumber`, `BuyerUserName`, `BuyTime`, `AwardGoldCoin` ) " +
                    " values " +
                    "(@OrderNumber, @BuyerUserName, @BuyTime, @AwardGoldCoin); ";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textC;
                mycmd.Parameters.AddWithValue("@OrderNumber", buyOrder.StonesOrder.OrderNumber);
                string encryptUserName = DESEncrypt.EncryptDES(buyOrder.BuyerUserName);
                mycmd.Parameters.AddWithValue("@BuyerUserName", encryptUserName);
                mycmd.Parameters.AddWithValue("@BuyTime", buyOrder.BuyTime);
                mycmd.Parameters.AddWithValue("@AwardGoldCoin", buyOrder.AwardGoldCoin);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public BuyStonesOrder[] GetBuyStonesOrderList(string userName, DateTime beginTime, DateTime endTime)
        {
            if (beginTime >= endTime)
            {
                return null;
            }
            BuyStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("select b.*, s.* ");
                builder.Append("from buystonesrecord b ");
                builder.Append("left join sellstonesorder s on s.OrderNumber = b.OrderNumber ");
                builder.Append(" where ");
                if (!string.IsNullOrEmpty(userName))
                {
                    builder.Append(" b.BuyerUserName = @BuyerUserName and ");
                }
                builder.Append(" b.BuyTime >= @beginTime and b.BuyTime < @endTime ;");
                string cmdText = builder.ToString();

                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@beginTime", beginTime);
                mycmd.Parameters.AddWithValue("@endTime", endTime);
                if (!string.IsNullOrEmpty(userName))
                {
                    string encryptUserName = DESEncrypt.EncryptDES(userName);
                    mycmd.Parameters.AddWithValue("@BuyerUserName", encryptUserName);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    orders = MetaDBAdapter<BuyStonesOrder>.GetBuyStonesOrderFromDataTable(dt);
                }
                mycmd.Dispose();

                return orders;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderState">null表示全部状态, 可以多种状态组合查询</param>
        /// <param name="userName">""表示全部玩家</param>
        /// <returns></returns>
        public SellStonesOrder[] GetSellOrderList(int[] orderStates, string userName, DateTime beginTime, DateTime endTime)
        {
            if (beginTime >= endTime)
            {
                return null;
            }
            SellStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                StringBuilder builder = new StringBuilder();
                builder.Append("select s.* from sellstonesorder s ");
                builder.Append(" where s.SellTime >= @beginTime and s.SellTime < @endTime ");

                if (!string.IsNullOrEmpty(userName))
                {
                    builder.Append(" and s.SellerUserName = @SellerUserName ");
                }
                if (orderStates != null && orderStates.Length != 0)
                {
                    builder.Append(" and s.OrderState in ( ");
                    for (int i = 0; i < orderStates.Length; i++)
                    {
                        builder.Append(" @OrderState" + i.ToString());
                        if (i != orderStates.Length - 1)
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append(" )");
                }

                string cmdText = builder.ToString();
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@beginTime", beginTime);
                mycmd.Parameters.AddWithValue("@endTime", endTime);
                if (orderStates != null && orderStates.Length != 0)
                {
                    for (int i = 0; i < orderStates.Length; i++)
                    {
                        mycmd.Parameters.AddWithValue("@OrderState" + i.ToString(), orderStates[i]);
                    }
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    string encryptUserName = DESEncrypt.EncryptDES(userName);
                    mycmd.Parameters.AddWithValue("@SellerUserName", encryptUserName);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    orders = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(dt);
                }

                mycmd.Dispose();

                return orders;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName">""表示所有玩家</param>
        /// <returns></returns>
        public LockSellStonesOrder[] GetLockSellStonesOrderList(string userName)
        {
            LockSellStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select l.*, s.* " +
                                "from locksellstonesorder l " +
                                "left join sellstonesorder s on s.OrderNumber = l.OrderNumber ";
                if (!string.IsNullOrEmpty(userName))
                {
                    cmdText += " where l.LockedByUserName = @LockedByUserName ";
                }

                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null)
                {
                    orders = MetaDBAdapter<LockSellStonesOrder>.GetLockStonesOrderListFromDataTable(dt);
                }
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
            SellStonesOrder order = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select s.* from sellstonesorder s where s.OrderNumber = @OrderNumber";

                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    order = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(dt)[0];
                }
                mycmd.Dispose();

                return order;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }
    }
}
