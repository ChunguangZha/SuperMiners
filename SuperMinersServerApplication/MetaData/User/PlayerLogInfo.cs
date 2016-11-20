using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class PlayerLoginInfo
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        /// <summary>
        /// 最近一次
        /// </summary>
        [DataMember]
        public string LastLoginIP { get; set; }

        [DataMember]
        public string LastLoginMac { get; set; }

        [DataMember]
        public MyDateTime LastLoginTime { get; set; }

        [DataMember]
        public MyDateTime LastLogoutTime { get; set; }

        /// <summary>
        /// 倒数第二次
        /// </summary>
        [DataMember]
        public string PreviousLoginIP { get; set; }

        [DataMember]
        public string PreviousLoginMac { get; set; }

        [DataMember]
        public MyDateTime PreviousLoginTime { get; set; }

        [DataMember]
        public MyDateTime PreviousLogoutTime { get; set; }
    }
}
