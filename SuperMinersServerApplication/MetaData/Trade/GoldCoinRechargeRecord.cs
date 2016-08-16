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
        /// 花费灵币
        /// </summary>
        public decimal SpendRMB = 0;

        /// <summary>
        /// 获取金币值
        /// </summary>
        public decimal GainGoldCoin = 0;

        public DateTime CreateTime;

        /// <summary>
        /// 临时表不保存该字段
        /// </summary>
        public DateTime PayTime;
    }
}
