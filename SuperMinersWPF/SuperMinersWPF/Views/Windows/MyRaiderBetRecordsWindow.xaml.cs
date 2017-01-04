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
    /// Interaction logic for MyRaiderBetRecordsWindow.xaml
    /// </summary>
    public partial class MyRaiderBetRecordsWindow : Window
    {
        public MyRaiderBetRecordsWindow()
        {
            InitializeComponent();
            this.datagrid.ItemsSource = App.GameRaiderofLostArkVMObject.ListPlayerHistoryBetRecords;
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
            int pageIndex = (int)this.numPageIndex.Value;

            App.GameRaiderofLostArkVMObject.AsyncGetPlayerHistoryBetRecords(GlobalData.PageItemsCount, pageIndex);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

    }
}
