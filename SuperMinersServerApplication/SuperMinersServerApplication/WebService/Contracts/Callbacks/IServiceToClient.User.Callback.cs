using MetaData.User;
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
        void Kickout(string token);

        [Callback]
        void KickoutByUser(string token);

        [Callback]
        void LogedIn(string token);

        [Callback]
        void LogedOut(string token);

        [Callback]
        void PlayerInfoChanged(string token);
    }
}
