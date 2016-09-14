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
    /// Interaction logic for WithdrawRMBHistoryRecordControl.xaml
    /// </summary>
    public partial class WithdrawRMBHistoryRecordControl : UserControl
    {
        public WithdrawRMBHistoryRecordControl()
        {
            InitializeComponent();
            this.dgRecords.ItemsSource = App.TradeHistoryVMObject.ListHistoryWithdrawRecords;
        }

        private void cmbIsPay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbIsPay == null || this.panelAdminPayed == null)
            {
                return;
            }

            if (this.cmbIsPay.SelectedIndex <= 0)
            {
                this.panelAdminPayed.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.panelAdminPayed.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Search()
        {
            bool isPayed = this.cmbIsPay.SelectedIndex == 1;
            string playerUserName = GlobalData.CurrentUser.UserName;
            MyDateTime beginCreateTime = this.dpStartCreateTime.ValueTime;
            MyDateTime endCreateTime = this.dpEndCreateTime.ValueTime;

            string adminUserName = "";
            MyDateTime beginPayTime = null;
            MyDateTime endPayTime = null;
            if (isPayed)
            {
                adminUserName = this.txtAdminUserName.Text;
                beginPayTime = this.dpStartPayTime.ValueTime;
                endPayTime = this.dpEndPayTime.ValueTime;
            }

            int pageIndex = (int)this.numPageIndex.Value;

            App.TradeHistoryVMObject.AsyncGetWithdrawRMBRecordList(isPayed, playerUserName, beginCreateTime, endCreateTime,
                adminUserName, beginPayTime, endPayTime, 30, pageIndex);
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
            if (App.TradeHistoryVMObject.ListHistoryWithdrawRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
