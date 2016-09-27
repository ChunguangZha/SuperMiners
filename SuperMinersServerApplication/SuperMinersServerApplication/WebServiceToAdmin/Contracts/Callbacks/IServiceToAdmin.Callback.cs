using MetaData.Game.Roulette;
using MetaData.Trade;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Contracts
{
    public partial interface IServiceToAdmin
    {
        [Callback]
        void LogedIn(string token);

        [Callback]
        void LogedOut(string token);

        [Callback]
        void KickoutByUser(string token);

        [Callback]
        void SomebodyWithdrawRMB(string token, WithdrawRMBRecord record);

        [Callback]
        void SomebodyWinRouletteAward(string token, RouletteWinnerRecord record);
    }
}
