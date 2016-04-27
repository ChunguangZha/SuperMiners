using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    /// <summary>
    /// 收款账户信息
    /// </summary>
    [DataContract]
    public class IncomeMoneyAccount
    {
        [DataMember]
        public string IncomeMoneyAlipay { get; set; }

        [DataMember]
        public string IncomeMoneyAlipayRealName { get; set; }

        /// <summary>
        /// 收款二维码图片序列化后
        /// </summary>
        [DataMember]
        public byte[] Alipay2DCode { get; set; }
    }
}
