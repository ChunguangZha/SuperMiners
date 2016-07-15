using SuperMinersCustomServiceSystem.Model;
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

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for EditPlayerWindow.xaml
    /// </summary>
    public partial class EditPlayerWindow : Window
    {
        PlayerInfoUIModel _player = null;
        public EditPlayerWindow(PlayerInfoUIModel player)
        {
            InitializeComponent();

            this._player = player;
            this.DataContext = _player;
        }

        private void btnEditAlipay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditExp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditRMB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
