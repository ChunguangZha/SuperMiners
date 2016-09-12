using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for StoneOrderResolveException.xaml
    /// </summary>
    public partial class StoneOrderResolveExceptionWindow : Window
    {
        private SynchronizationContext _syn;
        private LockSellStonesOrderUIModel _order = null;

        public StoneOrderResolveExceptionWindow(LockSellStonesOrderUIModel order)
        {
            InitializeComponent();
            this._order = order;
            this.DataContext = this._order;
            _syn = SynchronizationContext.Current;

            GlobalData.Client.HandleExceptionStoneOrderSucceedCompleted += Client_HandleExceptionStoneOrderSucceedCompleted;
            GlobalData.Client.HandleExceptionStoneOrderFailedCompleted += Client_HandleExceptionStoneOrderFailedCompleted;
        }

        void Client_HandleExceptionStoneOrderSucceedCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("操作失败。");
                return;
            }

            if (e.Result == OperResult.RESULTCODE_TRUE)
            {
                MessageBox.Show("操作成功。");
                _syn.Post(o =>
                {
                    this.DialogResult = true;
                }, null);
            }
            else
            {
                MessageBox.Show(OperResult.GetMsg(e.Result));
            }
        }

        void Client_HandleExceptionStoneOrderFailedCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show("操作失败。");
                return;
            }

            if (e.Result == OperResult.RESULTCODE_TRUE)
            {
                MessageBox.Show("操作成功。");
                _syn.Post(o =>
                {
                    this.DialogResult = true;
                }, null);
            }
            else
            {
                MessageBox.Show("操作失败。原因：" + OperResult.GetMsg(e.Result));
            }
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认订单支付成功，该操作不可恢复！", "确认订单成功", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                FillAlipayInfoWindow win = new FillAlipayInfoWindow(this._order.LockedByUserName, this._order.OrderNumber);
                if (win.ShowDialog() == true)
                {
                    AlipayRechargeRecord alipayInfo = win.AlipayPayInfo;

                    App.BusyToken.ShowBusyWindow("正在提交数据...");
                    GlobalData.Client.HandleExceptionStoneOrderSucceed(alipayInfo);
                }
            }
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认订单支付失败，该操作不可恢复！", "确认订单失败", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.HandleExceptionStoneOrderFailed(this._order.OrderNumber);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
