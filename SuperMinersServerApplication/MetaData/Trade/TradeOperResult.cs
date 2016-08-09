using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class TradeOperResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int ResultCode;

        public int TradeType;

        public string AlipayLink;


        public const int RESULTCODE_SUCCEED = 0;

    }
}
