using MetaData.Game.Roulette;
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
        void GameRouletteWinNotify(string token, string awardInfo);

        [Callback]
        void GameRouletteWinRealAwardPaySucceed(string token, RouletteWinnerRecord record);
    }
}
