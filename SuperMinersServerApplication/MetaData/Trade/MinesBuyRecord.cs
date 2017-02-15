using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 矿山购买记录
    /// </summary>
    [DataContract]
    public class MinesBuyRecord
    {
        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName = "";

        [DataMember]
        public string OrderNumber = "";

        /// <summary>
        /// 花费的灵币数
        /// </summary>
        [DataMember]
        public int SpendRMB = 0;

        /// <summary>
        /// 获取矿山数
        /// </summary>
        [DataMember]
        public decimal GainMinesCount = 0;

        /// <summary>
        /// 获取的矿石储量，将该值累加到用户的StonesReserves值中
        /// </summary>
        [DataMember]
        public int GainStonesReserves = 0;

        public DateTime CreateTime;

        [DataMember]
        public MyDateTime MyCreateTime
        {
            get
            {
                return MyDateTime.FromDateTime(CreateTime);
            }
            set
            {
                this.CreateTime = value.ToDateTime();
            }
        }

        /// <summary>
        /// 临时表不保存该字段
        /// </summary>
        public DateTime PayTime;

        [DataMember]
        public MyDateTime MyPayTime
        {
            get
            {
                return MyDateTime.FromDateTime(PayTime);
            }
            set
            {
                this.PayTime = value.ToDateTime();
            }
        }
    }
}
