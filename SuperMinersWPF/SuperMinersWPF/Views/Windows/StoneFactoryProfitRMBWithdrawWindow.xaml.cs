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
    /// Interaction logic for StoneFactoryProfitRMBWithdrawWindow.xaml
    /// </summary>
    public partial class StoneFactoryProfitRMBWithdrawWindow : Window
    {
        public decimal WithdrawRMB = 0;
        private decimal maxWithdrawRMB;

        public StoneFactoryProfitRMBWithdrawWindow(decimal maxWithdrawRMB)
        {
            InitializeComponent();
            this.maxWithdrawRMB = maxWithdrawRMB;
            this.txtWithdrawableRMB.Text = maxWithdrawRMB.ToString("0.00");
            this.numWithdrawRMB.Maximum = (double)maxWithdrawRMB;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numWithdrawRMB.Value == 0)
            {
                MyMessageBox.ShowInfo("请输入要提取的金额");
                return;
            }
            if ((decimal)this.numWithdrawRMB.Value > maxWithdrawRMB)
            {
                MyMessageBox.ShowInfo("没有足够的灵币");
                return;
            }
            else
            {
                this.WithdrawRMB = (decimal)this.numWithdrawRMB.Value;
            }
            this.DialogResult = true;
        }
    }
}
