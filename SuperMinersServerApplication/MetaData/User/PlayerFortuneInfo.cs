using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    public enum FortuneType
    {
        Exp,
        RMB,
        GoldCoin,
        Mine,
        Miner,
        Stone,
        Diamond
    }

    /// <summary>
    /// 玩家财富信息
    /// </summary>
    [DataContract]
    public class PlayerFortuneInfo
    {
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 贡献值
        /// </summary>
        [DataMember]
        public float Exp { get; set; }

        [DataMember]
        public float RMB { get; set; }

        [DataMember]
        public float GoldCoin { get; set; }

        /// <summary>
        /// 矿山数
        /// </summary>
        [DataMember]
        public float MinesCount { get; set; }

        /// <summary>
        /// 矿石储量。该值为累计值，即每次购买矿山，将矿山储量值累加到该值上。
        /// </summary>
        [DataMember]
        public float StonesReserves { get; set; }

        /// <summary>
        /// 累计总产出矿石数。和StonesReserves的差值，为当前可开采的矿石数。
        /// </summary>
        [DataMember]
        public float TotalProducedStonesCount { get; set; }

        /// <summary>
        /// 矿工数
        /// </summary>
        [DataMember]
        public float MinersCount { get; set; }

        /// <summary>
        /// 库存矿石数
        /// </summary>
        [DataMember]
        public float StockOfStones { get; set; }

        /// <summary>
        /// 临时生产矿石数，非数据库字段，记录在内存中，只有玩家点击收取，才会调用方法把产量值保存到数据库中。
        /// </summary>
        [DataMember]
        public float TempOutputStones { get; set; }

        /// <summary>
        /// 非数据库字段，为TempOutputStones的扩充体
        /// </summary>
        [DataMember]
        public PlayerTempOutputStoneInfo PlayerTempOutputStoneInfo { get; set; }

        /// <summary>
        /// 冻结的矿石数
        /// </summary>
        [DataMember]
        public float FreezingStones { get; set; }

        /// <summary>
        /// 库存钻石数
        /// </summary>
        [DataMember]
        public float StockOfDiamonds { get; set; }

        /// <summary>
        /// 冻结的钻石数
        /// </summary>
        [DataMember]
        public float FreezingDiamonds { get; set; }

        public PlayerFortuneInfo CopyTo()
        {
            PlayerFortuneInfo infoB = new PlayerFortuneInfo()
            {
                Exp = this.Exp,
                FreezingDiamonds = this.FreezingDiamonds,
                FreezingStones = this.FreezingStones,
                GoldCoin = this.GoldCoin,
                MinersCount = this.MinersCount,
                MinesCount = this.MinesCount,
                RMB = this.RMB,
                StockOfDiamonds = this.StockOfDiamonds,
                StockOfStones = this.StockOfStones,
                StonesReserves = this.StonesReserves,
                TempOutputStones = this.TempOutputStones,
                TotalProducedStonesCount = this.TotalProducedStonesCount,
                UserName = this.UserName
            };

            return infoB;
        }
    }
}
