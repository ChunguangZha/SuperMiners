using SuperMinersWPF.Models;
using SuperMinersWPF.StringResources;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Views;
using SuperMinersWPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private System.Threading.SynchronizationContext _syn;

        public bool IsBackToLogin = false;

        public Window1()
        {
            InitializeComponent();

            this.Closing += Window1_Closing;

            //this.Title = Strings.Title + System.Configuration.ConfigurationManager.AppSettings["softwareversion"];
            this.Title = "内测版";

            this._syn = System.Threading.SynchronizationContext.Current;
            GlobalData.Client.SetContext(this._syn);
            GlobalData.Client.Error += new EventHandler(Client_Error);
            GlobalData.Client.OnSendMessage += new Action<string>(Client_OnSendMessage);
            GlobalData.Client.OnKickout += new Action(Client_OnKickout);
            GlobalData.Client.OnKickoutByUser += Client_OnKickoutByUser;
            App.UserVMObject.StartListen();
            this.DataContext = GlobalData.CurrentUser;

            this.panelNotice.DataContext = App.NoticeVMObject;
        }

        void Client_OnKickoutByUser()
        {
            this._syn.Post(s =>
            {
                MyMessageBox.ShowInfo("您的账户在其它电脑登录，如非本人操作，请及时修改密码。");
                this.IsBackToLogin = true;
                this.Close();
            }, null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.NoticeVMObject.LastNoticeChanged += NoticeVMObject_LastNoticeChanged;
            GlobalData.Client.LogoutCompleted += Client_LogoutCompleted;
            App.NoticeVMObject.AsyncGetNewNotices();

            if (GlobalData.CurrentUser.GroupType == MetaData.User.PlayerGroupType.AgentPlayer)
            {
                App.UserVMObject.AsyncGetAgentUserInfo();
            }

            App.GameRouletteVMObject.AsyncGetAllAwardItems();
            App.GameRouletteVMObject.AsyncGetAllAwardRecord(-1, null, null, -1, -1, 20, 1);
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
            AddEventHandlers();
        }

        public void AddEventHandlers()
        {
            this.controlFunny.AddEventHandlers();
        }

        public void RemoveEventHandlers()
        {
            this.controlFunny.RemoveEventHandlers();
        }

        void Client_LogoutCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("退出失败. " + e.Error);
                }

                MyMessageBox.ShowInfo(e.Result.ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show("退出异常。" + exc.Message);
            }
        }

        void NoticeVMObject_LastNoticeChanged()
        {
            this.linkNotice.Inlines.Clear();
            if (App.NoticeVMObject.LastedNotice != null)
            {
                this.linkNotice.Inlines.Add(App.NoticeVMObject.LastedNotice.Title);
            }
        }

        void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                App.UserVMObject.StopListen();
                GlobalData.Client.Logout();
                RemoveEventHandlers();
                LogHelper.Instance.AddErrorLog("客户端" + GlobalData.CurrentUser.UserName + "已退出.", null);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("退出异常.", exc);
            }
        }

        void Client_OnKickout()
        {
            this._syn.Post(s =>
            {
                MyMessageBox.ShowInfo("您已经被管理员强制退出登录，请联系系统管理员。");
                this.IsBackToLogin = true;
                this.Close();
            }, null);
        }

        private void Client_OnSendMessage(string msg)
        {
            MyMessageBox.ShowInfo(msg);
        }
        
        private void Client_Error(object sender, EventArgs e)
        {
            this._syn.Post(s =>
            {
                if (GlobalData.IsLogined)
                {
                    GlobalData.InitToken(null);
                    MyMessageBox.ShowInfo("网络异常，或系统故障，无法连接服务器，请稍后重试。");
                }
                this.IsBackToLogin = false;
                this.Close();
            }, null);
        }

        private void btnInvitationFriends_Click(object sender, RoutedEventArgs e)
        {
            //if (GlobalData.AgentUserInfo == null)
            {
                App.UserReferrerTreeVMObject.AsyncGetUserReferrerTree();
                InvitationFriendsWindow win = new InvitationFriendsWindow();
                win.DataContext = App.UserReferrerTreeVMObject;
                win.ShowDialog();
            }
            //else
            //{

            //}
        }

        private void btnRMBRecharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoldCoinRecharge_Click(object sender, RoutedEventArgs e)
        {
            BuyGoldCoinWindow win = new BuyGoldCoinWindow();
            win.ShowDialog();
        }

        private void btnMinesBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyMineWindow win = new BuyMineWindow();
            win.ShowDialog();
        }

        private void btnMinersBuy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool showWin = true;
                if (GlobalData.CurrentUser.TempOutputStones > 0)
                {
                    showWin = false;
                    App.UserVMObject.SuspendListen();
                    GatherStonesForBuyMinerWindow winGatherStones = new GatherStonesForBuyMinerWindow();
                    if (winGatherStones.ShowDialog() == true)
                    {
                        showWin = true;
                    }
                    else
                    {
                        App.UserVMObject.ResumeListen(false);
                    }
                }

                if (showWin)
                {
                    BuyMinerWindow win = new BuyMinerWindow();
                    win.ShowDialog();
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Buy Miners Exception", exc);
                MyMessageBox.ShowInfo("购买矿工异常");
            }
        }

        private void btnDiamondsSell_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStonesSell_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUser.SellableStones < 1)
            {
                MyMessageBox.ShowInfo("没有可出售的" + Strings.Stone);
                return;
            }
            SellStonesWindow win = new SellStonesWindow();
            win.ShowDialog();
        }

        private void btnGetMoney_Click(object sender, RoutedEventArgs e)
        {
            WithdrawRMBWindow win = new WithdrawRMBWindow();
            win.ShowDialog();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow win = new SettingWindow();
            win.ShowDialog();
        }

        private void btnGatherStones_Click(object sender, RoutedEventArgs e)
        {
            if ((int)GlobalData.CurrentUser.TempOutputStones > 0)
            {
                App.UserVMObject.SuspendListen();
                GatherStonesWindow win = new GatherStonesWindow();
                if (win.ShowDialog() == false)
                {
                    //只有取消收取时，在此处恢复；否则在VM回调事件里恢复。
                    App.UserVMObject.ResumeListen(false);
                }
            }
        }
        
        private void btnBuyDope_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBox.ShowInfo("该功能暂未开放，敬请期待...");
        }

        private void btnUseDope_Click(object sender, RoutedEventArgs e)
        {
            MyMessageBox.ShowInfo("该功能暂未开放，敬请期待...");
        }

        private void btnShowDigStonesArea_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Visible;
            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
            this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowStonesMarket_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
            this.controlStonesMarket.Visibility = System.Windows.Visibility.Visible;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
            this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
            this.controlChat.Visibility = System.Windows.Visibility.Visible;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;

            //App.TopListVMObject.AsyncGetExpTopList();
            //this.controlTopList.Visibility = System.Windows.Visibility.Visible;
            //this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlGameHelper.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowChat_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            this.controlChat.Visibility = System.Windows.Visibility.Visible;
            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowFunny_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            this.controlFunny.Visibility = System.Windows.Visibility.Visible;
            this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowMySuperMiners_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Visible;
            this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;

            //this.controlGameHelper.Visibility = System.Windows.Visibility.Visible;
            //this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlTopList.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlChat.Visibility = System.Windows.Visibility.Collapsed;
            //this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void linkNotice_Click(object sender, RoutedEventArgs e)
        {
            if (App.NoticeVMObject.LastedNotice == null)
            {
                return;
            }

            ListAllNoticesWindow win = new ListAllNoticesWindow();
            win.SetCurrentNotice(App.NoticeVMObject.LastedNotice);
            win.ShowDialog();
        }

    }
}
