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

        [DataMember]
        public bool IsLocked;

    }
}
