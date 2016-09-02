using MetaData.ActionLog;
using MetaData.SystemConfig;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class MessageViewModel : INotifyPropertyChanged
    {
        //bool isStartedListen = false;

        //private Thread _threadListen;

        private int _systemAllPlayerCount;
        public int SystemAllPlayerCount
        {
            get { return this._systemAllPlayerCount; }
            private set
            {
                this._systemAllPlayerCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SystemAllPlayerCount"));
                }
            }
        }

        private int _systemAllMinerCount;
        public int SystemAllMinerCount
        {
            get { return this._systemAllMinerCount; }
            private set
            {
                this._systemAllMinerCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SystemAllMinerCount"));
                }
            }
        }

        public decimal _systemAllOutputStoneCount;
        public decimal SystemAllOutputStoneCount
        {
            get { return this._systemAllOutputStoneCount; }
            private set
            {
                this._systemAllOutputStoneCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SystemAllOutputStoneCount"));
                }
            }
        }

        private int LogMaxCount = 100;
        private ObservableCollection<PlayerActionLogUIModel> _listPlayerActionLog = new ObservableCollection<PlayerActionLogUIModel>();

        public ObservableCollection<PlayerActionLogUIModel> ListPlayerActionLog
        {
            get { return this._listPlayerActionLog; }
        }

        public void AsyncGetSystemConfig()
        {
            GlobalData.Client.GetGameConfig();
        }

        public void AsyncGetPlayerAction()
        {
            try
            {
                DateTime dtLast;
                if (ListPlayerActionLog.Count == 0)
                {
                    //取24小时之前的消息
                    dtLast = DateTime.Now.AddDays(-1);
                }
                else
                {
                    dtLast = ListPlayerActionLog[0].Time;
                }

                App.BusyToken.ShowBusyWindow("正在加载数据...");
                GlobalData.Client.GetPlayerAction(dtLast.Year, dtLast.Month, dtLast.Day, dtLast.Hour, dtLast.Minute, dtLast.Second);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetPlayerAction error.", exc);
            }
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetPlayerActionCompleted += Client_GetPlayerActionCompleted;
            GlobalData.Client.GetGameConfigCompleted += Client_GetGameConfigCompleted;
            GlobalData.Client.OnSendPlayerActionLog += Client_OnSendPlayerActionLog;
            GlobalData.Client.OnSendGameConfig += Client_OnSendGameConfig;
        }

        void Client_GetGameConfigCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.SystemConfig.SystemConfigin1> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            bool isOK = true;
            if (e.Error != null || e.Result == null ||
                e.Result.AwardReferrerConfigList == null ||
                e.Result.GameConfig == null ||
                e.Result.RegisterUserConfig == null)
            {
                MyMessageBox.ShowInfo("获取配置信息失败。");

                isOK = false;
            }
            else
            {
                GlobalData.GameConfig = e.Result.GameConfig;
                GlobalData.RegisterUserConfig = e.Result.RegisterUserConfig;
                GlobalData.AwardReferrerLevelConfig = new AwardReferrerLevelConfig();
                GlobalData.AwardReferrerLevelConfig.SetListAward(new List<AwardReferrerConfig>(e.Result.AwardReferrerConfigList));
                isOK = true;
            }

            if (this.GetSystemConfigCompleted != null)
            {
                this.GetSystemConfigCompleted(isOK);
            }
        }

        void Client_OnSendGameConfig()
        {
            GlobalData.Client.GetGameConfig();
        }

        void Client_OnSendPlayerActionLog()
        {
            AsyncGetPlayerAction();
        }

        void Client_GetPlayerActionCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.ActionLog.PlayerActionLog[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Cancelled)
            {
                return;
            }            

            if (e.Error != null || e.Result == null || e.Result.Length == 0)
            {
                return;
            }

            //服务器返回的记录是按时间升序排列。
            //需要将其降序显示
            var lastLogFromServer = e.Result[e.Result.Length - 1];
            this.SystemAllMinerCount = lastLogFromServer.SystemAllMinerCount;
            this.SystemAllOutputStoneCount = lastLogFromServer.SystemAllOutputStoneCount;
            this.SystemAllPlayerCount = lastLogFromServer.SystemAllPlayerCount;

            if (ListPlayerActionLog.Count >= this.LogMaxCount)
            {
                int deleteLastNo = ListPlayerActionLog.Count - e.Result.Length;
                if (deleteLastNo < 0) deleteLastNo = 0;

                //从后往前删
                for (int i = ListPlayerActionLog.Count - 1; i >= deleteLastNo; i--)
                {
                    ListPlayerActionLog.RemoveAt(i);
                }
            }

            PlayerActionLogUIModel lastLogFromClient = null;
            if (ListPlayerActionLog.Count > 0)
            {
                lastLogFromClient = ListPlayerActionLog[0];
            }
            for (int i = 0; i < e.Result.Length; i++)
            {
                var log = e.Result[i];
                if (lastLogFromClient != null)
                {
                    if (lastLogFromClient.Time >= log.Time)
                    {
                        continue;
                    }
                }
                ListPlayerActionLog.Insert(0, new PlayerActionLogUIModel(log));
            }

            if (GetPlayerActionCompleted != null)
            {
                GetPlayerActionCompleted(null, null);
            }
        }

        public event EventHandler GetPlayerActionCompleted;
        public event Action<bool> GetSystemConfigCompleted;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
