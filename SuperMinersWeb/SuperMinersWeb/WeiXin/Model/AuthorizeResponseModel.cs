using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SuperMinersWeb.WeiXin.Model
{
    [DataContract]
    public class AuthorizeResponseModel
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public int expires_in { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
        [DataMember]
        public string openid { get; set; }
        [DataMember]
        public string scope { get; set; }
    }
}