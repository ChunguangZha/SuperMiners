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
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                return String.Empty;
            }
            if (ClientManager.IsExistUserName(userName))
            {
                return "ISLOGGED";
            }
            string token = String.Empty;
            try
            {
                PlayerInfo player = PlayerController.Instance.LoginPlayer(userName, password);
                if (null != player)
                {
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
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfo GetPlayerInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    PlayerInfo user = PlayerController.Instance.GetOnlinePlayerInfo(userName);
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
    }
}
