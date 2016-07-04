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
            GlobalData.Client.GetNotFinishedStonesOrderCompleted += Client_GetNotFinishedStonesOrderCompleted;
            GlobalData.Client.AutoMatchLockSellStoneCompleted += Client_AutoMatchLockSellStoneCompleted;
            GlobalData.Client.CheckUserHasNotPayOrderCompleted += Client_CheckUserHasNotPayOrderCompleted;
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
                return;
            }

            if (e.Result == null)
            {
                MyMessageBox.ShowInfo("没有找到合适的订单。");
                return;
            }

            this._syn.Post(o =>
            {
                BuyStonesWindow win = new BuyStonesWindow(new Models.SellStonesOrderUIModel(e.Result));
                if (win.ShowDialog() == true)
                {

                }
                else
                {

                }
            }, null);
        }

        void Client_GetNotFinishedStonesOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.SellStonesOrder[]> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MyMessageBox.ShowInfo("获取矿石卖单失败。");
                return;
            }

            this.AllNotFinishStonesOrder.Clear();
            for (int i = 0; i < e.Result.Length; i++)
            {
                var item = e.Result[i];
                this.AllNotFinishStonesOrder.Add(new SellStonesOrderUIModel(item));
            }
        }
    }
}
