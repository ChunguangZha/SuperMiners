using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SuperMinersWeiXin.Model
{
    [DataContract]
    public class WeiXinUserInfoModel
    {
        [DataMember]
        public string openid { get; set; }

        [DataMember]
        public string nickname { get; set; }

        [DataMember]
        public string sex { get; set; }

        [DataMember]
        public string province { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string headimgurl { get; set; }

        [DataMember]
        public string[] privilege { get; set; }

        [DataMember]
        public string unionid { get; set; }

    }
}