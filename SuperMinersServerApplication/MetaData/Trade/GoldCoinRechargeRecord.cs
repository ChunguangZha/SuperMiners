using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    public class GoldCoinRechargeRecord
    {
        public string UserName = "";

        public string OrderNumber = "";

        /// <summary>
        /// 充值金额
        /// </summary>
        public float RechargeMoney = 0;

        /// <summary>
        /// 获取金币值
        /// </summary>
        public float GainGoldCoin = 0;

        public DateTime Time;
    }
}
