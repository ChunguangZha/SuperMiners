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


        public int BetIn(string token, GambleStoneItemColor color, int stoneCount, bool isGravel)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = null;
                try
                {
                    userName = ClientManager.GetClientUserName(token);

                    int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                    {
                        int innerResult = PlayerController.Instance.GambleBetIn(userName, stoneCount, isGravel, myTrans);
                        if (innerResult == OperResult.RESULTCODE_TRUE)
                        {
                            var playerInfo = PlayerController.Instance.GetPlayerInfo(userName);
                            return GambleStoneController.Instance.BetIn(color, stoneCount, playerInfo.SimpleInfo.UserID, userName);
                        }

                        return innerResult;
                    },
                    exc =>
                    {
                        LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] 下注赌石游戏 Inner异常。color： " + color.ToString() + "; stoneCount: " + stoneCount.ToString() + "; isGravel: " + isGravel.ToString(), exc);
                    });

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[ " + userName + " ] 下注赌石游戏 异常。color： " + color.ToString() + "; stoneCount: " + stoneCount.ToString() + "; isGravel: " + isGravel.ToString(), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
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

        #endregion
    }
}
