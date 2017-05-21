using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    [DataContract]
    public class GameConfig
    {
        /// <summary>
        /// 人民币兑换RMB
        /// </summary>
        [DataMember]
        public decimal Yuan_RMB = 10;

        /// <summary>
        /// RMB兑换金币
        /// </summary>
        [DataMember]
        public decimal RMB_GoldCoin = 1000;

        /// <summary>
        /// 购买矿山所需要的RMB
        /// </summary>
        [DataMember]
        public decimal RMB_Mine = 3000;

        /// <summary>
        /// 购买矿工所需要的金币
        /// </summary>
        [DataMember]
        public decimal GoldCoin_Miner = 10000;

        /// <summary>
        /// 多少矿石等价于1RMB
        /// </summary>
        [DataMember]
        public decimal Stones_RMB = 10;

        /// <summary>
        /// 一区钻石与灵币兑换比例（1钻石：1灵币）；二区钻石不与灵币有对应关系，只在商城使用
        /// </summary>
        [DataMember]
        public decimal Diamonds_RMB = 1;

        /// <summary>
        /// 多少积分等价于1RMB
        /// </summary>
        [DataMember]
        public int ShoppingCredits_RMB = 1;

        /// <summary>
        /// 每个矿工每小时生产矿石数
        /// </summary>
        [DataMember]
        public decimal OutputStonesPerHour = 0.138m;

        /// <summary>
        /// 矿山储量随机
        /// </summary>
        [DataMember]
        public bool MineReservesIsRandom = true;

        /// <summary>
        /// 每座矿山的矿石储量
        /// </summary>
        [DataMember]
        public decimal StonesReservesPerMines = 100000;

        /// <summary>
        /// VIP玩家 每座矿山的最小矿石储量
        /// </summary>
        [DataMember]
        public int MinStonesReservesPerMine_VIPPlayer = 90000;

        /// <summary>
        /// VIP玩家 每座矿山的最大矿石储量
        /// </summary>
        [DataMember]
        public int MaxStonesReservesPerMine_VIPPlayer = 120000;

        /// <summary>
        /// 每座矿山的最小矿石储量
        /// </summary>
        [DataMember]
        public int MinStonesReservesPerMine_NormalPlayer = 70000;

        /// <summary>
        /// 每座矿山的最大矿石储量
        /// </summary>
        [DataMember]
        public int MaxStonesReservesPerMine_NormalPlayer = 110000;

        /// <summary>
        /// 可开采矿山储量下限值，低于20000时可见，可开采
        /// </summary>
        [DataMember]
        public int WorkableReservesVisibleLimitDown = 20000;

        /// <summary>
        /// 临时生产矿石有效记录时间（小时），超出时间且没有收取，则不记生产。
        /// </summary>
        [DataMember]
        public int TempStoneOutputValidHour = 24;

        /// <summary>
        /// 矿石买家奖励金币倍数
        /// </summary>
        [DataMember]
        public decimal StoneBuyerAwardGoldCoinMultiple = 0.05m;

        /// <summary>
        /// 出售矿石手续费比例数
        /// </summary>
        [DataMember]
        public decimal ExchangeExpensePercent = 0.05m;

        /// <summary>
        /// 提现手续费手续费最小金额
        /// </summary>
        [DataMember]
        public decimal ExchangeExpenseMinNumber;

        /// <summary>
        /// 玩家可以拥有最大矿工数
        /// </summary>
        [DataMember]
        public int UserMaxHaveMinersCount = 5000;

        /// <summary>
        /// 买家锁定订单时间（分钟）
        /// </summary>
        [DataMember]
        public int BuyOrderLockTimeMinutes = 30;

        /// <summary>
        /// 允许提现的贡献值级别
        /// </summary>
        [DataMember]
        public int CanExchangeMinExp = 50;

        /// <summary>
        /// 允许打折的贡献值级别
        /// </summary>
        [DataMember]
        public int CanDiscountMinExp = 100;

        /// <summary>
        /// 打折
        /// </summary>
        [DataMember]
        public decimal Discount = 0.99m;

        /// <summary>
        /// 幸运转盘玩一次，需要的矿石数
        /// </summary>
        [DataMember]
        public int RouletteSpendStone = 100;

        /// <summary>
        /// 限制玩家提现间隔（小时）
        /// </summary>
        [DataMember]
        public int LimitWithdrawIntervalHours = 6;

        /// <summary>
        /// 当玩家被锁定时间超过此值天数时，会被删除
        /// </summary>
        [DataMember]
        public int DeleteUser_WhenLockedExpireDays = 7;

        /// <summary>
        /// 一手矿石数为1000
        /// </summary>
        [DataMember]
        public int HandStoneCount = 1000;

        /// <summary>
        /// 夺宝奇兵服务费
        /// </summary>
        [DataMember]
        public decimal RaiderExpense = 0.1m;

        /// <summary>
        /// 系统矿场总储量五千万
        /// </summary>
        [DataMember]
        public int LimitStoneCount = 50000000;

        /// <summary>
        /// VIP级别间距（贡献值）
        /// </summary>
        [DataMember]
        public int PlayerVIPInterval = 2000;

        [DataMember]
        public int StackMarketMorningOpenTime = 9;
        [DataMember]
        public int StackMarketMorningCloseTime = 12;

        [DataMember]
        public int StackMarketAfternoonOpenTime = 13;
        [DataMember]
        public int StackMarketAfternoonCloseTime = 17;

        [DataMember]
        public int StackMarketNightOpenTime = 18;
        [DataMember]
        public int StackMarketNightCloseTime = 23;

        [DataMember]
        public static int Server1StackMarketMinPrice = 1;

        [DataMember]
        public static int Server2StackMarketMinPrice = 100;

        [DataMember]
        public int GambleStoneRedColorWinTimes = 2;
        [DataMember]
        public int GambleStoneGreenColorWinTimes = 3;
        [DataMember]
        public int GambleStoneBlueWinTimes = 6;
        [DataMember]
        public int GambleStonePurpleWinTimes = 50;
        [DataMember]
        public int GambleStone_DailyProfitStoneObjective = 5000;
        [DataMember]
        public int GambleStone_Round_InningCount = 64;
        [DataMember]
        public int GambleStone_OneBetStoneCount = 10;

        [DataMember]
        public int GravelMin = 10;

        /// <summary>
        /// 远程协助服务充值返灵币倍数，充1元人民币，返10灵币
        /// </summary>
        [DataMember]
        public int RemoteServerRechargeReturnShoppingCreditsTimes = 10;

        /// <summary>
        /// 神灵许愿最大值
        /// </summary>
        [DataMember]
        public int MaxMakeAVowToGodGravelValue = 50;

        /// <summary>
        /// 每天可以许愿的最大次数
        /// </summary>
        [DataMember]
        public int MaxMakeAVowTimesOfOneDay = 3;

        /// <summary>
        /// 开启一次矿石加工厂，需要1000积分
        /// </summary>
        [DataMember]
        public int OpenFactoryNeedShoppingCredit = 1000;
    }
}
