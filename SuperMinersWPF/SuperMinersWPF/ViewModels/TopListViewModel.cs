using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class TopListViewModel
    {
        private ObservableCollection<TopListInfoUIModel> _listExpTopList = new ObservableCollection<TopListInfoUIModel>();

        public ObservableCollection<TopListInfoUIModel> ListExpTopList
        {
            get
            {
                return this._listExpTopList;
            }
        }

        private ObservableCollection<TopListInfoUIModel> _listStoneTopList = new ObservableCollection<TopListInfoUIModel>();

        public ObservableCollection<TopListInfoUIModel> ListStoneTopList
        {
            get { return _listStoneTopList; }
        }

        private ObservableCollection<TopListInfoUIModel> _listGoldCoinTopList = new ObservableCollection<TopListInfoUIModel>();

        public ObservableCollection<TopListInfoUIModel> ListGoldCoinTopList
        {
            get { return _listGoldCoinTopList; }
        }

        private ObservableCollection<TopListInfoUIModel> _listMinerTopList = new ObservableCollection<TopListInfoUIModel>();

        public ObservableCollection<TopListInfoUIModel> ListMinerTopList
        {
            get { return _listMinerTopList; }
        }

        private ObservableCollection<TopListInfoUIModel> _listReferrerCountTopList = new ObservableCollection<TopListInfoUIModel>();

        public ObservableCollection<TopListInfoUIModel> ListReferrerCountTopList
        {
            get { return _listReferrerCountTopList; }
        }


        public void AsyncGetExpTopList()
        {
            App.BusyToken.ShowBusyWindow("正在加载排行榜...");
            GlobalData.Client.GetExpTopList();
        }

        public void AsyncGetStoneTopList()
        {
            App.BusyToken.ShowBusyWindow("正在加载排行榜...");
            GlobalData.Client.GetStoneTopList();
        }

        public void AsyncGetMinerTopList()
        {
            App.BusyToken.ShowBusyWindow("正在加载排行榜...");
            GlobalData.Client.GetMinerTopList();
        }

        public void AsyncGetGoldCoinTopList()
        {
            App.BusyToken.ShowBusyWindow("正在加载排行榜...");
            GlobalData.Client.GetGoldCoinTopList();
        }

        public void AsyncGetReferrerTopList()
        {
            App.BusyToken.ShowBusyWindow("正在加载排行榜...");
            GlobalData.Client.GetReferrerTopList();
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetExpTopListCompleted += Client_GetExpTopListCompleted;
            GlobalData.Client.GetStoneTopListCompleted += Client_GetStoneTopListCompleted;
            GlobalData.Client.GetMinerTopListCompleted += Client_GetMinerTopListCompleted;
            GlobalData.Client.GetGoldCoinTopListCompleted += Client_GetGoldCoinTopListCompleted;
            GlobalData.Client.GetReferrerTopListCompleted += Client_GetReferrerTopListCompleted;
        }

        void Client_GetReferrerTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
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
                    MyMessageBox.ShowInfo("查询排行榜失败。");
                    return;
                }

                this.ListReferrerCountTopList.Clear();
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var item = e.Result[i];
                    this.ListReferrerCountTopList.Add(new TopListInfoUIModel(i, item));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetGoldCoinTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
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
                    MyMessageBox.ShowInfo("查询排行榜失败。");
                    return;
                }

                this.ListGoldCoinTopList.Clear();
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var item = e.Result[i];
                    this.ListGoldCoinTopList.Add(new TopListInfoUIModel(i, item));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetMinerTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
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
                    MyMessageBox.ShowInfo("查询排行榜失败。");
                    return;
                }

                this.ListMinerTopList.Clear();
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var item = e.Result[i];
                    this.ListMinerTopList.Add(new TopListInfoUIModel(i, item));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetStoneTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
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
                    MyMessageBox.ShowInfo("查询排行榜失败。");
                    return;
                }

                this.ListStoneTopList.Clear();
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var item = e.Result[i];
                    this.ListStoneTopList.Add(new TopListInfoUIModel(i, item));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        void Client_GetExpTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
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
                    MyMessageBox.ShowInfo("查询排行榜失败。");
                    return;
                }

                this.ListExpTopList.Clear();
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var item = e.Result[i];
                    this.ListExpTopList.Add(new TopListInfoUIModel(i, item));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

    }
}
