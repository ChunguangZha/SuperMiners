using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Shopping
{
    [DataContract]
    public class PlayerBuyDiamondShoppingItemRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public int DiamondShoppingItemID;

        [DataMember]
        public MyDateTime BuyTime;
    }
}
