using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class GravelController
    {
        public PlayerGravelInfo GetPlayerGravelInfo()
        {
            return null;
        }


        public void Init()
        {
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                //DailyTime = DateTime.Now.AddMinutes(2),
                DailyTime = new DateTime(2000, 1, 1, 0, 0, 0),
                Task = DistributeGravel
            });
        }

        public void DistributeGravel()
        {

        }
        
    }
}
