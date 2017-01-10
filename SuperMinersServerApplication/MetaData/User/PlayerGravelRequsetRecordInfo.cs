using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class PlayerGravelRequsetRecordInfo
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public MyDateTime RequestDate;

        [DataMember]
        public bool IsResponsed = false;

        [DataMember]
        public MyDateTime ResponseDate;

        [DataMember]
        public int Gravel;
    }

    [DataContract]
    public class GravelCreateRecordInfo
    {
        [DataMember]
        public MyDateTime CreateDate;

        [DataMember]
        public int AllPlayerCount;

        [DataMember]
        public int RequestPlayerCount;

        [DataMember]
        public int ResponseGravelCount;
    }

    [DataContract]
    public class GravelCompoundRecordInfo
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public MyDateTime CompoundDate;

        [DataMember]
        public int FromGravelCount;

        [DataMember]
        public int ToStoneCount;
    }

    [DataContract]
    public class PlayerGravelInfo
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public int Gravel;
    }


}
