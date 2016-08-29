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
    /// Interaction logic for ViewPlayerAlipayPayRecordWindow.xaml
    /// </summary>
    public partial class ViewPlayerAlipayPayRecordWindow : Window
    {
        public ViewPlayerAlipayPayRecordWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        public void SetUser(string buyer)
        {
            this.Title += "  ----" + buyer;

            //if(GlobalData.Client.getmi
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
