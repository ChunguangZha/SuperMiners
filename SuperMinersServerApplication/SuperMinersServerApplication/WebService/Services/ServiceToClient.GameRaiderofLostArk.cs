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
        public MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo GetCurrentRaiderRoundInfo(string token)
        {
            throw new NotImplementedException();
        }

        public MetaData.OperResultObject JoinRaider(string token, string userName, int roundID, int betStoneCount)
        {
            throw new NotImplementedException();
        }

        public MetaData.Game.RaideroftheLostArk.PlayerBetInfo[] GetPlayerselfBetInfo(string token, int roundID, int pageItemCount, int pageIndex)
        {
            throw new NotImplementedException();
        }
    }
}
