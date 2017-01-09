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
    /// Interaction logic for KLineControl.xaml
    /// </summary>
    public partial class KLineControl : UserControl
    {
        public KLineControl()
        {
            InitializeComponent();
        }

        private void rbtnSwitch_Checked(object sender, RoutedEventArgs e)
        {
            this.rbtnSwitch.Content = "日线图";
            this.klineRealTimeControl.Visibility = System.Windows.Visibility.Collapsed;
            this.klineDayControl.Visibility = System.Windows.Visibility.Visible;
        }

        private void rbtnSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            this.rbtnSwitch.Content = "分时图";
            this.klineRealTimeControl.Visibility = System.Windows.Visibility.Visible;
            this.klineDayControl.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
