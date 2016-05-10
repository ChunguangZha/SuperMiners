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
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public class ServiceToAdmin : IServiceToAdmin
    {
        public string LoginAdmin(string adminName, string password, string key)
        {
            if (string.IsNullOrEmpty(adminName) || String.IsNullOrEmpty(password))
            {
                return string.Empty;
            }
            string token = string.Empty;
            try
            {
                AdminInfo admin = DBProvider.AdminDBProvider.GetAdmin(adminName);
                if (null != admin)
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.FromXmlString(key);

                    token = Guid.NewGuid().ToString();
                    RSAProvider.SetRSA(token, rsa);
                    RSAProvider.LoadRSA(token);

                    AdminManager.AddClient(adminName, token);
                    //lock (this._callbackDicLocker)
                    //{
                    //    this._callbackDic[token] = new Queue<CallbackInfo>();
                    //}

                    LogHelper.Instance.AddInfoLog("Admin login, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token));

                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("Web Admin login failed, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token), ex);
            }
            //if (!string.IsNullOrEmpty(token))
            //{
            //    new Thread(new ParameterizedThreadStart(o =>
            //    {
            //        this.LogedIn(o.ToString());
            //    })).Start(token);
            //}

            return token;
        }

        public bool LogoutAdmin(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                RSAProvider.RemoveRSA(token);
                AdminManager.RemoveClient(token);
                //lock (this._callbackDicLocker)
                //{
                //    this._callbackDic.Remove(token);
                //}
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

        public Model.UserInfo[] GetPlayers(string token, int isOnline, int isLocked)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string adminName = AdminManager.GetClientUserName(token);
                    PlayerInfo[] players = DBProvider.UserDBProvider.GetAllPlayers();
                    if (players == null)
                    {
                        return null;
                    }
                    UserInfo[] users = new UserInfo[players.Length];
                    for (int i = 0; i < players.Length; i++)
                    {
                        users[i] = new UserInfo()
                        {
                            SimpleInfo = players[i].SimpleInfo,
                            isOnline = ClientManager.IsExistUserName(players[i].SimpleInfo.UserName),
                            LoginIP = ClientManager.GetClientIP(ClientManager.GetToken(players[i].SimpleInfo.UserName)),
                            FortuneInfo = players[i].FortuneInfo,
                        };
                    }

                    return users;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayers", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public void DeletePlayers(string token, string actionPassword, string[] userNames)
        {
            throw new NotImplementedException();
        }

        public bool LogOutPlayers(string token, string actionPassword, string[] userNames)
        {
            throw new NotImplementedException();
        }

        public bool LockPlayers(string token, string actionPassword, string[] userNames)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePlayerInfo(string token, string actionPassword, MetaData.User.PlayerSimpleInfo simpleInfo, MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            throw new NotImplementedException();
        }
    }
}
