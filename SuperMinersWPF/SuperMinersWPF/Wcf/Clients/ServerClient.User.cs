using MetaData;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        public event Action OnKickout;
        public event Action OnLogedIn;
        public event Action OnLogedOut;
        //public event Action OnSetPlayerInfo;

        #region Logion

        public event EventHandler<WebInvokeEventArgs<string>> LoginCompleted;
        public void Login(string userName, string password, string key)
        {
            this._invoker.Invoke<string>(this._context, "Login", this.LoginCompleted, userName, password, key);
        }

        #endregion

        #region Logout

        public event EventHandler<WebInvokeEventArgs<bool>> LogoutCompleted;
        public void Logout()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<bool>(this._context, "Logout", this.LogoutCompleted, GlobalData.Token);
                GlobalData.InitToken(null);
            }
        }

        #endregion

        #region GetPlayerInfo

        public event EventHandler<WebInvokeEventArgs<PlayerInfo>> GetPlayerInfoCompleted;
        public void GetPlayerInfo()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<PlayerInfo>(this._context, "GetPlayerInfo", this.GetPlayerInfoCompleted, GlobalData.Token);
            }
        }

        #endregion

        #region Callback

        public void RaiseOnKickout()
        {
            Action handler = this.OnKickout;
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
        public void RaiseOnPlayerInfoChanged()
        {
            this.GetPlayerInfo();
            //Action handler = this.OnSetPlayerInfo;
            //if (null != handler)
            //{
            //    handler();
            //}
        }
        #endregion
    }
}
