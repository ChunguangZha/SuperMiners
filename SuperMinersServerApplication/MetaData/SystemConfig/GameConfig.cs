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
        /// 多少钻石等价于1RMB
        /// </summary>
        [DataMember]
        public decimal Diamonds_RMB = 1;

        /// <summary>
        /// 每个矿工每小时生产矿石数
        /// </summary>
        [DataMember]
        public decimal OutputStonesPerHour = 0.138m;

        /// <summary>
        /// 矿山储量随机
        /// </summary>
        [DataMember]
        public bool MineReservesIsRandom = false;

        /// <summary>
        /// 每座矿山的矿石储量
        /// </summary>
        [DataMember]
        public decimal StonesReservesPerMines = 100000;

        /// <summary>
        /// 每座矿山的最小矿石储量
        /// </summary>
        [DataMember]
        public decimal MinStonesReservesPerMine = 50000;

        /// <summary>
        /// 每座矿山的最大矿石储量
        /// </summary>
        [DataMember]
        public decimal MaxStonesReservesPerMine = 100000;

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
        /// 提现手续费比例数
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

    }
}
