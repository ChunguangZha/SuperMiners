using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.View.Windows;
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
    /// Interaction logic for RouletteActiveWinRealAwardListControl.xaml
    /// </summary>
    public partial class RouletteActiveWinRealAwardListControl : UserControl
    {
        public RouletteActiveWinRealAwardListControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.GameRouletteVMObject.ListNotPayRouletteWinnerRecords;
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RouletteWinnerRecordUIModel record = btn.DataContext as RouletteWinnerRecordUIModel;
            if (record == null)
            {
                return;
            }

            GameRouletteWinAwardWindow win = new GameRouletteWinAwardWindow(record);
            if (win.ShowDialog() == true)
            {

            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            App.GameRouletteVMObject.AsyncGetNotPayWinAwardRecords();
        }
    }
}
