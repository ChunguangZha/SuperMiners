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
        public decimal Exp { get; set; }

        /// <summary>
        /// 信誉值(以玩家购买矿石量计算)
        /// </summary>
        [DataMember]
        public long CreditValue { get; set; }

        /// <summary>
        /// 灵币，玩家提现时，从该值中减去相应灵币，加到冻结灵币中。
        /// </summary>
        [DataMember]
        public decimal RMB { get; set; }

        /// <summary>
        /// 冻结灵币，玩家可以同时多次提现，则冻结灵币值会累加。
        /// </summary>
        [DataMember]
        public decimal FreezingRMB { get; set; }

        [DataMember]
        public decimal GoldCoin { get; set; }

        /// <summary>
        /// 矿山数
        /// </summary>
        [DataMember]
        public decimal MinesCount { get; set; }

        /// <summary>
        /// 矿石储量。该值为累计值，即每次购买矿山，将矿山储量值累加到该值上。
        /// </summary>
        [DataMember]
        public decimal StonesReserves { get; set; }

        /// <summary>
        /// 累计总产出矿石数。和StonesReserves的差值，为当前可开采的矿石数。
        /// </summary>
        [DataMember]
        public decimal TotalProducedStonesCount { get; set; }

        /// <summary>
        /// 矿工数
        /// </summary>
        [DataMember]
        public decimal MinersCount { get; set; }

        /// <summary>
        /// 库存矿石数（可出售矿石：StockOfStones-FreezingStones），挂单出售矿石时，不从该值减，而是添加到冻结矿石中。如交易成功，再从该值减。
        /// </summary>
        [DataMember]
        public decimal StockOfStones { get; set; }

        public DateTime? TempOutputStonesStartTime = null;
        [DataMember]
        public string TempOutputStonesStartTimeString
        {
            get
            {
                if (this.TempOutputStonesStartTime == null)
                {
                    return "";
                }
                return this.TempOutputStonesStartTime.ToString();
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        TempOutputStonesStartTime = DateTime.Parse(value);
                    }
                }
                catch (Exception)
                {
                    TempOutputStonesStartTime = null;
                }
            }
        }

        [DataMember]
        public decimal TempOutputStones;

        /// <summary>
        /// 冻结的矿石数
        /// </summary>
        [DataMember]
        public decimal FreezingStones { get; set; }

        /// <summary>
        /// 库存钻石数(锁定钻石时，直接从该值减数，加到FreezingDiamonds中)
        /// </summary>
        [DataMember]
        public decimal StockOfDiamonds { get; set; }

        /// <summary>
        /// 冻结的钻石数
        /// </summary>
        [DataMember]
        public decimal FreezingDiamonds { get; set; }

        /// <summary>
        /// 矿石出售券
        /// </summary>
        [DataMember]
        public int StoneSellQuan { get; set; }

        /// <summary>
        /// 默认为false
        /// </summary>
        [DataMember]
        public bool FirstRechargeGoldCoinAward { get; set; }

        /// <summary>
        /// 可用积分
        /// </summary>
        [DataMember]
        public decimal ShoppingCreditsEnabled { get; set; }

        /// <summary>
        /// 冻结积分
        /// </summary>
        [DataMember]
        public decimal ShoppingCreditsFreezed { get; set; }

        [DataMember]
        public MyDateTime UserRemoteServerValidStopTime { get; set; }

        /// <summary>
        /// 标识是否为长期远程协助服务用户（false为单次用户，单次可累计。如购买了单次，又购买长期，则视为长期）
        /// </summary>
        [DataMember]
        public bool IsLongTermRemoteServiceUser { get; set; }

        /// <summary>
        /// 单次用户可用服务次数
        /// </summary>
        [DataMember]
        public int UserRemoteServiceValidTimes { get; set; }

        /// <summary>
        /// 神灵许愿时间（以DayofYear来保存）
        /// </summary>
        [DataMember]
        public int MakeAVowToGodTime_DayofYear { get; set; }

        /// <summary>
        /// 最近一次许愿次数（不能超过系统设置最大值）
        /// </summary>
        [DataMember]
        public int MakeAVowToGodTimesLastDay { get; set; }

        public PlayerFortuneInfo CopyTo()
        {
            PlayerFortuneInfo infoB = new PlayerFortuneInfo()
            {
                Exp = this.Exp,
                CreditValue = this.CreditValue,
                FreezingDiamonds = this.FreezingDiamonds,
                FreezingStones = this.FreezingStones,
                GoldCoin = this.GoldCoin,
                MinersCount = this.MinersCount,
                MinesCount = this.MinesCount,
                RMB = this.RMB,
                FreezingRMB = this.FreezingRMB,
                TempOutputStonesStartTime = this.TempOutputStonesStartTime,
                StockOfDiamonds = this.StockOfDiamonds,
                StockOfStones = this.StockOfStones,
                StonesReserves = this.StonesReserves,
                TempOutputStones = this.TempOutputStones,
                TotalProducedStonesCount = this.TotalProducedStonesCount,
                UserName = this.UserName,
                FirstRechargeGoldCoinAward = this.FirstRechargeGoldCoinAward,
                StoneSellQuan = this.StoneSellQuan,
                ShoppingCreditsEnabled = this.ShoppingCreditsEnabled,
                ShoppingCreditsFreezed = this.ShoppingCreditsFreezed,
                UserRemoteServerValidStopTime = this.UserRemoteServerValidStopTime,
                IsLongTermRemoteServiceUser = this.IsLongTermRemoteServiceUser,
                UserRemoteServiceValidTimes = this.UserRemoteServiceValidTimes,
                MakeAVowToGodTime_DayofYear = this.MakeAVowToGodTime_DayofYear,
                MakeAVowToGodTimesLastDay = this.MakeAVowToGodTimesLastDay,
            };

            return infoB;
        }

        public void CopyFrom(PlayerFortuneInfo fortuneInfo)
        {
            this.Exp = fortuneInfo.Exp;
            this.CreditValue = fortuneInfo.CreditValue;
            this.FreezingDiamonds = fortuneInfo.FreezingDiamonds;
            this.FreezingStones = fortuneInfo.FreezingStones;
            this.GoldCoin = fortuneInfo.GoldCoin;
            this.MinersCount = fortuneInfo.MinersCount;
            this.MinesCount = fortuneInfo.MinesCount;
            this.RMB = fortuneInfo.RMB;
            this.FreezingRMB = fortuneInfo.FreezingRMB;
            this.TempOutputStonesStartTime = fortuneInfo.TempOutputStonesStartTime;
            this.StockOfDiamonds = fortuneInfo.StockOfDiamonds;
            this.StockOfStones = fortuneInfo.StockOfStones;
            this.StonesReserves = fortuneInfo.StonesReserves;
            this.TempOutputStones = fortuneInfo.TempOutputStones;
            this.TotalProducedStonesCount = fortuneInfo.TotalProducedStonesCount;
            this.UserName = fortuneInfo.UserName;
            this.FirstRechargeGoldCoinAward = fortuneInfo.FirstRechargeGoldCoinAward;
            this.StoneSellQuan = fortuneInfo.StoneSellQuan;
            this.ShoppingCreditsEnabled = fortuneInfo.ShoppingCreditsEnabled;
            this.ShoppingCreditsFreezed = fortuneInfo.ShoppingCreditsFreezed;
            this.UserRemoteServerValidStopTime = fortuneInfo.UserRemoteServerValidStopTime;
            this.IsLongTermRemoteServiceUser = fortuneInfo.IsLongTermRemoteServiceUser;
            this.UserRemoteServiceValidTimes = fortuneInfo.UserRemoteServiceValidTimes;
            this.MakeAVowToGodTime_DayofYear = fortuneInfo.MakeAVowToGodTime_DayofYear;
            this.MakeAVowToGodTimesLastDay = fortuneInfo.MakeAVowToGodTimesLastDay;
        }

        public override string ToString()
        {
            return MetaData.Utility.JsonSerializeTest<PlayerFortuneInfo>.SaveToJson(this);
        }
    }
}
