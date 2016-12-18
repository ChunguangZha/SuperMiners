using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class PlayerLockedInfo
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public bool LockedLogin { get; set; }

        [DataMember]
        public MyDateTime LockedLoginTime { get; set; }

        /// <summary>
        /// 锁定过期时间
        /// </summary>
        [DataMember]
        public int ExpireDays { get; set; }

    }
}
