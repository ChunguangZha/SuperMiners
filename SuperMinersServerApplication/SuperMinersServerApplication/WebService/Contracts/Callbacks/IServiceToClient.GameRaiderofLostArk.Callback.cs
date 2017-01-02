using MetaData.Game.RaideroftheLostArk;
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
        void PlayerJoinRaiderSucceed(string token, RaiderRoundMetaDataInfo roundInfo);

        [Callback]
        void PlayerWinedRaiderNotify(string token, RaiderRoundMetaDataInfo roundInfo);
    }
}
