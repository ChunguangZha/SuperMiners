using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    public class StoneHandBuyInfo
    {
        public int UserID;

        public decimal BuyPrice;

        public int BuyStoneCount;

        public StoneHandBuyState BuyState;

        /// <summary>
        /// 挂单时间
        /// </summary>
        public MyDateTime HandTime;

        /// <summary>
        /// 完成时间
        /// </summary>
        public MyDateTime FinishedTime;
    }

    public enum StoneHandBuyState
    {
        Waiting,
        Succeed,
        Rejected
    }
}
