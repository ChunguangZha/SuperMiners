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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for StonesMarketUserControl.xaml
    /// </summary>
    public partial class StonesMarketUserControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public StonesMarketUserControl()
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.listboxSellOrders.ItemsSource = App.StoneOrderVMObject.AllNotFinishStonesOrder;
            App.StoneOrderVMObject.AsyncGetNotFinishedStonesOrder();
            GlobalData.Client.CheckUserHasNotPayOrderCompleted += Client_CheckUserHasNotPayOrderCompleted;
            GlobalData.Client.AutoMatchLockSellStoneCompleted += Client_AutoMatchLockSellStoneCompleted;
        }

        void Client_AutoMatchLockSellStoneCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.SellStonesOrder> e)
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

            GlobalData.Client.AutoMatchLockSellStone(null, (int)numBuyStones.Value);
        }

        private void numBuyStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnAutoMatchOrder_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.CheckUserHasNotPayOrder(null);
        }
    }
}
