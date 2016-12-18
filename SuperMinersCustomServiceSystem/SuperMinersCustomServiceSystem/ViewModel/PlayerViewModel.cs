using MetaData;
using MetaData.User;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PlayerInfoUIModel> _listAllPlayers = new ObservableCollection<PlayerInfoUIModel>();

        public ObservableCollection<PlayerInfoUIModel> ListAllPlayers
        {
            get { return this._listAllPlayers; }
        }

        private ObservableCollection<PlayerInfoUIModel> _listFilteredPlayers = new ObservableCollection<PlayerInfoUIModel>();

        public ObservableCollection<PlayerInfoUIModel> ListFilteredPlayers
        {
            get { return this._listFilteredPlayers; }
        }

        //private ObservableCollection<PlayerLoginInfoUIModel> _listPlayerLoginInfo = new ObservableCollection<PlayerLoginInfoUIModel>();

        //public ObservableCollection<PlayerLoginInfoUIModel> ListPlayerLoginInfo
        //{
        //    get { return _listPlayerLoginInfo; }
        //}


        public int PlayersCount
        {
            get
            {
                return this.ListFilteredPlayers.Count;
            }
        }

        //public void AsyncGetUserLoginLog(int userID)
        //{
        //    if (GlobalData.Client != null && GlobalData.Client.IsConnected)
        //    {
        //        App.BusyToken.ShowBusyWindow("正在加载玩家登录日志");
        //        GlobalData.Client.GetUserLoginLog(userID);
        //    }
        //}

        public void AsyncGetListPlayers()
        {
            if (GlobalData.Client != null && GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在加载所有玩家信息...");
                GlobalData.Client.GetPlayers();
            }
        }

        public void AsyncGetPlayer(string userName)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在加载玩家信息...");
                GlobalData.Client.GetPlayer(userName);
            }
        }

        public void AsyncChangePlayerInfo(PlayerInfoUIModel player, string actionPassword)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在保存玩家信息...");
                GlobalData.Client.ChangePlayer(player.ParentObject, actionPassword);
            }
        }

        public void AsyncDeletePlayerInfos(string[] playerUserNames, string actionPassword)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在删除玩家...");
                GlobalData.Client.DeletePlayers(playerUserNames, actionPassword);
            }
        }

        public void AsyncLockPlayerInfos(string playerUserName, string actionPassword, int expireDays)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在锁定玩家...");
                GlobalData.Client.LockPlayer(playerUserName, actionPassword, expireDays);
            }
        }

        public void AsyncUnLockPlayerInfos(string playerUserName, string actionPassword)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在解锁玩家...");
                GlobalData.Client.UnlockPlayer(playerUserName, actionPassword);
            }
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

            ListFilteredPlayers.Clear();
            foreach (var item in this.ListAllPlayers)
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
                    ListFilteredPlayers.Add(item);
                }
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PlayersCount"));
            }
        }

        public int GetCheckPlayersCount()
        {
            int result = 0;
            foreach (var item in this.ListFilteredPlayers)
            {
                if (item.Checked)
                {
                    result++;
                }
            }

            return result;
        }

        public PlayerInfoUIModel GetFirstCheckedPlayer()
        {
            foreach (var item in this.ListFilteredPlayers)
            {
                if (item.Checked)
                {
                    return item;
                }
            }

            return null;
        }

        public void RegisterEvents()
        {
            GlobalData.Client.GetPlayersCompleted += Client_GetPlayersCompleted;
            GlobalData.Client.ChangePlayerCompleted += Client_ChangePlayerCompleted;
            GlobalData.Client.DeletePlayersCompleted += Client_DeletePlayersCompleted;
            GlobalData.Client.LockPlayerCompleted += Client_LockPlayerCompleted;
            GlobalData.Client.UnlockPlayerCompleted += Client_UnlockPlayerCompleted;
            GlobalData.Client.GetPlayerCompleted += Client_GetPlayerCompleted;
            //GlobalData.Client.GetUserLoginLogCompleted += Client_GetUserLoginLogCompleted;
        }

        //void Client_GetUserLoginLogCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<PlayerLoginInfo[]> e)
        //{
        //    try
        //    {
        //        App.BusyToken.CloseBusyWindow();

        //        ListPlayerLoginInfo.Clear();

        //        if (e.Error != null)
        //        {
        //            MessageBox.Show("获取玩家登录日志失败。");
        //            return;
        //        }

        //        if (e.Result != null)
        //        {
        //            foreach (var item in e.Result)
        //            {
        //                var player = this.ListAllPlayers.FirstOrDefault(p => p.UserID == item.UserID);
        //                string userName = player == null ? "" : player.UserName;
        //                this.ListPlayerLoginInfo.Add(new PlayerLoginInfoUIModel(item, userName));
        //            }
        //        }        
        //    }
        //    catch (Exception exc)
        //    {
        //        MyMessageBox.ShowInfo("获取玩家信息,服务器回调异常。信息为：" + exc.Message);
        //    }
        //}

        void Client_GetPlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SuperMinersServerApplication.Model.PlayerInfoLoginWrap> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null || e.Result == null)
                {
                    MessageBox.Show("获取玩家信息失败。");
                    return;
                }

                var user = this.ListAllPlayers.FirstOrDefault(u => u.UserName == e.Result.SimpleInfo.UserName);
                if (user != null)
                {
                    user.ParentObject = e.Result;
                }
                //user = this.ListFilteredPlayers.FirstOrDefault(u => u.UserName == e.Result.SimpleInfo.UserName);
                //if (user != null)
                //{
                //    user.ParentObject = e.Result;
                //}                
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取玩家信息,服务器回调异常。信息为：" + exc.Message);
            }
        }

        void Client_UnlockPlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("解锁玩家失败，服务器返回错误，信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("解锁玩家成功。");
                    //this.AsyncGetListPlayers();
                }
                else
                {
                    MyMessageBox.ShowInfo("解锁玩家失败。");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("解锁玩家失败，回调处理异常，信息为：" + exc.Message);
            }
        }

        void Client_LockPlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("锁定玩家失败，服务器返回错误，信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("锁定玩家成功。");
                    //this.AsyncGetListPlayers();
                }
                else
                {
                    MyMessageBox.ShowInfo("锁定玩家失败。");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("锁定玩家失败，回调处理异常，信息为：" + exc.Message);
            }
        }

        void Client_DeletePlayersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<DeleteResultInfo> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MessageBox.Show("删除玩家信息失败。");
                return;
            }
            if (e.Result.AllSucceed)
            {
                MessageBox.Show("删除玩家信息成功。");
            }
            else
            {
                if (e.Result.FailedList == null || e.Result.FailedList.Length == 0 ||e.Result.SucceedList == null || e.Result.SucceedList.Length == 0)
                {
                    MessageBox.Show("删除玩家信息失败。");
                }
                else
                {
                    StringBuilder builderS = new StringBuilder();
                    StringBuilder builderF = new StringBuilder();
                    foreach (var item in e.Result.SucceedList)
                    {
                        builderS.Append(item);
                        builderS.Append(",");
                    }
                    foreach (var item in e.Result.FailedList)
                    {
                        builderF.Append(item);
                        builderF.Append(",");
                    }
                    MessageBox.Show("用户名：" + builderS.ToString(0, builderS.Length - 1) + "  删除成功，用户名：" + builderF.ToString(0, builderF.Length - 1) + " 删除失败。");
                }
            }

            this.AsyncGetListPlayers();
        }

        void Client_ChangePlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MessageBox.Show("保存玩家信息服务器返回失败，错误信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("保存玩家信息成功。");
                }
                else
                {
                    MessageBox.Show("保存玩家信息失败。" + OperResult.GetMsg(e.Result));
                }

            }
            catch (Exception exc)
            {

            }
        }

        void Client_GetPlayersCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SuperMinersServerApplication.Model.PlayerInfoLoginWrap[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MessageBox.Show("获取玩家信息失败。");
                return;
            }

            this.ListAllPlayers.Clear();
            this.ListFilteredPlayers.Clear();

            foreach (var item in e.Result)
            {
                var player = new PlayerInfoUIModel(item);
                this.ListAllPlayers.Add(player);
                this.ListFilteredPlayers.Add(player);
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("PlayersCount"));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
