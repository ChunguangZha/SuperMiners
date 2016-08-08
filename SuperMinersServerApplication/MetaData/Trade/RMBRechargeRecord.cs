using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class RMBRechargeRecord
    {
        public string UserName = "";

        public string AlipayTradeNo = "";

        /// <summary>
        /// 系统交易单号
        /// </summary>
        public string SysTradeNo = "";

        /// <summary>
        /// 充值金额
        /// </summary>
        public float RechargeMoney = 0;

        /// <summary>
        /// 获取RMB值
        /// </summary>
        public float GainRMB = 0;

        public DateTime Time;
    }
}
