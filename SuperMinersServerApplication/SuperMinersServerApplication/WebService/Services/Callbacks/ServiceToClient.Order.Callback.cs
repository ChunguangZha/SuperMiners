using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {
        #region IServiceToClient Members


        public void OrderListChanged(string token)
        {
            this.InvokeCallback(token, "OrderListChanged");
        }

        public void OrderAlipayPaySucceed(string token, int tradeType, string orderNumber)
        {
            this.InvokeCallback(token, "OrderAlipayPaySucceed", tradeType, orderNumber);
        }

        public void AppealOrderFailed(string token, int tradeType, string orderNumber)
        {
            this.InvokeCallback(token, "AppealOrderFailed", tradeType, orderNumber);
        }

        #endregion

    }
}
