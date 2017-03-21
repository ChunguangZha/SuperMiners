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

namespace SuperMinersCustomServiceSystem.View.Controls.TradeSystem
{
    /// <summary>
    /// Interaction logic for StoneDelegateBuyTradeHistoryRecordControl.xaml
    /// </summary>
    public partial class StoneDelegateBuyTradeHistoryRecordControl : UserControl
    {
        public StoneDelegateBuyTradeHistoryRecordControl()
        {
            InitializeComponent();

            this.DataContext = App.StoneDelegateTradeVMObject;
        }

        public void SetBuyerUserName(string buyerUserName)
        {
            this.txtBuyerUserName.Text = buyerUserName;
            Search();
        }

        private void Search()
        {
            string buyerUserName = this.txtBuyerUserName.Text.Trim();

            MyDateTime beginPayTime = this.dpStartPayTime.ValueTime;
            MyDateTime endPayTime = this.dpEndPayTime.ValueTime;

            int pageIndex = (int)this.numPageIndex.Value;

            App.StoneDelegateTradeVMObject.AsyncGetStoneDelegateBuyOrderInfo(buyerUserName, beginPayTime, endPayTime, GlobalData.PageItemsCount, pageIndex);
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
            if (App.StoneDelegateTradeVMObject.ListStoneDelegateBuyOrders.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
