using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    public class AwardReferrerLevelConfig
    {
        private object _lockListAward = new object();
        private List<AwardReferrerConfig> listAward = new List<AwardReferrerConfig>();

        public void SetListAward(List<AwardReferrerConfig> lists)
        {
            lock (_lockListAward)
            {
                this.listAward = lists;
            }
        }

        public List<AwardReferrerConfig> GetListAward()
        {
            lock (_lockListAward)
            {
                return this.listAward;
            }
        }

        public int AwardLevelCount
        {
            get
            {
                lock (_lockListAward) { return this.listAward.Count; }
            }
        }

        /// <summary>
        /// 级别从1开始，向上递增
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public AwardReferrerConfig GetAwardByLevel(int level)
        {
            lock (_lockListAward)
            {
                if (0 < level && level <= this.AwardLevelCount)
                {
                    return this.listAward[level - 1];
                }
            }

            return null;
        }
    }

    /// <summary>
    /// 奖励推荐人配置，设置多级奖励
    /// </summary>
    [DataContract]
    public class AwardReferrerConfig
    {
        /// <summary>
        /// 推荐层级
        /// </summary>
        [DataMember]
        public int ReferLevel = 1;

        /// <summary>
        /// 奖励推荐人贡献值
        /// </summary>
        [DataMember]
        public decimal AwardReferrerExp = 1;

        /// <summary>
        /// 奖励推荐人金币数
        /// </summary>
        [DataMember]
        public decimal AwardReferrerGoldCoin = 5;

        /// <summary>
        /// 奖励推荐人矿山数
        /// </summary>
        [DataMember]
        public decimal AwardReferrerMines = 0;

        /// <summary>
        /// 奖励推荐人矿工数
        /// </summary>
        [DataMember]
        public int AwardReferrerMiners = 0;

        /// <summary>
        /// 奖励推荐人矿石数
        /// </summary>
        [DataMember]
        public decimal AwardReferrerStones = 0.05m;

        /// <summary>
        /// 奖励推荐人钻石数
        /// </summary>
        [DataMember]
        public decimal AwardReferrerDiamond = 0.0m;

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(string.Format("第{0}级推荐奖励：", this.ReferLevel));
            if (AwardReferrerExp > 0)
            {
                strBuilder.Append(string.Format("贡献值-[{0}],", this.AwardReferrerExp));
            }
            if (AwardReferrerGoldCoin > 0)
            {
                strBuilder.Append(string.Format("金币-[{0}],", this.AwardReferrerGoldCoin));
            }
            if (AwardReferrerMines > 0)
            {
                strBuilder.Append(string.Format("矿山-[{0}],", this.AwardReferrerMines));
            }
            if (AwardReferrerMiners > 0)
            {
                strBuilder.Append(string.Format("矿工-[{0}],", this.AwardReferrerMiners));
            }
            if (AwardReferrerStones > 0)
            {
                strBuilder.Append(string.Format("矿石-[{0}],", this.AwardReferrerStones));
            }
            if (AwardReferrerDiamond > 0)
            {
                strBuilder.Append(string.Format("钻石-[{0}],", this.AwardReferrerDiamond));
            }

            return strBuilder.ToString(0, strBuilder.Length - 1);
        }
    }
}
