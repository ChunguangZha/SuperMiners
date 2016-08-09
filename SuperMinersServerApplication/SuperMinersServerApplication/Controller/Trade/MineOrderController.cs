using MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    class MineOrderController
    {
        private object _lock = new object();
        private Dictionary<string, MinesBuyRecord> _listRecord = new Dictionary<string, MinesBuyRecord>();

        public bool AddBuyRecord(MinesBuyRecord record)
        {
            lock (this._lock)
            {
                this._listRecord.Add(record.OrderNumber, record);
            }

            return true;
        }

        public int BuyMineByRMB(string userName, int minesCount)
        {
            PlayerRunnable playerrun = this.GetOnlinePlayerRunnable(userName);
            if (playerrun == null)
            {
                return -1;
            }

            return playerrun.BuyMineByRMB(minesCount);
        }

        public string BuyMineByAlipay(string userName, int minesCount)
        {
            
        }

    }
}
