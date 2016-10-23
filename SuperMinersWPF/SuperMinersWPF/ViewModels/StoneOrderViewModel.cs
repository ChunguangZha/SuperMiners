using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF.ViewModels
{
    class StoneOrderViewModel
    {
        private object _lockAllNotFinishStoneOrder = new object();
        private ObservableCollection<SellStonesOrderUIModel> _allNotFinishStoneOrder = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> AllNotFinishStoneOrder
        {
            get { return _allNotFinishStoneOrder; }
        }

        private ObservableCollection<LockSellStonesOrderUIModel> _myBuyNotFinishedStoneOrders = new ObservableCollection<LockSellStonesOrderUIModel>();

        public ObservableCollection<LockSellStonesOrderUIModel> MyBuyNotFinishedStoneOrders
        {
            get { return _myBuyNotFinishedStoneOrders; }
        }

        private ObservableCollection<SellStonesOrderUIModel> _mySellNotFinishedStonesOrders = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> MySellNotFinishedStoneOrders
        {
            get { return _mySellNotFinishedStonesOrders; }
        }

        private ObservableCollection<BuyStonesOrderUIModel> _listMyBuyStoneHistoryOrders = new ObservableCollection<BuyStonesOrderUIModel>();

        public ObservableCollection<BuyStonesOrderUIModel> ListMyBuyStoneHistoryOrders
        {
            get { return _listMyBuyStoneHistoryOrders; }
        }

        private ObservableCollection<SellStonesOrderUIModel> _listMySellStoneHistoryOrders = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> ListMySellStoneHistoryOrders
        {
            get { return _listMySellStoneHistoryOrders; }
        }



        System.Timers.Timer _timer = new System.Timers.Timer(1000);

        public LockSellStonesOrderUIModel GetFirstLockedStoneOrder()
        {
            if (this._myBuyNotFinishedStoneOrders == null || this._myBuyNotFinishedStoneOrders.Count == 0)
            {
                return null;
            }

            return this._myBuyNotFinishedStoneOrders[0];
        }

        public void AsyncCancelBuyStoneOrder(string orderNumber)
        {
            App.BusyToken.ShowBusyWindow("正在取消矿石订单...");
            GlobalData.Client.ReleaseLockOrder(orderNumber, null);
        }

        public void AsyncPayOrderByRMB(string orderNumber, decimal valueRMB)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.PayStoneOrderByRMB(orderNumber, valueRMB, null);
        }

        public void AsyncCheckPlayerHasNotPayedOrder()
        {
            App.BusyToken.ShowBusyWindow("正在您是否有未完成的订单...");
            GlobalData.Client.CheckUserHasNotPayOrder(null);
        }

        public void AsyncAutoMatchStonesOrder(int stoneCount)
        {
            App.BusyToken.ShowBusyWindow("正在匹配订单");
            GlobalData.Client.AutoMatchLockSellStone(stoneCount, null);
        }

        public void AsyncLockStoneOrder(string orderNumber)
        {
            App.BusyToken.ShowBusyWindow("正在锁定订单...");
            GlobalData.Client.LockSellStone(orderNumber, null);
        }
        
        public void AsyncGetOrderLockedBySelf()
        {
            App.BusyToken.ShowBusyWindow("正在获取您的订单...");
            GlobalData.Client.GetOrderLockedBySelf(null);
        }

        public void AsyncGetAllNotFinishedSellOrders()
        {
            App.BusyToken.ShowBusyWindow("正在加载新的订单...");
            GlobalData.Client.GetAllNotFinishedSellOrders(null);
        }

        public void AsyncCancelSellStoneOrder(string orderNumber)
        {
            App.BusyToken.ShowBusyWindow("正在取消订单...");
            GlobalData.Client.CancelSellStone(orderNumber, null);
        }

        public void AsyncSetStoneOrderPayException(string orderNumber)
        {
            App.BusyToken.ShowBusyWindow("正在提交申诉...");
            GlobalData.Client.SetStoneOrderPayException(orderNumber, null);
        }

        public void AsyncSearchUserBuyStoneOrders(string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在查询矿石买入订单记录");
            ListMyBuyStoneHistoryOrders.Clear();
            GlobalData.Client.SearchUserBuyStoneOrders(sellerUserName, orderNumber, orderState, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex, null);
        }

        public void AsyncSearchUserSellStoneOrders(string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在查询矿石卖出订单记录");
            ListMySellStoneHistoryOrders.Clear();
            GlobalData.Client.SearchUserSellStoneOrders(orderNumber, orderState, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex, null);
        }

        public void RegisterEvent()
        {
            _timer.Elapsed += Timer_Elapsed;
            GlobalData.Client.GetOrderLockedBySelfCompleted += Client_GetOrderLockedBySelfCompleted;
            GlobalData.Client.AutoMatchLockSellStoneCompleted += Client_AutoMatchLockSellStoneCompleted;
            GlobalData.Client.CheckUserHasNotPayOrderCompleted += Client_CheckUserHasNotPayOrderCompleted;
            GlobalData.Client.PayStoneOrderByRMBCompleted += Client_PayOrderByRMBCompleted;
            GlobalData.Client.GetAllNotFinishedSellOrdersCompleted += Client_GetAllNotFinishedSellOrdersCompleted;
            GlobalData.Client.OnOrderAlipayPaySucceed += Client_OnOrderAlipayPaySucceed;
            GlobalData.Client.OnOrderListChanged += Client_OnOrderListChanged;
            GlobalData.Client.LockSellStoneCompleted += Client_LockSellStoneCompleted;
            GlobalData.Client.CancelSellStoneCompleted += Client_CancelSellStoneCompleted;
            GlobalData.Client.SetStoneOrderPayExceptionCompleted += Client_SetStoneOrderPayExceptionCompleted;
            GlobalData.Client.OnAppealOrderFailed += Client_OnAppealOrderFailed;
            GlobalData.Client.SearchUserBuyStoneOrdersCompleted += Client_SearchUserBuyStoneOrdersCompleted;
            GlobalData.Client.SearchUserSellStoneOrdersCompleted += Client_SearchUserSellStoneOrdersCompleted;
            GlobalData.Client.ReleaseLockOrderCompleted += Client_ReleaseLockOrderCompleted;
        }

        void Client_ReleaseLockOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("取消矿石订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result)
                {
                    MessageBox.Show("取消矿石订单成功。");
                    this.MyBuyNotFinishedStoneOrders.Clear();
                }
                else
                {
                    MessageBox.Show("取消矿石订单失败。");
                }

                AsyncGetAllNotFinishedSellOrders();

                if (ReleaseLockOrderCompleted != null)
                {
                    ReleaseLockOrderCompleted(e.Result);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询矿石出售订单回调处理异常。" + exc.Message);
            }
        }

        void Client_SearchUserSellStoneOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SellStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿石出售订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListMySellStoneHistoryOrders.Add(new SellStonesOrderUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询矿石出售订单回调处理异常。" + exc.Message);
            }
        }

        void Client_SearchUserBuyStoneOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<BuyStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询矿石买入订单失败。" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListMyBuyStoneHistoryOrders.Add(new BuyStonesOrderUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("查询矿石买入订单回调处理异常。" + exc.Message);
            }
        }

        void Client_OnAppealOrderFailed(int arg1, string arg2)
        {
            AsyncGetOrderLockedBySelf();
            AsyncGetAllNotFinishedSellOrders();
        }

        /// <summary>
        /// RESULTCODE_USER_NOT_EXIST; RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_EXCEPTION; RESULTCODE_ORDER_NOT_BE_LOCKED; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_TRUE; RESULTCODE_FALSE;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Client_SetStoneOrderPayExceptionCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_LockSellStoneCompleted Exception。", e.Error);
                    return;
                }

                bool isOK = false;
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    isOK = true;
                    MyMessageBox.ShowInfo("申诉提交成功，等待管理员处理");
                }
                else if (e.Result == OperResult.RESULTCODE_ORDER_BUY_SUCCEED)
                {
                    isOK = true;
                    MyMessageBox.ShowInfo("矿石购买成功。");
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    isOK = false;
                    MyMessageBox.ShowInfo("申诉提交失败，原因：" + OperResult.GetMsg(e.Result));
                }

                if (SetStoneOrderExceptionFinished != null)
                {
                    SetStoneOrderExceptionFinished(isOK);
                }

                AsyncGetOrderLockedBySelf();
                AsyncGetAllNotFinishedSellOrders();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_CancelSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_LockSellStoneCompleted Exception。", e.Error);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("订单取消成功");
                }
                else
                {
                    MyMessageBox.ShowInfo("订单取消失败，原因：" + OperResult.GetMsg(e.Result));
                }

                App.UserVMObject.AsyncGetPlayerInfo();
                AsyncGetAllNotFinishedSellOrders();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_LockSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_LockSellStoneCompleted Exception。", e.Error);
                    return;
                }

                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("订单锁定失败。");
                    return;
                }

                var lockedOrder = new LockSellStonesOrderUIModel(e.Result);
                this.MyBuyNotFinishedStoneOrders.Clear();
                this.MyBuyNotFinishedStoneOrders.Add(lockedOrder);

                lock (_lockAllNotFinishStoneOrder)
                {
                    var orderSell = this._allNotFinishStoneOrder.FirstOrDefault(s => s.OrderNumber == lockedOrder.OrderNumber);
                    if (orderSell != null)
                    {
                        orderSell.OrderState = SellOrderState.Lock;
                    }
                }

                this._timer.Start();
                if (StoneOrderLockSucceed != null)
                {
                    StoneOrderLockSucceed(lockedOrder);
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_OnOrderListChanged()
        {
            AsyncGetAllNotFinishedSellOrders();
        }

        void Client_OnOrderAlipayPaySucceed(int tradeType, string orderNumber)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                App.UserVMObject.AsyncGetPlayerInfo();

                switch ((AlipayTradeInType)tradeType)
                {
                    case AlipayTradeInType.BuyStone:
                        var lockedOrder = this.GetFirstLockedStoneOrder();
                        if (lockedOrder != null && lockedOrder.OrderNumber == orderNumber)
                        {
                            MyMessageBox.ShowInfo("矿石购买成功。");
                            this.AsyncGetAllNotFinishedSellOrders();

                            if (StoneOrderPaySucceed != null)
                            {
                                StoneOrderPaySucceed();
                            }
                            this._myBuyNotFinishedStoneOrders.Clear();
                        }
                        else
                        {
                            var sellOrder = this.MySellNotFinishedStoneOrders.FirstOrDefault(o => o.OrderNumber == orderNumber);
                            if (sellOrder != null)
                            {
                                MyMessageBox.ShowInfo("矿石订单 " + orderNumber + " 已成功出售。");
                                this.AsyncGetAllNotFinishedSellOrders();
                                App.UserVMObject.AsyncGetPlayerInfo();
                            }
                        }
                        break;
                    case AlipayTradeInType.BuyMine:
                        if (BuyMineAlipayPaySucceed != null)
                        {
                            BuyMineAlipayPaySucceed();
                        }
                        break;
                    case AlipayTradeInType.BuyMiner:
                        break;
                    case AlipayTradeInType.BuyRMB:
                        break;
                    case AlipayTradeInType.BuyGoldCoin:
                        if (BuyGoldCoinAlipayPaySucceed != null)
                        {
                            BuyGoldCoinAlipayPaySucceed();
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetAllNotFinishedSellOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SellStonesOrder[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_GetAllNotFinishedSellOrdersCompleted Exception。", e.Error);
                    return;
                }
                if (e.Result != null)
                {
                    lock (_lockAllNotFinishStoneOrder)
                    {
                        this._allNotFinishStoneOrder.Clear();
                        this._mySellNotFinishedStonesOrders.Clear();
                        var listOrderTimeASC = e.Result.OrderBy(s => s.SellTime).OrderBy(s=>s.OrderStateInt);
                        foreach (var item in listOrderTimeASC)
                        {
                            var uiobj = new SellStonesOrderUIModel(item);
                            this._allNotFinishStoneOrder.Add(uiobj);
                            if (uiobj.SellerUserName == GlobalData.CurrentUser.UserName && uiobj.OrderState != SellOrderState.Finish)
                            {
                                this._mySellNotFinishedStonesOrders.Add(uiobj);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var lockedOrder = this.GetFirstLockedStoneOrder();
                if (lockedOrder == null)
                {
                    this._timer.Stop();
                }
                else
                {
                    //LogHelper.Instance.AddErrorLog("正在检查锁定的矿石订单" + lockedOrder.OrderNumber, null);
                    if (lockedOrder.ValidTimeSecondsTickDown() <= 0)
                    {
                        //LogHelper.Instance.AddErrorLog("矿石订单" + lockedOrder.OrderNumber + "，锁定超时，正在解锁", null);
                        this._timer.Stop();
                        this.AsyncCancelBuyStoneOrder(lockedOrder.OrderNumber);
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_PayOrderByRMBCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_PayOrderByRMBCompleted Exception。", e.Error);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    //此处屏蔽提示消息，在交易成功的回调函数中提示。
                    //MyMessageBox.ShowInfo("购买矿石成功。");

                    if (StoneOrderPaySucceed != null)
                    {
                        StoneOrderPaySucceed();
                    }
                    this.MyBuyNotFinishedStoneOrders.Clear();
                    MyMessageBox.ShowInfo("购买矿石成功。");
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("购买矿石失败。原因：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_CheckUserHasNotPayOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_CheckUserHasNotPayOrderCompleted Exception。", e.Error);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("您当前有未支付的订单，请先完成支付后，再购买新的矿石。");
                    return;
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_AutoMatchLockSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.LockSellStonesOrder> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    LogHelper.Instance.AddErrorLog("Client_AutoMatchLockSellStoneCompleted Exception。", e.Error);
                    return;
                }

                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("没有找到合适的订单。");
                    return;
                }

                var lockedOrder = new LockSellStonesOrderUIModel(e.Result);
                this.MyBuyNotFinishedStoneOrders.Clear();
                this.MyBuyNotFinishedStoneOrders.Add(lockedOrder);

                lock (_lockAllNotFinishStoneOrder)
                {
                    var orderSell = this._allNotFinishStoneOrder.FirstOrDefault(s => s.OrderNumber == lockedOrder.OrderNumber);
                    if (orderSell != null)
                    {
                        orderSell.OrderState = SellOrderState.Lock;
                    }
                }

                this._timer.Start();
                if (StoneOrderLockSucceed != null)
                {
                    StoneOrderLockSucceed(lockedOrder);
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetOrderLockedBySelfCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取未完成的订单失败。");
                    LogHelper.Instance.AddErrorLog("Client_GetOrderLockedBySelfCompleted Exception。", e.Error);
                    return;
                }

                this.MyBuyNotFinishedStoneOrders.Clear();
                if (e.Result != null)
                {
                    var lockedOrder = new LockSellStonesOrderUIModel(e.Result);
                    this.MyBuyNotFinishedStoneOrders.Add(lockedOrder);
                    this._timer.Start();
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        public event Action<LockSellStonesOrderUIModel> StoneOrderLockSucceed;
        public event Action StoneOrderPaySucceed;
        //public event Action StoneOrderLockTimeOut;
        public event Action<bool> ReleaseLockOrderCompleted;
        public event Action<bool> SetStoneOrderExceptionFinished;

        public event Action BuyGoldCoinAlipayPaySucceed;
        public event Action BuyMineAlipayPaySucceed;

    }
}
