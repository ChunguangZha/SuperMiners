using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 矿工购买记录。（注：矿工是金币交易，不需要订单号）
    /// </summary>
    public class MinersBuyRecord
    {
        public string UserName = "";

        /// <summary>
        /// 花费的金币数
        /// </summary>
        public decimal SpendGoldCoin = 0;

        /// <summary>
        /// 获取矿工数
        /// </summary>
        public int GainMinersCount = 0;

        public DateTime Time;
    }
}
