using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
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
        public float AwardGoldCoin;
    }
}
