using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    [DataContract]
    public class StoneStackDailyRecordInfo
    {
        [DataMember]
        public MyDateTime Day;

        //[DataMember]
        //public MyDateTime OpenTime;

        //[DataMember]
        //public MyDateTime CloseTime;

        private decimal _openPrice;

        [DataMember]
        public decimal OpenPrice
        {
            get
            {
                return this._openPrice;
            }
            set
            {
                this._openPrice = value;
                if (value != 0)
                {
                    this.LimitUpPrice = value * 1.2m;
                    this.LimitDownPrice = value * 0.8m;
                }
            }
        }

        [DataMember]
        public decimal ClosePrice;

        /// <summary>
        /// 涨停价= OpenPrice * 120%
        /// </summary>
        [DataMember]
        public decimal LimitUpPrice;

        /// <summary>
        /// 跌停价= OpenPrice * 80%
        /// </summary>
        [DataMember]
        public decimal LimitDownPrice;

        /// <summary>
        /// 最低成交价
        /// </summary>
        [DataMember]
        public decimal MinTradeSucceedPrice;

        /// <summary>
        /// 最高成交价
        /// </summary>
        [DataMember]
        public decimal MaxTradeSucceedPrice;

        [DataMember]
        public decimal TradeSucceedStoneHandSum;

        [DataMember]
        public decimal TradeSucceedRMBSum;

        [DataMember]
        public decimal DelegateSellStoneSum;

        [DataMember]
        public decimal DelegateBuyStoneSum;

    }
}
