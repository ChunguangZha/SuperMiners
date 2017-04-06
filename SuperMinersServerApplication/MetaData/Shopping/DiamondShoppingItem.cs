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



    }

    public enum DiamondsShoppingItemType
    {
        RealThing,
        PhoneFee,

    }

    public enum SellState
    {
        OnSell,
        OffSell,
    }
}
