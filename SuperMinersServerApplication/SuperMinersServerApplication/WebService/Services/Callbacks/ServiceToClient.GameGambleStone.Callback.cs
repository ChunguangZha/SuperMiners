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
        #region IServiceToClient Members


        public void GambleStoneWinNotify(string token, MetaData.Game.GambleStone.GambleStoneRoundInfo roundInfo, MetaData.Game.GambleStone.GambleStoneInningInfo inningInfo, MetaData.Game.GambleStone.GambleStonePlayerBetRecord maxWinner)
        {
            this.InvokeCallback(token, "GambleStoneWinNotify", roundInfo, inningInfo, maxWinner);
        }

        #endregion
    }
}
