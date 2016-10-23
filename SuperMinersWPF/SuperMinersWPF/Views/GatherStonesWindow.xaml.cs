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
    /// Interaction logic for GatherStonesWindow.xaml
    /// </summary>
    public partial class GatherStonesWindow : Window
    {
        int gatherableStoneOutput;

        public GatherStonesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtAllOutputStones.Text = GlobalData.CurrentUser.TempOutputStones.ToString("0.00");
            gatherableStoneOutput = (int)GlobalData.CurrentUser.TempOutputStones;
            this.txtGatherableStones.Text = gatherableStoneOutput.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            App.UserVMObject.AsyncGatherStones(gatherableStoneOutput);
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
