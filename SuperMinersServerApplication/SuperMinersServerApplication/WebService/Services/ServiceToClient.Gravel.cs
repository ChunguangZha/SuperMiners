using DataBaseProvider;
using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Controller;
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
        public int RequestGravel(string token)
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

                    int result = playerRunner.RequestGravel();
                    if (result != OperResult.RESULTCODE_TRUE)
                    {
                        return result;
                    }

                    result = GravelController.Instance.RequestGravel(playerRunner.BasePlayer.SimpleInfo.UserID);
                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "] 申请碎石");
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] RequestGravel Exception", exc);
                    return OperResult.RESULTCODE_FALSE;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int GetGravel(string token)
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

                    PlayerGravelRequsetRecordInfo record = null;
                    int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                    {
                        int innerResult;
                        record = GravelController.Instance.GetGravel(playerRunner.BasePlayer.SimpleInfo.UserID, myTrans, out innerResult);
                        if (innerResult != OperResult.RESULTCODE_TRUE)
                        {
                            return innerResult;
                        }
                        if (record == null)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                        innerResult = playerRunner.GetGravel(record, myTrans);

                        return innerResult;
                    },
                    exc =>
                    {
                        if (exc != null)
                        {
                            LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetGravel Transaction Oper Exception", exc);
                        }
                    });

                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        if (record != null)
                        {
                            LogHelper.Instance.AddInfoLog("玩家[" + userName + "] 领取 " + record.Gravel + " 碎石");
                        }
                    }
                    else
                    {
                        playerRunner.RefreshGravel();
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetGravel Exception", exc);
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
