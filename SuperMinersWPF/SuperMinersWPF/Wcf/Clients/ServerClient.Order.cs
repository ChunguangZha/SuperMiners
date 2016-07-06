using MetaData;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Clients
{
    public partial class ServerClient
    {
        public event Action<int, string> OnOrderAlipayPaySucceed;
        public event Action OnOrderListChanged;

        #region SellStone

        public event EventHandler<WebInvokeEventArgs<int>> SellStoneCompleted;
        public void SellStone(int sellStonesCount, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "SellStone", this.SellStoneCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, sellStonesCount);
        }

        #endregion

        #region GetOrderLockedBySelf

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder>> GetOrderLockedBySelfCompleted;
        public void GetOrderLockedBySelf(object userState)
        {
            this._invoker.InvokeUserState<LockSellStonesOrder>(this._context, "GetOrderLockedBySelf", this.GetOrderLockedBySelfCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region GetAllNotFinishedSellOrders

        public event EventHandler<WebInvokeEventArgs<SellStonesOrder[]>> GetAllNotFinishedSellOrdersCompleted;
        public void GetAllNotFinishedSellOrders(object userState)
        {
            this._invoker.InvokeUserState<SellStonesOrder[]>(this._context, "GetAllNotFinishedSellOrders", this.GetAllNotFinishedSellOrdersCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region CheckUserHasNotPayOrder

        public event EventHandler<WebInvokeEventArgs<bool>> CheckUserHasNotPayOrderCompleted;
        public void CheckUserHasNotPayOrder(object userState)
        {
            this._invoker.InvokeUserState<bool>(this._context, "CheckUserHasNotPayOrder", this.CheckUserHasNotPayOrderCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region AutoMatchLockSellStone

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder>> AutoMatchLockSellStoneCompleted;
        public void AutoMatchLockSellStone(int buyStonesCount, object userState)
        {
            this._invoker.InvokeUserState<LockSellStonesOrder>(this._context, "AutoMatchLockSellStone", this.AutoMatchLockSellStoneCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, buyStonesCount);
        }

        #endregion

        #region ReleaseLockOrder

        public event EventHandler<WebInvokeEventArgs<bool>> ReleaseLockOrderCompleted;
        public void ReleaseLockOrder(object userState)
        {
            this._invoker.InvokeUserState<bool>(this._context, "ReleaseLockOrder", this.ReleaseLockOrderCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region PayOrderByRMB

        public event EventHandler<WebInvokeEventArgs<bool>> PayOrderByRMBCompleted;
        public void PayOrderByRMB(string orderNumber, float rmb, object userState)
        {
            this._invoker.InvokeUserState<bool>(this._context, "PayOrderByRMB", this.PayOrderByRMBCompleted, userState, GlobalData.Token, orderNumber, rmb);
        }

        #endregion

        //#region PayOrderByAlipay

        //public event EventHandler<WebInvokeEventArgs<string>> PayOrderByAlipayCompleted;
        //public void PayOrderByAlipay(string orderNumber, float rmb, object userState)
        //{
        //    this._invoker.InvokeUserState<string>(this._context, "PayOrderByAlipay", this.PayOrderByAlipayCompleted, userState, GlobalData.Token, orderNumber, rmb);
        //}

        //#endregion

        #region Callback

        public void RaiseOnOrderAlipayPaySucceed(int tradeType, string orderNumber)
        {
            Action<int, string> handler = this.OnOrderAlipayPaySucceed;
            if (null != handler)
            {
                handler(tradeType, orderNumber);
            }
        }

        public void RaiseOnOrderListChanged()
        {
            Action handler = this.OnOrderListChanged;
            if (null != handler)
            {
                handler();
            }
        }

        #endregion
    }
}
