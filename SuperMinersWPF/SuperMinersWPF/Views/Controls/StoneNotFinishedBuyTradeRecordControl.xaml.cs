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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for StoneNotFinishedTradeRecordControl.xaml
    /// </summary>
    public partial class StoneNotFinishedBuyTradeRecordControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public StoneNotFinishedBuyTradeRecordControl()
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.IsLogined)
            {
                return;
            }

            this.listboxMyBuyOrders.ItemsSource = App.StoneOrderVMObject.MyBuyNotFinishedStoneOrders;
            this.listboxMySellOrders.ItemsSource = App.StoneOrderVMObject.MySellNotFinishedStoneOrders;

            Binding bind = new Binding()
            {
                Source = App.StoneOrderVMObject
            };
            this.expBuyOrders.SetBinding(Expander.DataContextProperty, bind);

        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            //支付宝支付
            var lockedOrder = App.StoneOrderVMObject.GetFirstLockedStoneOrder();
            if (lockedOrder != null)
            {
                MyWebPage.ShowMyWebPage(lockedOrder.PayUrl);
            }
        }

        private void btnAppeal_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            LockSellStonesOrderUIModel lockStoneOrder = btn.DataContext as LockSellStonesOrderUIModel;
            if (lockStoneOrder == null)
            {
                return;
            }

            System.Windows.Forms.DialogResult result = MyMessageBox.ShowQuestionOKCancel("没有接收到支付宝付款信息。如确实付款，请点击【确定】，将对订单进行申诉，同时联系管理员进行处理，否则请点击【取消】。注意：三次恶意订单申诉，请被永久封号。");
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                App.StoneOrderVMObject.AsyncSetStoneOrderPayException(lockStoneOrder.OrderNumber);
            }
        }

        private void btnRefreshMySelfOrders_Click(object sender, RoutedEventArgs e)
        {
            App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
        }

        private void btnCancelPay_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            LockSellStonesOrderUIModel lockStoneOrder = btn.DataContext as LockSellStonesOrderUIModel;
            if (lockStoneOrder == null)
            {
                return;
            }

            App.StoneOrderVMObject.AsyncCancelBuyStoneOrder(lockStoneOrder.OrderNumber);
        }

        private void btnCancelSellStone_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            SellStonesOrderUIModel sellStoneOrder = btn.DataContext as SellStonesOrderUIModel;
            if (sellStoneOrder == null)
            {
                return;
            }

            App.StoneOrderVMObject.AsyncCancelSellStoneOrder(sellStoneOrder.OrderNumber);
        }

    }
}
