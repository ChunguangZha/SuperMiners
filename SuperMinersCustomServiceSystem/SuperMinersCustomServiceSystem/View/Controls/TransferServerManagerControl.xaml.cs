using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TransferServerManagerControl.xaml
    /// </summary>
    public partial class TransferServerManagerControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public TransferServerManagerControl()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.PlayerVMObject == null || GlobalData.CurrentAdmin == null)
            {
                return;
            }

            App.PlayerVMObject.AsyncGetAllTransferPlayerRecords();
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject.ListFilterPlayerTransferRecords
            };

            this.datagridPlayerInfos.SetBinding(DataGrid.ItemsSourceProperty, bind);

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = "";
            this.cmbTransferState.SelectedIndex = 1;
            App.PlayerVMObject.AsyncGetAllTransferPlayerRecords();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.PlayerVMObject.SearchTransferPlayers(this.txtUserName.Text.Trim(), cmbTransferState.SelectedIndex);
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is OldPlayerTransferRegisterInfoUIModel)
                {
                    OldPlayerTransferRegisterInfoUIModel player = this.datagridPlayerInfos.SelectedItem as OldPlayerTransferRegisterInfoUIModel;

                    GlobalData.Client.GetPlayerCompleted += Client_GetPlayerCompleted;
                    GlobalData.Client.GetPlayer(player.UserName);
                }
            }
            catch (Exception exc)
            {

            }
        }

        void Client_GetPlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SuperMinersServerApplication.Model.PlayerInfoLoginWrap> e)
        {
            try
            {
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取用户信息失败。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("获取用户信息失败。");
                    return;
                }

                this._syn.Post(o =>
                {
                    EditPlayerWindow win = new EditPlayerWindow(new PlayerInfoUIModel(e.Result));
                    win.ShowDialog();

                }, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
                            this.ViewPlayerBuyMineRecords(player.UserLoginName);
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
                            this.ViewPlayerBuyGoldCoinRecords(player.UserLoginName);
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
                            this.ViewPlayerBuyMinerRecords(player.UserLoginName);
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
                            ViewPlayerSellStoneOrderRecords(player.UserLoginName);
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
                            ViewPlayerLockedStoneOrderRecords(player.UserLoginName);
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
                            ViewPlayerBuyStoneOrderRecords(player.UserLoginName);
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
                            ViewPlayerAlipayRechargeRecords(player.UserLoginName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void PlayerListContextMenu_ViewRMBWithdrawRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerRMBWithdrawRecords != null)
                        {
                            ViewPlayerRMBWithdrawRecords(player.UserLoginName);
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
        public event Action<string> ViewPlayerRMBWithdrawRecords;
    }
}
