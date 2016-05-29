using MetaData.ActionLog;
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

        public float _systemAllOutputStoneCount;
        public float SystemAllOutputStoneCount
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
                    dtLast = ListPlayerActionLog[ListPlayerActionLog.Count - 1].Time;
                }

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
            GlobalData.Client.OnSendPlayerActionLog += Client_OnSendPlayerActionLog;
        }

        void Client_OnSendPlayerActionLog()
        {
            AsyncGetPlayerAction();
        }

        void Client_GetPlayerActionCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.ActionLog.PlayerActionLog[]> e)
        {
            if (e.Cancelled)
            {
                return;
            }            

            if (e.Error != null || e.Result == null || e.Result.Length == 0)
            {
                return;
            }

            var lastLogFromServer = e.Result[e.Result.Length - 1];
            this.SystemAllMinerCount = lastLogFromServer.SystemAllMinerCount;
            this.SystemAllOutputStoneCount = lastLogFromServer.SystemAllOutputStoneCount;
            this.SystemAllPlayerCount = lastLogFromServer.SystemAllPlayerCount;

            if (ListPlayerActionLog.Count >= this.LogMaxCount)
            {
                for (int i = 0; i < e.Result.Length; i++)
                {
                    ListPlayerActionLog.RemoveAt(0);
                }
            }

            PlayerActionLogUIModel lastLogFromClient = null;
            if (ListPlayerActionLog.Count > 0)
            {
                lastLogFromClient = ListPlayerActionLog[ListPlayerActionLog.Count - 1];
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
                ListPlayerActionLog.Add(new PlayerActionLogUIModel(log));
            }

            if (GetPlayerActionCompleted != null)
            {
                GetPlayerActionCompleted(null, null);
            }
        }

        public event EventHandler GetPlayerActionCompleted;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
