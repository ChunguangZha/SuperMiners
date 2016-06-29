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
                var waitOrderDBObjects = DBProvider.OrderDBProvider.GetSellOrderList((int)SellOrderState.Wait, "");
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

        public bool AddSellOrder(SellStonesOrder order)
        {
            lock (this._lockListSellOrders)
            {
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
