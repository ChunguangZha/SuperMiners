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
    /// Interaction logic for ViewPlayerBuyStoneRecordWindow.xaml
    /// </summary>
    public partial class ViewPlayerBuyStoneRecordWindow : Window
    {
        private ObservableCollection<BuyStonesOrderUIModel> _list = new ObservableCollection<BuyStonesOrderUIModel>();

        public ViewPlayerBuyStoneRecordWindow()
        {
            InitializeComponent();
            this.datagrid.ItemsSource = _list;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.GetBuyStonesOrderListCompleted += Client_GetBuyStonesOrderListCompleted;
        }

        void Client_GetBuyStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.BuyStonesOrder[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MessageBox.Show("获取玩家购买矿石记录失败。");
                return;
            }

            this._list.Clear();

            foreach (var item in e.Result)
            {
                this._list.Add(new BuyStonesOrderUIModel(item));
            }
        }

        public void SetUser(string buyer)
        {
            this.Title += "  ----" + buyer;
            GlobalData.Client.GetBuyStonesOrderList(buyer, new MetaData.MyDateTime(), new MetaData.MyDateTime());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
