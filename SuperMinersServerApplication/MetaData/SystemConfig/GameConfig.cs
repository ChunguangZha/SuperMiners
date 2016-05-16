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
        public float Yuan_RMB = 10;

        /// <summary>
        /// RMB兑换金币
        /// </summary>
        [DataMember]
        public float RMB_GoldCoin = 1000;

        /// <summary>
        /// 购买矿山所需要的RMB
        /// </summary>
        [DataMember]
        public float RMB_Mine = 3000;

        /// <summary>
        /// 购买矿工所需要的金币
        /// </summary>
        [DataMember]
        public float GoldCoin_Miner = 10000;

        /// <summary>
        /// 多少矿石等价于1RMB
        /// </summary>
        [DataMember]
        public float Stones_RMB = 10;

        /// <summary>
        /// 多少钻石等价于1RMB
        /// </summary>
        [DataMember]
        public float Diamonds_RMB = 1;

        /// <summary>
        /// 每个矿工每小时生产矿石数
        /// </summary>
        [DataMember]
        public float OutputStonesPerHour = 0.138f;

        /// <summary>
        /// 每座矿山的矿石储量
        /// </summary>
        [DataMember]
        public float StonesReservesPerMines = 100000;

        /// <summary>
        /// 临时生产矿石有效记录时间（小时），超出时间且没有收取，则不记生产。
        /// </summary>
        [DataMember]
        public int TempStoneOutputValidHour = 3;

        /// <summary>
        /// 矿石买家奖励金币倍数
        /// </summary>
        [DataMember]
        public float StoneBuyerAwardGoldCoinMultiple = 0.05f;

        /// <summary>
        /// 提现手续费比例数
        /// </summary>
        [DataMember]
        public float ExchangeExpensePercent = 0.05f;

        /// <summary>
        /// 提现手续费手续费最小金额
        /// </summary>
        [DataMember]
        public float ExchangeExpenseMinNumber;

        /// <summary>
        /// 玩家可以拥有最大矿工数
        /// </summary>
        [DataMember]
        public int UserMaxHaveMinersCount = 50;

        /// <summary>
        /// 买家锁定订单时间（分钟）
        /// </summary>
        [DataMember]
        public int BuyOrderLockTimeMinutes = 30;

    }
}
