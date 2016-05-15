using SuperMinersWPF.StringResources;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Views;
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

        public Window1()
        {
            InitializeComponent();

            this.Closing += Window1_Closing;

            this.Title = Strings.Title + " 内测 0.03 版";

            this._syn = System.Threading.SynchronizationContext.Current;
            GlobalData.Client.SetContext(this._syn);
            GlobalData.Client.Error += new EventHandler(Client_Error);
            GlobalData.Client.OnSendMessage += new Action<string>(Client_OnSendMessage);
            GlobalData.Client.OnKickout += new Action(Client_OnKickout);
            App.UserVMObject.StartListen();
            this.DataContext = GlobalData.CurrentUser;
            this.gridActionMessage.DataContext = App.MessageVMObject;

            //App.MessageVMObject.StartListen();

            imgDigStones.Image = System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\Images\\DigStones.gif");
            imgDigStones.Size = new System.Drawing.Size(200, 200);
        }

        void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //App.MessageVMObject.StopListen();
                App.UserVMObject.StopListen();
                GlobalData.Client.Logout();

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
                this.Close();
            }, null);
        }

        private void btnInvitationFriends_Click(object sender, RoutedEventArgs e)
        {
            InvitationFriendsWindow win = new InvitationFriendsWindow();
            win.ShowDialog();
        }

        private void btnRMBRecharge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoldCoinRecharge_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnMinesBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyMineWindow win = new BuyMineWindow();
            win.ShowDialog();
        }

        private void btnMinersBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyMinerWindow win = new BuyMinerWindow();
            win.ShowDialog();
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
                App.UserVMObject.AsyncGatherStones((int)GlobalData.CurrentUser.TempOutputStones);
            }
        }

    }
}
