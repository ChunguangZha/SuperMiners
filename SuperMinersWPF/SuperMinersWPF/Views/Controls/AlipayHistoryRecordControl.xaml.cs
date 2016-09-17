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
    /// Interaction logic for AlipayHistoryRecordControl.xaml
    /// </summary>
    public partial class AlipayHistoryRecordControl : UserControl
    {
        public AlipayHistoryRecordControl()
        {
            InitializeComponent();
            this.datagrid.ItemsSource = App.TradeHistoryVMObject.ListAllAlipayRecords;
            this.dpStartPayTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now.AddDays(-7));
            this.dpEndPayTime.ValueTime = MyDateTime.FromDateTime(DateTime.Now);
        }

        private void Search()
        {
            string orderNumber = this.txtOrderNumber.Text.Trim();
            string alipayOrderNumber = this.txtAlipayOrderNumber.Text.Trim();
            string buyerEmail = this.txtBuyerEmail.Text.Trim();
            string playerUserName = GlobalData.CurrentUser.UserName;
            MyDateTime beginPayTime = this.dpStartPayTime.ValueTime;
            MyDateTime endPayTime = this.dpEndPayTime.ValueTime;
            endPayTime.Hour = 23;
            endPayTime.Minute = 59;
            endPayTime.Second = 59;

            int pageIndex = (int)this.numPageIndex.Value;

            App.TradeHistoryVMObject.AsyncGetAllAlipayRechargeRecords(orderNumber, alipayOrderNumber, buyerEmail, playerUserName, beginPayTime, endPayTime, GlobalData.PageItemsCount, pageIndex);
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
            if (App.TradeHistoryVMObject.ListAllAlipayRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
