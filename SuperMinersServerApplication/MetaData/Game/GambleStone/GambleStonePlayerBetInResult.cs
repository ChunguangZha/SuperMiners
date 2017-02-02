using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    [DataContract]
    public class GambleStonePlayerBetInResult
    {
        [DataMember]
        public GambleStonePlayerBetRecord PlayerBetRecord;

        [DataMember]
        public int ResultCode;
    }
}
