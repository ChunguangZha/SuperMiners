using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    [DataContract]
    public class GoldCoinRechargeRecord
    {
        public int UserID;

        [DataMember]
        public string UserName = "";

        [DataMember]
        public string OrderNumber = "";

        /// <summary>
        /// 花费灵币
        /// </summary>
        [DataMember]
        public decimal SpendRMB = 0;

        /// <summary>
        /// 获取金币值
        /// </summary>
        [DataMember]
        public decimal GainGoldCoin = 0;

        [DataMember]
        public DateTime CreateTime;

        /// <summary>
        /// 临时表不保存该字段
        /// </summary>
        [DataMember]
        public DateTime PayTime;
    }
}
