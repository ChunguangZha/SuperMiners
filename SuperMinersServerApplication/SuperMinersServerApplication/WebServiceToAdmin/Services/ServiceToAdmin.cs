using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Model;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ServiceToAdmin : IServiceToAdmin, IDisposable
    {
        private Dictionary<string, Queue<CallbackInfo>> _callbackDic = new Dictionary<string, Queue<CallbackInfo>>();
        private readonly object _callbackDicLocker = new object();

        private System.Timers.Timer _userStateCheck = new System.Timers.Timer(10000);

        private void _userStateCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._userStateCheck.Stop();
            try
            {
                string[] tokens = AdminManager.GetInvalidClients();
                if (null != tokens)
                {
                    foreach (var token in tokens)
                    {
                        //PlayerController.Instance.LogoutPlayer(ClientManager.GetClientUserName(token));
                        RSAProvider.RemoveRSA(token);
                        AdminManager.RemoveClient(token);
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

        public string LoginAdmin(string adminName, string password, string mac, string key)
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(adminName) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(mac) || String.IsNullOrEmpty(key))
            {
                return token;
            }
            try
            {
                AdminInfo admin = DBProvider.AdminDBProvider.GetAdmin(adminName);
                if (null == admin)
                {
                    return token;
                }
                if (admin.LoginPassword != password)
                {
                    return token;
                }

                if (admin.Macs == null || admin.Macs.Length == 0)
                {
                    if (admin.Macs.FirstOrDefault(s => s.ToLower() == mac.ToLower()) == null)
                    {
                        return token;
                    }
                }

                token = AdminManager.GetToken(adminName);
                if (!string.IsNullOrEmpty(token))
                {
                    //new Thread(new ParameterizedThreadStart(o =>
                    //{
                    //    this.KickoutByUser(o.ToString());
                    //})).Start(token);

                    LogoutAdmin(token);
                    return "ISLOGGED";
                }

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);

                token = Guid.NewGuid().ToString();
                RSAProvider.SetRSA(token, rsa);
                RSAProvider.LoadRSA(token);

                AdminManager.AddClient(adminName, admin.ActionPassword, token);
                lock (this._callbackDicLocker)
                {
                    this._callbackDic[token] = new Queue<CallbackInfo>();
                }

                LogHelper.Instance.AddInfoLog("Admin login, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token));

            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("Admin login failed, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token), ex);
            }

            return token;
        }

        public bool LogoutAdmin(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                RSAProvider.RemoveRSA(token);
                AdminManager.RemoveClient(token);
                lock (this._callbackDicLocker)
                {
                    this._callbackDic.Remove(token);
                }
                //if (!string.IsNullOrEmpty(token))
                //{
                //    new Thread(new ParameterizedThreadStart(o =>
                //    {
                //        this.LogedOut(o.ToString());
                //    })).Start(token);
                //}
                return true;
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfoLoginWrap[] GetPlayers(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string adminUserName = AdminManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(adminUserName))
                    {
                        return null;
                    }

                    PlayerInfo[] players = DBProvider.UserDBProvider.GetAllPlayers();
                    if (players == null)
                    {
                        return null;
                    }
                    PlayerInfoLoginWrap[] users = new PlayerInfoLoginWrap[players.Length];
                    for (int i = 0; i < players.Length; i++)
                    {
                        users[i] = new PlayerInfoLoginWrap();
                        users[i].SimpleInfo = players[i].SimpleInfo;
                        users[i].FortuneInfo = players[i].FortuneInfo;
                        users[i].isOnline = ClientManager.IsExistUserName(players[i].SimpleInfo.UserName);
                        if (users[i].isOnline)
                        {
                            users[i].LoginIP = ClientManager.GetClientIP(ClientManager.GetToken(players[i].SimpleInfo.UserName));
                        }
                    }

                    return users;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetPlayers Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool LockPlayer(string token, string actionPassword, string playerUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.LockPlayer(playerUserName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.LockPlayer", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UnlockPlayer(string token, string actionPassword, string playerUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.UnlockPlayer(playerUserName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.UnlockPlayer", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UpdatePlayerFortuneInfo(string token, string actionPassword, MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.ChangePlayerFortuneInfo(fortuneInfo);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.UpdatePlayerFortuneInfo", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ChangePlayerPassword(string token, string actionPassword, string playerUserName, string newPassword)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.ChangePasswordByAdmin(playerUserName, newPassword);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.ChangePlayerPassword", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this._userStateCheck.Dispose();
        }

        #endregion
    }
}
