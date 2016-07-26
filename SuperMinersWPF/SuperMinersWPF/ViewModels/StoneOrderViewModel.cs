﻿using MetaData.Trade;
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



        System.Timers.Timer _timer = new System.Timers.Timer(1000);

        public LockSellStonesOrderUIModel GetFirstLockedStoneOrder()
        {
            if (this._myBuyNotFinishedStoneOrders == null || this._myBuyNotFinishedStoneOrders.Count == 0)
            {
                return null;
            }

            return this._myBuyNotFinishedStoneOrders[0];
        }

        public void AsyncPayOrderByRMB(string orderNumber, float valueRMB)
        {
            App.BusyToken.ShowBusyWindow("正在提交服务器...");
            GlobalData.Client.PayOrderByRMB(orderNumber, valueRMB, null);
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

        public void RegisterEvent()
        {
            _timer.Elapsed += Timer_Elapsed;
            GlobalData.Client.GetOrderLockedBySelfCompleted += Client_GetOrderLockedBySelfCompleted;
            GlobalData.Client.AutoMatchLockSellStoneCompleted += Client_AutoMatchLockSellStoneCompleted;
            GlobalData.Client.CheckUserHasNotPayOrderCompleted += Client_CheckUserHasNotPayOrderCompleted;
            GlobalData.Client.PayOrderByRMBCompleted += Client_PayOrderByRMBCompleted;
            GlobalData.Client.GetAllNotFinishedSellOrdersCompleted += Client_GetAllNotFinishedSellOrdersCompleted;
            GlobalData.Client.OnOrderAlipayPaySucceed += Client_OnOrderAlipayPaySucceed;
            GlobalData.Client.OnOrderListChanged += Client_OnOrderListChanged;
            GlobalData.Client.LockSellStoneCompleted += Client_LockSellStoneCompleted;
        }

        void Client_LockSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
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

        void Client_OnOrderListChanged()
        {
            AsyncGetAllNotFinishedSellOrders();
        }

        void Client_OnOrderAlipayPaySucceed(int tradeType, string orderNumber)
        {
            App.BusyToken.CloseBusyWindow();
            if (tradeType == (int)TradeType.StoneTrade)
            {
                var lockedOrder = this.GetFirstLockedStoneOrder();
                if (lockedOrder != null && lockedOrder.OrderNumber == orderNumber)
                {
                    MyMessageBox.ShowInfo("矿石购买成功。");
                    App.UserVMObject.AsyncGetPlayerInfo();
                    this.AsyncGetAllNotFinishedSellOrders();

                    if (StoneOrderPaySucceed != null)
                    {
                        StoneOrderPaySucceed();
                    }
                    this._myBuyNotFinishedStoneOrders.Clear();
                }
            }
        }

        void Client_GetAllNotFinishedSellOrdersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SellStonesOrder[]> e)
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
                    var listOrderTimeASC = e.Result.OrderBy(s => s.SellTime);
                    foreach (var item in listOrderTimeASC)
                    {
                        var uiobj = new SellStonesOrderUIModel(item);
                        this._allNotFinishStoneOrder.Add(uiobj);
                        if (uiobj.SellerUserName == GlobalData.CurrentUser.UserName)
                        {
                            this._mySellNotFinishedStonesOrders.Add(uiobj);
                        }
                    }
                }
            }
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var lockedOrder = this.GetFirstLockedStoneOrder();
            if (lockedOrder == null)
            {
                this._timer.Stop();
            }
            else
            {
                if (lockedOrder.ValidTimeSecondsTickDown() <= 0)
                {
                    this._timer.Stop();
                    GlobalData.Client.ReleaseLockOrder(lockedOrder.OrderNumber, null);
                    if (StoneOrderLockTimeOut != null)
                    {
                        StoneOrderLockTimeOut();
                    }
                    this.MyBuyNotFinishedStoneOrders.Clear();
                }
            }
        }

        void Client_PayOrderByRMBCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
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
            if (e.Result)
            {
                MyMessageBox.ShowInfo("矿石购买成功。");

                if (StoneOrderPaySucceed != null)
                {
                    StoneOrderPaySucceed();
                }
                this.MyBuyNotFinishedStoneOrders.Clear();
            }
            else
            {
                MyMessageBox.ShowInfo("矿石购买失败。");
            }
        }

        void Client_CheckUserHasNotPayOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
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

        void Client_AutoMatchLockSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.LockSellStonesOrder> e)
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

        void Client_GetOrderLockedBySelfCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
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

            if (e.Result != null)
            {
                var lockedOrder = new LockSellStonesOrderUIModel(e.Result);
                this.MyBuyNotFinishedStoneOrders.Clear();
                this.MyBuyNotFinishedStoneOrders.Add(lockedOrder);
                this._timer.Start();
            }
        }

        public event Action<LockSellStonesOrderUIModel> StoneOrderLockSucceed;
        public event Action StoneOrderPaySucceed;
        public event Action StoneOrderLockTimeOut;

    }
}