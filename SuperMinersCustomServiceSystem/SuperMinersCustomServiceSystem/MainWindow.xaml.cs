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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject.ListFilteredPlayers
            };

            this.datagridPlayerInfos.SetBinding(DataGrid.ItemsSourceProperty, bind);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = "";
            this.txtAlipayAccount.Text = "";
            this.txtReferrerUserName.Text = "";
            this.cmbLocked.SelectedIndex = 0;
            this.cmbOnline.SelectedIndex = 0;
            App.PlayerVMObject.AsyncGetListPlayers();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.PlayerVMObject.SearchPlayers(this.txtUserName.Text.Trim(), this.txtAlipayAccount.Text.Trim(), this.txtReferrerUserName.Text.Trim(), this.cmbLocked.SelectedIndex, this.cmbOnline.SelectedIndex);
            this.txtCount.Text = App.PlayerVMObject.ListFilteredPlayers.Count.ToString();
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            if (App.PlayerVMObject.GetCheckPlayersCount() != 1)
            {
                MessageBox.Show("请选择一个玩家进行修改。");
                return;
            }

            PlayerInfoUIModel player = App.PlayerVMObject.GetFirstCheckedPlayer();
            if (player == null)
            {
                MessageBox.Show("请选择一个玩家进行修改。");
                return;
            }
        }

    }
}
