using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    class PlayerViewModel
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

        public void AsyncGetListPlayers()
        {
            App.BusyToken.ShowBusyWindow("正在加载所有玩家信息...");
            GlobalData.Client.GetPlayers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAlipayAccount"></param>
        /// <param name="referrerUserName"></param>
        /// <param name="isLocked">0表示全部，1表示已锁定，2表示非锁定</param>
        /// <param name="isOnline">0表示全部，1表示在线，2表示离线</param>
        public void SearchPlayers(string userName, string userAlipayAccount, string referrerUserName, int isLocked, int isOnline)
        {
            bool checkUserName = false;
            bool checkUserAlipay = false;
            bool checkUserReferrer = false;
            bool checkLockedState = false;
            bool checkOnlineState = false;

            ListFilteredPlayers.Clear();
            foreach (var item in this.ListAllPlayers)
            {
                checkUserName = checkUserAlipay = checkUserReferrer = checkLockedState = checkOnlineState = false;

                if (string.IsNullOrEmpty(userName) || item.UserName.Contains(userName))
                {
                    checkUserName = true;
                }
                if (string.IsNullOrEmpty(userAlipayAccount) || item.Alipay.Contains(userAlipayAccount))
                {
                    checkUserAlipay = true;
                }
                if (string.IsNullOrEmpty(referrerUserName) || item.ReferrerUserName.Contains(referrerUserName))
                {
                    checkUserReferrer = true;
                }
                if (isLocked >= 0)
                {
                    if (isLocked == 0)
                    {
                        checkLockedState = true;
                    }
                    else if (isLocked == 1)
                    {
                        if (item.IsLocked)
                        {
                            checkLockedState = true;
                        }
                    }
                    else if (isLocked == 2)
                    {
                        if (!item.IsLocked)
                        {
                            checkLockedState = true;
                        }
                    }
                }
                if (isOnline >= 0)
                {
                    if (isOnline == 0)
                    {
                        checkOnlineState = true;
                    }
                    else if (isOnline == 1)
                    {
                        if (item.Online)
                        {
                            checkOnlineState = true;
                        }
                    }
                    else if (isOnline == 2)
                    {
                        if (!item.Online)
                        {
                            checkOnlineState = true;
                        }
                    }
                }

                if (checkUserName && checkUserAlipay && checkUserReferrer && checkLockedState && checkOnlineState)
                {
                    ListFilteredPlayers.Add(item);
                }
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
        }
    }
}
