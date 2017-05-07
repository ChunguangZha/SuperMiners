using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Shopping
{
    /// <summary>
    /// 积分商城，全部商品用积分购买
    /// </summary>
    [DataContract]
    public class VirtualShoppingItem
    {
        [DataMember]
        public int ID;

        /// <summary>
        /// MaxLength:100
        /// </summary>
        [DataMember]
        public string Name;

        /// <summary>
        /// MaxLength:2000
        /// </summary>
        [DataMember]
        public string Remark;

        [DataMember]
        public SellState SellState;

        /// <summary>
        /// 玩家最多可以购买该商品次数，小于等于0表示不限购
        /// </summary>
        [DataMember]
        public int PlayerMaxBuyableCount;

        /// <summary>
        /// 价值积分
        /// </summary>
        [DataMember]
        public decimal ValueShoppingCredits;

        [DataMember]
        public decimal GainExp;

        [DataMember]
        public decimal GainRMB;

        [DataMember]
        public decimal GainGoldCoin;

        [DataMember]
        public decimal GainMine_StoneReserves;

        [DataMember]
        public decimal GainMiner;

        [DataMember]
        public decimal GainStone;

        [DataMember]
        public decimal GainDiamond;

        [DataMember]
        public decimal GainShoppingCredits;

        [DataMember]
        public decimal GainGravel;

        [DataMember]
        public byte[] IconBuffer;

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
