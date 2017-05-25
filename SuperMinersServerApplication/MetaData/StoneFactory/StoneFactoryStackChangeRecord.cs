using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.StoneFactory
{
    /// <summary>
    /// 加工厂股权变更记录，入出都记录（正数为投入；负数为提取）
    /// </summary>
    public class StoneFactoryStackChangeRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        /// <summary>
        /// （正数为投入；负数为提取）
        /// </summary>
        [DataMember]
        public int JoinStoneStackCount;

        [DataMember]
        public MyDateTime Time;
    }

}
