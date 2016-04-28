using MetaData.ActionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region GetPlayerAction

        public event EventHandler<WebInvokeEventArgs<PlayerActionLog[]>> GetPlayerActionCompleted;
        public void GetPlayerAction(int year, int month, int day, int hour, int minute, int second)
        {
            this._invoker.Invoke<PlayerActionLog[]>(this._context, "GetPlayerAction", this.GetPlayerActionCompleted, GlobalData.Token, year, month, day, hour, minute, second);
        }

        #endregion

    }
}
