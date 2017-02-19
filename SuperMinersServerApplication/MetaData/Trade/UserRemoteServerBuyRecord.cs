using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class UserRemoteServerBuyRecord
    {
        [DataMember]
        public int UserID;

        /// <summary>
        /// 非数据库字段
        /// </summary>
        [DataMember]
        public string UserName;

        [DataMember]
        public RemoteServerType ServerType;

        [DataMember]
        public int PayMoneyYuan;

        [DataMember]
        public string OrderNumber;

        [DataMember]
        public int GetShoppingCredits;

        [DataMember]
        public MyDateTime BuyRemoteServerTime;


    }

}
