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
        private XunLingMineStateInfoUIModel _AllSystemState = new XunLingMineStateInfoUIModel(new XunLingMineStateInfo());

        public XunLingMineStateInfoUIModel AllSystemState
        {
            get { return _AllSystemState; }
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

        public void AsyncGetAllXunLingMineFortuneState()
        {
            GlobalData.Client.GetAllXunLingMineFortuneState();
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

                //App.BusyToken.ShowBusyWindow("正在加载数据...");
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
            GlobalData.Client.GetAllXunLingMineFortuneStateCompleted += Client_GetAllXunLingMineFortuneStateCompleted;
        }

        void Client_GetAllXunLingMineFortuneStateCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<XunLingMineStateInfo> e)
        {
            try
            {
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取矿场信息失败。");
                    return;
                }

                this.AllSystemState.ParentObject = e.Result;

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取矿场信息失败。");
                LogHelper.Instance.AddErrorLog("获取矿场信息失败。", exc);
            }
        }

        void Client_GetGameConfigCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.SystemConfig.SystemConfigin1> e)
        {
            try
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

                AsyncGetAllXunLingMineFortuneState();

                if (this.GetSystemConfigCompleted != null)
                {
                    this.GetSystemConfigCompleted(isOK);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
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
            try
            {
                //App.BusyToken.CloseBusyWindow();
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

                //PlayerActionLogUIModel lastLogFromClient = null;
                //if (ListPlayerActionLog.Count > 0)
                //{
                //    lastLogFromClient = ListPlayerActionLog[0];
                //}
                for (int i = 0; i < e.Result.Length; i++)
                {
                    var newLog = e.Result[i];
                    if (!this.JudgeLogExists(newLog))
                    {
                        ListPlayerActionLog.Insert(0, new PlayerActionLogUIModel(newLog));
                    }
                }

                if (GetPlayerActionCompleted != null)
                {
                    GetPlayerActionCompleted(null, null);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("服务器连接失败。");
                LogHelper.Instance.AddErrorLog("服务器连接失败。", exc);
            }
        }

        private bool JudgeLogExists(PlayerActionLog newLog)
        {
            bool isExists = false;
            for (int i = 0; i < this.ListPlayerActionLog.Count; i++)
            {
                var oldLog = this.ListPlayerActionLog[i];
                if (oldLog.Time < newLog.Time)
                {
                    isExists = false;
                    break;
                }
                if (oldLog.Time == newLog.Time)
                {
                    if (oldLog.ParentObject.UserName == newLog.UserName
                        && oldLog.ParentObject.ActionType == newLog.ActionType
                        && oldLog.ParentObject.OperNumber == newLog.OperNumber
                        && oldLog.ParentObject.Remark == newLog.Remark)
                    {
                        isExists = true;
                        break;
                    }
                }
            }

            return isExists;
        }

        public event EventHandler GetPlayerActionCompleted;
        public event Action<bool> GetSystemConfigCompleted;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
