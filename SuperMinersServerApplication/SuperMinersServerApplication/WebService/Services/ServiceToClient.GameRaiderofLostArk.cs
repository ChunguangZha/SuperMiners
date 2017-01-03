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
                        if (result == OperResult.RESULTCODE_TRUE)
                        {
                            myTrans.Commit();
                        }
                        else
                        {
                            myTrans.Rollback();
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

                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        NotifyAllPlayerBetInfo(RaidersofLostArkController.Instance.CurrentRoundInfo);
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

        private void NotifyAllPlayerBetInfo(RaiderRoundMetaDataInfo roundInfo)
        {
            var allClients = ClientManager.AllClients;
            foreach (var client in allClients)
            {
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.PlayerJoinRaiderSucceed(o.ToString(), roundInfo);
                })).Start(client.Token);
            }
        }

        private void NotifyAllPlayerRaiderWinner(RaiderRoundMetaDataInfo roundInfo)
        {
            var allClients = ClientManager.AllClients;
            foreach (var client in allClients)
            {
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.PlayerWinedRaiderNotify(o.ToString(), roundInfo);
                })).Start(client.Token);
            }
        }
        
        public MetaData.Game.RaideroftheLostArk.PlayerBetInfo[] GetPlayerselfBetInfo(string token, int roundID, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return DBProvider.GameRaiderofLostArkDBProvider.GetPlayerBetInfoByRoundID(roundID, pageItemCount, pageIndex);
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
