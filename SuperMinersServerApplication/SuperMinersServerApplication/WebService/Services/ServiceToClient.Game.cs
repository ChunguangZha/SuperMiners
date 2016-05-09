using SuperMinersServerApplication.Controller;
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

        public int BuyMiner(string token, string userName, int minersCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return PlayerController.Instance.BuyMiner(userName, minersCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public int BuyMine(string token, string userName, int minesCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return PlayerController.Instance.BuyMine(userName, minesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public float GatherStones(string token, string userName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return PlayerController.Instance.GatherStones(userName);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
