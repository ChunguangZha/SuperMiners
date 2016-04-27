using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 矿工购买记录
    /// </summary>
    public class MinersBuyRecord
    {
        public string UserName = "";

        /// <summary>
        /// 花费的金币数
        /// </summary>
        public int SpendGoldCoin = 0;

        /// <summary>
        /// 获取矿工数
        /// </summary>
        public int GainMinersCount = 0;

        public DateTime Time;
    }
}
