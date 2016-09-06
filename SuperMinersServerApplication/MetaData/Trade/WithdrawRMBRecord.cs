using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class WithdrawRMBRecord
    {
        [DataMember]
        public int id;

        [DataMember]
        public string PlayerUserName = "";

        [DataMember]
        public decimal WidthdrawRMB = 0;

        /// <summary>
        /// 提现人民币金额全部向下取整
        /// </summary>
        [DataMember]
        public int ValueYuan = 0;

        public DateTime CreateTime;
        [DataMember]
        public string CreateTimeString
        {
            get
            {
                return this.CreateTime.ToString();
            }
            set
            {
                try
                {
                    CreateTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    CreateTime = Common.INVALIDTIME;
                }
            }
        }


        [DataMember]
        public bool IsPayedSucceed = false;

        [DataMember]
        public string AdminUserName;

        [DataMember]
        public string AlipayOrderNumber = "";

        public DateTime? PayTime;
        [DataMember]
        public string PayTimeString
        {
            get
            {
                if (PayTime == null || !PayTime.HasValue)
                {
                    return "";
                }
                return this.PayTime.Value.ToString();
            }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        PayTime = null;
                    }
                    PayTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    PayTime = Common.INVALIDTIME;
                }
            }
        }

    }
}
