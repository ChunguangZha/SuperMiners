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
