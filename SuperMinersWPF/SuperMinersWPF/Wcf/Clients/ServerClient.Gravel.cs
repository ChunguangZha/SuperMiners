using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region RequestGravel

        public event EventHandler<WebInvokeEventArgs<int>> RequestGravelCompleted;
        public void RequestGravel(object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "RequestGravel", this.RequestGravelCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region GetGravel

        public event EventHandler<WebInvokeEventArgs<int>> GetGravelCompleted;
        public void GetGravel(object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "GetGravel", this.GetGravelCompleted, userState, GlobalData.Token);
        }

        #endregion

    }
}
