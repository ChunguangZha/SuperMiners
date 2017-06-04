using MetaData;
using Microsoft.Win32;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using SuperMinersCustomServiceSystem.View.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (App.PlayerVMObject == null || GlobalData.CurrentAdmin == null)
            {
                return;
            }

            App.PlayerVMObject.AsyncGetListPlayers();

            //if (GlobalData.CurrentAdmin.GroupType != MetaData.User.AdminGroupType.CEO)
            //{
            //    this.btnDeletePlayer.IsEnabled = false;
            //    this.btnEditPlayerInfo.IsEnabled = false;
            //    this.btnLockPlayer.IsEnabled = false;
            //    this.btnSetPlayerAsAgent.IsEnabled = false;
            //    this.btnUnLockPlayer.IsEnabled = false;
            //}

            //GlobalData.Client.TransferPlayerToCompleted += Client_TransferPlayerToCompleted;
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject
            };

            this.panelPlayersManager.SetBinding(GroupBox.DataContextProperty, bind);

            if (GlobalData.ServerType == ServerType.Server2)
            {
                MenuItem mitem1 = new MenuItem() { Header = "查看玩家远程服务购买记录" };
                mitem1.Click += PlayerListContextMenu_ViewRemoteServiceBuyRecordItem_Click;
                this.cmenuDataGridPlayer.Items.Add(mitem1);

                MenuItem mitem2 = new MenuItem() { Header = "查看玩家远程完成记录" };
                mitem2.Click += PlayerListContextMenu_ViewRemoteServiceHandleRecordItem_Click;
                this.cmenuDataGridPlayer.Items.Add(mitem2);

                this.btnAddNewRemoteServiceHandled.Visibility = System.Windows.Visibility.Visible;
            }
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
            App.PlayerVMObject.SearchPlayers(this.txtUserLoginName.Text.Trim(), this.txtUserName.Text.Trim(), 0, this.txtAlipayAccount.Text.Trim(), this.txtReferrerUserName.Text.Trim(), this.txtInvitationCode.Text.Trim(), this.cmbLocked.SelectedIndex, this.cmbOnline.SelectedIndex, this.txtLoginIP.Text.Trim(), this.txtLoginMac.Text.Trim());
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;


                    //GlobalData.Client.TransferPlayerToCompleted += Client_TransferPlayerToCompleted;
                    //GlobalData.Client.TransferPlayerTo(player.ParentObject.SimpleInfo, player.ParentObject.FortuneInfo);

                    EditPlayerWindow win = new EditPlayerWindow(player);
                    win.ShowDialog();
                }
            }
            catch (Exception exc)
            {

            }
        }

        //void Client_TransferPlayerToCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        //{
        //    try
        //    {
        //        GlobalData.Client.TransferPlayerToCompleted -= Client_TransferPlayerToCompleted;
        //        if (e.Error != null)
        //        {
        //            MessageBox.Show(e.Error.Message);
        //            return;
        //        }
                
        //        MessageBox.Show(MetaData.OperResult.GetMsg(e.Result));
        //    }
        //    catch (Exception exc)
        //    {
        //        MessageBox.Show(exc.Message);
        //    }
        //}

        private void btnDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    InputActionPasswordWindow win = new InputActionPasswordWindow();
                    if (win.ShowDialog() == true)
                    {
                        string ActionPassword = win.ActionPassword;
                        PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                        if (MessageBox.Show("删除玩家【" + player.UserName + "】？该操作不可恢复，请确认？", "请确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            App.PlayerVMObject.AsyncDeletePlayerInfos(new string[] { player.UserName }, ActionPassword);
                        }
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDig = new SaveFileDialog();
            saveDig.Filter = "CSV文件(*.csv)|.csv";
            if (saveDig.ShowDialog() == true)
            {
                string fileName = saveDig.FileName;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("用户名,昵称,支付宝账户,支付宝真实姓名,注册时间,注册IP,推荐人,邀请码,上一次登录时间,上一次收取矿石时间,"+
                    "是否被锁定,锁定时间,锁定天数,是否在线,当前登录IP,贡献值,信誉值,灵币,冻结灵币,金币,矿石储量,累计总产出,矿石量,冻结矿石,矿工数,钻石量");
                foreach (var item in App.PlayerVMObject.ListFilteredPlayers)
                {
                    #region
                    builder.Append(item.UserLoginName);
                    builder.Append(",");
                    builder.Append(item.UserName);
                    builder.Append(",");
                    builder.Append(item.Alipay);
                    builder.Append(",");
                    builder.Append(item.AlipayRealName);
                    builder.Append(",");
                    builder.Append(item.RegisterTime);
                    builder.Append(",");
                    builder.Append(item.RegisterIP);
                    builder.Append(",");
                    builder.Append(item.ReferrerUserName);
                    builder.Append(",");
                    builder.Append(item.InvitationCode);
                    builder.Append(",");
                    builder.Append(item.LastLoginTime);
                    builder.Append(",");
                    builder.Append(item.LastGatherStoneTime);
                    builder.Append(",");
                    builder.Append(item.IsLockedString);
                    builder.Append(",");
                    builder.Append(item.LockedTimeString);
                    builder.Append(",");
                    builder.Append(item.LockedExpireDays);
                    builder.Append(",");
                    builder.Append(item.Online);
                    builder.Append(",");
                    builder.Append(item.LastLoginIP);
                    builder.Append(",");
                    builder.Append(item.Exp);
                    builder.Append(",");
                    builder.Append(item.CreditValue);
                    builder.Append(",");
                    builder.Append(item.RMB);
                    builder.Append(",");
                    builder.Append(item.FreezingRMB);
                    builder.Append(",");
                    builder.Append(item.GoldCoin);
                    builder.Append(",");
                    builder.Append(item.StonesReserves);
                    builder.Append(",");
                    builder.Append(item.TotalProducedStonesCount);
                    builder.Append(",");
                    builder.Append(item.StockOfStones);
                    builder.Append(",");
                    builder.Append(item.FreezingStones);
                    builder.Append(",");
                    builder.Append(item.MinersCount);
                    builder.Append(",");
                    builder.Append(item.StockOfDiamonds);
                    builder.AppendLine();
                    #endregion
                }

                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(stream, UTF8Encoding.UTF8);
                    writer.Write(builder.ToString());
                    writer.Dispose();
                }

                MyMessageBox.ShowInfo("保存CSV文件成功");
            }
        }

        private void btnLockPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player.IsLocked)
                    {
                        MyMessageBox.ShowInfo("该玩家已经被锁定");
                        return;
                    }
                    InputActionPasswordWindow win = new InputActionPasswordWindow();
                    if (win.ShowDialog() == true)
                    {
                        string ActionPassword = win.ActionPassword;
                        InputValueWindow winInputValue = new InputValueWindow("请确认", "请输入要锁定玩家登录天数", 100, 1);
                        if (winInputValue.ShowDialog() == true)
                        {
                            App.PlayerVMObject.AsyncLockPlayerInfos(player.UserLoginName, ActionPassword, (int)winInputValue.Value);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("锁定玩家异常。" + exc.Message);
            }
        }

        private void btnUnLockPlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (!player.IsLocked)
                    {
                        MyMessageBox.ShowInfo("该玩家没有被锁定");
                        return;
                    }
                    InputActionPasswordWindow win = new InputActionPasswordWindow();
                    if (win.ShowDialog() == true)
                    {
                        string ActionPassword = win.ActionPassword;

                        if (MessageBox.Show("请确认要解锁玩家【" + player.UserLoginName + "】", "请确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            App.PlayerVMObject.AsyncUnLockPlayerInfos(player.UserLoginName, ActionPassword);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("解锁玩家异常。" + exc.Message);
            }
        }

        private void btnSetPlayerAsAgent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (MessageBox.Show("请确认要将玩家【" + player.UserLoginName + "】设置为代理？", "请确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        InputActionPasswordWindow win = new InputActionPasswordWindow();
                        if (win.ShowDialog() == true)
                        {
                            //string ActionPassword = win.ActionPassword;

                            EditAgentInfoWindow winEdit = new EditAgentInfoWindow(player.UserID, player.UserLoginName);
                            winEdit.ShowDialog();
                            if (winEdit.ISOK)
                            {
                                App.PlayerVMObject.AsyncGetListPlayers();
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("解锁玩家异常。" + exc.Message);
            }
        }

        private void btnViewPlayerExpRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                    ViewPlayerExpChangedRecordsWindow win = new ViewPlayerExpChangedRecordsWindow(player.UserID);
                    win.ShowDialog();
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnViewPlayerLoginLogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                    ViewPlayerLoginLogsWindow win = new ViewPlayerLoginLogsWindow(player.UserID);
                    win.ShowDialog();
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnAddNewRemoteServiceHandled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;

                    EditPlayerRemoteServiceHandleWindow win = new EditPlayerRemoteServiceHandleWindow();
                    win.AddNewRecord(player.UserName);
                    win.ShowDialog();
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
                            ViewPlayerRMBWithdrawRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void PlayerListContextMenu_ViewRemoteServiceBuyRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerRemoteServiceBuyRecords != null)
                        {
                            ViewPlayerRemoteServiceBuyRecords(player.UserName);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void PlayerListContextMenu_ViewRemoteServiceHandleRecordItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is PlayerInfoUIModel)
                {
                    PlayerInfoUIModel player = this.datagridPlayerInfos.SelectedItem as PlayerInfoUIModel;
                    if (player != null)
                    {
                        if (ViewPlayerRemoteServiceHandleRecords != null)
                        {
                            ViewPlayerRemoteServiceHandleRecords(player.UserName);
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
        public event Action<string> ViewPlayerRemoteServiceBuyRecords;
        public event Action<string> ViewPlayerRemoteServiceHandleRecords;


        //private List<int> listTransferFailedUserID = new List<int>();
        //private List<int> listTransferOKUserID = new List<int>();

        //private void btnAutoTransferServer_Click(object sender, RoutedEventArgs e)
        //{
        //    string serverUri2 = System.Configuration.ConfigurationManager.AppSettings["ServerUri2"];
        //    GlobalData.Client.Init(serverUri2);
        //    this.listOutput.Items.Add("正在自动转区");

        //    for (int i = 0; i < App.PlayerVMObject.ListAllPlayers.Count; i++)
        //    {
        //        var player = App.PlayerVMObject.ListAllPlayers[i];
        //        if (!player.IsLocked)
        //        {
        //            this.listOutput.Items.Add("正在转移第" + (i + 1) + "个玩家：" + player.UserID + " -- " + player.UserLoginName);
        //            GlobalData.Client.TransferPlayerTo(player.ParentObject.SimpleInfo, player.ParentObject.FortuneInfo, "","", player.UserID);
        //        }
        //    }

        //    string serverUri1 = System.Configuration.ConfigurationManager.AppSettings["ServerUri1"];
        //    GlobalData.Client.Init(serverUri1);

        //    List<string> listToDeleteUserName = new List<string>();
        //    foreach (var userID in listTransferOKUserID)
        //    {
        //        var player = App.PlayerVMObject.ListAllPlayers.FirstOrDefault(p => p.UserID == userID);
        //        listToDeleteUserName.Add(player.UserName);
        //    }

        //    MyMessageBox.ShowInfo("一共" + App.PlayerVMObject.ListAllPlayers.Count + "个玩家，自动转移成功" + listTransferOKUserID.Count + "个");

        //    App.PlayerVMObject.AsyncDeletePlayerInfos(listToDeleteUserName.ToArray(), GlobalData.CurrentAdmin.ActionPassword);

        //}

        //void Client_TransferPlayerToCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        //{
        //    try
        //    {
        //        int userID = Convert.ToInt32(e.UserState);
        //        //string serverUri1 = System.Configuration.ConfigurationManager.AppSettings["ServerUri1"];
        //        //GlobalData.Client.Init(serverUri1);

        //        GlobalData.Client.TransferPlayerToCompleted -= Client_TransferPlayerToCompleted;
        //        if (e.Error != null)
        //        {
        //            this.listOutput.Items.Add("玩家[" + userID + "] 转移失败，原因为：" + e.Error.Message);
        //            listTransferFailedUserID.Add(userID);
        //            return;
        //        }
        //        if (e.Result != OperResult.RESULTCODE_TRUE)
        //        {
        //            this.listOutput.Items.Add("玩家[" + userID + "] 转移失败，原因为：" + OperResult.GetMsg(e.Result));
        //            listTransferFailedUserID.Add(userID);
        //            return;
        //        }

        //        this.listOutput.Items.Add("玩家[" + userID + "] 转移成功");
        //        this.listTransferOKUserID.Add(userID);

        //    }
        //    catch (Exception exc)
        //    {
        //        MyMessageBox.ShowInfo(exc.Message);
        //    }
        //}

    }
}
