using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SuperMinersWeb.WeiXin.Model
{
    [DataContract]
    public class ErrorModel
    {
        [DataMember]
        public int errcode { get; set; }
        
        [DataMember]
        public string errmsg { get; set; }
    }
}