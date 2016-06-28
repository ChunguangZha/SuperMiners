using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    [DataContract]
    public class LockSellStonesOrder
    {
        /// <summary>
        /// 订单编号，以日期+卖方用户名HashCode+4位随机数
        /// </summary>
        [DataMember]
        public SellStonesOrder StonesOrder;

        [DataMember]
        public string LockedByUserName = null;

        public DateTime LockedTime;
        [DataMember]
        public string LockedTimeString
        {
            get
            {
                if (this.LockedTime == null)
                {
                    return "";
                }
                return this.LockedTime.ToString();
            }
            set
            {
                try
                {
                    LockedTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    LockedTime = Common.INVALIDTIME;
                }
            }
        }

    }
}
