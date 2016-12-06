using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class GatherTempOutputStoneResult
    {
        [DataMember]
        public int OperResult { get; set; }

        [DataMember]
        public int GatherStoneCount { get; set; }
    }
}
