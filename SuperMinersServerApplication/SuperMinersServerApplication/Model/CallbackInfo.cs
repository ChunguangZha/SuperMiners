using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Model
{
    [DataContract]
    public class CallbackInfo
    {
        [DataMember]
        public string MethodName
        {
            get;
            set;
        }

        [DataMember]
        public string[] Parameters
        {
            get;
            set;
        }
    }
}
