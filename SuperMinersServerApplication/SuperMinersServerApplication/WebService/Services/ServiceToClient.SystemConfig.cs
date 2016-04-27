using MetaData.SystemConfig;
using SuperMinersServerApplication.Encoder;
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
        public GameConfig GetGameConfig(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return GlobalConfig.GameConfig;
            }
            else
            {
                throw new Exception();
            }
        }

        public IncomeMoneyAccount GetIncomeMoneyAccount(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return GlobalConfig.IncomeMoneyAccount;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
