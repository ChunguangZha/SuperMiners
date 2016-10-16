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
    /// Interaction logic for EditPlayerMinerCountWindow.xaml
    /// </summary>
    public partial class EditPlayerMinerCountWindow : Window
    {
        public int ChangedMinerCount;

        public EditPlayerMinerCountWindow(string userName, decimal currentMiners)
        {
            InitializeComponent();
            this.txtUserName.Text = userName;
            this.txtCurrentMinerCount.Text = currentMiners.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ChangedMinerCount = (int)this.numChangedMinerCount.Value;
            this.DialogResult = true;
        }
    }
}
