using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for ViewPlayerLockStoneRecordWindow.xaml
    /// </summary>
    public partial class ViewPlayerLockStoneRecordWindow : Window
    {
        private ObservableCollection<LockSellStonesOrderUIModel> _list = new ObservableCollection<LockSellStonesOrderUIModel>();


        public ViewPlayerLockStoneRecordWindow()
        {
            InitializeComponent();
            this.datagrid.ItemsSource = _list;

            GlobalData.Client.GetLockedStonesOrderListCompleted += Client_GetLockedStonesOrderListCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.GetLockedStonesOrderListCompleted -= Client_GetLockedStonesOrderListCompleted;
        }

        void Client_GetLockedStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.LockSellStonesOrder[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MessageBox.Show("获取玩家锁定矿石记录失败。");
                return;
            }

            this._list.Clear();

            foreach (var item in e.Result)
            {
                this._list.Add(new LockSellStonesOrderUIModel(item));
            }

        }

        public void SetUser(string buyer)
        {
            this.Title += "  ----" + buyer;
            GlobalData.Client.GetLockedStonesOrderList(buyer);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HandleButtonContext_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            LockSellStonesOrderUIModel lockStoneObject = btn.DataContext as LockSellStonesOrderUIModel;
            if (lockStoneObject == null)
            {
                return;
            }

            StoneOrderResolveExceptionWindow win = new StoneOrderResolveExceptionWindow(lockStoneObject);
            if (win.ShowDialog() == true)
            {
                GlobalData.Client.GetLockedStonesOrderList(lockStoneObject.LockedByUserName);
            }
        }
    }
}
