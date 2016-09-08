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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for EditPlayerGoldCoinWindow.xaml
    /// </summary>
    public partial class EditPlayerGoldCoinWindow : Window
    {
        decimal _oldGoldCoin;

        public decimal ChangedGoldCoin;

        public EditPlayerGoldCoinWindow(string userName, decimal goldCoin)
        {
            InitializeComponent();
            this.txtUserName.Text = userName;
            this._oldGoldCoin = goldCoin;
            this.txtCurrentGoldCoin.Text = goldCoin.ToString("f2");
        }

        private void numGoldCoinValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.cmbOper == null || this.numGoldCoinChanged == null)
            {
                return;
            }

            if (this.cmbOper.SelectedIndex == 0)
            {
                this.numGoldCoinChanged.Value = (double)this._oldGoldCoin + numGoldCoinValue.Value;
            }
            else
            {
                this.numGoldCoinChanged.Value = (double)this._oldGoldCoin - numGoldCoinValue.Value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ChangedGoldCoin = (decimal)this.numGoldCoinChanged.Value;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cmbOper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbOper == null || this.numGoldCoinChanged == null)
            {
                return;
            }

            if (this.cmbOper.SelectedIndex == 0)
            {
                this.numGoldCoinChanged.Value = (double)this._oldGoldCoin + numGoldCoinValue.Value;
            }
            else
            {
                this.numGoldCoinChanged.Value = (double)this._oldGoldCoin - numGoldCoinValue.Value;
            }
        }
    }
}
