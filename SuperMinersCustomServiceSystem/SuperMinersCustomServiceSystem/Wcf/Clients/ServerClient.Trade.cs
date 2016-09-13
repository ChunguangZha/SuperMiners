using MetaData;
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
        public void GetSellStonesOrderList(string sellerUserName, string orderNumber, int orderType, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<SellStonesOrder[]>(this._context, "GetSellStonesOrderList", this.GetSellStonesOrderListCompleted, GlobalData.Token, sellerUserName, orderNumber, orderType, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
        }

        public event EventHandler<WebInvokeEventArgs<LockSellStonesOrder[]>> GetLockedStonesOrderListCompleted;
        public void GetLockedStonesOrderList(string buyerUserName)
        {
            this._invoker.Invoke<LockSellStonesOrder[]>(this._context, "GetLockedStonesOrderList", this.GetLockedStonesOrderListCompleted, GlobalData.Token, buyerUserName);
        }

        public event EventHandler<WebInvokeEventArgs<BuyStonesOrder[]>> GetBuyStonesOrderListCompleted;
        public void GetBuyStonesOrderList(string sellerUserName, string orderNumber, string buyUserName, int orderType, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            this._invoker.Invoke<BuyStonesOrder[]>(this._context, "GetBuyStonesOrderList", this.GetBuyStonesOrderListCompleted, GlobalData.Token, sellerUserName, orderNumber, buyUserName, orderType, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex);
        }

        //public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMinesFinishedRecordListCompleted;
        //public void GetBuyMinesFinishedRecordList(string buyerUserName, MyDateTime startDate, MyDateTime endDate)
        //{
        //    this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMinesFinishedRecordList", this.GetBuyMinesFinishedRecordListCompleted, GlobalData.Token, buyerUserName, startDate, endDate);
        //}

        //public event EventHandler<WebInvokeEventArgs<MinesBuyRecord[]>> GetBuyMinesNotFinishedRecordListCompleted;
        //public void GetBuyMinesNotFinishedRecordList(string buyerUserName)
        //{
        //    this._invoker.Invoke<MinesBuyRecord[]>(this._context, "GetBuyMinesNotFinishedRecordList", this.GetBuyMinesNotFinishedRecordListCompleted, GlobalData.Token, buyerUserName);
        //}

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

    }
}
