using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Controller.Trade;
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

        public string CreateOrderRecord(string userName, int minesCount)
        {
            DateTime timenow = DateTime.Now;
                
            string orderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, AlipayTradeInType.BuyMine);
            MinesBuyRecord record = new MinesBuyRecord()
            {
                OrderNumber = orderNumber,
                CreateTime = timenow,
                UserName = userName,
                GainMinesCount = minesCount,
                GainStonesReserves = minesCount * (int)GlobalConfig.GameConfig.StonesReservesPerMines,
                SpendRMB = (int)Math.Ceiling(minesCount * GlobalConfig.GameConfig.RMB_Mine)
            };

            lock (this._lock)
            {
                this._listRecord.Add(record.OrderNumber, record);
            }

            return orderNumber;
        }

        public bool SaveTempMineTradeRecord(string orderNumber)
        {

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
