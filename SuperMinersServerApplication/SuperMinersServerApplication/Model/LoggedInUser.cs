using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Model
{
    [DataContract]
    public class LoggedInUser
    {
        [DataMember]
        public string SessionID
        {
            get;
            set;
        }

        [DataMember]
        public string UserId
        {
            get;
            set;
        }

        [DataMember]
        public string IP
        {
            get;
            set;
        }

        [DataMember]
        public DateTime TimeLoggedIn
        {
            get;
            set;
        }

        [DataMember]
        public DateTime TimeLoggedOut
        {
            get;
            set;
        }

        [DataMember]
        public string ClientType
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }

}
