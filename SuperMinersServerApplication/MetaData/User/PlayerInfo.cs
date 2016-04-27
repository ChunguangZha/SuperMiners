using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    [DataContract]
    public class PlayerInfo
    {
        [DataMember]
        public PlayerSimpleInfo SimpleInfo = new PlayerSimpleInfo();

        [DataMember]
        public PlayerFortuneInfo FortuneInfo = new PlayerFortuneInfo();
    }
}
