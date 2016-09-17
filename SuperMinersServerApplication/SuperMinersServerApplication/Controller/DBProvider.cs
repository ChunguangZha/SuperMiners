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

        //public static RechargeDBProvider RechargeDBProvider = new RechargeDBProvider();

        public static AdminDBProvider AdminDBProvider = new AdminDBProvider();

        public static StoneOrderDBProvider StoneOrderDBProvider = new StoneOrderDBProvider();

        public static BuyMinerRecordDBProvider BuyMinerRecordDBProvider = new BuyMinerRecordDBProvider();

        public static MineRecordDBProvider MineRecordDBProvider = new MineRecordDBProvider();

        public static AlipayRecordDBProvider AlipayRecordDBProvider = new AlipayRecordDBProvider();

        public static GoldCoinRecordDBProvider GoldCoinRecordDBProvider = new GoldCoinRecordDBProvider();

        public static WaitToAwardExpRecordDBProvider WaitToAwardExpRecordDBProvider = new WaitToAwardExpRecordDBProvider();

        public static WithdrawRMBRecordDBProvider WithdrawRMBRecordDBProvider = new WithdrawRMBRecordDBProvider();

        public static ExpChangeRecordDBProvider ExpChangeRecordDBProvider = new ExpChangeRecordDBProvider();

        public static TestUserLogStateDBProvider TestUserLogStateDBProvider = new TestUserLogStateDBProvider();
    }
}
