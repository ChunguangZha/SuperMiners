using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class BuyStonesOrder
    {
        /// <summary>
        /// </summary>
        [DataMember]
        public SellStonesOrder StonesOrder;

        [DataMember]
        public string BuyerUserName = "";

        public DateTime BuyTime;
        [DataMember]
        public string BuyTimeString
        {
            get
            {
                return this.BuyTime.ToString();
            }
            set
            {
                try
                {
                    BuyTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    BuyTime = Common.INVALIDTIME;
                }
            }
        }

        [DataMember]
        public decimal AwardGoldCoin;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("买方: ");
            builder.Append(this.BuyerUserName);
            builder.Append(",");
            builder.Append("购买时间: ");
            builder.Append(this.BuyTime);
            builder.Append(",");
            builder.Append(StonesOrder.ToString());

            return builder.ToString();
        }
    }
}
