using MetaData.Game.Roulette;
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
        public void GameRouletteWinNotify(string token, string awardInfo)
        {
            this.InvokeCallback(token, "GameRouletteWinNotify", awardInfo);
        }

        public void GameRouletteWinRealAwardPaySucceed(string token, RouletteWinnerRecord record)
        {
            this.InvokeCallback(token, "GameRouletteWinRealAwardPaySucceed", record);
        }
    }
}
