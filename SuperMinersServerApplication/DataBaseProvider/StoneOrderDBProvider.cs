﻿using MetaData;
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
        public PlayerLastSellStoneRecord GetPlayerLastSellStoneRecord(int userID)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlText = "select * from playerlastsellstonerecord where `UserID` = @UserID;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                myconn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                var records = MetaDBAdapter<PlayerLastSellStoneRecord>.GetPlayerLastSellStoneRecord(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();

                if (records.Length == 0)
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
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public bool AddSellOrder(SellStonesOrder order, int userID, CustomerMySqlTransaction trans)
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

                PlayerLastSellStoneRecord lastrecord = new PlayerLastSellStoneRecord()
                {
                    UserID = userID,
                    SellStoneOrderNumber = order.OrderNumber,
                    SellTime = order.SellTime
                };
                string cmdTextB = "delete from playerlastsellstonerecord where `UserID` = @UserID ;" +
                    "insert into playerlastsellstonerecord " +
                    "(`UserID`, `SellStoneOrderNumber`, `SellTime` ) " +
                    " values " +
                    "(@UserID, @OrderNumber, @SellTime ); ";

                mycmd.CommandText = cmdTextB;
                mycmd.Parameters.AddWithValue("@UserID", lastrecord.UserID);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool CancelSellOrder(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA = "delete from sellstonesorder where OrderNumber = @OrderNumber;";

                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);

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

        public bool UpdateAllSellOrderState(SellOrderState oldState, SellOrderState newState, CustomerMySqlTransaction trans)
        {
            //1.修改订单状态；
            //2.删除锁定信息记录。
            MySqlCommand mycmd = null;
            try
            {
                string textA = "update sellstonesorder set OrderState = @NewOrderState where OrderState = @OldOrderState;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = textA;
                mycmd.Parameters.AddWithValue("@NewOrderState", (int)newState);
                mycmd.Parameters.AddWithValue("@OldOrderState", (int)oldState);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
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

        public bool SetSellOrderException(string orderNumber)
        {
            //1.修改订单状态；
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            MySqlCommand mycmd = null;
            try
            {
                string textA = "update sellstonesorder set OrderState = @OrderState where OrderNumber = @OrderNumber;";

                myconn.Open();
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = textA;
                mycmd.Parameters.AddWithValue("@OrderState", (int)SellOrderState.Exception);
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

        public bool CheckBuyStoneOrderExist(string userName, string orderNumber)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select count(id) from buystonesrecord where OrderNumber = @OrderNumber and BuyerUserName = @BuyerUserName ";
                mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                mycmd.Parameters.AddWithValue("@BuyerUserName", DESEncrypt.EncryptDES(userName));
                mycmd.CommandText = sqlTextA;
                object objValue = mycmd.ExecuteScalar();
                mycmd.Dispose();

                int value = Convert.ToInt32(objValue);
                if (value == 1)
                {
                    return true;
                }

                return false;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sellerUserName"></param>
        /// <param name="orderNumber"></param>
        /// <param name="buyUserName"></param>
        /// <param name="orderState">0表示全部</param>
        /// <param name="myBeginCreateTime"></param>
        /// <param name="myEndCreateTime"></param>
        /// <param name="myBeginBuyTime"></param>
        /// <param name="myEndBuyTime"></param>
        /// <param name="pageItemCount"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public BuyStonesOrder[] GetBuyStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            BuyStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select b.*, s.*, f.CreditValue as SellerCreditValue, f.Exp as SellerExpValue " +
                                    "from buystonesrecord b " +
                                    "left join sellstonesorder s on s.OrderNumber = b.OrderNumber " +
                                "left join playerfortuneinfo f on f.userId = (select u.id from playersimpleinfo u where u.UserName = s.SellerUserName)";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(sellerUserName))
                {
                    builder.Append(" s.SellerUserName = @SellerUserName ");
                    string encryptSellerUserName = DESEncrypt.EncryptDES(sellerUserName);
                    mycmd.Parameters.AddWithValue("@SellerUserName", encryptSellerUserName);
                }
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                }
                if (!string.IsNullOrEmpty(buyUserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" b.BuyerUserName = @BuyerUserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(buyUserName);
                    mycmd.Parameters.AddWithValue("@BuyerUserName", encryptUserName);
                }
                if (orderState > 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderState = @OrderState ");
                    mycmd.Parameters.AddWithValue("@OrderState", orderState);
                }
                if (myBeginCreateTime != null && !myBeginCreateTime.IsNull && myEndCreateTime != null && !myEndCreateTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginCreateTime = myBeginCreateTime.ToDateTime();
                    DateTime endCreateTime = myEndCreateTime.ToDateTime();
                    if (beginCreateTime >= endCreateTime)
                    {
                        return null;
                    }
                    builder.Append(" s.SellTime >= @beginCreateTime and s.SellTime < @endCreateTime ");
                    mycmd.Parameters.AddWithValue("@beginCreateTime", beginCreateTime);
                    mycmd.Parameters.AddWithValue("@endCreateTime", endCreateTime);
                }
                if (myBeginBuyTime != null && !myBeginBuyTime.IsNull && myEndBuyTime != null && !myEndBuyTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime beginBuyTime = myBeginBuyTime.ToDateTime();
                    DateTime endBuyTime = myEndBuyTime.ToDateTime();
                    if (beginBuyTime >= endBuyTime)
                    {
                        return null;
                    }
                    builder.Append(" b.BuyTime >= @beginBuyTime and b.BuyTime < @endBuyTime ");
                    mycmd.Parameters.AddWithValue("@beginBuyTime", beginBuyTime);
                    mycmd.Parameters.AddWithValue("@endBuyTime", endBuyTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by b.id desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

                mycmd.CommandText = sqlAllText;

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table != null)
                {
                    orders = MetaDBAdapter<BuyStonesOrder>.GetBuyStonesOrderFromDataTable(table);
                }
                table.Clear();
                table.Dispose();
                adapter.Dispose();

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
        public SellStonesOrder[] GetSellOrderList(string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            SellStonesOrder[] orders = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select s.*, f.CreditValue as SellerCreditValue, f.Exp as SellerExpValue from sellstonesorder s " +
                                "left join playerfortuneinfo f on f.userId = (select u.id from playersimpleinfo u where u.UserName = s.SellerUserName)";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(sellerUserName))
                {
                    builder.Append(" s.SellerUserName = @SellerUserName ");
                    string encryptSellerUserName = DESEncrypt.EncryptDES(sellerUserName);
                    mycmd.Parameters.AddWithValue("@SellerUserName", encryptSellerUserName);
                }
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                }
                if (myBeginCreateTime != null && !myBeginCreateTime.IsNull && myEndCreateTime != null && !myEndCreateTime.IsNull)
                {
                    DateTime beginTime = myBeginCreateTime.ToDateTime();
                    DateTime endTime = myEndCreateTime.ToDateTime();
                    if (beginTime >= endTime)
                    {
                        return null;
                    }
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append("  s.SellTime >= @beginTime and s.SellTime < @endTime ");
                    mycmd.Parameters.AddWithValue("@beginTime", beginTime);
                    mycmd.Parameters.AddWithValue("@endTime", endTime);
                }

                if (orderState > 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderState = @OrderState ");
                    mycmd.Parameters.AddWithValue("@OrderState", orderState);
                }

                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by s.SellTime desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

                mycmd.CommandText = sqlAllText;

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table != null)
                {
                    orders = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(table);
                }

                table.Clear();
                table.Dispose();
                adapter.Dispose();

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
        public LockSellStonesOrder[] GetLockSellStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState)
        {
            LockSellStonesOrder[] orders = null;
            MySqlConnection myconn = MyDBHelper.Instance.CreateConnection();
            try
            {
                DataTable table = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                MySqlCommand mycmd = myconn.CreateCommand();

                string sqlTextA = "select l.*, s.*, f.CreditValue as SellerCreditValue, f.Exp as SellerExpValue " +
                                "from locksellstonesorder l " +
                                "left join sellstonesorder s on s.OrderNumber = l.OrderNumber " +
                                "left join playerfortuneinfo f on f.userId = (select u.id from playersimpleinfo u where u.UserName = s.SellerUserName)";

                StringBuilder builder = new StringBuilder();
                if (!string.IsNullOrEmpty(sellerUserName))
                {
                    builder.Append(" s.SellerUserName = @SellerUserName ");
                    string encryptSellerUserName = DESEncrypt.EncryptDES(sellerUserName);
                    mycmd.Parameters.AddWithValue("@SellerUserName", encryptSellerUserName);
                }
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderNumber = @OrderNumber ");
                    mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);
                }
                if (!string.IsNullOrEmpty(buyUserName))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" l.LockedByUserName = @BuyerUserName ");
                    string encryptUserName = DESEncrypt.EncryptDES(buyUserName);
                    mycmd.Parameters.AddWithValue("@BuyerUserName", encryptUserName);
                }
                if (orderState > 0)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" and ");
                    }
                    builder.Append(" s.OrderState = @OrderState ");
                    mycmd.Parameters.AddWithValue("@OrderState", orderState);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }
                
                string sqlAllText = sqlTextA + sqlWhere;
                mycmd.CommandText = sqlAllText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table != null)
                {
                    orders = MetaDBAdapter<LockSellStonesOrder>.GetLockStonesOrderListFromDataTable(table);
                }
                table.Clear();
                table.Dispose();
                adapter.Dispose();

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
                DataTable table = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select s.*, f.CreditValue as SellerCreditValue, f.Exp as SellerExpValue " +
                                " from sellstonesorder s  left join playerfortuneinfo f on f.userId = (select u.id from playersimpleinfo u where u.UserName = s.SellerUserName)" + 
                                " where s.OrderNumber = @OrderNumber";

                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@OrderNumber", orderNumber);

                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                if (table != null && table.Rows.Count > 0)
                {
                    order = MetaDBAdapter<SellStonesOrder>.GetSellStonesOrderFromDataTable(table)[0];
                }
                table.Clear();
                table.Dispose();
                adapter.Dispose();

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
