using SuperMinersServerApplication.Controller.Game;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public partial class ServiceToAdmin : IServiceToAdmin
    {

        public MetaData.Game.Roulette.RouletteAwardItem[] GetAwardItems(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return RouletteAwardController.Instance.GetAwardItems();
            }
            else
            {
                throw new Exception();
            }
        }

        public bool SetAwardItems(string token, MetaData.Game.Roulette.RouletteAwardItem[] items)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return RouletteAwardController.Instance.SetAwardItems(items);
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.Roulette.RouletteWinnerRecord[] GetNotPayWinAwardRecords(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return RouletteAwardController.Instance.GetNotPayWinAwardRecords();
            }
            else
            {
                throw new Exception();
            }
        }

        public int PayAward(string token, string adminUserName, string playerUserName, int recordID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return RouletteAwardController.Instance.PayAward(adminUserName, playerUserName, recordID);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
