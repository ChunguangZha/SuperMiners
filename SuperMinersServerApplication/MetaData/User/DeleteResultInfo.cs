using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class DeleteResultInfo
    {
        [DataMember]
        public bool AllSucceed = false;

        [DataMember]
        public string[] SucceedList;

        [DataMember]
        public string[] FailedList;
    }
}
