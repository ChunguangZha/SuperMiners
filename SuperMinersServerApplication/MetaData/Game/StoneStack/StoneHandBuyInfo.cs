using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.StoneStack
{
    /// <summary>
    /// 矿石委托买入订单
    /// </summary>
    [DataContract]
    public class StoneDelegateBuyOrderInfo
    {
        [DataMember]
        public int UserID;

        /// <summary>
        /// 一手矿石委托买入的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public StackTradeUnit BuyUnit = null;

        [DataMember]
        public StoneDelegateBuyState BuyState;

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

    public enum StoneDelegateBuyState
    {
        Waiting,
        Succeed,
        Rejected
    }
}
