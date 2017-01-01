using MetaData;
using MetaData.Game.StoneStack;
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
        public event Action OnDelegateStoneOrderTradeSucceed;

        #region Callback

        public void RaiseOnDelegateStoneOrderTradeSucceed()
        {
            Action handler = this.OnDelegateStoneOrderTradeSucceed;
            if (null != handler)
            {
                handler();
            }
        }

        #endregion

        #region DelegateSellStone

        public event EventHandler<WebInvokeEventArgs<int>> DelegateSellStoneCompleted;
        public void DelegateSellStone(int sellStoneHandsCount, decimal price, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "DelegateSellStone", this.DelegateSellStoneCompleted, userState, GlobalData.Token, sellStoneHandsCount, price);
        }

        #endregion

        #region CancelDelegateSellStone

        public event EventHandler<WebInvokeEventArgs<int>> CancelDelegateSellStoneCompleted;
        public void CancelDelegateSellStone(StoneDelegateSellOrderInfo sellOrder, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "CancelDelegateSellStone", this.CancelDelegateSellStoneCompleted, userState, GlobalData.Token, sellOrder);
        }

        #endregion

        #region GetNotFinishedDelegateSellStoneOrders

        public event EventHandler<WebInvokeEventArgs<StoneDelegateSellOrderInfo[]>> GetNotFinishedDelegateSellStoneOrdersCompleted;
        public void GetNotFinishedDelegateSellStoneOrders(object userState)
        {
            this._invoker.InvokeUserState<StoneDelegateSellOrderInfo[]>(this._context, "GetNotFinishedDelegateSellStoneOrders", this.GetNotFinishedDelegateSellStoneOrdersCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region GetFinishedDelegateSellStoneOrders

        public event EventHandler<WebInvokeEventArgs<StoneDelegateSellOrderInfo[]>> GetFinishedDelegateSellStoneOrdersCompleted;
        public void GetFinishedDelegateSellStoneOrders(MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex, object userState)
        {
            this._invoker.InvokeUserState<StoneDelegateSellOrderInfo[]>(this._context, "GetFinishedDelegateSellStoneOrders", this.GetFinishedDelegateSellStoneOrdersCompleted, userState, GlobalData.Token, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
        }

        #endregion

        #region DelegateBuyStone

        public event EventHandler<WebInvokeEventArgs<OperResultObject>> DelegateBuyStoneCompleted;
        public void DelegateBuyStone(int buyStoneHandsCount, decimal price, PayType paytype, object userState)
        {
            this._invoker.InvokeUserState<OperResultObject>(this._context, "DelegateBuyStone", this.DelegateBuyStoneCompleted, userState, GlobalData.Token, buyStoneHandsCount, price, paytype);
        }

        #endregion

        #region CancelDelegateBuyStone

        public event EventHandler<WebInvokeEventArgs<int>> CancelDelegateBuyStoneCompleted;
        public void CancelDelegateBuyStone(StoneDelegateBuyOrderInfo buyOrder, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "CancelDelegateBuyStone", this.CancelDelegateBuyStoneCompleted, userState, GlobalData.Token, buyOrder);
        }

        #endregion

        #region GetNotFinishedDelegateBuyStoneOrders

        public event EventHandler<WebInvokeEventArgs<StoneDelegateBuyOrderInfo[]>> GetNotFinishedDelegateBuyStoneOrdersCompleted;
        public void GetNotFinishedDelegateBuyStoneOrders(object userState)
        {
            this._invoker.InvokeUserState<StoneDelegateBuyOrderInfo[]>(this._context, "GetNotFinishedDelegateBuyStoneOrders", this.GetNotFinishedDelegateBuyStoneOrdersCompleted, userState, GlobalData.Token);
        }

        #endregion

        #region GetFinishedDelegateBuyStoneOrders

        public event EventHandler<WebInvokeEventArgs<StoneDelegateBuyOrderInfo[]>> GetFinishedDelegateBuyStoneOrdersCompleted;
        public void GetFinishedDelegateBuyStoneOrders(MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex, object userState)
        {
            this._invoker.InvokeUserState<StoneDelegateBuyOrderInfo[]>(this._context, "GetFinishedDelegateBuyStoneOrders", this.GetFinishedDelegateBuyStoneOrdersCompleted, userState, GlobalData.Token, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
        }

        #endregion

        #region GetTodayStoneStackInfo

        public event EventHandler<WebInvokeEventArgs<TodayStoneStackTradeRecordInfo>> GetTodayStoneStackInfoCompleted;
        public void GetTodayStoneStackInfo(object userState)
        {
            this._invoker.InvokeUserState<TodayStoneStackTradeRecordInfo>(this._context, "GetTodayStoneStackInfo", this.GetTodayStoneStackInfoCompleted, userState, GlobalData.Token);
        }

        #endregion

    }
}
