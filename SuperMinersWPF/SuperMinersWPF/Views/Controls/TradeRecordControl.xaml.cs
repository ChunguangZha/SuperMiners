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
    /// Interaction logic for TradeRecordControl.xaml
    /// </summary>
    public partial class TradeRecordControl : UserControl
    {
        public TradeRecordControl()
        {
            InitializeComponent();
        }

        private void cmbTradeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.controlAlipayHistory == null || this.controlGoldCoinHistory == null)
            {
                return;
            }
            HideAllControls();

            switch (this.cmbTradeType.SelectedIndex)
            {
                case 0:
                    this.controlAlipayHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 1:
                    this.controlGoldCoinHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 2:
                    this.controlMinerHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 3:
                    this.controlMineHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 4:
                    this.controlWithdrawHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 5:
                    this.controlStoneNotFinishedRecord.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 6:
                    this.controlStoneBuyHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 7:
                    this.controlStoneSellHistory.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void HideAllControls()
        {
            this.controlAlipayHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlGoldCoinHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMinerHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMineHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlWithdrawHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneNotFinishedRecord.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneBuyHistory.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStoneSellHistory.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
