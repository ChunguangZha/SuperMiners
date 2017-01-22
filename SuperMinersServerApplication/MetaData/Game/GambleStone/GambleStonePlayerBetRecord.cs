using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    /// <summary>
    /// 玩家下注记录。数据库中，每月一张表（64局为一轮）一张表存 约64800 * N 条记录
    /// </summary>
    [DataContract]
    public class GambleStonePlayerBetRecord
    {
        [DataMember]
        public int UserID;

        /// <summary>
        /// 非数据库字段
        /// </summary>
        [DataMember]
        public string UserName;

        [DataMember]
        public MyDateTime Time;

        [DataMember]
        public int RoundID;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string InningID;

        [DataMember]
        public int BetRedStone;

        [DataMember]
        public int BetGreenStone;

        [DataMember]
        public int BetBlueStone;

        [DataMember]
        public int BetPurpleStone;

        [DataMember]
        public int WinnedStone;
    }
}
