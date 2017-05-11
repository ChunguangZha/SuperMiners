using MetaData.Shopping;
using SuperMinersWPF.Models;
using SuperMinersWPF.StringResources;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Views;
using SuperMinersWPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //this.Title = "内测版";

            this._syn = System.Threading.SynchronizationContext.Current;

            //if (GlobalData.ServerType == ServerType.Server1)
            //{
            //    this.btnMall.Visibility = System.Windows.Visibility.Collapsed;
            //    this.lblGravel.Visibility = System.Windows.Visibility.Collapsed;
            //    this.txtGravel.Visibility = System.Windows.Visibility.Collapsed;
            //    this.btnRequestGravel.Visibility = System.Windows.Visibility.Collapsed;
            //    this.btnInvitationFriends.Visibility = System.Windows.Visibility.Collapsed;
            //    this.btnMakeAVowToGod.Visibility = System.Windows.Visibility.Collapsed;
            //    this.lblMakeAVowToGod.Visibility = System.Windows.Visibility.Collapsed;

            //    this.lblShoppingCredits.Visibility = System.Windows.Visibility.Collapsed;
            //    this.txtShoppingCredits.Visibility = System.Windows.Visibility.Collapsed;
            //    this.btnGetShoppingCredits.Visibility = System.Windows.Visibility.Collapsed;

            //    this.btnGetMoney.IsEnabled = true;
            //    this.btnStonesSell.IsEnabled = true;
                
            //    this.Title = Strings.Title + System.Configuration.ConfigurationManager.AppSettings["softwareversion"] + "     迅灵一区";
            //}
            //else
            //{
                this.btnMall.Visibility = System.Windows.Visibility.Visible;
                this.lblGravel.Visibility = System.Windows.Visibility.Visible;
                this.txtGravel.Visibility = System.Windows.Visibility.Visible;
                this.btnRequestGravel.Visibility = System.Windows.Visibility.Visible;
                this.btnInvitationFriends.Visibility = System.Windows.Visibility.Visible;
                this.btnMakeAVowToGod.Visibility = System.Windows.Visibility.Visible;
                this.lblMakeAVowToGod.Visibility = System.Windows.Visibility.Visible;

                this.lblShoppingCredits.Visibility = System.Windows.Visibility.Visible;
                this.txtShoppingCredits.Visibility = System.Windows.Visibility.Visible;
                this.btnGetShoppingCredits.Visibility = System.Windows.Visibility.Visible;
                
                this.btnGetMoney.IsEnabled = false;
                this.btnStonesSell.IsEnabled = false;

                this.Title = Strings.Title + System.Configuration.ConfigurationManager.AppSettings["softwareversion"] + "     迅灵矿场";
            //}

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
            //App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();

            App.StackStoneVMObject.AsyncGetTodayRealTimeTradeRecords();
            App.StackStoneVMObject.AsyncGetAllNotFinishedSellOrders();
            App.StackStoneVMObject.AsyncGetAllNotFinishedBuyOrders();
            App.StackStoneVMObject.AsyncGetAllStoneStackDailyRecords();
            App.GameRaiderofLostArkVMObject.AsyncGetHistoryRaiderRoundRecords(GlobalData.PageItemsCount, 1);
            App.GameRaiderofLostArkVMObject.AsyncGetCurrentRaiderRoundInfo();
            App.GambleStoneVMObject.Init();

            this.controlMySuperMiners.Init();
            AddEventHandlers();
        }

        public void AddEventHandlers()
        {
            this.controlFunny.AddEventHandlers();
            this.controlStack.AddEventHandlers();
        }

        public void RemoveEventHandlers()
        {
            this.controlFunny.RemoveEventHandlers();
            this.controlStack.RemoveEventHandlers();
        }

        void Client_LogoutCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("退出失败. " + e.Error);
                }

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
                App.StackStoneVMObject.StopListen();
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

            if (App.StackStoneVMObject.TodayStackInfo.MarketState != MetaData.Game.StoneStack.StackMarketState.Opening)
            {
                MyMessageBox.ShowInfo("还未开市，无法出售矿石");
                return;
            }

            DelegateSellStoneWindows win = new DelegateSellStoneWindows();
            win.ShowDialog();
            //SellStonesWindow win = new SellStonesWindow();
            //win.ShowDialog();
        }

        private void btnGetMoney_Click(object sender, RoutedEventArgs e)
        {
            WithdrawRMBWindow win = new WithdrawRMBWindow();
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

        private void CloseAllControl()
        {
            this.controlMall.Visibility = System.Windows.Visibility.Collapsed;
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStonesMarket.Visibility = System.Windows.Visibility.Collapsed;
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Collapsed;
            this.controlFunny.Visibility = System.Windows.Visibility.Collapsed;
            this.controlStack.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnShowDigStonesArea_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            CloseAllControl();
            this.controlDigStoneArea.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnShowStonesMarket_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            //App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
            CloseAllControl();
            this.controlStack.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnShowTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            CloseAllControl();
        }

        private void btnShowChat_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            CloseAllControl();
        }

        private void btnShowFunny_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            CloseAllControl();
            this.controlFunny.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnShowMySuperMiners_Checked(object sender, RoutedEventArgs e)
        {
            if (this.controlDigStoneArea == null)
            {
                return;
            }

            CloseAllControl();
            this.controlMySuperMiners.Visibility = System.Windows.Visibility.Visible;

        }

        private void btnMall_Click(object sender, RoutedEventArgs e)
        {
            CloseAllControl();
            App.ShoppingVMObject.AsyncGetVirtualShoppingItem();
            App.ShoppingVMObject.AsyncGetDiamondShoppingItem(DiamondsShoppingItemType.LiveThing);
            App.UserVMObject.AsyncGetPostAddressList();
            this.controlMall.Visibility = System.Windows.Visibility.Visible;
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

        private void btnRequestGravel_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUser.ParentObject.GravelInfo == null)
            {
                MyMessageBox.ShowInfo("您无法申请碎片");
                return;
            }

            if (GlobalData.CurrentUser.ParentObject.GravelInfo.GravelState == MetaData.User.PlayerGravelState.Requestable)
            {
                App.UserVMObject.AsyncRequestGravel();
            }
            else if (GlobalData.CurrentUser.ParentObject.GravelInfo.GravelState == MetaData.User.PlayerGravelState.Getable)
            {
                App.UserVMObject.AsyncGetGravel();
            }
        }

        private void btnGetCredits_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://goto.xlore.net/Login.aspx"));
        }

        private void btnExchangeDiamonds_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMakeAVowToGod_Click(object sender, RoutedEventArgs e)
        {
            App.UserVMObject.AsyncMakeAVowToGod();
        }

    }
}
