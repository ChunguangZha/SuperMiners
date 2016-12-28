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

        public static GameRouletteDBProvider GameRouletteDBProvider = new GameRouletteDBProvider();

        public static AgentUserInfoDBProvider AgentUserInfoDBProvider = new AgentUserInfoDBProvider();

        public static AgentAwardRecordDBProvider AgentAwardRecordDBProvider = new AgentAwardRecordDBProvider();

        public static PlayerLoginInfoDBProvider PlayerLoginInfoDBProvider = new PlayerLoginInfoDBProvider();

        public static PlayerLockedInfoDBProvider PlayerLockedInfoDBProvider = new PlayerLockedInfoDBProvider();

        public static DeletedPlayerInfoDBProvider DeletedPlayerInfoDBProvider = new DeletedPlayerInfoDBProvider();

        public static StoneStackDBProvider StoneStackDBProvider = new StoneStackDBProvider();
    }
}
