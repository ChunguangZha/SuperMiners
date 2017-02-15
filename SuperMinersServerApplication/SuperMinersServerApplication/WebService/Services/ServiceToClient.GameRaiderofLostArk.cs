using DataBaseProvider;
using MetaData;
using MetaData.Game.RaideroftheLostArk;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Game;
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
        public PlayerRaiderRoundHistoryRecordInfo[] GetPlayerRaiderRoundHistoryRecordInfo(string token, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return DBProvider.GameRaiderofLostArkDBProvider.GetPlayerRaiderRoundHistoryRecordInfo(userName, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetCurrentRaiderRoundInfo Exception UserName: " + userName, exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo GetCurrentRaiderRoundInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RaidersofLostArkController.Instance.CurrentRoundInfo;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetCurrentRaiderRoundInfo Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public RaiderRoundMetaDataInfo[] GetHistoryRaiderRoundRecords(string token, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.GameRaiderofLostArkDBProvider.GetHistoryRaiderRoundRecords(pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetHistoryRaiderRoundRecords Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int JoinRaider(string token, int roundID, int betStoneCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    int result = OperResult.RESULTCODE_FALSE;

                    CustomerMySqlTransaction myTrans = null;
                    try
                    {
                        myTrans = MyDBHelper.Instance.CreateTrans();
                        result = playerRunner.JoinRaider(betStoneCount, myTrans);
                        if (result != OperResult.RESULTCODE_TRUE)
                        {
                            return result;
                        }
                        result = RaidersofLostArkController.Instance.Join(playerRunner.BasePlayer.SimpleInfo.UserID, userName, roundID, betStoneCount);
                        if (result == OperResult.RESULTCODE_TRUE || result == OperResult.RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART)
                        {
                            myTrans.Commit();
                        }
                        else
                        {
                            myTrans.Rollback();
                            playerRunner.RefreshFortune();
                        }
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        LogHelper.Instance.AddErrorLog("ServiceToClient.JoinRaider DB Transaction Exception. UserName:" + userName + "; RoundID: " + roundID  + "; BetStoneCount: " + betStoneCount, exc);

                        return OperResult.RESULTCODE_EXCEPTION;
                    }
                    finally
                    {
                        if (myTrans != null)
                        {
                            myTrans.Dispose();
                        }
                    }

                    if (result == OperResult.RESULTCODE_TRUE || result == OperResult.RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART)
                    {
                        //NotifyAllPlayerBetInfo(RaidersofLostArkController.Instance.CurrentRoundInfo);
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "] 在第" + roundID + "期 夺宝奇兵，下注" + betStoneCount + "矿石");
                        //PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.GameRaiderJoinBet, roundID, betStoneCount.ToString());
                    }
                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToClient.JoinRaider Exception. UserName:" + userName + "; RoundID: " + roundID + "; BetStoneCount: " + betStoneCount, exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        //private void NotifyPlayerToRefreshBetRecords(RaiderRoundMetaDataInfo roundInfo, List<string> listPlayerUserName)
        //{
        //    foreach (var username in listPlayerUserName)
        //    {
        //        string token = ClientManager.GetToken(username);
        //        new Thread(new ParameterizedThreadStart(o =>
        //        {
        //            this.PlayerJoinRaiderSucceed(o.ToString(), roundInfo);
        //        })).Start(token);
        //    }
        //}

        private void NotifyAllPlayerRaiderWinner(RaiderRoundMetaDataInfo roundInfo)
        {
            LogHelper.Instance.AddInfoLog("玩家[" + roundInfo.WinnerUserName + "] 赢得第" + roundInfo.ID + "期 夺宝奇兵，赢得" + roundInfo.WinStones + "矿石");

            PlayerActionController.Instance.AddLog(roundInfo.WinnerUserName, MetaData.ActionLog.ActionType.GameRaiderWin, roundInfo.ID, roundInfo.WinStones.ToString());

            //var token = ClientManager.GetToken(roundInfo.WinnerUserName);
            //new Thread(new ParameterizedThreadStart(o =>
            //{
            //    this.PlayerWinedRaiderNotify(o.ToString(), roundInfo);
            //})).Start(token);

        }
        
        public MetaData.Game.RaideroftheLostArk.RaiderPlayerBetInfo[] GetPlayerselfBetInfo(string token, int roundID, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return DBProvider.GameRaiderofLostArkDBProvider.GetPlayerBetInfoByRoundID(roundID, userName, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerselfBetInfo Exception. UserName: " + userName, exc);
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
