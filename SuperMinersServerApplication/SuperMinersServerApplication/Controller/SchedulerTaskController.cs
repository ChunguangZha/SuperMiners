using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class SchedulerTaskController
    {
        #region Single

        private static SchedulerTaskController _instance = new SchedulerTaskController();

        public static SchedulerTaskController Instance
        {
            get { return _instance; }
        }

        private SchedulerTaskController()
        {

        }

        #endregion


        private Timer _timer = null;

        private List<DailyTimerTask> _listTasks = new List<DailyTimerTask>();
        private object _lockTaskList = new object();

        public void Init()
        {
            _timer = new Timer(new TimerCallback(DoWork), null, 0, 10 * 1000);
        }

        public void DoWork(object state)
        {
            DateTime nowTime = DateTime.Now;

            try
            {
                lock (_lockTaskList)
                {
                    foreach (var item in _listTasks)
                    {
                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            //以分钟为准，以秒容易会被跳过
                            if (item.DailyTime.Hour == nowTime.Hour && item.DailyTime.Minute == nowTime.Minute)// && item.DailyTime.Second == nowTime.Second)
                            {
                                try
                                {
                                    item.Task();
                                }
                                catch (Exception exc)
                                {
                                    LogHelper.Instance.AddErrorLog("SchedulerTask inner " + item.Task.ToString() + " Exception.", exc);
                                }
                            }
                        }, item);
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("SchedulerTask DoWork Exception.", exc);
            }
        }

        public bool JoinTask(DailyTimerTask newTask)
        {
            lock (_lockTaskList)
            {
                _listTasks.Add(newTask);
            }

            return true;
        }
    }

    public class DailyTimerTask
    {
        public DateTime DailyTime;

        public Action Task;
    }

}
