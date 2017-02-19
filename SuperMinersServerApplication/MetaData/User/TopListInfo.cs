using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class TopListInfo
    {
        [DataMember]
        public string UserName;

        //[DataMember]
        //public string NickName;

        [DataMember]
        public decimal Value;
    }
}
