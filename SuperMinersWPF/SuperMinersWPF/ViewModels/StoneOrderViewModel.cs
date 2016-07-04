using MetaData.Trade;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class StoneOrderViewModel
    {
        private ObservableCollection<SellStonesOrderUIModel> _allNotFinishStonesOrder = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> AllNotFinishStonesOrder
        {
            get { return _allNotFinishStonesOrder; }
        }

        public LockSellStonesOrderUIModel LockedStonesOrder = null;
        System.Timers.Timer _timer = new System.Timers.Timer(1000);

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
        
        public void AsyncGetNotFinishedStonesOrder()
        {
            GlobalData.Client.GetNotFinishedStonesOrder(null);
        }

        public void RegisterEvent()
        {
            _timer.Elapsed += Timer_Elapsed;
            GlobalData.Client.GetNotFinishedStonesOrderCompleted += Client_GetNotFinishedStonesOrderCompleted;
            GlobalData.Client.AutoMatchLockSellStoneCompleted += Client_AutoMatchLockSellStoneCompleted;
            GlobalData.Client.CheckUserHasNotPayOrderCompleted += Client_CheckUserHasNotPayOrderCompleted;
            GlobalData.Client.PayOrderByRMBCompleted += Client_PayOrderByRMBCompleted;
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.LockedStonesOrder == null)
            {
                this._timer.Stop();
            }
            else
            {
                if (this.LockedStonesOrder.ValidTimeSecondsTickDown() <= 0)
                {
                    GlobalData.Client.ReleaseLockOrder(null);
                    if (OrderLockTimeOut != null)
                    {
                        OrderLockTimeOut();
                    }
                    this.LockedStonesOrder = null;
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
                MyMessageBox.ShowInfo("购买成功。");

                if (PayOrderSucceed != null)
                {
                    PayOrderSucceed();
                }
                this.LockedStonesOrder = null;
            }
            else
            {
                MyMessageBox.ShowInfo("购买失败。");
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

            LockedStonesOrder = new LockSellStonesOrderUIModel(e.Result);
            this._timer.Start();
            if (LockOrderSucceed != null)
            {
                LockOrderSucceed(LockedStonesOrder);
            }

        }

        void Client_GetNotFinishedStonesOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<LockSellStonesOrder> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("获取矿石卖单失败。");
                LogHelper.Instance.AddErrorLog("Client_GetNotFinishedStonesOrderCompleted Exception。", e.Error);
                return;
            }

            if (e.Result != null)
            {
                this.LockedStonesOrder = new LockSellStonesOrderUIModel(e.Result);
                this._timer.Start();
            }
        }

        public event Action<LockSellStonesOrderUIModel> LockOrderSucceed;
        public event Action PayOrderSucceed;
        public event Action OrderLockTimeOut;
    }
}
