using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    public static class StoneFactoryConfig
    {
        /// <summary>
        /// 开启一次矿石加工厂，需要1000积分
        /// </summary>
        public static int OpenFactoryNeedShoppingCredit = 1000;

        /// <summary>
        /// 矿石加工厂投入一万矿石为一股
        /// </summary>
        public static int StoneFactoryStone_Stack = 10000;

        /// <summary>
        /// 一组奴隶有100个矿石
        /// </summary>
        public static int OneGroupSlaveHasMiners = 100;

        /// <summary>
        /// 奴隶默认寿命2天，2天后需要补充食物
        /// </summary>
        public static int SlaveDefaultLiveDays = 2;

        /// <summary>
        /// 给奴隶投喂一次食物，可以存活秒数(两天)
        /// </summary>
        public static int OnceFeedFoodSlaveCanLivems = 172800;

        /// <summary>
        /// 工厂在没有矿石和奴隶的情况下，寿命3天
        /// </summary>
        public static int FactoryLiveDays = 3;

        /// <summary>
        /// 收益提现限制天数18天，即收益到账18天后才可提现。
        /// </summary>
        public static int ProfitRMBWithdrawLimitDays = 18;

        /// <summary>
        /// 工厂里的矿石可以取回的限制天数30天，即投入的矿石在30天后才可取回
        /// </summary>
        public static int StoneStackWithdrawLimitDays = 30;

        /// <summary>
        /// 新加入的矿石需冻结1天后，才可以投入生产
        /// </summary>
        public static int StoneFactoryStoneFreezingDays = 1;

        /// <summary>
        /// 工厂收益，给上级玩家反利点（返三级）
        /// </summary>
        public static decimal[] FactoryOutputProfitAwardRMBConfig = new decimal[] { 0.08m, 0.05m, 0.03m };

        /// <summary>
        /// 每一万矿石每天最小收益灵币
        /// </summary>
        public static decimal DailyMinProfit = 0m;

    }
}
