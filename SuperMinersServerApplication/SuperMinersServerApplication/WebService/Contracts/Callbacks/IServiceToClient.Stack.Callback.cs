using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Contracts
{
    public partial interface IServiceToClient
    {
        [Callback]
        void DelegateStoneOrderTradeSucceed(string token, string orderNumber, AlipayTradeInType tradeType);

    }
}
