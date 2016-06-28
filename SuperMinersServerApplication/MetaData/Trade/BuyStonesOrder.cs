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

        /// <summary>
        /// 买方付出的RMB数，该值不一定等于SellStonesOrder.GainRMB，应该按服务器配置计算得出。
        /// </summary>
        [DataMember]
        public float PayRMB;

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
