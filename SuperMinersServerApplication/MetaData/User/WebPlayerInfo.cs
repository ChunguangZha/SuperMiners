using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class WebPlayerInfo
    {
        [DataMember]
        public string UserLoginName;

        [DataMember]
        public string UserName;

        [DataMember]
        public string Token;

        [DataMember]
        public int ShoppingCredits;

        [DataMember]
        public MyDateTime UserRemoteServerValidStopTime = null;

        /// <summary>
        /// 标识是否为长期远程协助服务用户（false为单次用户，单次可累计。如购买了单次，又购买长期，则视为长期）
        /// </summary>
        [DataMember]
        public bool IsLongTermRemoteServiceUser { get; set; }

        /// <summary>
        /// 单次用户可用服务次数
        /// </summary>
        [DataMember]
        public int UserRemoteServiceValidTimes { get; set; }

        [DataMember]
        public bool IsLocked;

    }
}
