using SuperMinersWPF.Models;
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
    /// Interaction logic for DigStoneAreaUserControl.xaml
    /// </summary>
    public partial class DigStoneAreaUserControl : UserControl
    {
        public DigStoneAreaUserControl()
        {
            InitializeComponent();

            this.gridActionMessage.DataContext = App.MessageVMObject;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.IsLogined)
            {
                return;
            }

            App.MessageVMObject.GetPlayerActionCompleted += MessageVMObject_GetPlayerActionCompleted;
        }

        void MessageVMObject_GetPlayerActionCompleted(object sender, EventArgs e)
        {
            if (App.MessageVMObject.ListPlayerActionLog.Count > 0)
            {
                PlayerActionLogUIModel lastActionLog = App.MessageVMObject.ListPlayerActionLog[App.MessageVMObject.ListPlayerActionLog.Count - 1];
                //this.listboxActionMessage.ScrollIntoView(lastActionLog);
            }
        }

    }
}
