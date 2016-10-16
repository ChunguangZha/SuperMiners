﻿using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersCustomServiceSystem.Wcf.Channel;
using SuperMinersServerApplication.Model;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public class WebInvokeEventArgs : AsyncCompletedEventArgs
    {
        public WebInvokeEventArgs(Exception ex, bool cancelled, object userState)
            : base(ex, cancelled, userState)
        {
            this.Continue = false;
        }

        /// <summary>
        /// 0 to 100
        /// </summary>
        /// <param name="valure"></param>
        public void SetProgress(double valure)
        {
            //BusyToken.SetProgress(valure);
        }

        public bool Continue
        {
            get;
            set;
        }

        public bool IsDisconnected
        {
            get
            {
                if (null != this.Error)
                {
                    if (this.Error is WebException)
                    {
                        return (((WebException)this.Error).Status == WebExceptionStatus.UnknownError);
                    }
                }

                return false;
            }
        }
    }

    public class WebInvokeEventArgs<T> : WebInvokeEventArgs
    {
        private T _result;

        public WebInvokeEventArgs(T result, Exception ex, bool cancelled, object userState)
            : base(ex, cancelled, userState)
        {
            this.Continue = false;
            this._result = result;
        }

        public T Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return this._result;
            }
        }
    }

    public partial class ServerClient
    {
        public event EventHandler Error;

        public event Action OnKickoutByUser;
        public event Action OnLogedIn;
        public event Action OnLogedOut;

        private RestInvoker<IServiceToAdmin> _invoker = new RestInvoker<IServiceToAdmin>();
        private SynchronizationContext _context;

        public bool IsConnected { get; private set; }

        public ServerClient()
        {
            IsConnected = false;
            this._invoker.SetCallbackReceiver(this);
            this._invoker.Error += new EventHandler(_invoker_Error);
        }

        private void _invoker_Error(object sender, EventArgs e)
        {
            if (null != this.Error)
            {
                this.Error(this, EventArgs.Empty);
            }
        }

        public void Init(string host)
        {
            this._invoker.Init(String.Format("http://{0}:33123", host));
            IsConnected = true;
        }

        public void HandleCallback()
        {
            this._invoker.HandleCallback(this._context);
        }

        public void SetContext(SynchronizationContext context)
        {
            //BusyToken.SetContext(context);
            this._context = context;
        }


        #region Callback

        public void RaiseOnKickoutByUser()
        {
            Action handler = this.OnKickoutByUser;
            if (null != handler)
            {
                handler();
            }
        }
        public void RaiseOnLogedIn()
        {
            Action handler = this.OnLogedIn;
            if (null != handler)
            {
                handler();
            }
        }
        public void RaiseOnLogedOut()
        {
            Action handler = this.OnLogedOut;
            if (null != handler)
            {
                handler();
            }
        }

        #endregion

        #region IServiceToAdmin Members

        public event EventHandler<WebInvokeEventArgs<string>> LoginAdminCompleted;
        public void LoginAdmin(string adminName, string password, string mac, string key)
        {
            this._invoker.Invoke<string>(this._context, "LoginAdmin", this.LoginAdminCompleted, adminName, password, mac, key);
        }

        public event EventHandler<WebInvokeEventArgs<AdminInfo>> GetAdminInfoCompleted;
        public void GetAdminInfo()
        {
            this._invoker.Invoke<AdminInfo>(this._context, "GetAdminInfo", this.GetAdminInfoCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> LogoutAdminCompleted;
        public void LogoutAdmin()
        {
            this._invoker.Invoke<bool>(this._context, "LogoutAdmin", this.LogoutAdminCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<SystemConfigin1>> GetGameConfigCompleted;
        public void GetGameConfig()
        {
            this._invoker.Invoke<SystemConfigin1>(this._context, "GetGameConfig", this.GetGameConfigCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerInfoLoginWrap[]>> GetPlayersCompleted;
        public void GetPlayers()
        {
            this._invoker.Invoke<PlayerInfoLoginWrap[]>(this._context, "GetPlayers", this.GetPlayersCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerInfoLoginWrap>> GetPlayerCompleted;
        public void GetPlayer(string userName)
        {
            this._invoker.Invoke<PlayerInfoLoginWrap>(this._context, "GetPlayer", this.GetPlayerCompleted, GlobalData.Token, userName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePlayerCompleted;
        public void ChangePlayer(PlayerInfoLoginWrap player, string actionPassword)
        {
            this._invoker.Invoke<bool>(this._context, "ChangePlayer", this.ChangePlayerCompleted, GlobalData.Token, actionPassword, player);
        }

        public event EventHandler<WebInvokeEventArgs<DeleteResultInfo>> DeletePlayersCompleted;
        public void DeletePlayers(string[] playerUserNames, string actionPassword)
        {
            this._invoker.Invoke<DeleteResultInfo>(this._context, "DeletePlayers", this.DeletePlayersCompleted, GlobalData.Token, actionPassword, playerUserNames);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> LockPlayerCompleted;
        public void LockPlayer(string playerUserName, string actionPassword)
        {
            this._invoker.Invoke<bool>(this._context, "LockPlayer", this.LockPlayerCompleted, GlobalData.Token, actionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UnlockPlayerCompleted;
        public void UnlockPlayer(string playerUserName, string actionPassword)
        {
            this._invoker.Invoke<bool>(this._context, "UnlockPlayer", this.UnlockPlayerCompleted, GlobalData.Token, actionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UpdatePlayerFortuneInfoCompleted;
        public void UpdatePlayerFortuneInfo(MetaData.User.PlayerFortuneInfo fortuneInfo, string actionPassword)
        {
            this._invoker.Invoke<bool>(this._context, "UpdatePlayerFortuneInfo", this.UpdatePlayerFortuneInfoCompleted, GlobalData.Token, actionPassword, fortuneInfo);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePlayerPasswordCompleted;
        public void ChangePlayerPassword(string playerUserName, string newPassword, string actionPassword)
        {
            this._invoker.Invoke<bool>(this._context, "ChangePlayerPassword", this.ChangePlayerPasswordCompleted, GlobalData.Token, actionPassword, playerUserName, newPassword);
        }

        public event EventHandler<WebInvokeEventArgs<NoticeInfo[]>> GetNoticesCompleted;
        public void GetNotices()
        {
            this._invoker.Invoke<NoticeInfo[]>(this._context, "GetNotices", this.GetNoticesCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> CreateNoticeCompleted;
        public void CreateNotice(NoticeInfo notice)
        {
            this._invoker.Invoke<bool>(this._context, "CreateNotice", this.CreateNoticeCompleted, GlobalData.Token, notice);
        }

        public event EventHandler<WebInvokeEventArgs<int>> SetPlayerAsAgentCompleted;
        public void SetPlayerAsAgent(int userID, string userName, string agentReferURL)
        {
            this._invoker.Invoke<int>(this._context, "SetPlayerAsAgent", this.SetPlayerAsAgentCompleted, GlobalData.Token, userID, userName, agentReferURL);
        }

        #endregion
    }
}
