using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Shopping
{
    [DataContract]
    public class PlayerBuyVirtualShoppingItemRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public int VirtualShoppingItemID;

        [DataMember]
        public string VirtualShoppingItemName;

        [DataMember]
        public MyDateTime BuyTime;
    }
}
