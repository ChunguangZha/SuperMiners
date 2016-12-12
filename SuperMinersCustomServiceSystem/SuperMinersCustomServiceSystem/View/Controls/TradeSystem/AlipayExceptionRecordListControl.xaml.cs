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
    /// Interaction logic for AlipayExceptionRecordListControl.xaml
    /// </summary>
    public partial class AlipayExceptionRecordListControl : UserControl
    {
        public AlipayExceptionRecordListControl()
        {
            InitializeComponent();
            this.dgRecords.ItemsSource = App.AlipayRechargeVMObject.ListExceptionAlipayRecords;
        }

        private void btnHandleExceptionAlipayRecord_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            AlipayRechargeRecordUIModel alipayrecord = btn.DataContext as AlipayRechargeRecordUIModel;
            if (alipayrecord == null)
            {
                return;
            }

            HandleExceptionAlipayRecordWindow win = new HandleExceptionAlipayRecordWindow(alipayrecord);
            if (win.ShowDialog() == true)
            {
                Refresh();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            App.AlipayRechargeVMObject.AsyncGetAllExceptionAlipayRechargeRecords();
        }
    }
}
