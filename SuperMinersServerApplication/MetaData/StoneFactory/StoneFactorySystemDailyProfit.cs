using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.StoneFactory
{
    [DataContract]
    public class StoneFactorySystemDailyProfit
    {
        [DataMember]
        public int ID;

        /// <summary>
        /// 收益率
        /// </summary>
        [DataMember]
        public decimal profitRate;

        [DataMember]
        public MyDateTime Day;
    }
}
