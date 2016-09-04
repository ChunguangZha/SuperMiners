using MetaData.Trade;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public partial class ServiceToAdmin : IServiceToAdmin
    {
        #region IServiceToAdmin Members

        public void LogedIn(string token)
        {
            this.InvokeCallback(token, "LogedIn");
        }

        public void LogedOut(string token)
        {
            this.InvokeCallback(token, "LogedOut");
        }

        public void KickoutByUser(string token)
        {
            this.InvokeCallback(token, "KickoutByUser");
        }

        public void SomebodyWithdrawRMB(string token, WithdrawRMBRecord record)
        {
            this.InvokeCallback(token, "SomebodyWithdrawRMB", record);
        }

        #endregion
    }
}
