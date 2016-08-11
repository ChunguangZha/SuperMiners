using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 矿山购买记录
    /// </summary>
    public class MinesBuyRecord
    {
        public string UserName = "";

        public string OrderNumber = "";

        /// <summary>
        /// 花费的灵币数
        /// </summary>
        public int SpendRMB = 0;

        /// <summary>
        /// 获取矿山数
        /// </summary>
        public float GainMinesCount = 0;

        /// <summary>
        /// 获取的矿石储量，将该值累加到用户的StonesReserves值中
        /// </summary>
        public int GainStonesReserves = 0;

        public DateTime CreateTime;

        /// <summary>
        /// 临时表不保存该字段
        /// </summary>
        public DateTime PayTime;
    }
}
