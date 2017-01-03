using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.RaideroftheLostArk
{
    /// <summary>
    /// 此结构为保存到数据库的结构
    /// </summary>
    [DataContract]
    public class RaiderRoundMetaDataInfo
    {
        [DataMember]
        public int ID;

        /// <summary>
        /// Not Null
        /// </summary>
        [DataMember]
        public RaiderRoundState State = RaiderRoundState.Waiting;

        /// <summary>
        /// Default Null
        /// </summary>
        [DataMember]
        public MyDateTime StartTime;

        /// <summary>
        /// 非数据库字段
        /// </summary>
        [DataMember]
        public int CountDownTotalSecond;

        /// <summary>
        /// Default Null
        /// </summary>
        [DataMember]
        public int AwardPoolSumStones;

        /// <summary>
        /// Default Null
        /// </summary>
        [DataMember]
        public string WinnerUserName;

        /// <summary>
        /// Default Null
        /// </summary>
        [DataMember]
        public int WinStones;

        /// <summary>
        /// Default Null
        /// </summary>
        [DataMember]
        public MyDateTime EndTime;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ID:" + ID + ";");
            builder.Append("State:" + State.ToString() + ";");
            builder.Append("StartTime:" + StartTime + ";");
            builder.Append("CountDownSecond:" + CountDownTotalSecond + ";");
            builder.Append("AwardPoolSumStones:" + AwardPoolSumStones + ";");
            builder.Append("WinnerUserName:" + WinnerUserName + ";");
            builder.Append("WinStones:" + WinStones + ";");
            builder.Append("EndTime:" + EndTime + ";");
            return builder.ToString();
        }
    }

    [DataContract]
    public class PlayerBetInfo
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int RaiderRoundID;

        [DataMember]
        public string UserName;

        [DataMember]
        public int BetStones;

        [DataMember]
        public MyDateTime Time;
    }

    public enum RaiderRoundState
    {
        Waiting,
        Started,
        Finished
    }
}
