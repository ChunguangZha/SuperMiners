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



        public void AsyncGetExpTopList()
        {
            GlobalData.Client.GetExpTopList();
        }

        public void AsyncGetStoneTopList()
        {
            GlobalData.Client.GetStoneTopList();
        }

        public void AsyncGetMinerTopList()
        {
            GlobalData.Client.GetMinerTopList();
        }

        public void AsyncGetGoldCoinTopList()
        {
            GlobalData.Client.GetGoldCoinTopList();
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetExpTopListCompleted += Client_GetExpTopListCompleted;
            GlobalData.Client.GetStoneTopListCompleted += Client_GetStoneTopListCompleted;
            GlobalData.Client.GetMinerTopListCompleted += Client_GetMinerTopListCompleted;
            GlobalData.Client.GetGoldCoinTopListCompleted += Client_GetGoldCoinTopListCompleted;
        }

        void Client_GetGoldCoinTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
        {
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

        void Client_GetMinerTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
        {
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

        void Client_GetStoneTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
        {
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

        void Client_GetExpTopListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.User.TopListInfo[]> e)
        {
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

    }
}
