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

        public void DelegateStoneOrderTradeSucceed(string token, string orderNumber, MetaData.Trade.AlipayTradeInType tradeType)
        {
            this.InvokeCallback(token, "DelegateStoneOrderTradeSucceed", token, orderNumber, tradeType);
        }
    }
}
