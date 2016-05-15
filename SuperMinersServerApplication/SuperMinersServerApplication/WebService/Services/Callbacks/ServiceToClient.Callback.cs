using MetaData.ActionLog;
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
        public void SendMessage(string token, string message)
        {
            this.InvokeCallback(token, "SendMessage", message);
        }

        public void SendPlayerActionLog(string toke)
        {
            this.InvokeCallback(toke, "SendPlayerActionLog");
        }
    }
}
