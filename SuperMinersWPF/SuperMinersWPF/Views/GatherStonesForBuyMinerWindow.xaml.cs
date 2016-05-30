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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for GatherStonesForBuyMinerWindow.xaml
    /// </summary>
    public partial class GatherStonesForBuyMinerWindow : Window
    {
        public GatherStonesForBuyMinerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtAllOutputStones.Text = GlobalData.CurrentUser.TempOutputStones.ToString();
            this.txtGatherableStones.Text = ((int)GlobalData.CurrentUser.TempOutputStones).ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            App.UserVMObject.AsyncGatherStones((int)GlobalData.CurrentUser.TempOutputStones);
            this.DialogResult = true;
        }

        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            App.UserVMObject.AsyncGatherStones(0);
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
