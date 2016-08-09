using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    class MineOrderController
    {
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
