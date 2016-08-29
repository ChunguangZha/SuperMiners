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
        void OrderListChanged(string token);

        [Callback]
        void OrderAlipayPaySucceed(string token, int tradeType, string orderNumber);

        [Callback]
        void AppealOrderFailed(string token, int tradeType, string orderNumber);
    }
}
