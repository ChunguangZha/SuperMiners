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
        /// 从OperResult常量中取值
        /// </summary>
        public int ResultCode = OperResult.RESULTCODE_FALSE;

        public int TradeType;

        public string AlipayLink;

        public int PayType;

    }

}
