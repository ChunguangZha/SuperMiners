using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData
{
    /// <summary>
    /// 销售矿石派单
    /// </summary>
    [DataContract]
    public class SellStonesOrder
    {
        /// <summary>
        /// 订单编号，以日期+卖方用户名HashCode+4位随机数
        /// </summary>
        [DataMember]
        public string OrderNumber = "";

        /// <summary>
        /// 卖方用户名
        /// </summary>
        [DataMember]
        public string SellerUserName = "";

        /// <summary>
        /// 出售矿石数
        /// </summary>
        [DataMember]
        public int SellStonesCount = 0;

        /// <summary>
        /// 手续费
        /// </summary>
        [DataMember]
        public float Expense = 0;

        /// <summary>
        /// 可获取的RMB数
        /// </summary>
        [DataMember]
        public float GainRMB = 0;

        public DateTime SellTime;
        [DataMember]
        public string SellTimeString
        {
            get
            {
                return this.SellTime.ToString();
            }
            set
            {
                try
                {
                    SellTime = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    SellTime = Common.INVALIDTIME;
                }
            }
        }

        [DataMember]
        public SellOrderState OrderState = SellOrderState.Wait;

    }

    public enum SellOrderState
    {
        Wait,
        Lock,
        Finish,
        Exception
    }
}
