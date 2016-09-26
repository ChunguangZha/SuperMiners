using MetaData;
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

        public MetaData.Game.Roulette.RouletteAwardItem[] GetAwardItems(string token)
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

        public MetaData.Game.Roulette.RouletteWinAwardResult StartRoulette(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);

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

        public MetaData.Game.Roulette.RouletteWinnerRecord FinishRoulette(string token, int winAwardNumber)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);

                try
                {
                    var user = PlayerController.Instance.GetPlayerInfo(userName);
                    if (user == null)
                    {
                        return null;
                    }

                    var result = RouletteAwardController.Instance.Finish(user.SimpleInfo.UserID, userName, user.SimpleInfo.NickName, winAwardNumber);
                    if (result != null)
                    {
                        new Thread(new ParameterizedThreadStart(o =>
                        {
                            this.GameRouletteWinNotify(o.ToString(), result.ToString());
                        })).Start(token);

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

        public int TakeRouletteAward(string token, int recordID, string info1, string info2)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);

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
    }
}
