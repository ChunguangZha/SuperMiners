using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for StoneActiveOrderListControl.xaml
    /// </summary>
    public partial class StoneActiveOrderListControl : UserControl
    {
        private SynchronizationContext _syn = null;

        public StoneActiveOrderListControl()
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

            this.listboxAllSellOrders.ItemsSource = App.StoneOrderVMObject.AllNotFinishStoneOrder;
            //App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
            App.StoneOrderVMObject.StoneOrderLockSucceed += StoneOrderVMObject_LockOrderSucceed;

        }

        void StoneOrderVMObject_LockOrderSucceed(LockSellStonesOrderUIModel obj)
        {
            this._syn.Post(o =>
            {
                BuyStonesWindow win = new BuyStonesWindow(obj);
                if (win.ShowDialog() == true)
                {

                }
                else
                {

                }
            }, null);
        }

        private void btnRefreshOrderList_Click(object sender, RoutedEventArgs e)
        {
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            Button btnBuy = sender as Button;
            SellStonesOrderUIModel stoneOrder = btnBuy.DataContext as SellStonesOrderUIModel;
            if (stoneOrder != null)
            {
                App.StoneOrderVMObject.AsyncLockStoneOrder(stoneOrder.OrderNumber);
            }
        }

    }
}
