using MetaData;
using MetaData.Game.GambleStone;
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
    public class GambleStoneViewModel : BaseModel
    {
        private Thread _thrRefreshCurrentRound = null;

        private bool isStartedListen = false;

        private GambleStoneRoundInfoUIModel _currentRoundInfo = new GambleStoneRoundInfoUIModel(new MetaData.Game.GambleStone.GambleStoneRoundInfo());

        public GambleStoneRoundInfoUIModel CurrentRoundInfo
        {
            get
            {
                return this._currentRoundInfo;
            }
        }

        private GambleStoneInningInfoUIModel _currentInningInfo = new GambleStoneInningInfoUIModel(new MetaData.Game.GambleStone.GambleStoneInningInfo());

        public GambleStoneInningInfoUIModel CurrentInningInfo
        {
            get
            {
                return this._currentInningInfo;
            }
        }

        private GambleStonePlayerBetRecordUIModel _currentInningPlayerBetRecord = new GambleStonePlayerBetRecordUIModel(new GambleStonePlayerBetRecord());

        public GambleStonePlayerBetRecordUIModel CurrentInningPlayerBetRecord
        {
            get { return _currentInningPlayerBetRecord; }
        }

        private ObservableCollection<GambleStonePlayerBetRecordUIModel> _listPlayerBetRecords = new ObservableCollection<GambleStonePlayerBetRecordUIModel>();

        public ObservableCollection<GambleStonePlayerBetRecordUIModel> ListPlayerBetRecords
        {
            get { return _listPlayerBetRecords; }
        }
        


        public GambleStoneViewModel()
        {
        }

        public void Init()
        {
            AsyncGetCurrentGambleStoneRoundInfo();
            isStartedListen = true;
            this._thrRefreshCurrentRound = new Thread(RefreshCurrentRound);
            this._thrRefreshCurrentRound.IsBackground = true;
            this._thrRefreshCurrentRound.Name = "thrGambleStoneRefreshCurrentRound";
            this._thrRefreshCurrentRound.Start();
        }

        private void RefreshCurrentRound()
        {
            while (true)
            {
                if (!isStartedListen)
                {
                    break;
                }

                Thread.Sleep(1000);

                if (GlobalData.IsLogined)
                {
                    AsyncGetCurrentGambleStoneInningInfo();
                }
            }
        }

        public void StopListen()
        {
            if (isStartedListen)
            {
                isStartedListen = false;
            }
        }

        public void AsyncGetCurrentGambleStoneInningInfo()
        {
            GlobalData.Client.GetGambleStoneInningInfo(null);
        }

        public void AsyncGetCurrentGambleStoneRoundInfo()
        {
            GlobalData.Client.GetGambleStoneRoundInfo(null);
        }

        public void AsyncBetIn(GambleStoneItemColor color, int stoneCount, int gravelCount)
        {
            //App.BusyToken.ShowBusyWindow("正在下注...");
            GlobalData.Client.GambleStoneBetIn(color, stoneCount, gravelCount, null);
        }

        public void AsyncGetLastMonthGambleStonePlayerBetRecord()
        {
            App.BusyToken.ShowBusyWindow("正在加载数据...");
            GlobalData.Client.GetLastMonthGambleStonePlayerBetRecord(null);
        }

        public void RegisterEvents()
        {
            //GlobalData.Client.GetGambleStoneRoundInningCompleted += Client_GetGambleStoneRoundInningCompleted;
            GlobalData.Client.GambleStoneBetInCompleted += Client_GambleStoneBetInCompleted;
            GlobalData.Client.OnGambleStoneWinNotify += Client_OnGambleStoneWinNotify;
            GlobalData.Client.GetGambleStoneRoundInfoCompleted += Client_GetGambleStoneRoundInfoCompleted;
            GlobalData.Client.GetGambleStoneInningInfoCompleted += Client_GetGambleStoneInningInfoCompleted;
            GlobalData.Client.GetLastMonthGambleStonePlayerBetRecordCompleted += Client_GetLastMonthGambleStonePlayerBetRecordCompleted;     
        }

        void Client_GetLastMonthGambleStonePlayerBetRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<GambleStonePlayerBetRecord[]> e)
        {
            try
            {
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("查询下注记录失败，服务器返回失败。", e.Error);
                    return;
                }

                this.ListPlayerBetRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListPlayerBetRecords.Add(new GambleStonePlayerBetRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询下注记录失败。");
                LogHelper.Instance.AddErrorLog("查询下注记录失败。", exc);
            }
        }

        void Client_GetGambleStoneInningInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<GambleStoneInningInfo> e)
        {
            try
            {
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("获取疯狂猜石Inning信息，服务器返回失败。", e.Error);
                    this.CurrentInningInfo.ParentObject = null;
                    return;
                }

                this.CurrentInningInfo.ParentObject = e.Result;
                if (e.Result.InningIndex == 1)
                {
                    AsyncGetCurrentGambleStoneRoundInfo();
                }
                if (GambleStoneInningChanged != null)
                {
                    GambleStoneInningChanged(e.Result);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取疯狂猜石信息失败。");
                LogHelper.Instance.AddErrorLog("获取疯狂猜石Inning信息失败。", exc);
            }
        }

        void Client_GetGambleStoneRoundInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<GambleStoneRoundInfo> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    LogHelper.Instance.AddErrorLog("获取疯狂猜石Round信息，服务器返回失败。", e.Error);
                    this.CurrentRoundInfo.ParentObject = null;
                    return;
                }

                this.CurrentRoundInfo.ParentObject = e.Result;
                if (this.GambleStoneGetRoundInfoEvent != null)
                {
                    this.GambleStoneGetRoundInfoEvent(e.Result);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取疯狂猜石信息失败。");
                LogHelper.Instance.AddErrorLog("获取疯狂猜石Round信息失败。", exc);
            }
        }

        void Client_OnGambleStoneWinNotify(GambleStoneRoundInfo roundInfo, GambleStoneInningInfo inningInfo, GambleStonePlayerBetRecord maxWinner)
        {
            try
            {
                //this.CurrentRoundInfo.ParentObject = roundInfo;
                this.CurrentInningInfo.ParentObject = inningInfo;
                this.CurrentRoundInfo.SetWinnedInningColor(inningInfo.InningIndex - 1, inningInfo.WinnedColor);

                if (GambleStoneInningFinished != null)
                {
                    GambleStoneInningFinished(inningInfo, maxWinner);
                }

                if (!string.IsNullOrEmpty(this.CurrentInningPlayerBetRecord.InningID))
                {
                    this.CurrentInningPlayerBetRecord.Clear();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }

                //if (maxWinner != null && !string.IsNullOrEmpty(maxWinner.UserName))
                //{
                //    MyMessageBox.ShowInfo(maxWinner.UserName + " 赢得 " + maxWinner.WinnedStone + " 矿石");
                //}
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("疯狂猜石开奖失败。");
                LogHelper.Instance.AddErrorLog("疯狂猜石开奖失败。", exc);
            }
        }

        void Client_GambleStoneBetInCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<GambleStonePlayerBetInResult> e)
        {
            try
            {
                //App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("疯狂猜石下注失败。服务器返回错误。");
                    LogHelper.Instance.AddErrorLog("疯狂猜石下注失败。服务器返回错误。", e.Error);
                    return;
                }
                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("疯狂猜石下注失败。");
                    return;
                }
                if (e.Result.ResultCode != OperResult.RESULTCODE_TRUE)
                {
                    //MyMessageBox.ShowInfo("赌石娱乐下游失败。原因为：" + OperResult.GetMsg(e.Result.ResultCode));
                    return;
                }

                this.CurrentInningPlayerBetRecord.ParentObject = e.Result.PlayerBetRecord;
                App.UserVMObject.AsyncGetPlayerInfo();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("疯狂猜石下注失败。回调处理异常");
                LogHelper.Instance.AddErrorLog("疯狂猜石下注失败。回调处理异常。", exc);
            }
        }

        public event Action<GambleStoneInningInfo> GambleStoneInningChanged;
        public event Action<GambleStoneInningInfo, GambleStonePlayerBetRecord> GambleStoneInningFinished;
        public event Action<GambleStoneRoundInfo> GambleStoneGetRoundInfoEvent;
    }
}
