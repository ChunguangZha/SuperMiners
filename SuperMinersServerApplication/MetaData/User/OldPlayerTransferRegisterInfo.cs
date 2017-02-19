using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class OldPlayerTransferRegisterInfo
    {
        [DataMember]
        public int ID;

        [DataMember]
        public string UserName;

        [DataMember]
        public string AlipayAccount;

        [DataMember]
        public string AlipayRealName;

        [DataMember]
        public string Email;

        [DataMember]
        public MyDateTime SubmitTime;

        [DataMember]
        public bool isTransfered;

        [DataMember]
        public MyDateTime HandledTime;

        [DataMember]
        public string HandlerName;

    }
}
