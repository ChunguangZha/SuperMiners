using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.View;
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
        private System.Threading.SynchronizationContext _syn;
        public bool IsBackToLogin = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Title += "   --" + GlobalData.CurrentAdmin.UserName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._syn = System.Threading.SynchronizationContext.Current;
            CreateTreeView();
            BindUI();

            GlobalData.Client.OnKickoutByUser += Client_OnKickoutByUser;
        }
        
        private void CreateTreeView()
        {
            CreateTradeSystemTreeView();
        }

        private void CreateTradeSystemTreeView()
        {
            CreateTradeSystemTreeViewItem(App.MinerTradeVMObject);
            CreateTradeSystemTreeViewItem(App.MineTradeVMObject);
            CreateTradeSystemTreeViewItem(App.GoldCoinTradeVMObject);
            CreateTradeSystemTreeViewItem(App.StoneTradeVMObject);
            CreateTradeSystemTreeViewItem(App.WithdrawRMBVMObject);
            CreateTradeSystemTreeViewItem(App.AlipayRechargeVMObject);
        }

        public void CreateTradeSystemTreeViewItem(object ItemDataContext)
        {
            TreeViewItem tvItem = new TreeViewItem();
            tvItem.SetResourceReference(TreeViewItem.StyleProperty, "TVItemL2Style");

            Binding bind = new Binding()
            {
                Source = ItemDataContext
            };
            tvItem.SetBinding(TreeViewItem.DataContextProperty, bind);

            bind = new Binding("MenuHeader");
            tvItem.SetBinding(TreeViewItem.HeaderProperty, bind);

            tvItem.Items.Add(new TreeViewItem()
            {
                Header = "实时交易",
            });
            tvItem.Items.Add(new TreeViewItem()
            {
                Header = "交易记录",
            });

            this.tvL1TradeSystem.Items.Add(tvItem);
        }


        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject
            };

            this.panelPlayersManager.SetBinding(GroupBox.DataContextProperty, bind);

            App.PlayerVMObject.AsyncGetListPlayers();

            bind = new Binding()
            {
                Source = App.NoticeVMObject.ListAllNotices
            };
            this.datagridNotices.SetBinding(DataGrid.ItemsSourceProperty, bind);
            App.NoticeVMObject.AsyncGetAllNotice();
        }

        void Client_OnKickoutByUser()
        {
            this._syn.Post(s =>
            {
                MessageBox.Show("您的账户在其它电脑登录，如非本人操作，请及时修改密码。");
                this.IsBackToLogin = true;
                this.Close();
            }, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.LogoutAdmin();
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

        private void btnCreateNotices_Click(object sender, RoutedEventArgs e)
        {
            AddNoticeWindow win = new AddNoticeWindow();
            win.ShowDialog();
        }

        private void btnDeleteNotices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnclearAllNotices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelectAllNotices_Click(object sender, RoutedEventArgs e)
        {

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
                        ViewPlayerBuyMineRecordWindow win = new ViewPlayerBuyMineRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                        ViewPlayerGoldCoinRechargeRecordWindow win = new ViewPlayerGoldCoinRechargeRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                        ViewPlayerBuyMinerRecordWindow win = new ViewPlayerBuyMinerRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                        ViewPlayerSellStoneRecordWindow win = new ViewPlayerSellStoneRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                        ViewPlayerLockStoneRecordWindow win = new ViewPlayerLockStoneRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                        ViewPlayerBuyStoneRecordWindow win = new ViewPlayerBuyStoneRecordWindow();
                        win.Show();
                        win.SetUser(player.UserName);
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
                //if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                //{
                //    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                //    if (player != null)
                //    {
                //        ViewPlayerBuyStoneRecordWindow win = new ViewPlayerBuyStoneRecordWindow();
                //        win.Show();
                //        win.SetUser(player.UserName);
                //    }
                //}
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}
