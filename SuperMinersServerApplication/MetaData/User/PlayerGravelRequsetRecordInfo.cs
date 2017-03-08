using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class PlayerGravelRequsetRecordInfo
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        [DataMember]
        public MyDateTime RequestDate;

        [DataMember]
        public bool IsResponsed = false;

        [DataMember]
        public MyDateTime ResponseDate;

        [DataMember]
        public int Gravel;

        [DataMember]
        public bool IsGoted = false;
    }

    [DataContract]
    public class GravelDistributeRecordInfo
    {
        [DataMember]
        public MyDateTime CreateDate;

        [DataMember]
        public int AllPlayerCount;

        [DataMember]
        public int RequestPlayerCount;

        [DataMember]
        public int ResponseGravelCount;
    }

    [DataContract]
    public class PlayerGravelInfo
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public int Gravel;

        /// <summary>
        /// 如果该值等于null，或时间超过玩家注册7天以后，玩家不允许申领碎片。
        /// </summary>
        [DataMember]
        public MyDateTime FirstGetGravelTime;

        /// <summary>
        /// 非数据库字段
        /// </summary>
        [DataMember]
        public PlayerGravelState GravelState = PlayerGravelState.Disable;
    }

    public enum PlayerGravelState
    {
        /// <summary>
        /// 注册7天内从未申领过碎片，或贡献值大于50的。
        /// </summary>
        Disable,
        Requestable,
        Requested,
        Getable,
        Got

    }
}
