using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        #region GetGameConfig

        public event EventHandler<WebInvokeEventArgs<SystemConfigin1>> GetGameConfigCompleted;
        public void GetGameConfig()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<SystemConfigin1>(this._context, "GetGameConfig", this.GetGameConfigCompleted, GlobalData.Token);
            }
        }

        #endregion

        #region GetIncomeMoneyAccount

        //public event EventHandler<WebInvokeEventArgs<IncomeMoneyAccount>> GetIncomeMoneyAccountCompleted;
        //public void GetIncomeMoneyAccount()
        //{
        //    if (GlobalData.IsLogined)
        //    {
        //        this._invoker.Invoke<IncomeMoneyAccount>(this._context, "GetIncomeMoneyAccount", this.GetIncomeMoneyAccountCompleted, GlobalData.Token);
        //    }
        //}

        #endregion
    }
}
