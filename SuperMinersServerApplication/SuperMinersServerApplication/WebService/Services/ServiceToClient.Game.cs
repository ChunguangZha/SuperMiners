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

        public int GatherStones(string token, string userName, int stones)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return PlayerController.Instance.GatherStones(userName, stones);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 0表示成功；-1表示查询不到该用户; -2表示该用户不在线；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="sellStonesCount"></param>
        /// <returns></returns>
        public int SellStones(string token, string userName, int sellStonesCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return PlayerController.Instance.SellStones(userName, sellStonesCount);
            }
            else
            {
                throw new Exception();
            }
        }

    }
}
