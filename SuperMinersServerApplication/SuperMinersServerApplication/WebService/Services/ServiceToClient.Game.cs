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
        /// <summary>
        /// RESULTCODE_USER_NOT_EXIST; RESULTCODE_FALSE
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="minersCount"></param>
        /// <returns></returns>
        public int BuyMiner(string token, string userName, int minersCount)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (minersCount <= 0)
                {
                    return OperResult.RESULTCODE_FALSE;
                }

                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
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
                result.PayType = payType;
                if (minesCount <= 0)
                {
                    return result;
                }

                return OrderController.Instance.MineOrderController.BuyMine(userName, minesCount, payType);
            }
            else
            {
                throw new Exception();
            }
        }

        public TradeOperResult GoldCoinRecharge(string token, string userName, int goldCoinCount, int payType)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                TradeOperResult result = new TradeOperResult();
                result.PayType = payType;
                result.TradeType = (int)AlipayTradeInType.BuyGoldCoin;

                if (ClientManager.GetClientUserName(token) != userName)
                {
                    result.ResultCode = OperResult.RESULTCODE_USER_NOT_EXIST;
                }

                int valueRMB =(int)Math.Ceiling(goldCoinCount / GlobalConfig.GameConfig.RMB_GoldCoin);
                return OrderController.Instance.GoldCoinOrderController.RechargeGoldCoin(userName, valueRMB, goldCoinCount, payType);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="stones">-1表示清空临时产出</param>
        /// <returns></returns>
        public int GatherStones(string token, string userName, decimal stones)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }
                if (stones == 0)
                {
                    return OperResult.RESULTCODE_LACK_OF_BALANCE;
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
