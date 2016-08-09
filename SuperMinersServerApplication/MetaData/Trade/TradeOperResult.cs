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
        /// 0表示成功；-1表示失败；-2表示异常；
        /// </summary>
        public int ResultCode = -1;

        public int TradeType;

        public string AlipayLink;


        public const int RESULTCODE_SUCCEED = 0;

    }
}
