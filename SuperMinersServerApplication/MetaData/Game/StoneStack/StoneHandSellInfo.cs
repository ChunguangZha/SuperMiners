using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    public class StoneHandSellInfo
    {
        public int UserID;

        public decimal SellPrice;

        public int SellStoneCount;

        public StoneHandSellState SellState;

        /// <summary>
        /// 挂单时间
        /// </summary>
        public MyDateTime HandTime;

        /// <summary>
        /// 完成时间
        /// </summary>
        public MyDateTime FinishedTime;
    }

    public enum StoneHandSellState
    {
        Waiting,
        Succeed,
        Rejected
    }
}
