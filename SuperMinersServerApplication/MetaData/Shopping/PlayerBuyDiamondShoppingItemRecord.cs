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
        public string OrderNumber;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public int DiamondShoppingItemID;

        [DataMember]
        public string DiamondShoppingItemName;

        [DataMember]
        public string SendAddress;

        [DataMember]
        public MyDateTime BuyTime;

        [DataMember]
        public DiamondShoppingState ShoppingState;

        [DataMember]
        public string ExpressCompany;

        [DataMember]
        public string ExpressNumber;

        [DataMember]
        public string OperAdmin;

        [DataMember]
        public MyDateTime OperTime;
        
    }

    public enum DiamondShoppingState
    {
        Payed,
        Sended
    }
}
