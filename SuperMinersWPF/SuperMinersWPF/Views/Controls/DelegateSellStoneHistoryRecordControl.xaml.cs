using MetaData;
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
    /// Interaction logic for DelegateSellStoneHistoryRecordControl.xaml
    /// </summary>
    public partial class DelegateSellStoneHistoryRecordControl : UserControl
    {
        public DelegateSellStoneHistoryRecordControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.StackStoneVMObject.AllFinishedSellOrders;
            this.dpStartFinishedTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now.AddDays(-7));
            this.dpEndFinishedTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now);
        }

        private void Search()
        {
            string orderNumber = this.txtOrderNumber.Text.Trim();

            MyDateTime beginFinishedTime = this.dpStartFinishedTime.ValueTime;
            MyDateTime endFinishedTime = this.dpEndFinishedTime.ValueTime;
            endFinishedTime.Hour = 23;
            endFinishedTime.Minute = 59;
            endFinishedTime.Second = 59;

            int pageIndex = (int)this.numPageIndex.Value;

            App.StackStoneVMObject.AsyncGetAllFinishedSellOrders(beginFinishedTime, endFinishedTime, GlobalData.PageItemsCount, pageIndex);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.numPageIndex.Value > 1)
            {
                this.numPageIndex.Value = this.numPageIndex.Value - 1;
                Search();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (App.StoneOrderVMObject.ListMyBuyStoneHistoryOrders.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
