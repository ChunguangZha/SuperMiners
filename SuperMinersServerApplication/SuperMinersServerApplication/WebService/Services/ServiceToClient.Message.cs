using MetaData;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {

        public MetaData.ActionLog.PlayerActionLog[] GetPlayerAction(string token, int year, int month, int day, int hour, int minute, int second)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return PlayerActionController.Instance.GetActionLogList(year, month, day, hour, minute, second);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerAction Exception. ClientIP=" + ClientManager.GetClientIP(token)
                        + ", year=" + year.ToString() + ", month=" + month.ToString() + ", day=" + day.ToString()
                        + ", hour=" + hour.ToString() + ", minute=" + minute.ToString() + ", second=" + second.ToString(),
                        exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public NoticeInfo[] GetNotices(string token, int year, int month, int day, int hour, int minute, int second)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return NoticeController.Instance.GetAllNotices();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetNotices Exception. ClientIP=" + ClientManager.GetClientIP(token)
                        + ", year=" + year.ToString() + ", month=" + month.ToString() + ", day=" + day.ToString()
                        + ", hour=" + hour.ToString() + ", minute=" + minute.ToString() + ", second=" + second.ToString(),
                        exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
