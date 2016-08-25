using MetaData.Trade;
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
        private ObservableCollection<LockSellStonesOrder> _list = new ObservableCollection<LockSellStonesOrder>();


        public ViewPlayerLockStoneRecordWindow()
        {
            InitializeComponent();
            this.datagrid.ItemsSource = _list;

            GlobalData.Client.GetLockedStonesOrderListCompleted += Client_GetLockedStonesOrderListCompleted;
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
                this._list.Add(item);
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
