using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region SellStone

        public event EventHandler<WebInvokeEventArgs<bool>> SellStoneCompleted;
        public void SellStone(int sellStonesCount, object userState)
        {
            this._invoker.InvokeUserState<bool>(this._context, "SellStone", this.SellStoneCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, sellStonesCount);
        }

        #endregion

    }
}
