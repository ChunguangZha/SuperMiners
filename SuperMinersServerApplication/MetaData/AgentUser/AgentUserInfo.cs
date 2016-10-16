using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.AgentUser
{
    [DataContract]
    public class AgentUserInfo
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public PlayerInfo Player { get; set; }

        /// <summary>
        /// 累计奖励灵币。
        /// </summary>
        [DataMember]
        public decimal TotalAwardRMB { get; set; }

        [DataMember]
        public string InvitationURL { get; set; }
    }
}
