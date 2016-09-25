using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.Roulette
{
    [DataContract]
    public class RouletteWinAwardResult
    {
        [DataMember]
        public int WinAwardNumber;

        [DataMember]
        public int OperResultCode;
    }
}
