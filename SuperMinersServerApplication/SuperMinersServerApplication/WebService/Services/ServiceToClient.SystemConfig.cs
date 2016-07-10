using MetaData.SystemConfig;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {
        public SystemConfigin1 GetGameConfig(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                SystemConfigin1 config = new SystemConfigin1()
                {
                    GameConfig = GlobalConfig.GameConfig,
                    RegisterUserConfig = GlobalConfig.RegisterPlayerConfig,
                    AwardReferrerConfigList = GlobalConfig.AwardReferrerLevelConfig.GetListAward().ToArray()
                };

                return config;
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
