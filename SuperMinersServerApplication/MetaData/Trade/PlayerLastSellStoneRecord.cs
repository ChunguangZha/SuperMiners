using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    /// <summary>
    /// 记录玩家最后一次出售矿石信息
    /// </summary>
    public class PlayerLastSellStoneRecord
    {
        public int UserID;

        public string SellStoneOrderNumber;

        public DateTime SellTime;

    }
}
