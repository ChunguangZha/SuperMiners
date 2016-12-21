using MetaData.User;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DeletedPlayerInfoControl.xaml
    /// </summary>
    public partial class DeletedPlayerInfoControl : UserControl
    {
        ObservableCollection<PlayerInfoUIModel> ListAllDeletedPlayers = new ObservableCollection<PlayerInfoUIModel>();

        ObservableCollection<PlayerInfoUIModel> ListFilteredDeletedPlayers = new ObservableCollection<PlayerInfoUIModel>();

        public DeletedPlayerInfoControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.datagridPlayerInfos.ItemsSource = ListAllDeletedPlayers;
            if (GlobalData.Client != null)
            {
                GlobalData.Client.GetDeletedPlayersCompleted += Client_GetDeletedPlayersCompleted;
            }
        }

        void Client_GetDeletedPlayersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<PlayerInfo[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            try
            {
                ListAllDeletedPlayers.Clear();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询已删除玩家，服务器返回异常。信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListAllDeletedPlayers.Add(new PlayerInfoUIModel(new SuperMinersServerApplication.Model.PlayerInfoLoginWrap()
                        {
                            SimpleInfo = item.SimpleInfo,
                            FortuneInfo = item.FortuneInfo
                        }));
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = "";
            this.txtAlipayAccount.Text = "";
            this.txtReferrerUserName.Text = "";
            this.cmbLocked.SelectedIndex = 0;
            this.cmbOnline.SelectedIndex = 0;
            RefreshDB();
        }

        public void RefreshDB()
        {
            App.BusyToken.ShowBusyWindow("正在加载...");
            GlobalData.Client.GetDeletedPlayers();
            this.datagridPlayerInfos.ItemsSource = ListAllDeletedPlayers;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.SearchPlayers(this.txtUserName.Text.Trim(), cmbUserGroup.SelectedIndex - 1, this.txtAlipayAccount.Text.Trim(), this.txtReferrerUserName.Text.Trim(), this.txtInvitationCode.Text.Trim(), this.cmbLocked.SelectedIndex, this.cmbOnline.SelectedIndex, this.txtLoginIP.Text.Trim(), this.txtLoginMac.Text.Trim());

            this.datagridPlayerInfos.ItemsSource = ListFilteredDeletedPlayers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAlipayAccount"></param>
        /// <param name="referrerUserName"></param>
        /// <param name="isLocked">0表示全部，1表示已锁定，2表示非锁定</param>
        /// <param name="isOnline">0表示全部，1表示在线，2表示离线</param>
        public void SearchPlayers(string userName, int groupType, string userAlipayAccount, string referrerUserName, string invitationCode, int isLocked, int isOnline, string loginIP, string loginMac)
        {
            bool checkUserNameOK = false;
            bool checkGroupType = false;
            bool checkUserAlipayOK = false;
            bool checkUserReferrerOK = false;
            bool checkInvitationCodeOK = false;
            bool checkLockedStateOK = false;
            bool checkOnlineStateOK = false;
            bool checkLoginIPOK = false;
            bool checkLoginMacOK = false;

            ListFilteredDeletedPlayers.Clear();
            foreach (var item in this.ListAllDeletedPlayers)
            {
                checkUserNameOK = checkGroupType = checkUserAlipayOK = checkUserReferrerOK = checkInvitationCodeOK = checkLockedStateOK = checkOnlineStateOK = checkLoginIPOK = checkLoginMacOK = false;

                if (string.IsNullOrEmpty(userName) || item.UserName.Contains(userName))
                {
                    checkUserNameOK = true;
                }
                if (groupType < 0 || (int)item.GroupType == groupType)
                {
                    checkGroupType = true;
                }
                if (string.IsNullOrEmpty(userAlipayAccount) || item.Alipay.Contains(userAlipayAccount))
                {
                    checkUserAlipayOK = true;
                }
                if (string.IsNullOrEmpty(referrerUserName) || item.ReferrerUserName == referrerUserName)
                {
                    checkUserReferrerOK = true;
                }
                if (string.IsNullOrEmpty(invitationCode) || item.InvitationCode == invitationCode)
                {
                    checkInvitationCodeOK = true;
                }
                if (isLocked >= 0)
                {
                    if (isLocked == 0)
                    {
                        checkLockedStateOK = true;
                    }
                    else if (isLocked == 1)
                    {
                        if (item.IsLocked)
                        {
                            checkLockedStateOK = true;
                        }
                    }
                    else if (isLocked == 2)
                    {
                        if (!item.IsLocked)
                        {
                            checkLockedStateOK = true;
                        }
                    }
                }
                if (isOnline >= 0)
                {
                    if (isOnline == 0)
                    {
                        checkOnlineStateOK = true;
                    }
                    else if (isOnline == 1)
                    {
                        if (item.Online)
                        {
                            checkOnlineStateOK = true;
                        }
                    }
                    else if (isOnline == 2)
                    {
                        if (!item.Online)
                        {
                            checkOnlineStateOK = true;
                        }
                    }
                }
                if (string.IsNullOrEmpty(loginIP) || item.LastLoginIP == loginIP)
                {
                    checkLoginIPOK = true;
                }
                if (string.IsNullOrEmpty(loginMac) || item.LastLoginMac == loginMac)
                {
                    checkLoginMacOK = true;
                }

                if (checkUserNameOK && checkGroupType && checkUserAlipayOK && checkUserReferrerOK
                    && checkInvitationCodeOK && checkLockedStateOK && checkOnlineStateOK
                    && checkLoginIPOK && checkLoginMacOK)
                {
                    ListFilteredDeletedPlayers.Add(item);
                }
            }

            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs("PlayersCount"));
            //}
        }

    }
}
