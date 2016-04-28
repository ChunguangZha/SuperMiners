using MetaData.User;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class UserViewModel
    {
        public event EventHandler GetPlayerInfoCompleted;

        bool isStartedListen = false;

        /// <summary>
        /// 每分钟执行一次
        /// </summary>
        private System.Timers.Timer _timerUpdateStoneOutput = new System.Timers.Timer(1000 * 60);

        public void StartListen()
        {
            if (!isStartedListen)
            {
                isStartedListen = true;
                _timerUpdateStoneOutput.Elapsed += TimerUpdateStoneOutput_Elapsed;
                _timerUpdateStoneOutput.Start();
            }
        }

        void TimerUpdateStoneOutput_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (GlobalData.IsLogined)
                {
                    GlobalData.CurrentUser.TempOutputStones += GlobalData.CurrentUser.AllOutputPerHour / 60f;
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("TimerUpdateStoneOutput_Elapsed", exc);
            }
        }

        public void StopListen()
        {
            if (isStartedListen)
            {
                isStartedListen = false;
                _timerUpdateStoneOutput.Stop();
                _timerUpdateStoneOutput.Elapsed -= TimerUpdateStoneOutput_Elapsed;
            }
        }

        public void AsyncGetPlayerInfo()
        {
            GlobalData.Client.GetPlayerInfo();
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetPlayerInfoCompleted += Client_GetPlayerInfoCompleted;
        }

        void Client_GetPlayerInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<PlayerInfo> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MyMessageBox.ShowInfo("获取用户信息失败。");
                GlobalData.Client.Logout();
                return;
            }

            GlobalData.InitUser(e.Result);

            if (GetPlayerInfoCompleted != null)
            {
                GetPlayerInfoCompleted(null, null);
            }
        }

    }
}
