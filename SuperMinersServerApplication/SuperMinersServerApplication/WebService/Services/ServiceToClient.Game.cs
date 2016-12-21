using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
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
        public OperResultObject WithdrawRMB(string token, string userName, int getRMBCount)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                OperResultObject resultObj = new OperResultObject();
                try
                {
                    if (getRMBCount <= 0)
                    {
                        resultObj.OperResultCode = OperResult.RESULTCODE_FALSE;
                    }

                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        resultObj.OperResultCode = OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    resultObj = PlayerController.Instance.CreateWithdrawRMB(userName, getRMBCount);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 灵币提现异常，提现灵币为:" + getRMBCount, exc);
                    resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                }

                return resultObj;
            }
            else
            {
                throw new Exception();
            }
        }

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
                try
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
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 购买矿工异常，购买矿工数为:" + minersCount, exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
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
                try
                {
                    TradeOperResult result = new TradeOperResult();
                    result.PayType = payType;
                    if (minesCount <= 0)
                    {
                        return result;
                    }

                    return OrderController.Instance.MineOrderController.BuyMine(userName, minesCount, payType);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 购买矿山异常，购买矿山数为:" + minesCount + ",支付类型为:" + ((PayType)payType).ToString(), exc);
                    return null;
                }
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
                try
                {
                    TradeOperResult result = new TradeOperResult();
                    result.PayType = payType;
                    result.TradeType = (int)AlipayTradeInType.BuyGoldCoin;

                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        result.ResultCode = OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    int valueRMB = (int)Math.Ceiling(goldCoinCount / GlobalConfig.GameConfig.RMB_GoldCoin);
                    return OrderController.Instance.GoldCoinOrderController.RechargeGoldCoin(userName, valueRMB, goldCoinCount, payType);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 金币充值异常，充值金币数为:" + goldCoinCount + ",支付类型为:" + ((PayType)payType).ToString(), exc);
                    return null;
                }
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
                try
                {
                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }
                    if (stones == 0)
                    {
                        return OperResult.RESULTCODE_LACK_OF_BALANCE;
                    }
                    GatherTempOutputStoneResult result = PlayerController.Instance.GatherStones(userName, stones);
                    return result.OperResult;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 收取矿石异常，矿石数为:" + stones, exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
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
                try
                {
                    return DBProvider.UserDBProvider.GetExpTopList();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("获取贡献榜异常", exc);
                    return null;
                }
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
                try
                {
                    return DBProvider.UserDBProvider.GetStoneTopList();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("获取矿石榜异常", exc);
                    return null;
                }
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
                try
                {
                    return TopListController.Instance.GetMinerTopList();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("获取矿工榜异常", exc);
                    return null;
                }
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
                try
                {
                    return TopListController.Instance.GetReferrerTopList();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("获取推荐榜异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
