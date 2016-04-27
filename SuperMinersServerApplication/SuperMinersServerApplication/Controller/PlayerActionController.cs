using MetaData.ActionLog;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class PlayerActionController
    {
        #region Single

        private static PlayerActionController _instance = new PlayerActionController();

        public static PlayerActionController Instance
        {
            get
            {
                return _instance;
            }
        }

        private PlayerActionController()
        {

        }

        #endregion

        private List<PlayerActionLog> list = new List<PlayerActionLog>();
        private int maxListCount = 100;
        private object _lockList = new object();

        public void AddLog(string userName, ActionType actionType, int operNumber, string remark = "")
        {
            try
            {
                PlayerActionLog log = new PlayerActionLog()
                {
                    UserName = userName,
                    ActionType = actionType,
                    OperNumber = operNumber,
                    Time = DateTime.Now,
                    Remark = remark
                };

                lock (this._lockList)
                {
                    if (list.Count >= maxListCount)
                    {
                        list.RemoveRange(0, 10);
                    }
                    list.Add(log);
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Add PlayerAction Controller Exception. UserName=" + userName
                    + ", ActionType=" + actionType.ToString() + ", operNumber=" + operNumber.ToString() 
                    + ", remark=" + remark, exc);
            }
        }

        /// <summary>
        /// 最多返回100条记录
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public PlayerActionLog[] GetActionLogList(int year, int month, int day, int hour, int minute, int second)
        {
            DateTime time = new DateTime(year, month, day, hour, minute, second);

            lock (this._lockList)
            {
                var resultLogs = list.TakeWhile(l =>
                {
                    return l.Time >= time;
                });

                return resultLogs.ToArray<PlayerActionLog>();
            }
        }
    }
}
