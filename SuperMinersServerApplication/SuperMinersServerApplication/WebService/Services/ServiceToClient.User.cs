using MetaData;
using MetaData.ActionLog;
using MetaData.AgentUser;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Model;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {
        //private System.Timers.Timer _userStateCheck = new System.Timers.Timer(10000);

        //private void _userStateCheck_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    this._userStateCheck.Stop();
        //    try
        //    {
        //        string[] tokens = ClientManager.GetInvalidClients();
        //        if (null != tokens)
        //        {
        //            foreach (var token in tokens)
        //            {
        //                string userName = ClientManager.GetClientUserName(token);
        //                LogHelper.Instance.AddInfoLog("玩家 [" + userName + "] 掉线，退出登录， IP=" + ClientManager.GetClientIP(token));

        //                PlayerController.Instance.LogoutPlayer(userName);
        //                RSAProvider.RemoveRSA(token);
        //                ClientManager.RemoveClient(token);
        //                lock (this._callbackDicLocker)
        //                {
        //                    this._callbackDic.Remove(token);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (App.ServiceToRun.IsStarted)
        //        {
        //            this._userStateCheck.Start();
        //        }
        //    }
        //}

        public OperResultObject Login(string userName, string password, string key, string mac, string clientVersion)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            OperResultObject resultObj = new OperResultObject();

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                resultObj.OperResultCode = OperResult.RESULTCODE_PARAM_INVALID;
                return resultObj;
            }
            if (!string.IsNullOrEmpty(GlobalConfig.CurrentClientVersion) && GlobalConfig.CurrentClientVersion != clientVersion)
            {
                resultObj.OperResultCode = OperResult.RESULTCODE_CLIENT_VERSION_OLD;
                //resultObj.Message = "VERSIONERROR";
                return resultObj;
            }

            string token = ClientManager.GetToken(userName);
            if (!string.IsNullOrEmpty(token))
            {
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.KickoutByUser(o.ToString());
                })).Start(token);

                //return "ISLOGGED";
            }

            string ip = ClientManager.GetCurrentIP();
            try
            {
                PlayerInfo player = DBProvider.UserDBProvider.GetPlayer(userName);
                if (player == null)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                    return resultObj;
                }
                if (password != player.SimpleInfo.Password)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                    return resultObj;
                }

                resultObj = PlayerController.Instance.CheckPlayerIsLocked(player.SimpleInfo.UserID, player.SimpleInfo.UserName);
                if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return resultObj;
                }

                if (player.SimpleInfo.InvitationCode == GlobalData.TestInvitationCode)
                {
                    try
                    {
                        var logState = DBProvider.TestUserLogStateDBProvider.GetTestUserLogStateByMac(mac);
                        if (logState == null)
                        {
                            logState = DBProvider.TestUserLogStateDBProvider.GetTestUserLogStateByUserName(userName);
                            if (logState != null)
                            {
                                resultObj.OperResultCode = OperResult.RESULTCODE_USERLOGIN_ISTESTUSER_LOGINLIMIT;
                                return resultObj;
                            }
                            DBProvider.TestUserLogStateDBProvider.AddTestUserLogState(userName, mac, ip);
                        }
                        else
                        {
                            if (logState.UserName != userName)
                            {
                                resultObj.OperResultCode = OperResult.RESULTCODE_USERLOGIN_ISTESTUSER_LOGINLIMIT;
                                return resultObj;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        LogHelper.Instance.AddErrorLog("Test User [" + userName + "] Add Exception.", exc);
                    }
                }

                player.SimpleInfo.LastLoginIP = ip;
                player.SimpleInfo.LastLoginMac = mac;
                PlayerController.Instance.LoginPlayer(player);

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);

                token = Guid.NewGuid().ToString();
                RSAProvider.SetRSA(token, rsa);
                RSAProvider.LoadRSA(token);

                ClientManager.AddClient(userName, token);
                lock (this._callbackDicLocker)
                {
                    this._callbackDic[token] = new Queue<CallbackInfo>();
                }

                LogHelper.Instance.AddInfoLog("玩家 [" + userName + "] 登录矿场, IP=" + ip + ", Mac=" + mac);

            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("玩家 [" + userName + "] 登录矿场失败, IP=" + ip + ", Mac=" + mac, ex);
            }
            if (!string.IsNullOrEmpty(token))
            {
                PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.Login, 1);
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.LogedIn(o.ToString());
                })).Start(token);
            }

            resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
            resultObj.Message = token;
            return resultObj;
        }

        public bool Logout(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    PlayerController.Instance.LogoutPlayer(userName);
                    RSAProvider.RemoveRSA(token);
                    ClientManager.RemoveClient(token);
                    lock (this._callbackDicLocker)
                    {
                        this._callbackDic.Remove(token);
                    }
                    if (!string.IsNullOrEmpty(token))
                    {
                        new Thread(new ParameterizedThreadStart(o =>
                        {
                            this.LogedOut(o.ToString());
                        })).Start(token);
                    }

                    LogHelper.Instance.AddInfoLog("玩家 [" + userName + "] 退出矿场, IP=" + ClientManager.GetClientIP(token));

                    return true;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("Logout Error", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfo GetPlayerInfo(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    PlayerInfo user = PlayerController.Instance.GetPlayerInfo(userName);
                    return user;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerInfo", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ChangePassword(string token, string oldPassword, string newPassword)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    return PlayerController.Instance.ChangePassword(userName, oldPassword, newPassword);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ChangePassword", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int ChangePlayerSimpleInfo(string token, string nickName, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (string.IsNullOrEmpty(nickName) || string.IsNullOrEmpty(alipayAccount) || string.IsNullOrEmpty(alipayRealName))
                    {
                        return OperResult.RESULTCODE_PARAM_INVALID;
                    }
                    string userName = ClientManager.GetClientUserName(token);
                    int value = PlayerController.Instance.ChangePlayerSimpleInfo(userName, nickName, alipayAccount, alipayRealName, IDCardNo, email, qq);
                    if (value == OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "]修改了用户信息，修改后昵称为：" + nickName + "，支付宝账户为：" + alipayAccount + "，实名为：" + alipayRealName + "，邮箱为：" + email + "， QQ为：" + qq);
                    }

                    return value;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ChangePlayerSimpleInfo", exc);
                    return OperResult.RESULTCODE_FALSE;
                }
            }
            else
            {
                throw new Exception();
            }
        }
        
        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int CheckUserAlipayExist(string token, string alipayAccount, string alipayRealName)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (string.IsNullOrEmpty(alipayAccount) || string.IsNullOrEmpty(alipayRealName))
                    {
                        return -2;
                    }
                    int result = PlayerController.Instance.CheckUserAlipayAccountExist(alipayAccount);
                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        return result;
                    }
                    result = PlayerController.Instance.CheckUserAlipayRealNameExist(alipayRealName);
                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("CheckUserAlipayExist Exception. alipayAccount: " + alipayAccount
                        + ", alipayRealName: " + alipayRealName, exc);

                    return -1;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public UserReferrerTreeItem[] GetUserReferrerTree(string token, string userName)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    List<UserReferrerTreeItem> results = new List<UserReferrerTreeItem>();

                    UserReferrerTreeItem parent = DBProvider.UserDBProvider.GetUserReferrerUpTree(userName);
                    UserReferrerTreeItem[] childrens = DBProvider.UserDBProvider.GetUserReferrerDownTree(userName);
                    if (parent != null)
                    {
                        parent.Level = -1;
                        results.Add(parent);
                    }
                    if (childrens != null)
                    {
                        foreach (var child in childrens)
                        {
                            child.Level = 1;
                            results.Add(child);
                        }
                    }

                    return results.ToArray();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetUserReferrerTree Exception. userName: " + userName, exc);

                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public AgentUserInfo GetAgentUserInfo(string token, string userName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var playerInfo = PlayerController.Instance.GetPlayerInfo(userName);
                    if (playerInfo == null)
                    {
                        return null;
                    }

                    return DBProvider.AgentUserInfoDBProvider.GetAgentUserInfo(playerInfo);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetAgentUserInfo Exception. userName: " + userName, exc);

                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public XunLingMineStateInfo GetAllXunLingMineFortuneState(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.UserDBProvider.GetAllXunLingMineFortuneState();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetAllXunLingMineFortuneState Exception. ", exc);

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
