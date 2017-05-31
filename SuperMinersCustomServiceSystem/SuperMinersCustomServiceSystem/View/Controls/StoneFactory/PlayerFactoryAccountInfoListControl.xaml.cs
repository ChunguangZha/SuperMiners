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

namespace SuperMinersCustomServiceSystem.View.Controls.StoneFactory
{
    /// <summary>
    /// Interaction logic for PlayerFactoryAccountInfoListControl.xaml
    /// </summary>
    public partial class PlayerFactoryAccountInfoListControl : UserControl
    {
        public PlayerFactoryAccountInfoListControl()
        {
            InitializeComponent();

            this.DataContext = App.StoneFactoryVMObject;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.StoneFactoryVMObject.FiltePlayerFactoryAccount(this.txtUserName.Text.Trim(), this.cmbUserGroup.SelectedIndex);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = "";
            this.cmbUserGroup.SelectedIndex = 0;

            App.StoneFactoryVMObject.AsyncGetAllPlayerStoneFactoryAccountInfos();
        }
    }
}
