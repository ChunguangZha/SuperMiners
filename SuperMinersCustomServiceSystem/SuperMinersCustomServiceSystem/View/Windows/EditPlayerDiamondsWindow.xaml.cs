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
    /// Interaction logic for EditPlayerDiamondsWindow.xaml
    /// </summary>
    public partial class EditPlayerDiamondsWindow : Window
    {
        string _userName;
        decimal _currentStackDiamonds;

        public bool IsOK { get; private set; }
        public decimal ChangedStackDiamonds { get; private set; }
        public decimal ChangedFreezingDiamonds { get; private set; }

        public EditPlayerDiamondsWindow(string userName, decimal currentStackDiamonds, decimal currentFreezingDiamonds)
        {
            InitializeComponent();
            this._userName = userName;
            this._currentStackDiamonds = currentStackDiamonds;

            this.txtUserName.Text = userName;
            this.txtCurrentStackDiamonds.Text = currentStackDiamonds.ToString();
            this.txtStackDiamondsChanged.Value = (double)currentStackDiamonds;

            this.txtCurrentFreezingDiamonds.Text = currentFreezingDiamonds.ToString();
            this.txtFreezingDiamondsChanged.Value = (double)currentFreezingDiamonds;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ChangedStackDiamonds = (decimal)this.txtStackDiamondsChanged.Value;
            this.ChangedFreezingDiamonds = (decimal)this.txtFreezingDiamondsChanged.Value;

            this.IsOK = true;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.IsOK = false;
            this.DialogResult = false;
        }
        
    }
}
