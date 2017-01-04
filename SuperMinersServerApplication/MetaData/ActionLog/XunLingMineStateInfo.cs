using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.ActionLog
{
    [DataContract]
    public class XunLingMineStateInfo
    {
        [DataMember]
        public int AllPlayerCount;

        [DataMember]
        public int AllMinersCount;

        [DataMember]
        public int AllStonesReserves;

        [DataMember]
        public decimal AllProducedStonesCount;

        [DataMember]
        public decimal AllStockOfStones;

        /// <summary>
        /// 所有矿石量（包括库存量和未开采量）
        /// </summary>
        [DataMember]
        public decimal AllStonesCount; 
    }
}
