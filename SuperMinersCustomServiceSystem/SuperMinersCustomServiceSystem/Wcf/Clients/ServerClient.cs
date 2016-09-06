using MetaData;
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

    public class ServerClient
    {
        public event EventHandler Error;

        public event Action OnKickoutByUser;
        public event Action OnLogedIn;
        public event Action OnLogedOut;
        public event Action<WithdrawRMBRecord> OnSomebodyWithdrawRMB;

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
            this._invoker.Init(String.Format("http://{0}:33123", host));
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
        public void RaiseOnSomebodyWithdrawRMB(WithdrawRMBRecord record)
        {
            Action<WithdrawRMBRecord> handler = this.OnSomebodyWithdrawRMB;
            if (null != handler)
            {
                handler(record);
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

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePlayerCompleted;
        public void ChangePlayer(PlayerInfoLoginWrap player)
        {
            this._invoker.Invoke<bool>(this._context, "ChangePlayer", this.ChangePlayerCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, player);
        }

        public event EventHandler<WebInvokeEventArgs<DeleteResultInfo>> DeletePlayersCompleted;
        public void DeletePlayers(string[] playerUserNames)
        {
            this._invoker.Invoke<DeleteResultInfo>(this._context, "DeletePlayers", this.DeletePlayersCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, playerUserNames);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> LockPlayerCompleted;
        public void LockPlayer(string playerUserName)
        {
            this._invoker.Invoke<bool>(this._context, "LockPlayer", this.LockPlayerCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UnlockPlayerCompleted;
        public void UnlockPlayer(string playerUserName)
        {
            this._invoker.Invoke<bool>(this._context, "UnlockPlayer", this.UnlockPlayerCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, playerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> UpdatePlayerFortuneInfoCompleted;
        public void UpdatePlayerFortuneInfo(MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            this._invoker.Invoke<bool>(this._context, "UpdatePlayerFortuneInfo", this.UpdatePlayerFortuneInfoCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, fortuneInfo);
        }

        public event EventHandler<WebInvokeEventArgs<bool>> ChangePlayerPasswordCompleted;
        public void ChangePlayerPassword(string playerUserName, string newPassword)
        {
            this._invoker.Invoke<bool>(this._context, "ChangePlayerPassword", this.ChangePlayerPasswordCompleted, GlobalData.Token, GlobalData.CurrentAdmin.ActionPassword, playerUserName, newPassword);
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

        public event EventHandler<WebInvokeEventArgs<SellStonesOrder[]>> GetSellStonesOrderListCompleted;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sellerUserName"></param>
        /// <param name="sellOrderState">小于0表示全部</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void GetSellStonesOrderList(string sellerUserName, int sellOrderState, MyDateTime startDate, MyDateTime endDate)
        {
            this._invoker.Invoke<SellStonesOrder[]>(this._context, "GetSellStonesOrderList", this.GetSellStonesOrderListCompleted, GlobalData.Token, sellerUserName, sellOrderState, startDate, endDate);
        }

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder[]>> GetLockedStonesOrderListCompleted;
        public void GetLockedStonesOrderList(string buyerUserName)
        {
            this._invoker.Invoke<LockSellStonesOrder[]>(this._context, "GetLockedStonesOrderList", this.GetLockedStonesOrderListCompleted, GlobalData.Token, buyerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<BuyStonesOrder[]>> GetBuyStonesOrderListCompleted;
        public void GetBuyStonesOrderList(string buyerUserName, MyDateTime startDate, MyDateTime endDate)
        {
            this._invoker.Invoke<BuyStonesOrder[]>(this._context, "GetBuyStonesOrderList", this.GetBuyStonesOrderListCompleted, GlobalData.Token, buyerUserName, startDate, endDate);
        }

        public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMinesFinishedRecordListCompleted;
        public void GetBuyMinesFinishedRecordList(string buyerUserName, MyDateTime startDate, MyDateTime endDate)
        {
            this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMinesFinishedRecordList", this.GetBuyMinesFinishedRecordListCompleted, GlobalData.Token, buyerUserName, startDate, endDate);
        }

        public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMinesNotFinishedRecordListCompleted;
        public void GetBuyMinesNotFinishedRecordList(string buyerUserName)
        {
            this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMinesNotFinishedRecordList", this.GetBuyMinesNotFinishedRecordListCompleted, GlobalData.Token, buyerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<int>> HandleExceptionStoneOrderSucceedCompleted;
        public void HandleExceptionStoneOrderSucceed(AlipayRechargeRecord alipayRecord)
        {
            this._invoker.Invoke<int>(this._context, "HandleExceptionStoneOrderSucceed", this.HandleExceptionStoneOrderSucceedCompleted, GlobalData.Token, alipayRecord);
        }

        public event EventHandler<WebInvokeEventArgs<int>> HandleExceptionStoneOrderFailedCompleted;
        public void HandleExceptionStoneOrderFailed(string orderNumber)
        {
            this._invoker.Invoke<int>(this._context, "HandleExceptionStoneOrderFailed", this.HandleExceptionStoneOrderFailedCompleted, GlobalData.Token, orderNumber);
        }

        public event EventHandler<WebInvokeEventArgs<int>> PayWithdrawRMBRecordCompleted;
        public void PayWithdrawRMBRecord(WithdrawRMBRecord record)
        {
            this._invoker.Invoke<int>(this._context, "PayWithdrawRMBRecord", this.PayWithdrawRMBRecordCompleted, GlobalData.Token, record);
        }

        #endregion
    }
}
