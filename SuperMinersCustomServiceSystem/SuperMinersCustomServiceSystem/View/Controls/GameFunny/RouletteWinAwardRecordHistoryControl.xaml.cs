using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Controls.GameFunny
{
    /// <summary>
    /// Interaction logic for RouletteWinAwardRecordHistoryControl.xaml
    /// </summary>
    public partial class RouletteWinAwardRecordHistoryControl : UserControl
    {
        public RouletteWinAwardRecordHistoryControl()
        {
            InitializeComponent();

            this.datagrid.ItemsSource = App.GameRouletteVMObject.ListAllPayRouletteWinnerRecords;
            this.cmbAwardItems.ItemsSource = App.GameRouletteVMObject.ListToComboxRouletteAwardItems;
        }

        public void SetUserName(string userName)
        {
            this.txtUserName.Text = userName;
            Search();
        }

        private void Search()
        {
            int isGot = this.cmbIsGot.SelectedIndex - 1;
            int isPay = this.cmbIsPay.SelectedIndex - 1;
            string playerUserName = this.txtUserName.Text;
            var selectedAwardItem = this.cmbAwardItems.SelectedItem;
            if(selectedAwardItem==null)
            {
                MyMessageBox.ShowInfo("请选择中奖信息");
                return;
            }

            var awardItem = selectedAwardItem as RouletteAwardItemUIModel;

            MyDateTime beginWinTime = this.dpStartWinTime.ValueTime;
            MyDateTime endWinTime = this.dpEndWinTime.ValueTime;

            int pageIndex = (int)this.numPageIndex.Value;

            App.GameRouletteVMObject.AsyncGetAllPayWinAwardRecords(playerUserName, awardItem.ID, beginWinTime, endWinTime,
                isGot, isPay, 30, pageIndex);
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
            if (App.WithdrawRMBVMObject.ListHistoryWithdrawRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }
    }
}
