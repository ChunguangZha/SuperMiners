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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for StonesMarketUserControl.xaml
    /// </summary>
    public partial class StonesMarketUserControl : UserControl
    {
        public StonesMarketUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.listboxSellOrders.ItemsSource = App.StoneOrderVMObject.AllNotFinishStonesOrder;
            App.StoneOrderVMObject.AsyncGetNotFinishedStonesOrder();
        }

        private void numBuyStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
