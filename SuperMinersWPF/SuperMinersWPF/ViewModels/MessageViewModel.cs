using MetaData.ActionLog;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class MessageViewModel : INotifyPropertyChanged
    {
        bool isStartedListen = false;

        /// <summary>
        /// 每分钟执行一次
        /// </summary>
        private System.Timers.Timer _timerListenMessage = new System.Timers.Timer(1000 * 60);

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

        public void StartListen()
        {
            if (!isStartedListen)
            {
                isStartedListen = true;
                _timerListenMessage.Elapsed += TimerListenMessage_Elapsed;
                _timerListenMessage.Start();
            }
        }

        void TimerListenMessage_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AsyncGetPlayerAction();
        }

        public void StopListen()
        {
            if (isStartedListen)
            {
                isStartedListen = false;
                _timerListenMessage.Stop();
                _timerListenMessage.Elapsed -= TimerListenMessage_Elapsed;
            }
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

            }
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetPlayerActionCompleted += Client_GetPlayerActionCompleted;
        }

        void Client_GetPlayerActionCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.ActionLog.PlayerActionLog[]> e)
        {
            GlobalData.IsOffline = false;

            if (e.Cancelled)
            {
                return;
            }            

            if (e.Error != null || e.Result == null || e.Result.Length == 0)
            {
                return;
            }

            var lastLog = e.Result[e.Result.Length - 1];
            this.SystemAllMinerCount = lastLog.SystemAllMinerCount;
            this.SystemAllOutputStoneCount = lastLog.SystemAllOutputStoneCount;
            this.SystemAllPlayerCount = lastLog.SystemAllPlayerCount;

            if (ListPlayerActionLog.Count >= this.LogMaxCount)
            {
                for (int i = 0; i < e.Result.Length; i++)
                {
                    ListPlayerActionLog.RemoveAt(0);
                }
            }

            for (int i = 0; i < e.Result.Length; i++)
            {
                var log = e.Result[i];
                ListPlayerActionLog.Add(new PlayerActionLogUIModel(log));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
