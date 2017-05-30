using MetaData.SystemConfig;
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
    /// Interaction logic for StoneFactoryStoneWithdrawWindow.xaml
    /// </summary>
    public partial class StoneFactoryStoneWithdrawWindow : Window
    {
        public int WithdrawStoneStack = 0;
        private int maxWithdrawStoneStack;

        public StoneFactoryStoneWithdrawWindow(int maxWithdrawStoneStack)
        {
            InitializeComponent();
            this.maxWithdrawStoneStack = maxWithdrawStoneStack;
            this.txtWithdrawableStone.Text = (maxWithdrawStoneStack * StoneFactoryConfig.StoneFactoryStone_Stack).ToString();
            this.numWithdrawRMB.Maximum = maxWithdrawStoneStack;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numWithdrawRMB.Value == 0)
            {
                MyMessageBox.ShowInfo("请输入要取出的矿石");
                return;
            }
            if (this.numWithdrawRMB.Value > maxWithdrawStoneStack)
            {
                MyMessageBox.ShowInfo("没有足够的矿石");
                return;
            }
            else
            {
                this.WithdrawStoneStack = (int)this.numWithdrawRMB.Value;
            }
            this.DialogResult = true;
        }
    }
}
