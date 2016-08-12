using MetaData;
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
        private System.Timers.Timer _userStateCheck = new System.Timers.Timer(10000);

        private void _userStateCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._userStateCheck.Stop();
            try
            {
                string[] tokens = ClientManager.GetInvalidClients();
                if (null != tokens)
                {
                    foreach (var token in tokens)
                    {
                        PlayerController.Instance.LogoutPlayer(ClientManager.GetClientUserName(token));
                        RSAProvider.RemoveRSA(token);
                        ClientManager.RemoveClient(token);
                        lock (this._callbackDicLocker)
                        {
                            this._callbackDic.Remove(token);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (App.ServiceToRun.IsStarted)
                {
                    this._userStateCheck.Start();
                }
            }
        }

        public string Login(string userName, string password, string key)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return String.Empty;
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
            try
            {
                PlayerInfo player = PlayerController.Instance.LoginPlayer(userName, password);
                if (null != player)
                {
                    if (player.SimpleInfo.LockedLogin)
                    {
                        return "LOCKED";
                    }

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

                    LogHelper.Instance.AddInfoLog("Web login, userId=" + userName + ", remoteIP=" + ClientManager.GetClientIP(token));

                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("Web login failed", ex);
            }
            if (!string.IsNullOrEmpty(token))
            {
                PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.Login, 1);
                new Thread(new ParameterizedThreadStart(o =>
                {
                    this.LogedIn(o.ToString());
                })).Start(token);
            }

            return token;
        }

        public bool Logout(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    PlayerController.Instance.LogoutPlayer(ClientManager.GetClientUserName(token));
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

        public bool ChangePlayerSimpleInfo(string token, string nickName, string alipayAccount, string alipayRealName, string email, string qq)
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
                        return false;
                    }
                    string userName = ClientManager.GetClientUserName(token);
                    return PlayerController.Instance.ChangePlayerSimpleInfo(userName, nickName, alipayAccount, alipayRealName, email, qq);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ChangePlayerSimpleInfo", exc);
                    return false;
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
                    int count = DBProvider.UserDBProvider.GetPlayerCountByAlipay(alipayAccount, alipayRealName);
                    return count;
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
    }
}
