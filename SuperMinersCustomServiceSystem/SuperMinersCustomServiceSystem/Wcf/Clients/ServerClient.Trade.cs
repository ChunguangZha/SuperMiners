using MetaData;
using MetaData.Game.StoneStack;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.Wcf.Clients
{
    public partial class ServerClient
    {

        public event EventHandler<WebInvokeEventArgs<GoldCoinRechargeRecord[]>> GetFinishedGoldCoinRechargeRecordListCompleted;
        public void GetFinishedGoldCoinRechargeRecordList(string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<GoldCoinRechargeRecord[]>(this._context, "GetFinishedGoldCoinRechargeRecordList", this.GetFinishedGoldCoinRechargeRecordListCompleted, GlobalData.Token, playerUserName, orderNumber, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<MinersBuyRecord[]>> GetBuyMinerFinishedRecordListCompleted;
        public void GetBuyMinerFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<MinersBuyRecord[]>(this._context, "GetBuyMinerFinishedRecordList", this.GetBuyMinerFinishedRecordListCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMineFinishedRecordListCompleted;
        public void GetBuyMineFinishedRecordList(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMineFinishedRecordList", this.GetBuyMineFinishedRecordListCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<SellStonesOrder[]>> GetSellStonesOrderListCompleted;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sellerUserName"></param>
        /// <param name="sellOrderState">小于0表示全部</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void GetSellStonesOrderList(string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<SellStonesOrder[]>(this._context, "GetSellStonesOrderList", this.GetSellStonesOrderListCompleted, GlobalData.Token, sellerUserName, orderNumber, orderState, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder[]>> GetLockedStonesOrderListCompleted;
        public void GetLockedStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState)
        {
            this._invoker.Invoke<LockSellStonesOrder[]>(this._context, "GetLockedStonesOrderList", this.GetLockedStonesOrderListCompleted, GlobalData.Token, sellerUserName, orderNumber, buyUserName, orderState);
        }

        public event EventHandler<WebInvokeEventArgs<BuyStonesOrder[]>> GetBuyStonesOrderListCompleted;
        public void GetBuyStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<BuyStonesOrder[]>(this._context, "GetBuyStonesOrderList", this.GetBuyStonesOrderListCompleted, GlobalData.Token, sellerUserName, orderNumber, buyUserName, orderState, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<int>> HandleExceptionAlipayRechargeRecordCompleted;
        public void HandleExceptionAlipayRechargeRecord(AlipayRechargeRecord exceptionRecord, object userState)
        {
            this._invoker.InvokeUserState<int>(this._context, "HandleExceptionAlipayRechargeRecord", this.HandleExceptionAlipayRechargeRecordCompleted, userState, GlobalData.Token, exceptionRecord);
        }

        //public event EventHandler<WebInvokeEventArgs<int>> AgreeExceptionStoneOrderCompleted;
        //public void AgreeExceptionStoneOrder(AlipayRechargeRecord alipayRecord)
        //{
        //    this._invoker.Invoke<int>(this._context, "AgreeExceptionStoneOrder", this.AgreeExceptionStoneOrderCompleted, GlobalData.Token, alipayRecord);
        //}

        public event EventHandler<WebInvokeEventArgs<int>> RejectExceptionStoneOrderCompleted;
        public void RejectExceptionStoneOrder(string orderNumber)
        {
            this._invoker.Invoke<int>(this._context, "RejectExceptionStoneOrder", this.RejectExceptionStoneOrderCompleted, GlobalData.Token, orderNumber);
        }

        public event EventHandler<WebInvokeEventArgs<ExpChangeRecord[]>> GetExpChangeRecordCompleted;
        public void GetExpChangeRecord(int userID)
        {
            this._invoker.Invoke<ExpChangeRecord[]>(this._context, "GetExpChangeRecord", this.GetExpChangeRecordCompleted, GlobalData.Token, userID);
        }

        public event EventHandler<WebInvokeEventArgs<StoneDelegateBuyOrderInfo[]>> GetStoneDelegateBuyOrderInfoCompleted;
        public void GetStoneDelegateBuyOrderInfo(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<StoneDelegateBuyOrderInfo[]>(this._context, "GetStoneDelegateBuyOrderInfo", this.GetStoneDelegateBuyOrderInfoCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<StoneDelegateSellOrderInfo[]>> GetStoneDelegateSellOrderInfoCompleted;
        public void GetStoneDelegateSellOrderInfo(string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<StoneDelegateSellOrderInfo[]>(this._context, "GetStoneDelegateSellOrderInfo", this.GetStoneDelegateSellOrderInfoCompleted, GlobalData.Token, playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
        }

    }
}
