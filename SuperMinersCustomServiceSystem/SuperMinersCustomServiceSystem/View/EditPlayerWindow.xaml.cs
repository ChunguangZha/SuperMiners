using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.View;
using SuperMinersCustomServiceSystem.View.Windows;
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

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for EditPlayerWindow.xaml
    /// </summary>
    public partial class EditPlayerWindow : Window
    {
        PlayerInfoUIModel _player = null;
        public EditPlayerWindow(PlayerInfoUIModel player)
        {
            InitializeComponent();

            this._player = player;
            this.DataContext = _player;
        }

        private void btnEditAlipay_Click(object sender, RoutedEventArgs e)
        {
            EditPlayerAlipayWindow win = new EditPlayerAlipayWindow(this._player.UserName, this._player.Alipay, this._player.AlipayRealName);
            if (win.ShowDialog() == true)
            {
                this._player.Alipay = win.AlipayAccount;
                this._player.AlipayRealName = win.AlipayRealName;
            }
        }

        private void btnEditExp_Click(object sender, RoutedEventArgs e)
        {
            EditPlayerExpWindow win = new EditPlayerExpWindow(this._player.UserName, this._player.Exp);
            if (win.ShowDialog() == true)
            {
                this._player.SetExp(win.ExpChanged);
            }
        }

        private void btnEditRMB_Click(object sender, RoutedEventArgs e)
        {
            EditPlayerRMBWindow win = new EditPlayerRMBWindow(this._player.UserName, this._player.RMB);
            if (win.ShowDialog() == true)
            {
                this._player.SetRMB(win.ChangedRMB);
            }
        }

        private void btnEditGoldCoin_Click(object sender, RoutedEventArgs e)
        {
            EditPlayerGoldCoinWindow win = new EditPlayerGoldCoinWindow(this._player.UserName, this._player.GoldCoin);
            if (win.ShowDialog() == true)
            {
                this._player.SetGoldCoin(win.ChangedGoldCoin);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            InputActionPasswordWindow win = new InputActionPasswordWindow();
            if (win.ShowDialog() == true)
            {
                if (MessageBox.Show("确定要修改玩家信息？此操作不可更改。", "确认修改", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    App.PlayerVMObject.AsyncChangePlayerInfo(this._player);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
