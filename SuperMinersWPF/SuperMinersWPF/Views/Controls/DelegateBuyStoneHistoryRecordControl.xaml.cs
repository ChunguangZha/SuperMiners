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
    /// Interaction logic for DelegateBuyStoneHistoryRecordControl.xaml
    /// </summary>
    public partial class DelegateBuyStoneHistoryRecordControl : UserControl
    {
        public DelegateBuyStoneHistoryRecordControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.StackStoneVMObject.AllFinishedBuyOrders;
            this.dpStartCreateTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now.AddDays(-7));
            this.dpEndCreateTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now);
        }

        private void Search()
        {
            string orderNumber = this.txtOrderNumber.Text.Trim();

            MyDateTime beginCreateTime = this.dpStartCreateTime.ValueTime;
            MyDateTime endCreateTime = this.dpEndCreateTime.ValueTime;
            endCreateTime.Hour = 23;
            endCreateTime.Minute = 59;
            endCreateTime.Second = 59;

            int pageIndex = (int)this.numPageIndex.Value;

            App.StackStoneVMObject.AsyncGetAllFinishedBuyOrders(beginCreateTime, endCreateTime, GlobalData.PageItemsCount, pageIndex);
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
