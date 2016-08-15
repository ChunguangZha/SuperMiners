using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for BuyStonesWindow.xaml
    /// </summary>
    public partial class BuyStonesWindow : Window
    {
        private LockSellStonesOrderUIModel _lockedOrder;

        public LockSellStonesOrderUIModel LockedOrder
        {
            get { return _lockedOrder; }
            set { _lockedOrder = value; }
        }

        private System.Threading.SynchronizationContext _syn;


        public BuyStonesWindow(LockSellStonesOrderUIModel order)
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
            this._lockedOrder = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.ReleaseLockOrderCompleted += Client_ReleaseLockOrderCompleted;
            //GlobalData.Client.PayOrderByAlipayCompleted += Client_PayOrderByAlipayCompleted;
            App.StoneOrderVMObject.StoneOrderLockTimeOut += StoneOrderVMObject_OrderLockTimeOut;
            App.StoneOrderVMObject.StoneOrderPaySucceed += StoneOrderVMObject_PayOrderSucceed;
            
            this.DataContext = this.LockedOrder;

            float awardGoldCoin = this.LockedOrder.SellStonesCount * GlobalData.GameConfig.StoneBuyerAwardGoldCoinMultiple;
            this.txtAwardGoldCoin.Text = ((int)awardGoldCoin).ToString();
            if (GlobalData.CurrentUser.RMB < this.LockedOrder.ValueRMB)
            {
                this.chkPayType.IsChecked = true;
                this.chkPayType.IsEnabled = false;
            }
            else
            {
                this.chkPayType.IsChecked = true;
                this.chkPayType.IsEnabled = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.ReleaseLockOrderCompleted -= Client_ReleaseLockOrderCompleted;
            //GlobalData.Client.PayOrderByAlipayCompleted -= Client_PayOrderByAlipayCompleted;
            App.StoneOrderVMObject.StoneOrderLockTimeOut -= StoneOrderVMObject_OrderLockTimeOut;
            App.StoneOrderVMObject.StoneOrderPaySucceed -= StoneOrderVMObject_PayOrderSucceed;
        }

        void StoneOrderVMObject_PayOrderSucceed()
        {
            this.Close();
        }

        void StoneOrderVMObject_OrderLockTimeOut()
        {
            this.btnOK.IsEnabled = false;
            _syn.Post(o =>
            {
                MyMessageBox.ShowInfo("订单锁定时间超时，已被取消，如已经付款，请与客服联系。");
                this.Close();
            }, null);
        }

        //void Client_PayOrderByAlipayCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<string> e)
        //{
        //    if (e.Cancelled)
        //    {
        //        return;
        //    }

        //    if (e.Error != null)
        //    {
        //        MyMessageBox.ShowInfo("连接服务器失败。");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(e.Result))
        //    {
        //        MyMessageBox.ShowInfo("购买失败。");
        //        return;
        //    }

        //    this.Close();
        //}

        void Client_ReleaseLockOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                _syn.Post(o =>
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                }, null);
                return;
            }

            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (chkPayType.IsChecked == true)//支付宝支付
            {
                MyWebPage.ShowMyWebPage(this.LockedOrder.PayUrl);
                MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
            }
            else
            {
                App.StoneOrderVMObject.AsyncPayOrderByRMB(LockedOrder.OrderNumber, LockedOrder.ValueRMB);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.ReleaseLockOrder(this._lockedOrder.OrderNumber, null);
        }

        private void chkPayType_Checked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "支付宝支付";
        }

        private void chkPayType_Unchecked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "灵币支付";
        }
    }
}
