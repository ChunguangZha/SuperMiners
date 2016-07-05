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

        [DataMember]
        public string PayUrl = null;

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

        /// <summary>
        /// 非数据库字段。该订单已经锁定的时间长度（秒），当该值大于等于服务器设置的订单超时时间，则订单自动解锁。
        /// 每次返回给Wcf之前，用服务器当前时间-LockedTime得出。客户端以该值为基本进行倒计（会有小量的时间差）
        /// </summary>
        [DataMember]
        public int OrderLockedTimeSpan;
    }
}
