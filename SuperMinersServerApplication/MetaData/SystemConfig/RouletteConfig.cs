using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    [DataContract]
    public class RouletteConfig
    {
        [DataMember]
        public decimal RouletteLargeWinMultiple = 2.5m;
    }
}
