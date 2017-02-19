using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    /// <summary>
    /// 玩家远程协助服务记录
    /// </summary>
    [DataContract]
    public class UserRemoteHandleServiceRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public MyDateTime ServiceTime;

        [DataMember]
        public string WorkerName;

        [DataMember]
        public string ServiceContent;

    }
}
