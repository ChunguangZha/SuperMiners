using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class AdminInfo
    {
        /// <summary>
        /// maxlength=15
        /// </summary>
        [DataMember]
        public string UserName;

        /// <summary>
        /// minlength= 6, maxlength=15
        /// </summary>
        [DataMember]
        public string LoginPassword;

        /// <summary>
        /// minlength= 6, maxlength=15
        /// </summary>
        [DataMember]
        public string ActionPassword;

        [DataMember]
        public string[] Macs;
    }
}
