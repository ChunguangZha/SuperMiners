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
        private Dictionary<string, SellStonesOrderUIModel> dicNotFinishedSellOrders = new Dictionary<string, SellStonesOrderUIModel>();

        public void Init()
        {
            dicNotFinishedSellOrders.Clear();
            var sellOrderDBObjects = DBProvider.OrderDBProvider.GetAllFinishedSellOrders();
            if (sellOrderDBObjects != null)
            {
                foreach (var item in sellOrderDBObjects)
                {
                    dicNotFinishedSellOrders.Add(item.OrderNumber, new SellStonesOrderUIModel(item));
                }
            }
        }

        public SellStonesOrder[] GetNotFinishedSellOrders()
        {
            lock (_lockListSellOrders)
            {
                List<SellStonesOrder> orders = new List<SellStonesOrder>();
                foreach (var item in dicNotFinishedSellOrders.Values)
                {
                    if (item.CheckOrderState() != SellOrderState.Finish)
                    {
                        orders.Add(item.ParentObject);
                    }
                }

                return orders.ToArray();
            }
        }

        public bool AddSellOrder(SellStonesOrder order)
        {
            lock (this._lockListSellOrders)
            {
                dicNotFinishedSellOrders.Add(order.OrderNumber, new SellStonesOrderUIModel(order));
            }

            return true;
        }

        public bool LockSellOrder(string sellOrderNumber, string buyerUserName)
        {
            SellStonesOrderUIModel order = null;
            lock (this._lockListSellOrders)
            {
                this.dicNotFinishedSellOrders.TryGetValue(sellOrderNumber, out order);
            }

            if (order == null)
            {
                return false;
            }

            bool isOK = order.LockOrder(buyerUserName);
            if (isOK)
            {
                var trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    isOK = DBProvider.OrderDBProvider.UpdateSellOrderState(order.ParentObject, trans);
                }
                catch (Exception exc)
                {
                    order.UnlockOrder(buyerUserName);
                    LogHelper.Instance.AddErrorLog("玩家：" + buyerUserName + " 锁定订单：" + sellOrderNumber + ", 异常。", exc);
                    trans.Rollback();
                }
                finally
                {
                    trans.Dispose();
                }
            }

            return isOK;
        }
    }
}
