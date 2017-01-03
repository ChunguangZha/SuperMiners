using MetaData;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    public class GameRaiderofLostArkViewModel
    {
        private Thread _thrRefreshCurrentRound = null;

        private RaiderRoundMetaDataInfoUIModel _currentRaiderRound = new RaiderRoundMetaDataInfoUIModel(new MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo());

        public RaiderRoundMetaDataInfoUIModel CurrentRaiderRound
        {
            get
            {
                return this._currentRaiderRound;
            }
        }

        private ObservableCollection<PlayerBetInfoUIModel> _listSelfBetRecords = new ObservableCollection<PlayerBetInfoUIModel>();

        public ObservableCollection<PlayerBetInfoUIModel> ListSelfBetRecords
        {
            get { return _listSelfBetRecords; }
        }

        private ObservableCollection<RaiderRoundMetaDataInfoUIModel> _listHistoryRaiderRoundRecords = new ObservableCollection<RaiderRoundMetaDataInfoUIModel>();

        public ObservableCollection<RaiderRoundMetaDataInfoUIModel> ListHistoryRaiderRoundRecords
        {
            get { return _listHistoryRaiderRoundRecords; }
        }


        public void AsyncGetCurrentRaiderRoundInfo()
        {
            GlobalData.Client.GetCurrentRaiderRoundInfo(null);
        }

        public void AsyncJoinRaider(int roundID, int betStoneCount)
        {
            App.BusyToken.ShowBusyWindow("正在提交数据...");
            GlobalData.Client.JoinRaider(roundID, betStoneCount, null);
        }

        public void AsyncGetHistoryRaiderRoundRecords(int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载数据...");
            GlobalData.Client.GetHistoryRaiderRoundRecords(pageItemCount, pageIndex, pageIndex);
        }

        public void AsyncGetSelfCurrentRoundBetInfos()
        {
            if (this.CurrentRaiderRound == null || this.CurrentRaiderRound.ID == 0)
            {
                return;
            }
            App.BusyToken.ShowBusyWindow("正在加载数据...");
            GlobalData.Client.GetPlayerselfBetInfo(this.CurrentRaiderRound.ID, -1, -1, null);
        }

        private bool isStartedListen = false;

        private void RefreshCurrentRound()
        {
            while (true)
            {
                if (!isStartedListen)
                {
                    break;
                }

                Thread.Sleep(1000);
                AsyncGetCurrentRaiderRoundInfo();
            }
        }

        public void StopListen()
        {
            if (isStartedListen)
            {
                isStartedListen = false;
            }
        }

        public void RegisterEvents()
        {
            //GlobalData.Client.GetCurrentRaiderRoundInfoCompleted += Client_GetCurrentRaiderRoundInfoCompleted;
            //GlobalData.Client.JoinRaiderCompleted += Client_JoinRaiderCompleted;
            //GlobalData.Client.OnPlayerJoinRaiderSucceed += Client_OnPlayerJoinRaiderSucceed;
            //GlobalData.Client.OnPlayerWinedRaiderNotify += Client_OnPlayerWinedRaiderNotify;
            //GlobalData.Client.GetHistoryRaiderRoundRecordsCompleted += Client_GetHistoryRaiderRoundRecordsCompleted;
            //GlobalData.Client.GetPlayerselfBetInfoCompleted += Client_GetPlayerselfBetInfoCompleted;

            //isStartedListen = true;
            //this._thrRefreshCurrentRound = new Thread(RefreshCurrentRound);
            //this._thrRefreshCurrentRound.IsBackground = true;
            //this._thrRefreshCurrentRound.Name = "thrRefreshCurrentRound";
            //this._thrRefreshCurrentRound.Start();
        }

        void Client_GetPlayerselfBetInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.RaideroftheLostArk.PlayerBetInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("Client_GetPlayerselfBetInfoCompleted Server Return Exception", e.Error);
                    return;
                }

                this.ListSelfBetRecords.Clear();
                foreach (var item in e.Result)
                {
                    this.ListSelfBetRecords.Add(new PlayerBetInfoUIModel(item));
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetPlayerselfBetInfoCompleted Exception", exc);
            }
        }

        void Client_GetHistoryRaiderRoundRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("Client_GetHistoryRaiderRoundRecordsCompleted Server Return Exception", e.Error);
                    return;
                }
                int pageIndex = Convert.ToInt32(e.UserState);
                if (pageIndex == 1)
                {
                    this.ListHistoryRaiderRoundRecords.Clear();
                }

                foreach (var item in e.Result)
                {
                    this.ListHistoryRaiderRoundRecords.Add(new RaiderRoundMetaDataInfoUIModel(item));
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetHistoryRaiderRoundRecordsCompleted Exception", exc);
            }
        }

        void Client_OnPlayerWinedRaiderNotify(MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo roundInfo)
        {
            try
            {
                //this.CurrentRaiderRound.ParentObject = roundInfo;
                //AsyncGetHistoryRaiderRoundRecords(GlobalData.PageItemsCount, 1);

                //if (SelfWinRoundID != e.Result.ID)
                //{
                //    SelfWinRoundID = e.Result.ID;
                //    MyMessageBox.ShowInfo(string.Format("恭喜您成为第{0}期夺宝奇兵得主，您赢得{1}矿石奖励", e.Result.ID, e.Result.WinStones));
                //}

                MyMessageBox.ShowInfo(string.Format("恭喜您成为第{0}期夺宝奇兵得主，您赢得{1}矿石奖励", roundInfo.ID, roundInfo.WinStones));

                App.UserVMObject.AsyncGetPlayerInfo();
                AsyncGetHistoryRaiderRoundRecords(GlobalData.PageItemsCount, 1);
                this.ListSelfBetRecords.Clear();
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetCurrentRaiderRoundInfoCompleted Exception", exc);
            }
        }

        void Client_OnPlayerJoinRaiderSucceed(MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo roundInfo)
        {
            try
            {
                this.CurrentRaiderRound.ParentObject = roundInfo;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetCurrentRaiderRoundInfoCompleted Exception", exc);
            }
        }

        void Client_JoinRaiderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("Client_JoinRaiderCompleted Server Return Exception", e.Error);
                    MyMessageBox.ShowInfo("下注夺宝奇兵服务器返回失败，原因为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("下注成功，请等待开奖");
                    AsyncGetSelfCurrentRoundBetInfos();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("下注失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetCurrentRaiderRoundInfoCompleted Exception", exc);
            }
        }

        private int SelfWinRoundID = -1;

        void Client_GetCurrentRaiderRoundInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo> e)
        {
            try
            {
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("Client_GetCurrentRaiderRoundInfoCompleted Server Return Exception", e.Error);
                    return;
                }

                this.CurrentRaiderRound.ParentObject = e.Result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Client_GetCurrentRaiderRoundInfoCompleted Exception", exc);
            }
        }

    }
}
