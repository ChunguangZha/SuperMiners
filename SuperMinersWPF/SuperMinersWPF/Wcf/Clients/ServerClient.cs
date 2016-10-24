using SuperMinersServerApplication.WebService.Contracts;
using SuperMinersWPF.Wcf.Channel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
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

        public event Action<string> OnSendMessage;
        public event Action OnSendPlayerActionLog;
        public event Action OnSendGameConfig;
        public event Action<string> OnSendNewNotice;

        private RestInvoker<IServiceToClient> _invoker = null;
        private SynchronizationContext _context;

        public ServerClient()
        {
            _invoker = new RestInvoker<IServiceToClient>(); 
            this._invoker.SetCallbackReceiver(this);
            this._invoker.Error += new EventHandler(_invoker_Error);
        }

        public bool IsEnable
        {
            get
            {
                return this._invoker != null;
            }
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

        public void SetContext(SynchronizationContext context)
        {
            //BusyToken.SetContext(context);
            this._context = context;
        }

        public void RaiseOnSendMessage(string msg)
        {
            Action<string> handler = this.OnSendMessage;
            if (null != handler)
            {
                handler(msg);
            }
        }

        public void RaiseOnSendPlayerActionLog()
        {
            Action handler = this.OnSendPlayerActionLog;
            if (null != handler)
            {
                handler();
            }
        }

        public void RaiseOnSendGameConfig()
        {
            Action handler = this.OnSendGameConfig;
            if (null != handler)
            {
                handler();
            }
        }

        public void RaiseOnSendNewNotice(string title)
        {
            Action<string> handler = this.OnSendNewNotice;
            if (null != handler)
            {
                handler(title);
            }
        }

    }
}
