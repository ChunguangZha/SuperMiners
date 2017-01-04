using MetaData.ActionLog;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

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

        public event Action PlayerActionAdded;
        
        private List<PlayerActionLog> list = new List<PlayerActionLog>();
        private int maxListCount = 100;
        private object _lockList = new object();

        public void AddLog(string userName, ActionType actionType, decimal operNumber, string remark = "")
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

                if (PlayerActionAdded != null)
                {
                    PlayerActionAdded();
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
                List<PlayerActionLog> listSearchResults = new List<PlayerActionLog>();
                foreach (var item in list)
                {
                    if (item.Time > time)
                    {
                        listSearchResults.Add(item);
                    }
                }

                PlayerActionLog[] actions = listSearchResults.ToArray();
                if (actions.Length > 0)
                {
                    PlayerActionLog lastLog = actions[actions.Length - 1];
                    //lastLog.SystemAllPlayerCount = DBProvider.UserDBProvider.GetAllPlayerCount();
                    //lastLog.SystemAllMinerCount = (int)DBProvider.UserDBProvider.GetAllMinersCount();
                    //lastLog.SystemAllOutputStoneCount = DBProvider.UserDBProvider.GetAllOutputStonesCount();
                }

                return actions;
            }
        }

        public void LoadActionLogs()
        {
            try
            {
                if (File.Exists(GlobalData.UserActionLogFile))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PlayerActionLog[]));
                    using (FileStream stream = File.Open(GlobalData.UserActionLogFile, FileMode.Open))
                    {
                        PlayerActionLog[] logs = xmlSerializer.Deserialize(stream) as PlayerActionLog[];
                        if (logs != null)
                        {
                            lock (this._lockList)
                            {
                                this.list = new List<PlayerActionLog>(logs);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public void SaveActionLogs()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(PlayerActionLog[]));
                using (FileStream stream = File.Open(GlobalData.UserActionLogFile, FileMode.Create))
                {
                    PlayerActionLog[] logs = this.list.ToArray();
                    xmlSerializer.Serialize(stream, logs);
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Save Action Logs Exception", exc);
            }
        }

    }
}
