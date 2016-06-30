using DataBaseProvider;
using MetaData;
using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class OrderController
    {
        #region Single Stone

        private static OrderController _instance = new OrderController();

        public static OrderController Instance
        {
            get { return _instance; }
        }

        private OrderController()
        {

        }

        #endregion

        object _lockListSellOrders = new object();
        private Dictionary<string, OrderRunnable> dicSellOrders = new Dictionary<string, OrderRunnable>();

        private List<BuyStonesOrder> listBuyStonesOrderLast20 = new List<BuyStonesOrder>();

        public bool Init()
        {
            try
            {
                dicSellOrders.Clear();
                var waitOrderDBObjects = DBProvider.OrderDBProvider.GetSellOrderList(new int[] { (int)SellOrderState.Wait }, "");
                foreach (var item in waitOrderDBObjects)
                {
                    var runnable = new OrderRunnable(item);
                    dicSellOrders.Add(item.OrderNumber, new OrderRunnable(item));
                }

                var lockedOrderDBObjects = DBProvider.OrderDBProvider.GetLockSellStonesOrderList("");
                foreach (var item in lockedOrderDBObjects)
                {
                    var runnable = new OrderRunnable(item);
                    dicSellOrders.Add(item.StonesOrder.OrderNumber, runnable);
                }

                var buyOrderRecords = DBProvider.OrderDBProvider.GetBuyStonesOrderListLast20();
                if (buyOrderRecords == null)
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>();
                }
                else
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>(buyOrderRecords);
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init OrderController Error", exc);
                return false;
            }
        }

        public SellStonesOrder[] GetSellOrders()
        {
            lock (_lockListSellOrders)
            {
                List<SellStonesOrder> orders = new List<SellStonesOrder>();
                foreach (var item in dicSellOrders.Values)
                {
                    orders.Add(item.GetSellOrder());
                }

                return orders.ToArray();
            }
        }

        private float GetExpense(float valueRMB)
        {
            float expense = valueRMB * GlobalConfig.GameConfig.ExchangeExpensePercent / 100;
            if (expense < GlobalConfig.GameConfig.ExchangeExpenseMinNumber)
            {
                expense = GlobalConfig.GameConfig.ExchangeExpenseMinNumber;
            }
            return expense;
        }

        private string CreateOrderNumber(string userName, DateTime time)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(time.Year.ToString("0000"));
            builder.Append(time.Month.ToString("00"));
            builder.Append(time.Day.ToString("00"));
            builder.Append(time.Hour.ToString("00"));
            builder.Append(time.Minute.ToString("00"));
            builder.Append(time.Second.ToString("00"));
            builder.Append(time.Millisecond.ToString("0000"));
            builder.Append(userName.GetHashCode());
            builder.Append((new Random()).Next(1000, 9999));
            return builder.ToString();
        }

        public bool CreateSellOrder(string userName, int sellStonesCount)
        {
            float valueRMB = sellStonesCount / GlobalConfig.GameConfig.Stones_RMB;
            DateTime time = DateTime.Now;
            SellStonesOrder order = new SellStonesOrder()
            {
                OrderNumber = CreateOrderNumber(userName, time),
                SellStonesCount = sellStonesCount,
                OrderState = SellOrderState.Wait,
                SellerUserName = userName,
                ValueRMB = valueRMB,
                Expense = GetExpense(valueRMB),
                SellTime = time,
            };

            lock (this._lockListSellOrders)
            {
                var myTrans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    DBProvider.OrderDBProvider.AddSellOrder(order, myTrans);
                    myTrans.Commit();
                }
                catch (Exception exc)
                {
                    myTrans.Rollback();
                    LogHelper.Instance.AddErrorLog("Add Sell Order Exception: " + order.ToString(), exc);
                }
                finally
                {
                    myTrans.Dispose();
                }

                dicSellOrders.Add(order.OrderNumber, new OrderRunnable(order));
            }

            return true;
        }

        public bool LockSellOrder(string sellOrderNumber, string buyerUserName)
        {
            OrderRunnable order = null;
            lock (this._lockListSellOrders)
            {
                this.dicSellOrders.TryGetValue(sellOrderNumber, out order);
            }

            if (order == null)
            {
                return false;
            }

            return order.Lock(buyerUserName);
        }

        public bool Pay(string buyerUserName, float moneyYuan, CustomerMySqlTransaction trans)
        {
            var runnable = FindRunnableByBuyUserName(buyerUserName);
            if (runnable == null)
            {
                return false;
            }

            if (runnable.Pay(moneyYuan, 0, trans))
            {
                lock (this._lockListSellOrders)
                {
                    this.dicSellOrders.Remove(runnable.OrderNumber);
                }

                return true;
            }

            return false;
        }

        private OrderRunnable FindRunnableByBuyUserName(string buyerUserName)
        {
            lock (this._lockListSellOrders)
            {
                foreach (var item in this.dicSellOrders.Values)
                {
                    if (item.CheckBuyerName(buyerUserName))
                    {
                        return item;
                    }
                }

                return null;
            }
        }
    }
}
