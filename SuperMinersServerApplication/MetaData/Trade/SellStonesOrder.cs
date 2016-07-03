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
        /// 买方需要支付的RMB数（即总价值），卖方可收获的RMB=ValueRMB-Expense;
        /// </summary>
        [DataMember]
        public float ValueRMB = 0;

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

        public SellOrderState OrderState
        {
            get
            {
                return (SellOrderState)this.OrderStateInt;
            }
            set
            {
                this.OrderStateInt = (int)value;
            }
        }

        [DataMember]
        public int OrderStateInt;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("订单号: ");
            builder.Append(this.OrderNumber);
            builder.Append(",");
            builder.Append("卖方: ");
            builder.Append(this.SellerUserName);
            builder.Append(",");
            builder.Append("出售矿石数: ");
            builder.Append(this.SellStonesCount);
            builder.Append(",");
            builder.Append("手续费: ");
            builder.Append(this.Expense);
            builder.Append(",");
            builder.Append("价值人民币: ");
            builder.Append(this.ValueRMB);
            builder.Append(",");
            builder.Append("出售时间: ");
            builder.Append(this.SellTime);

            return builder.ToString();
        }
    }

    public enum SellOrderState
    {
        Wait = 1,
        Lock,
        Finish,
        Exception
    }
}
