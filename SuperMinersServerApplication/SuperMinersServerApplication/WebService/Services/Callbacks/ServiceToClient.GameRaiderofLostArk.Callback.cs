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
        public void PlayerJoinRaiderSucceed(string token, MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo roundInfo)
        {
            this.InvokeCallback(token, "PlayerJoinRaiderSucceed", roundInfo);
        }

        public void PlayerWinedRaiderNotify(string token, MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo roundInfo)
        {
            this.InvokeCallback(token, "PlayerWinedRaiderNotify", roundInfo);
        }

    }
}
