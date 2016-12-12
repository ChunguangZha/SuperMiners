using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for HandleExceptionStoneOrderWindow.xaml
    /// </summary>
    public partial class HandleExceptionStoneOrderWindow : Window
    {
        private SynchronizationContext _syn;
        private LockSellStonesOrderUIModel _lockStoneOrder;

        public HandleExceptionStoneOrderWindow(LockSellStonesOrderUIModel lockStoneOrder)
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
            this._lockStoneOrder = lockStoneOrder;
            this.DataContext = lockStoneOrder;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.SearchExceptionAlipayRechargeRecordCompleted += Client_SearchExceptionAlipayRechargeRecordCompleted;
            GlobalData.Client.RejectExceptionStoneOrderCompleted += Client_RejectExceptionStoneOrderCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.SearchExceptionAlipayRechargeRecordCompleted -= Client_SearchExceptionAlipayRechargeRecordCompleted;
            GlobalData.Client.RejectExceptionStoneOrderCompleted -= Client_RejectExceptionStoneOrderCompleted;
        }

        void Client_RejectExceptionStoneOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("拒绝矿石订单申诉失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    _syn.Post(o =>
                    {
                        MessageBox.Show("拒绝矿石订单申诉成功。");
                        this.DialogResult = true;
                    }, null);
                }
                else
                {
                    MessageBox.Show("拒绝矿石订单申诉失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("拒绝矿石订单申诉回调操作异常。原因为：" + exc.Message);
            }
        }

        void Client_SearchExceptionAlipayRechargeRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.AlipayRechargeRecord> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("搜索支付宝付款记录失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == null)
                {
                    MessageBox.Show("没有搜索到支付宝付款记录。");
                    return;
                }
                else
                {
                    _syn.Post(o =>
                    {
                        try
                        {
                            HandleExceptionAlipayRecordWindow win = new HandleExceptionAlipayRecordWindow(new AlipayRechargeRecordUIModel(e.Result));
                            if (win.ShowDialog() == true)
                            {
                                this.DialogResult = true;
                            }
                        }
                        catch (Exception exc)
                        {
                            MyMessageBox.ShowInfo(exc.Message);
                        }
                    }, null);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("搜索支付宝付款记录回调操作异常。原因为：" + exc.Message);
            }
        }

        private void btnCreateAlipay_Click(object sender, RoutedEventArgs e)
        {
            HandleExceptionAlipayRecordWindow win = new HandleExceptionAlipayRecordWindow();
            if (win.ShowDialog() == true)
            {
                this.DialogResult = true;
            }
        }

        private void btnSearchAlipay_Click(object sender, RoutedEventArgs e)
        {
            App.BusyToken.ShowBusyWindow("正在搜索支付宝付款记录...");
            GlobalData.Client.SearchExceptionAlipayRechargeRecord(this._lockStoneOrder.OrderNumber);
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            App.BusyToken.ShowBusyWindow("正在提交拒绝订单...");
            GlobalData.Client.RejectExceptionStoneOrder(this._lockStoneOrder.OrderNumber);
        }
    }
}
