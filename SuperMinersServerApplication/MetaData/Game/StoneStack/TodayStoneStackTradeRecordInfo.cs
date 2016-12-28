using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    /// <summary>
    /// 向客户端展示当天交易信息
    /// </summary>
    [DataContract]
    public class TodayStoneStackTradeRecordInfo
    {
        [DataMember]
        public StoneStackDailyRecordInfo DailyInfo = new StoneStackDailyRecordInfo();

        /// <summary>
        /// 卖1到卖10的信息(价格从低到高排序)
        /// </summary>
        [DataMember]
        public StackTradeUnit[] SellOrderList = new StackTradeUnit[10];

        /// <summary>
        /// 买一到买10的信息(价格从高到低排序)
        /// </summary>
        [DataMember]
        public StackTradeUnit[] BuyOrderList = new StackTradeUnit[10];
        
    }

    [DataContract]
    public class StackTradeUnit
    {
        /// <summary>
        /// 一手矿石的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public decimal Price;

        /// <summary>
        /// 交易的矿石手数。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public int TradeStoneHandCount;
    }

}
