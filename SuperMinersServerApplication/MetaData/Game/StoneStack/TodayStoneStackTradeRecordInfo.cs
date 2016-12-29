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
        /// 卖1到卖10的信息(价格从低到高排序)，服务器上保存所有，客户端去显示前10项
        /// </summary>
        public List<StackTradeUnit> SellOrderPriceCountList = new List<StackTradeUnit>();

        [DataMember]
        public StackTradeUnit[] Top10SellOrderList
        {
            get
            {
                return SellOrderPriceCountList.ToArray();
            }
            set
            {
                this.SellOrderPriceCountList = new List<StackTradeUnit>(value);
            }
        }

        /// <summary>
        /// 买一到买10的信息(价格从高到低排序)，服务器上保存所有，客户端去显示前10项
        /// </summary>
        public List<StackTradeUnit> BuyOrderPriceCountList = new List<StackTradeUnit>();

        [DataMember]
        public StackTradeUnit[] Top10BuyOrderList
        {
            get
            {
                return BuyOrderPriceCountList.ToArray();
            }
            set
            {
                this.BuyOrderPriceCountList = new List<StackTradeUnit>(value);
            }
        }

        
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
