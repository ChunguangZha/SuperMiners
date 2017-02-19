using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class UserRemoteServerItem
    {
        [DataMember]
        public int ID;

        [DataMember]
        public RemoteServerType ServerType;

        [DataMember]
        public int PayMoneyYuan;

        [DataMember]
        public string ShopName;

        [DataMember]
        public string Description;

    }

    public enum RemoteServerType
    {
        Once,
        OneMonth,
        HalfYear,
        OneYear,
    }

}
