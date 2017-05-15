using DataBaseProvider;
using MetaData;
using MetaData.Game.GambleStone;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Game;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
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
        #region IServiceToClient Members

        /// <summary>
        /// 下注赌石游戏
        /// </summary>
        /// <param name="token"></param>
        /// <param name="color"></param>
        /// <param name="stoneCount">矿石</param>
        /// <param name="gravelCount">碎片</param>
        /// <returns></returns>
        public GambleStonePlayerBetInResult GambleStoneBetIn(string token, GambleStoneItemColor color, int stoneCount, int gravelCount)
        {
            //优先使用碎片
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                GambleStonePlayerBetInResult betResult = new GambleStonePlayerBetInResult();
                try
                {
                    if (stoneCount == 0 && gravelCount == 0)
                    {
                        betResult.ResultCode = OperResult.RESULTCODE_PARAM_INVALID;
                        return betResult;
                    }

                    userName = ClientManager.GetClientUserName(token);

                    if (!GambleStoneController.Instance.CheckBetInable())
                    {
                        betResult.ResultCode = OperResult.RESULTCODE_GAME_GAMBLE_INNINGFINISHED;
                        return betResult;
                    }

                    int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                    {
                        int innerResult = PlayerController.Instance.GambleBetIn(userName, color, stoneCount, gravelCount, myTrans);
                        if (innerResult == OperResult.RESULTCODE_TRUE)
                        {
                            var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                            betResult = GambleStoneController.Instance.BetIn(color, stoneCount + gravelCount, playerInfo.SimpleInfo.UserID, userName);
                            innerResult = betResult.ResultCode;
                        }

                        return innerResult;
                    },
                    exc =>
                    {
                        PlayerController.Instance.RollbackUserFromDB(userName);
                        LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] 下注赌石游戏 Inner异常。color： " + color.ToString() + "; stoneCount: " + stoneCount.ToString() + "; gravelCount: " + gravelCount.ToString(), exc);
                    });
                    if (result != OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[ " + userName + " ] 下注赌石游戏失败，数据库操作已回滚");
                    }
                    betResult.ResultCode = result;
                    return betResult;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] 下注赌石游戏 异常。color： " + color.ToString() + "; stoneCount: " + stoneCount.ToString() + "; isGravel: " + gravelCount.ToString(), exc);
                    betResult.ResultCode = OperResult.RESULTCODE_EXCEPTION;
                    return betResult;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public GambleStoneRound_InningInfo GetGambleStoneRoundInning(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return GambleStoneController.Instance.GetCurrentInningInfo();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] GetGambleStoneRoundInning 异常。", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public GambleStoneRoundInfo GetGambleStoneRoundInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return GambleStoneController.Instance.GetGambleStoneRoundInfo();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] GetGambleStoneRoundInfo 异常。", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public GambleStoneInningInfo GetGambleStoneInningInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return GambleStoneController.Instance.GetGambleStoneInningInfo();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] GetGambleStoneInningInfo 异常。", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public GambleStonePlayerBetRecord[] GetLastMonthGambleStonePlayerBetRecord(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var player = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (player == null)
                    {
                        return null;
                    }

                    return DBProvider.GambleStoneDBProvider.GetLastMonthGambleStonePlayerBetRecord(player.SimpleInfo.UserID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] GetGambleStoneInningInfo 异常。", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }


        #endregion
    }
}
