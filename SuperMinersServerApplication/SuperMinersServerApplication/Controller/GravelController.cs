using DataBaseProvider;
using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class GravelController
    {
        #region Single Stone

        private static GravelController _instance = new GravelController();

        public static GravelController Instance
        {
            get { return _instance; }
        }

        private GravelController()
        {

        }

        #endregion

        private DateTime exeDistributeTime = new DateTime(2000, 1, 1, 0, 0, 0);
        //private bool todayIsDistributed = false;

        public void Init()
        {
#if !V1

            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(2),
                DailyTime = new DateTime(2000, 1, 1, 0, 0, 0),
                Task = DistributeGravel
            });

#endif
        }

        public void DistributeGravel()
        {
            try
            {
                //每天只执行一次
                DateTime nowtime = DateTime.Now;
                if (nowtime.DayOfYear == this.exeDistributeTime.DayOfYear)
                {
                    return;
                }
                this.exeDistributeTime = nowtime;

                PlayerGravelRequsetRecordInfo[] records = DBProvider.GravelDBProvider.GetLastDayPlayerGravelRequsetRecords(new MetaData.MyDateTime(DateTime.Now.AddDays(-1)), -1);
                if (records == null || records.Length == 0)
                {
                    return;
                }

                int playerCount = DBProvider.UserDBProvider.GetAllPlayerCount();

                GravelDistributeRecordInfo distributeRecord = new GravelDistributeRecordInfo();
                distributeRecord.AllPlayerCount = playerCount;
                distributeRecord.CreateDate = new MetaData.MyDateTime(DateTime.Now);
                distributeRecord.RequestPlayerCount = records.Length;

                int gravel = playerCount / records.Length;
                if (gravel < GlobalConfig.GameConfig.GravelMin)
                {
                    gravel = GlobalConfig.GameConfig.GravelMin;
                }
                foreach (var item in records)
                {
                    item.Gravel = gravel;
                    item.IsResponsed = true;
                    item.ResponseDate = distributeRecord.CreateDate;
                }

                MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                {
                    DBProvider.GravelDBProvider.UpdatePlayerGravelRequsetRecords(records, myTrans);
                    DBProvider.GravelDBProvider.SaveGravelDistributeRecordInfo(distributeRecord, myTrans);
                    return OperResult.RESULTCODE_TRUE;
                },
                exc =>
                {
                    LogHelper.Instance.AddErrorLog("GravelController.DistributeGravel Save ToDB Transaction Exception", exc);
                });

                if (PlayerGravelInfoChanged != null)
                {
                    foreach (var item in records)
                    {
                        PlayerGravelInfoChanged(item.UserName);
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GravelController.DistributeGravel Exception", exc);
            }
        }

        public int RequestGravel(int userID)
        {
            PlayerGravelRequsetRecordInfo[] records = DBProvider.GravelDBProvider.GetLastDayPlayerGravelRequsetRecords(new MetaData.MyDateTime(DateTime.Now), userID);
            if (records != null && records.Length > 0)
            {
                return OperResult.RESULTCODE_GRAVEL_REQUESTFAILED_TODAYREQUIED;
            }

            PlayerGravelRequsetRecordInfo request = new PlayerGravelRequsetRecordInfo();
            request.UserID = userID;
            request.RequestDate = new MyDateTime(DateTime.Now);
            DBProvider.GravelDBProvider.CreatePlayerGravelRequestRecord(request);

            return OperResult.RESULTCODE_TRUE;
        }

        public PlayerGravelRequsetRecordInfo GetGravel(int userID, CustomerMySqlTransaction myTrans, out int result)
        {
            //领取比请求要晚一天
            PlayerGravelRequsetRecordInfo[] records = DBProvider.GravelDBProvider.GetLastDayPlayerGravelRequsetRecords(new MetaData.MyDateTime(DateTime.Now.AddDays(-1)), userID);
            if (records == null || records.Length == 0)
            {
                result = OperResult.RESULTCODE_GRAVEL_GETFAILED_NOTHINGTOGET;
                return null;
            }

            PlayerGravelRequsetRecordInfo record = records[0];
            if (record.IsGoted)
            {
                result = OperResult.RESULTCODE_GRAVEL_GETFAILED_NOTHINGTOGET;
                return null;
            }
            record.IsGoted = true;
            DBProvider.GravelDBProvider.SetPlayerGravelRequsetRecordInfoIsGoted(record, myTrans);
            result = OperResult.RESULTCODE_TRUE;
            return record;
        }

        public event Action<string> PlayerGravelInfoChanged;
    }
}
