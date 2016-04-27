using SuperMinersWPF.Utility;
using SuperMinersWPF.Views;
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
using System.Windows.Shapes;

namespace SuperMinersWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SynchronizationContext _syn;

        public Window1()
        {
            InitializeComponent();

            this.Closing += Window1_Closing;

            this._syn = SynchronizationContext.Current;
            GlobalData.Client.SetContext(this._syn);
            GlobalData.Client.Error += new EventHandler(Client_Error);
            GlobalData.Client.OnSendMessage += new Action<string>(Client_OnSendMessage);
            GlobalData.Client.OnKickout += new Action(Client_OnKickout);
            GlobalData.Client.OnSetPlayerInfo += Client_OnSetPlayerInfo;
            App.UserVMObject.StartListen();

            this.DataContext = GlobalData.CurrentUser;
        }

        void Client_OnSetPlayerInfo(MetaData.User.PlayerInfo obj)
        {
            try
            {
                if (obj == null)
                {
                    return;
                }
                if (obj.SimpleInfo.UserName == GlobalData.CurrentUser.UserName)
                {
                    //TODO: 此时应该玩家所有正在进行的跟财富有关的操作。
                    GlobalData.CurrentUser.ParentObject = obj;
                }
            }
            catch (Exception)
            {

            }
        }

        void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.UserVMObject.StopListen();
            GlobalData.Client.Logout();
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
                GlobalData.IsOffline = true;
                MyMessageBox.ShowInfo("网络异常，或系统故障，无法连接服务器，请稍后重试。");
                //this.Close();
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

        }

        private void btnGetMoney_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGatherStones_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
