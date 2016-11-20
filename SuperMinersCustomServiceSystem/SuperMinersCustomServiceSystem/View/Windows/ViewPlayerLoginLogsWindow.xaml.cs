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
    /// Interaction logic for ViewPlayerLoginLogsWindow.xaml
    /// </summary>
    public partial class ViewPlayerLoginLogsWindow : Window
    {
        public ViewPlayerLoginLogsWindow(int userID)
        {
            InitializeComponent();

            //App.PlayerVMObject.AsyncGetUserLoginLog(userID);
            //this.dataGrid.ItemsSource = App.PlayerVMObject.ListPlayerLoginInfo;
        }
    }
}
