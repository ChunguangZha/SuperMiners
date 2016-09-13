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

namespace SuperMinersCustomServiceSystem.View.Controls
{
    /// <summary>
    /// Interaction logic for PlayerManagerControl.xaml
    /// </summary>
    public partial class PlayerManagerControl : UserControl
    {
        public PlayerManagerControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.PlayerVMObject.AsyncGetListPlayers();

        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject
            };

            this.panelPlayersManager.SetBinding(GroupBox.DataContextProperty, bind);

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
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                    EditPlayerWindow win = new EditPlayerWindow(player);
                    win.ShowDialog();
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                    if (MessageBox.Show("删除玩家【" + player.UserName + "】？该操作不可恢复，请确认？", "请确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        App.PlayerVMObject.AsyncDeletePlayerInfos(new string[] { player.UserName });
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }

        void PlayerListContextMenu_ViewBuyMineRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (this.ViewPlayerBuyMineRecords != null)
                        {
                            this.ViewPlayerBuyMineRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewRechargeGoldCoinRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (this.ViewPlayerBuyGoldCoinRecords != null)
                        {
                            this.ViewPlayerBuyGoldCoinRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewBuyMinerRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerBuyMinerRecords != null)
                        {
                            this.ViewPlayerBuyMinerRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewSellStoneRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerSellStoneOrderRecords != null)
                        {
                            ViewPlayerSellStoneOrderRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewLockStoneRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerLockedStoneOrderRecords != null)
                        {
                            ViewPlayerLockedStoneOrderRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewBuyStoneRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerBuyStoneOrderRecords != null)
                        {
                            ViewPlayerBuyStoneOrderRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void PlayerListContextMenu_ViewAlipayPayRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerAlipayRechargeRecords != null)
                        {
                            ViewPlayerAlipayRechargeRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        public event Action<string> ViewPlayerSellStoneOrderRecords;
        public event Action<string> ViewPlayerLockedStoneOrderRecords;
        public event Action<string> ViewPlayerBuyStoneOrderRecords;
        public event Action<string> ViewPlayerBuyMinerRecords;
        public event Action<string> ViewPlayerBuyMineRecords;
        public event Action<string> ViewPlayerBuyGoldCoinRecords;
        public event Action<string> ViewPlayerAlipayRechargeRecords;
    }
}
