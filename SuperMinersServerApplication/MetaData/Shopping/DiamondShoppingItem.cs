using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Shopping
{
    [DataContract]
    public class DiamondShoppingItem
    {
        [DataMember]
        public int ID;

        [DataMember]
        public string Name;

        [DataMember]
        public DiamondsShoppingItemType Type;

        ///// <summary>
        ///// 库存量
        ///// </summary>
        //[DataMember]
        //public int Stocks;

        /// <summary>
        /// MaxLength:2000
        /// </summary>
        [DataMember]
        public string Remark;

        [DataMember]
        public SellState SellState;

        [DataMember]
        public decimal ValueDiamonds;

        [DataMember]
        public byte[] IconBuffer;

        [DataMember]
        public string DetailText;

        [DataMember]
        public string[] DetailImageNames;

    }

    public enum DiamondsShoppingItemType
    {
        /// <summary>
        /// 生活用品
        /// </summary>
        LiveThing = 1,

        /// <summary>
        /// 数码产品
        /// </summary>
        Digital = 2,

        /// <summary>
        /// 食品专区
        /// </summary>
        Food = 3,

        /// <summary>
        /// 家用电器
        /// </summary>
        HomeAppliances = 4,

        /// <summary>
        /// 话费充值
        /// </summary>
        PhoneFee = 5,

    }

    public enum SellState
    {
        OnSell,
        OffSell,
    }
}
