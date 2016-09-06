using MetaData.Trade;
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

namespace SuperMinersCustomServiceSystem.View.Controls.TradeSystem
{
    /// <summary>
    /// Interaction logic for WithdrawRMBActiveControl.xaml
    /// </summary>
    public partial class WithdrawRMBActiveControl : UserControl
    {
        public WithdrawRMBActiveControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.WithdrawRMBVMObject.ListActiveWithdrawRecords;
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            WithdrawRMBRecordUIModel record = btn.DataContext as WithdrawRMBRecordUIModel;
            if (record == null)
            {
                return;
            }

            WithdrawRMBPayWindow win = new WithdrawRMBPayWindow(record);
            if (win.ShowDialog() == true)
            {
                App.WithdrawRMBVMObject.RemoveRecordFromActiveRecords(record);
            }
        }
    }
}
