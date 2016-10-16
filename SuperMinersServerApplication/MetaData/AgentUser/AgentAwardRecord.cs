using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.AgentUser
{
    [DataContract]
    public class AgentAwardRecord
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int AgentID { get; set; }

        [DataMember]
        public string AgentUserName { get; set; }

        [DataMember]
        public int PlayerID { get; set; }

        [DataMember]
        public string PlayerUserName { get; set; }

        [DataMember]
        public decimal PlayerInchargeRMB { get; set; }

        [DataMember]
        public decimal AgentAwardRMB { get; set; }

        [DataMember]
        public string PlayerInchargeContent { get; set; }

        [DataMember]
        public MyDateTime Time { get; set; }
    }
}
