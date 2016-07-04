using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
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
        private SellStonesOrderUIModel _sellOrder;

        public SellStonesOrderUIModel SellOrder
        {
            get { return _sellOrder; }
            set { _sellOrder = value; }
        }


        public BuyStonesWindow(SellStonesOrderUIModel order)
        {
            InitializeComponent();
            this._sellOrder = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.ReleaseLockOrderCompleted += Client_ReleaseLockOrderCompleted;
            GlobalData.Client.PayOrderByAlipayCompleted += Client_PayOrderByAlipayCompleted;
            GlobalData.Client.PayOrderByRMBCompleted += Client_PayOrderByRMBCompleted;
            
            this.DataContext = this.SellOrder;

            float awardGoldCoin = this.SellOrder.SellStonesCount * GlobalData.GameConfig.StoneBuyerAwardGoldCoinMultiple;
            this.txtAwardGoldCoin.Text = ((int)awardGoldCoin).ToString();
            if (GlobalData.CurrentUser.RMB < this.SellOrder.ValueRMB)
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

        void Client_PayOrderByRMBCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
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
                MyMessageBox.ShowInfo("购买成功。");
                this.Close();
            }
            else
            {
                MyMessageBox.ShowInfo("购买失败。");
            }
        }

        void Client_PayOrderByAlipayCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<string> e)
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

            if (string.IsNullOrEmpty(e.Result))
            {
                MyMessageBox.ShowInfo("购买失败。");
                return;
            }

            this.Close();
        }

        void Client_ReleaseLockOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
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

            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (chkPayType.IsChecked == true)//支付宝支付
            {
                GlobalData.Client.PayOrderByAlipay(SellOrder.OrderNumber, SellOrder.ValueRMB, null);
            }
            else
            {
                GlobalData.Client.PayOrderByRMB(SellOrder.OrderNumber, SellOrder.ValueRMB, null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.ReleaseLockOrder(null);
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
