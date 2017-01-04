using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.RaideroftheLostArk
{
    [DataContract]
    public class PlayerRaiderRoundHistoryRecordInfo
    {
        [DataMember]
        public RaiderRoundMetaDataInfo RoundInfo;

        [DataMember]
        public int BetJoinStoneCount;
    }
}
