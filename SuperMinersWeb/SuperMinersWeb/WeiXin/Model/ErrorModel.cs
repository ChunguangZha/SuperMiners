using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SuperMinersWeb.WeiXin.Model
{
    /// <summary>
    /// "errcode":40125,"errmsg":"invalid appsecret, view more at http:\/\/t.cn\/RAEkdVq, hints: [ req_id: JRk49a0882ns82 ]"}
    /// </summary>
    [DataContract]
    public class ErrorModel
    {
        [DataMember]
        public int errcode { get; set; }
        
        [DataMember]
        public string errmsg { get; set; }
    }
}