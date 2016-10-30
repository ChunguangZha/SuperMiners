using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Utility;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for RouletteMyWinnedRecordsWindow.xaml
    /// </summary>
    public partial class RouletteMyWinnedRecordsWindow : Window
    {
        public RouletteMyWinnedRecordsWindow()
        {
            InitializeComponent();

            Search();
            this.datagrid.ItemsSource = App.GameRouletteVMObject.ListMyWinAwardRecords;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                RouletteWinnerRecordUIModel recordUIObject = btn.DataContext as RouletteWinnerRecordUIModel;
                if (recordUIObject == null)
                {
                    return;
                }

                RouletteWinAwardTakeWindow win = new RouletteWinAwardTakeWindow(recordUIObject.ParentObject);
                win.ShowDialog();
                if (win.IsOK == true)
                {
                    Search();
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("操作失败。原因为：" + exc.Message);
            }
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
            if (App.GameRouletteVMObject.ListMyWinAwardRecords.Count > 0)
            {
                this.numPageIndex.Value = this.numPageIndex.Value + 1;
                Search();
            }
        }

        private void Search()
        {
            int isGot = this.cmbIsGot.SelectedIndex - 1;
            int isPay = this.cmbIsPay.SelectedIndex - 1;

            MyDateTime beginWinTime = this.dpStartWinTime.ValueTime;
            MyDateTime endWinTime = this.dpEndWinTime.ValueTime;

            int pageIndex = (int)this.numPageIndex.Value;

            App.GameRouletteVMObject.AsyncGetMyselfAwardRecord(-1, beginWinTime, endWinTime,
                isGot, isPay, 30, pageIndex);
        }

    }
}
