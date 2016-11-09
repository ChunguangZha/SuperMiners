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
        int gatherableStoneOutputInt;
        float gatherableStoneOutputFloat;

        public GatherStonesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            decimal a = GlobalData.CurrentUser.TempOutputStones * 100;
            gatherableStoneOutputFloat = ((int)a) / 100f;

            this.txtAllOutputStones.Text = gatherableStoneOutputFloat.ToString("0.00");
            gatherableStoneOutputInt = (int)GlobalData.CurrentUser.TempOutputStones;
            this.txtGatherableStones.Text = gatherableStoneOutputInt.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            App.UserVMObject.AsyncGatherStones(gatherableStoneOutputInt);
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
