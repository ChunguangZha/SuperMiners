using SuperMinersCustomServiceSystem.Model;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    public class GameRouletteViewModel
    {
        private ObservableCollection<RouletteAwardItemUIModel> _listAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListAwardItems
        {
            get { return _listAwardItems; }
            set { _listAwardItems = value; }
        }

        private ObservableCollection<string> _listActiveWinAwardInfos = new ObservableCollection<string>();

        public ObservableCollection<string> ListActiveWinAwardInfos
        {
            get { return _listActiveWinAwardInfos; }
            set { _listActiveWinAwardInfos = value; }
        }

        private ObservableCollection<RouletteWinnerRecordUIModel> _listWinAwardRecords = new ObservableCollection<RouletteWinnerRecordUIModel>();

        public ObservableCollection<RouletteWinnerRecordUIModel> ListWinAwardRecords
        {
            get { return _listWinAwardRecords; }
            set { _listWinAwardRecords = value; }
        }

        public GameRouletteViewModel()
        {
            GlobalData.Client.GetAwardItemsCompleted += Client_GetAwardItemsCompleted;
            GlobalData.Client.GetAllWinAwardRecordsCompleted += Client_GetAllWinAwardRecordsCompleted;
            GlobalData.Client.OnGameRouletteWinNotify += Client_OnGameRouletteWinNotify;
            GlobalData.Client.OnGameRouletteWinRealAwardPaySucceed += Client_OnGameRouletteWinRealAwardPaySucceed;
        }

        void Client_OnGameRouletteWinRealAwardPaySucceed(MetaData.Game.Roulette.RouletteWinnerRecord obj)
        {
            try
            {
                if (obj == null)
                {
                    LogHelper.Instance.AddErrorLog("幸运大转盘中奖支付，服务器回调返回空对象.", null);
                }
                MyMessageBox.ShowInfo("您在幸运大转盘中摇中的[" + obj.AwardItem.AwardName + "]大奖，平台已经成功支付，敬请查收");
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("幸运大转盘中奖支付，服务器回调返回后处理异常.", exc);
            }
        }

        void Client_OnGameRouletteWinNotify(string obj)
        {
            try
            {
                if (obj != null)
                {
                    if (this.ListActiveWinAwardInfos.Count > 10)
                    {
                        this.ListActiveWinAwardInfos.RemoveAt(this.ListActiveWinAwardInfos.Count - 1);
                    }
                    this.ListActiveWinAwardInfos.Insert(0, obj);
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_OnGameRouletteWinNotify Exceptioin.", exc);
            }
        }

        void Client_GetAllWinAwardRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteWinnerRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘中奖记录，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this._listWinAwardRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this._listWinAwardRecords.Add(new RouletteWinnerRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘中奖记录，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_GetAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteAwardItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘奖项，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListAwardItems.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListAwardItems.Add(new RouletteAwardItemUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘奖项，返回后处理异常。异常信息：" + exc.Message);
            }
        }
    }
}
