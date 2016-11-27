using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinAccess.Model
{
    [DataContract]
    public class AccessToken
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public int expires_in { get; set; }
    }
}
