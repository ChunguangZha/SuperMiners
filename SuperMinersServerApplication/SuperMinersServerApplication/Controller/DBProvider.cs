using DataBaseProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public static class DBProvider
    {
        public static UserInfoDBProvider UserDBProvider = new UserInfoDBProvider();

        public static SystemDBProvider SystemDBProvider = new SystemDBProvider();

        public static RechargeDBProvider RechargeDBProvider = new RechargeDBProvider();

        public static AdminDBProvider AdminDBProvider = new AdminDBProvider();

        public static OrderDBProvider OrderDBProvider = new OrderDBProvider();

        public static BuyMinerRecordDBProvider BuyMinerRecordDBProvider = new BuyMinerRecordDBProvider();
    }
}
