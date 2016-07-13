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

    public class ServerClient : IServiceToAdmin
    {
        public event EventHandler Error;

        public event Action<string> OnSendMessage;
        public event Action OnSendPlayerActionLog;
        public event Action OnSendGameConfig;
        public event Action<string> OnSendNewNotice;

        private RestInvoker<IServiceToAdmin> _invoker = new RestInvoker<IServiceToAdmin>();
        private SynchronizationContext _context;

        public ServerClient()
        {
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
            this._invoker.Init(String.Format("http://{0}:33101", host));
        }

        public void HandleCallback()
        {
            this._invoker.HandleCallback(this._context);
        }



        #region IServiceToAdmin Members

        public event EventHandler<WebInvokeEventArgs<string>> LoginAdminCompleted;
        public void LoginAdmin(string adminName, string password, string mac, string key)
        {
            this._invoker.Invoke<string>(this._context, "LoginAdmin", this.LoginAdminCompleted, adminName, password, mac, key);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> LogoutAdminCompleted;
        public void LogoutAdmin()
        {
            this._invoker.Invoke<bool>(this._context, "LogoutAdmin", this.LogoutAdminCompleted, GlobalData.Token);
        }

        public event EventHandler<WebInvokeEventArgs<PlayerInfoLoginWrap[]>> GetPlayersCompleted;
        public void GetPlayers(int isOnline, int isLocked)
        {
            this._invoker.Invoke<PlayerInfoLoginWrap[]>(this._context, "GetPlayers", this.GetPlayersCompleted, GlobalData.Token, isOnline, isLocked);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> LockPlayerCompleted;
        public void LockPlayer(string actionPassword, string playerUserName)
        {
            this._invoker.Invoke<bool>(this._context, "LockPlayer", this.LockPlayerCompleted, GlobalData.Token, actionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UnlockPlayerCompleted;
        public void UnlockPlayer(string actionPassword, string playerUserName)
        {
            this._invoker.Invoke<bool>(this._context, "UnlockPlayer", this.UnlockPlayerCompleted, GlobalData.Token, actionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UpdatePlayerFortuneInfoCompleted;
        public void UpdatePlayerFortuneInfo(string actionPassword, MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            this._invoker.Invoke<bool>(this._context, "UpdatePlayerFortuneInfo", this.UpdatePlayerFortuneInfoCompleted, GlobalData.Token, actionPassword, fortuneInfo);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePlayerPasswordCompleted;
        public void ChangePlayerPassword(string actionPassword, string playerUserName, string newPassword)
        {
            this._invoker.Invoke<bool>(this._context, "ChangePlayerPassword", this.ChangePlayerPasswordCompleted, GlobalData.Token, actionPassword, playerUserName, newPassword);
        }

        #endregion
    }
}
