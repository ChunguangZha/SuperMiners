using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region BuyMiner

        public event EventHandler<WebInvokeEventArgs<int>> BuyMinerCompleted;
        public void BuyMiner(int minersCount)
        {
            this._invoker.Invoke<int>(this._context, "BuyMiner", this.BuyMinerCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName, minersCount);
        }

        #endregion

        #region BuyMine

        public event EventHandler<WebInvokeEventArgs<int>> BuyMineCompleted;
        public void BuyMine(int minesCount)
        {
            this._invoker.Invoke<int>(this._context, "BuyMine", this.BuyMineCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName, minesCount);
        }

        #endregion

        #region GatherStones

        public event EventHandler<WebInvokeEventArgs<float>> GatherStonesCompleted;
        public void GatherStones()
        {
            this._invoker.Invoke<float>(this._context, "GatherStones", this.GatherStonesCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName);
        }

        #endregion

    }
}
