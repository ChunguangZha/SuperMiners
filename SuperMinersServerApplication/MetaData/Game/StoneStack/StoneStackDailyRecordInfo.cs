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

        [DataMember]
        private decimal _openPrice;

        public decimal OpenPrice
        {
            get
            {
                return this._openPrice;
            }
            set
            {

#if MetaData
#if V1

                if (value < SystemConfig.GameConfig.Server1StackMarketMinPrice)
                {
                    this._openPrice = SystemConfig.GameConfig.Server1StackMarketMinPrice;
                }
                else
                {
                    this._openPrice = value;
                }
                if (this._openPrice != 0)
                {
                    this.LimitUpPrice = Math.Round(this._openPrice * 1.1m, 2);
                    this.LimitDownPrice = Math.Round(this._openPrice * 0.9m, 2);
                    if (this.LimitDownPrice < SystemConfig.GameConfig.Server1StackMarketMinPrice)
                    {
                        this.LimitDownPrice = SystemConfig.GameConfig.Server1StackMarketMinPrice;
                    }
                }

#else
                
                if (value < SystemConfig.GameConfig.Server2StackMarketMinPrice)
                {
                    this._openPrice = SystemConfig.GameConfig.Server2StackMarketMinPrice;
                }
                else
                {
                    this._openPrice = value;
                }
                if (this._openPrice != 0)
                {
                    this.LimitUpPrice = Math.Round(this._openPrice * 1.1m, 2);
                    this.LimitDownPrice = Math.Round(this._openPrice * 0.9m, 2);
                    if (this.LimitDownPrice < SystemConfig.GameConfig.Server2StackMarketMinPrice)
                    {
                        this.LimitDownPrice = SystemConfig.GameConfig.Server2StackMarketMinPrice;
                    }
                }

#endif

#else

                this._openPrice = value;

#endif

            }
        }

        [DataMember]
        public decimal ClosePrice;

        /// <summary>
        /// 涨停价= OpenPrice * 110%(取两位小数)
        /// </summary>
        [DataMember]
        public decimal LimitUpPrice;

        /// <summary>
        /// 跌停价= OpenPrice * 90%(取两位小数)
        /// </summary>
        [DataMember]
        public decimal LimitDownPrice;

        /// <summary>
        /// 最低成交价(计买入价)
        /// </summary>
        [DataMember]
        public decimal MinTradeSucceedPrice = decimal.MaxValue;

        /// <summary>
        /// 最高成交价(计买入价)
        /// </summary>
        [DataMember]
        public decimal MaxTradeSucceedPrice;

        [DataMember]
        public int TradeSucceedStoneHandSum;

        [DataMember]
        public decimal TradeSucceedRMBSum;

        [DataMember]
        public int DelegateSellStoneSum;

        [DataMember]
        public int DelegateBuyStoneSum;

        public StoneStackDailyRecordInfo Copy()
        {
            return new StoneStackDailyRecordInfo()
            {
                Day = this.Day,
                OpenPrice = this.OpenPrice,
                ClosePrice = this.ClosePrice,
                DelegateBuyStoneSum = this.DelegateBuyStoneSum,
                DelegateSellStoneSum = this.DelegateSellStoneSum,
                LimitDownPrice = this.LimitDownPrice,
                LimitUpPrice = this.LimitUpPrice,
                MaxTradeSucceedPrice = this.MaxTradeSucceedPrice,
                MinTradeSucceedPrice = this.MinTradeSucceedPrice,
                TradeSucceedRMBSum = this.TradeSucceedRMBSum,
                TradeSucceedStoneHandSum = this.TradeSucceedStoneHandSum
            };
        }

    }
}
