using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    /// <summary>
    /// 矿石委托销售订单
    /// </summary>
    [DataContract]
    public class StoneDelegateSellOrderInfo
    {
        [DataMember]
        public int UserID;

        /// <summary>
        /// 一手矿石出售的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public StackTradeUnit SellUnit = null;

        [DataMember]
        public StoneDelegateSellState SellState;

        /// <summary>
        /// 挂单时间
        /// </summary>
        [DataMember]
        public MyDateTime DelegateTime;

        /// <summary>
        /// 完成时间
        /// </summary>
        [DataMember]
        public MyDateTime FinishedTime;
    }

    public enum StoneDelegateSellState
    {
        Waiting,
        Succeed,
        Rejected
    }
}
