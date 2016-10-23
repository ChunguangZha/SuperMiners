using MetaData;
using MetaData.Game.Roulette;
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

        public MetaData.Game.Roulette.RouletteAwardItem[] GetAwardItems(string token, string userName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return RouletteAwardController.Instance.GetAwardItems();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetAwardItems Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.Roulette.RouletteWinAwardResult StartRoulette(string token, string userName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string currentUserName = ClientManager.GetClientUserName(token);
                if (currentUserName != userName)
                {
                    return null;
                }

                try
                {
                    return RouletteAwardController.Instance.Start(userName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog(userName + " StartRoulette Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.Roulette.RouletteWinnerRecord FinishRoulette(string token, string userName, int winAwardIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string currentUserName = ClientManager.GetClientUserName(token);
                if (currentUserName != userName)
                {
                    return null;
                }

                try
                {
                    var user = PlayerController.Instance.GetPlayerInfo(userName);
                    if (user == null)
                    {
                        return null;
                    }

                    var result = RouletteAwardController.Instance.Finish(user.SimpleInfo.UserID, userName, user.SimpleInfo.NickName, winAwardIndex);
                    if (result != null)
                    {
                        RouletteWinAwardNotifyAllPlayers(result);

                        if (result.AwardItem.IsLargeAward)
                        {
                            PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.GameFunny, 1, result.ToString());
                        }
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog(userName + " FinishRoulette Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private void RouletteWinAwardNotifyAllPlayers(RouletteWinnerRecord result)
        {
            var allClients = ClientManager.AllClients;
            foreach (var client in allClients)
            {
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.GameRouletteWinNotify(o.ToString(), result.ToString());
                })).Start(client.Token);
            }
        }

        public int TakeRouletteAward(string token, string userName, int recordID, string info1, string info2)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string currentUserName = ClientManager.GetClientUserName(token);
                if (currentUserName != userName)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }

                try
                {
                    return RouletteAwardController.Instance.TakeAward(userName, recordID, info1, info2);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog(userName + " TakeRouletteAward Exception", exc);
                    return OperResult.RESULTCODE_FALSE;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public RouletteWinnerRecord[] GetAllWinAwardRecords(string token, string userName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string currentUserName = ClientManager.GetClientUserName(token);
                    if (currentUserName != userName)
                    {
                        return null;
                    }

                    return RouletteAwardController.Instance.GetAllPayWinAwardRecords(userName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToClient.GetAllPayWinAwardRecords Exception.", exc);
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
