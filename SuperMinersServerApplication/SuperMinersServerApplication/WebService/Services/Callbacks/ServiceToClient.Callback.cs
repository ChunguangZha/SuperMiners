﻿using MetaData.ActionLog;
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

        public void SendPlayerActionLog(string token)
        {
            this.InvokeCallback(token, "SendPlayerActionLog");
        }

        public void SendGameConfig(string token)
        {
            this.InvokeCallback(token, "SendGameConfig");
        }

        public void SendNewNotice(string token, string title)
        {
            this.InvokeCallback(token, "SendNewNotice", title);
        }
    }
}
