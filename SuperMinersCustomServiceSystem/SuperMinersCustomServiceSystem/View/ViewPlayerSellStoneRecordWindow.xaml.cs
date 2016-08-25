using MetaData;
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
    /// Interaction logic for ViewPlayerSellStoneRecordWindow.xaml
    /// </summary>
    public partial class ViewPlayerSellStoneRecordWindow : Window
    {
        private ObservableCollection<SellStonesOrderUIModel> _list = new ObservableCollection<SellStonesOrderUIModel>();

        public ViewPlayerSellStoneRecordWindow()
        {
            InitializeComponent();

            this.datagrid.ItemsSource = _list;
            GlobalData.Client.GetSellStonesOrderListCompleted += Client_GetSellStonesOrderListCompleted;
        }

        void Client_GetSellStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.SellStonesOrder[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MessageBox.Show("获取玩家出售矿石记录失败。");
                return;
            }

            this._list.Clear();

            foreach (var item in e.Result)
            {
                this._list.Add(new SellStonesOrderUIModel(item));
            }

        }

        public void SetUser(string seller)
        {
            this.Title += "  ----" + seller;
            GlobalData.Client.GetSellStonesOrderList(seller, new MyDateTime(), new MyDateTime());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
