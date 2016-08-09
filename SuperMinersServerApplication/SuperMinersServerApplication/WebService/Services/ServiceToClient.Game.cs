using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
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

        public int BuyMiner(string token, string userName, int minersCount, int payType)
        {
#if Delay

            Thread.Sleep(5000);

#endif

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

        public TradeOperResult BuyMine(string token, string userName, int minesCount, int payType)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                TradeOperResult result = new TradeOperResult();

                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return result;
                }

                DateTime timenow = DateTime.Now;
                string orderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, AlipayTradeInType.BuyMine);
                MinesBuyRecord record = new MinesBuyRecord()
                {
                    OrderNumber = orderNumber,
                     Time = timenow,
                      UserName = userName,
                      
                };

                if (payType == (int)PayType.Alipay)
                {

                }
                else if (payType == (int)PayType.RMB)
                {
                    int value = PlayerController.Instance.BuyMineByRMB(userName, minesCount);
                }

                return PlayerController.Instance.BuyMineByRMB(userName, minesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public int GoldCoinRecharge(string token, string userName, int goldCoinCount, int payType)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }

                return -1;
                //return PlayerController.Instance.BuyMineByRMB(userName, minesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public int GatherStones(string token, string userName, float stones)
        {
#if Delay

            Thread.Sleep(5000);

#endif

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

        public TopListInfo[] GetExpTopList(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                return DBProvider.UserDBProvider.GetExpTopList();
            }
            else
            {
                throw new Exception();
            }
        }

        public TopListInfo[] GetStoneTopList(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                return DBProvider.UserDBProvider.GetStoneTopList();
            }
            else
            {
                throw new Exception();
            }
        }

        public TopListInfo[] GetMinerTopList(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                return TopListController.Instance.GetMinerTopList();
            }
            else
            {
                throw new Exception();
            }
        }

        public TopListInfo[] GetGoldCoinTopList(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                return null;
            }
            else
            {
                throw new Exception();
            }
        }

        public TopListInfo[] GetReferrerTopList(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                return TopListController.Instance.GetReferrerTopList();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
