using SuperMinersCustomServiceSystem.Wcf.Channel;
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

        public bool LogoutAdmin(string token)
        {
            throw new NotImplementedException();
        }

        public SuperMinersServerApplication.Model.PlayerInfoLoginWrap[] GetPlayers(string token, int isOnline, int isLocked)
        {
            throw new NotImplementedException();
        }

        public bool LockPlayer(string token, string actionPassword, string playerUserName)
        {
            throw new NotImplementedException();
        }

        public bool UnlockPlayer(string token, string actionPassword, string playerUserName)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePlayerInfo(string token, string actionPassword, MetaData.User.PlayerSimpleInfo simpleInfo, MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
