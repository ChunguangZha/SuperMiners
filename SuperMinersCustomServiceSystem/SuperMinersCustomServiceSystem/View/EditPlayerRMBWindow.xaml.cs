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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditPlayerRMBWindow : Window
    {
        string _userName;
        decimal _currentRMB;

        public bool IsOK { get; private set; }
        public decimal ChangedRMB { get; private set; }

        public EditPlayerRMBWindow(string userName, decimal currentRMB)
        {
            InitializeComponent();
            this._userName = userName;
            this._currentRMB = currentRMB;

            this.txtUserName.Text = userName;
            this.txtCurrentRMB.Text = currentRMB.ToString();
            this.txtRMBChanged.Value = (double)currentRMB;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ChangedRMB = (decimal)this.txtRMBChanged.Value;
            this.IsOK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.IsOK = false;
            this.Close();
        }

        private void chkChangeType_Checked(object sender, RoutedEventArgs e)
        {
            if (this.panelInCharge != null)
            {
                this.panelInCharge.IsEnabled = true;
                this.txtRMBChanged.IsReadOnly = true;
            }
        }

        private void chkChangeType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.panelInCharge != null)
            {
                this.panelInCharge.IsEnabled = false;
                this.txtRMBChanged.IsReadOnly = false;
            }
        }

        private void NumericTextBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ChangedRMB = this._currentRMB + (((decimal)this.numYuanValue.Value) * GlobalData.GameConfig.Yuan_RMB);
            this.txtRMBChanged.Value = (double)this.ChangedRMB;
        }
    }
}
