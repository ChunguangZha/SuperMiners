using MetaData;
using SuperMinersServerApplication.UIModel;
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
        List<SellStonesOrderUIModel> listSellOrders = new List<SellStonesOrderUIModel>();

        public void Init()
        {
            listSellOrders.Clear();
            var sellOrderDBObjects = DBProvider.OrderDBProvider.GetAllSellOrders();
            if (sellOrderDBObjects != null)
            {
                foreach (var item in sellOrderDBObjects)
                {
                    listSellOrders.Add(new SellStonesOrderUIModel(item));
                }
            }
        }

        public bool AddSellOrder(SellStonesOrder order)
        {
            lock (this._lockListSellOrders)
            {
                listSellOrders.Add(new SellStonesOrderUIModel(order));
            }

            return true;
        }

        public bool LockSellOrder(string sellOrderNumber, string buyerUserName)
        {
            SellStonesOrderUIModel order = null;
            lock (this._lockListSellOrders)
            {
                order = this.listSellOrders.FirstOrDefault(o => o.OrderNumber.Equals(sellOrderNumber));
            }

            if (order == null)
            {
                return false;
            }

            return order.LockOrder(buyerUserName);
        }
    }
}
