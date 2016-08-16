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


        #region CancelSellStone


        /// <summary>
        /// RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_USER_NOT_EXIST; RESULTCODE_EXCEPTION; RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_ORDER_BE_LOCKED; RESULTCODE_TRUE; RESULTCODE_FALSE
        /// </summary>
        public event EventHandler<WebInvokeEventArgs<int>> CancelSellStoneCompleted;
        public void CancelSellStone(string orderNumber, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "CancelSellStone", this.CancelSellStoneCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, orderNumber);
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

        #region LockSellStone

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder>> LockSellStoneCompleted;
        public void LockSellStone(string orderNumber, object userState)
        {
            this._invoker.InvokeUserState<LockSellStonesOrder>(this._context, "LockSellStone", this.LockSellStoneCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, orderNumber);
        }

        #endregion

        #region ReleaseLockOrder

        public event EventHandler<WebInvokeEventArgs<bool>> ReleaseLockOrderCompleted;
        public void ReleaseLockOrder(string orderNumber, object userState)
        {
            this._invoker.InvokeUserState<bool>(this._context, "ReleaseLockOrder", this.ReleaseLockOrderCompleted, userState, GlobalData.Token, orderNumber);
        }

        #endregion

        #region PayStoneOrderByRMB

        public event EventHandler<WebInvokeEventArgs<int>> PayStoneOrderByRMBCompleted;
        public void PayStoneOrderByRMB(string orderNumber, decimal rmb, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "PayStoneOrderByRMB", this.PayStoneOrderByRMBCompleted, userState, GlobalData.Token, orderNumber, rmb);
        }

        #endregion

        #region SearchUserSellStoneOrders

        public event EventHandler<WebInvokeEventArgs<SellStonesOrder[]>> SearchUserSellStoneOrdersCompleted;
        public void SearchUserSellStoneOrders(int beginYear, int beginMonth, int beginDay, int endYear, int endMonth, int endDay, object userState)
        {
            this._invoker.InvokeUserState<SellStonesOrder[]>(this._context, "SearchUserSellStoneOrders", this.SearchUserSellStoneOrdersCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, beginYear, beginMonth, beginDay, endYear, endMonth, endDay);
        }

        #endregion

        #region SearchUserBuyStoneOrders

        public event EventHandler<WebInvokeEventArgs<BuyStonesOrder[]>> SearchUserBuyStoneOrdersCompleted;
        public void SearchUserBuyStoneOrders(int beginYear, int beginMonth, int beginDay, int endYear, int endMonth, int endDay, object userState)
        {
            this._invoker.InvokeUserState<BuyStonesOrder[]>(this._context, "SearchUserBuyStoneOrders", this.SearchUserBuyStoneOrdersCompleted, userState, GlobalData.Token, GlobalData.CurrentUser.UserName, beginYear, beginMonth, beginDay, endYear, endMonth, endDay);
        }

        #endregion

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
