using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class OrderDBProvider
    {
        public bool AddSellOrder(SellStonesOrder order, CustomerMySqlTransaction trans)
        {
            return false;
        }

        public SellStonesOrder[] GetAllSellOrders()
        {
            return null;
        }

        public SellStonesOrder GetSellOrder(string orderNumber)
        {
            return null;
        }
    }
}
