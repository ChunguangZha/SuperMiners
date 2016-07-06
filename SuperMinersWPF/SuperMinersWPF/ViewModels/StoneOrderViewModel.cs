using MetaData.Trade;
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
    class StoneOrderViewModel : INotifyPropertyChanged
    {
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



        //private LockSellStonesOrderUIModel _lockedStonesOrder = null;
        //public LockSellStonesOrderUIModel LockedStonesOrder
        //{
        //    get { return this._lockedStonesOrder; }
        //    set
        //    {
        //        this._lockedStonesOrder = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("LockedStonesOrder"));
        //            PropertyChanged(this, new PropertyChangedEventArgs("LockedStonesOrderVisible"));
        //        }
        //    }
        //}


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
            GlobalData.Client.PayOrderByRMB(orderNumber, valueRMB, null);
        }

        public void AsyncCheckPlayerHasNotPayedOrder()
        {
            GlobalData.Client.CheckUserHasNotPayOrder(null);
        }

        public void AsyncAutoMatchStonesOrder(int stoneCount)
        {
            GlobalData.Client.AutoMatchLockSellStone(stoneCount, null);
        }
        
        public void AsyncGetOrderLockedBySelf()
        {
            GlobalData.Client.GetOrderLockedBySelf(null);
        }

        public void AsyncGetAllNotFinishedSellOrders()
        {
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
        }

        void Client_OnOrderListChanged()
        {
            AsyncGetAllNotFinishedSellOrders();
        }

        void Client_OnOrderAlipayPaySucceed(int tradeType, string orderNumber)
        {
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
                    GlobalData.Client.ReleaseLockOrder(lockedOrder.OrderNumber, null);
                    if (StoneOrderLockTimeOut != null)
                    {
                        StoneOrderLockTimeOut();
                    }
                    this.MyBuyNotFinishedStoneOrders.Clear();
                    this._timer.Stop();
                }
            }
        }

        void Client_PayOrderByRMBCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
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

            this._timer.Start();
            if (StoneOrderLockSucceed != null)
            {
                StoneOrderLockSucceed(lockedOrder);
            }

        }

        void Client_GetOrderLockedBySelfCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
        {
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
