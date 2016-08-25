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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for StoneOrderResolveException.xaml
    /// </summary>
    public partial class StoneOrderResolveException : Window
    {
        private LockSellStonesOrderUIModel _order = null;

        public StoneOrderResolveException(LockSellStonesOrderUIModel order)
        {
            InitializeComponent();
            this._order = order;
            this.DataContext = this._order;
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认订单支付成功，该操作不可恢复！", "确认订单成功", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

            }
            else
            {

            }
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认订单支付失败，该操作不可恢复！", "确认订单失败", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

            }
            else
            {

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
