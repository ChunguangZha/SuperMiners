using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class StoneOrderViewModel
    {
        public void AsyncSellStone(int sellStonesCount)
        {
            GlobalData.Client.SellStone(sellStonesCount, null);
        }

        public void RegisterEvent()
        {
            GlobalData.Client.SellStoneCompleted += Client_SellStoneCompleted;
        }

        void Client_SellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            throw new NotImplementedException();
        }
    }
}
