using MetaData.User;
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
        public void Kickout(string token)
        {
            this.InvokeCallback(token, "Kickout");
        }

        public void KickoutByUser(string token)
        {
            this.InvokeCallback(token, "KickoutByUser");
        }

        public void LogedIn(string token)
        {
            this.InvokeCallback(token, "LogedIn");
        }

        public void LogedOut(string token)
        {
            this.InvokeCallback(token, "LogedOut");
        }

        public void PlayerInfoChanged(string token)
        {
            this.InvokeCallback(token, "PlayerInfoChanged");
        }
    }
}
