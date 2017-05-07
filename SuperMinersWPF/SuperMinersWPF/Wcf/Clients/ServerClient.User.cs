using MetaData;
using MetaData.ActionLog;
using MetaData.AgentUser;
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
        public event Action OnKickoutByUser;
        public event Action OnLogedIn;
        public event Action OnLogedOut;
        public event Action OnPlayerInfoChanged;

        #region Login

        public event EventHandler<WebInvokeEventArgs<OperResultObject>> LoginCompleted;
        public void Login(string UserLoginName, string password, string key, string mac, string clientVersion)
        {
            this._invoker.Invoke<OperResultObject>(this._context, "Login", this.LoginCompleted, UserLoginName, password, key, mac, clientVersion);
        }

        #endregion

        #region Logout

        public event EventHandler<WebInvokeEventArgs<bool>> LogoutCompleted;
        public void Logout()
        {
            if (GlobalData.IsLogined)
            {
                string token = GlobalData.Token;
                GlobalData.InitToken(null);
                this._invoker.Invoke<bool>(this._context, "Logout", this.LogoutCompleted, token);
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

        #region ChangePassword

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePasswordCompleted;
        public void ChangePassword(string oldPassword, string newPassword, object userState)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.InvokeUserState<bool>(this._context, "ChangePassword", this.ChangePasswordCompleted, userState, GlobalData.Token, oldPassword, newPassword);
            }
        }

        #endregion

        #region ChangePlayerSimpleInfo

        public event EventHandler<WebInvokeEventArgs<int>> ChangePlayerSimpleInfoCompleted;
        public void ChangePlayerSimpleInfo(string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, object userState)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.InvokeUserState<int>(this._context, "ChangePlayerSimpleInfo", this.ChangePlayerSimpleInfoCompleted, userState, GlobalData.Token, alipayAccount, alipayRealName, IDCardNo, email, qq);
            }
        }

        #endregion

        #region CheckUserAlipayExist

        public event EventHandler<WebInvokeEventArgs<int>> CheckUserAlipayExistCompleted;
        public void CheckUserAlipayExist(string alipayAccount, string alipayRealName, object userState)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.InvokeUserState<int>(this._context, "CheckUserAlipayExist", this.CheckUserAlipayExistCompleted, userState, GlobalData.Token, alipayAccount, alipayRealName);
            }
        }

        #endregion

        #region GetUserReferrerTree

        public event EventHandler<WebInvokeEventArgs<UserReferrerTreeItem[]>> GetUserReferrerTreeCompleted;
        public void GetUserReferrerTree(string userName, object userState)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.InvokeUserState<UserReferrerTreeItem[]>(this._context, "GetUserReferrerTree", this.GetUserReferrerTreeCompleted, userState, GlobalData.Token, userName);
            }
        }

        #endregion

        #region GetAgentUserInfo

        public event EventHandler<WebInvokeEventArgs<AgentUserInfo>> GetAgentUserInfoCompleted;
        public void GetAgentUserInfo()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<AgentUserInfo>(this._context, "GetAgentUserInfo", this.GetAgentUserInfoCompleted, GlobalData.Token, GlobalData.CurrentUser.UserName);
            }
        }

        #endregion

        #region GetAllXunLingMineFortuneState

        public event EventHandler<WebInvokeEventArgs<XunLingMineStateInfo>> GetAllXunLingMineFortuneStateCompleted;
        public void GetAllXunLingMineFortuneState()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<XunLingMineStateInfo>(this._context, "GetAllXunLingMineFortuneState", this.GetAllXunLingMineFortuneStateCompleted, GlobalData.Token);
            }
        }

        #endregion

        #region GetPlayerPostAddressList

        public event EventHandler<WebInvokeEventArgs<PostAddress[]>> GetPlayerPostAddressListCompleted;
        public void GetPlayerPostAddressList()
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<PostAddress[]>(this._context, "GetPlayerPostAddressList", this.GetPlayerPostAddressListCompleted, GlobalData.Token);
            }
        }

        #endregion

        #region AddAddress

        public event EventHandler<WebInvokeEventArgs<int>> AddAddressCompleted;
        public void AddAddress(PostAddress address)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<int>(this._context, "AddAddress", this.AddAddressCompleted, GlobalData.Token, address);
            }
        }

        #endregion

        #region UpdateAddress

        public event EventHandler<WebInvokeEventArgs<int>> UpdateAddressCompleted;
        public void UpdateAddress(PostAddress newAddress)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<int>(this._context, "UpdateAddress", this.UpdateAddressCompleted, GlobalData.Token, newAddress);
            }
        }

        #endregion

        #region DeleteAddress

        public event EventHandler<WebInvokeEventArgs<int>> DeleteAddressCompleted;
        public void DeleteAddress(int postAddressID)
        {
            if (GlobalData.IsLogined)
            {
                this._invoker.Invoke<int>(this._context, "DeleteAddress", this.DeleteAddressCompleted, GlobalData.Token, postAddressID);
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
        public void RaiseOnPlayerInfoChanged()
        {
            Action handler = this.OnPlayerInfoChanged;
            if (null != handler)
            {
                handler();
            }
        }
        #endregion
    }
}
