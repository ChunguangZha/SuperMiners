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
        public string OrderNumber;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public int VirtualShoppingItemID;

        [DataMember]
        public string VirtualShoppingItemName;

        [DataMember]
        public string SendAddress;

        [DataMember]
        public MyDateTime BuyTime;

        /// <summary>
        /// 是否已发货
        /// </summary>
        [DataMember]
        public bool IsSend;

        /// <summary>
        /// 快递公司
        /// </summary>
        [DataMember]
        public string ExpressCompany;

        /// <summary>
        /// 快递单号
        /// </summary>
        [DataMember]
        public string ExpressNumber;

        /// <summary>
        /// 操作管理员
        /// </summary>
        [DataMember]
        public string OperAdmin;
    }
}
