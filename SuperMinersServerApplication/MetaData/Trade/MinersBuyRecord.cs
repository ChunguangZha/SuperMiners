using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 矿工购买记录。（注：矿工是金币交易，不需要订单号）
    /// </summary>
    [DataContract]
    public class MinersBuyRecord
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName = "";

        /// <summary>
        /// 花费的金币数
        /// </summary>
        [DataMember]
        public decimal SpendGoldCoin = 0;

        /// <summary>
        /// 获取矿工数
        /// </summary>
        [DataMember]
        public int GainMinersCount = 0;

        public DateTime Time;

        [DataMember]
        public MyDateTime MyTime
        {
            get
            {
                return MyDateTime.FromDateTime(Time);
            }
            set
            {
                this.Time = value.ToDateTime();
            }
        }
    }
}
