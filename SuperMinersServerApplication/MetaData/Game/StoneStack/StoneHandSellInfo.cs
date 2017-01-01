using MetaData.SystemConfig;
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
        /// <summary>
        /// 订单号唯一，拆分的子订单使用新的订单号
        /// </summary>
        [DataMember]
        public string OrderNumber;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;
                
        /// <summary>
        /// 一手矿石出售的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public StackTradeUnit SellUnit = null;

        /// <summary>
        /// 实际完成的矿石手数。由于订单可能被拆分处理，所以该值一定小于等于委托值。
        /// </summary>
        [DataMember]
        public int FinishedStoneTradeHandCount = 0;

        [DataMember]
        public StoneDelegateSellState SellState = StoneDelegateSellState.Waiting;

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

        [DataMember]
        public bool IsSubOrder = false;

        /// <summary>
        /// 当为子订单是的父订单号
        /// </summary>
        [DataMember]
        public string ParentOrderNumber = null;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("OrderNumber: " + OrderNumber + "; ");
            builder.Append("UserID: " + UserID + "; ");
            builder.Append("UserName: " + UserName + "; ");
            builder.Append("Price: " + SellUnit.Price + "; ");
            builder.Append("TradeCount: " + SellUnit.TradeStoneHandCount + "; ");
            builder.Append("FinishedStoneTradeHandCount: " + FinishedStoneTradeHandCount + "; ");
            builder.Append("SellState: " + SellState + "; ");
            builder.Append("DelegateTime: " + DelegateTime.ToDateTime().ToString() + "; ");
            if (FinishedTime != null)
            {
                builder.Append("FinishedTime: " + FinishedTime.ToDateTime().ToString() + "; ");
            }
            builder.Append("IsSubOrder: " + IsSubOrder + "; ");
            if (ParentOrderNumber != null)
            {
                builder.Append("ParentOrderNumber: " + ParentOrderNumber + "; ");
            }

            return builder.ToString();
        }
    }

    public enum StoneDelegateSellState
    {
        Waiting,
        Succeed,
        Splited,
        Rejected,
        Cancel
    }
}
