using MetaData.SystemConfig;
using MetaData.Trade;
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
        /// 一手矿石委托买入的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public StackTradeUnit BuyUnit = null;

        [DataMember]
        public PayType PayType = PayType.RMB;

        /// <summary>
        /// 实际完成的矿石手数。由于订单可能被拆分处理，所以该值一定小于等于委托值。
        /// </summary>
        [DataMember]
        public int FinishedStoneTradeHandCount = 0;

        [DataMember]
        public StoneDelegateBuyState BuyState = StoneDelegateBuyState.Waiting;

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
        /// 当为子订单时的根订单号，
        /// </summary>
        [DataMember]
        public string ParentOrderNumber = null;

        [DataMember]
        public decimal AwardGoldCoin = 0;

        /// <summary>
        /// 只对于未完成挂单有效，如果支付方式为支付宝里，该变量保存支付链接
        /// </summary>
        [DataMember]
        public string AlipayLink;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("OrderNumber: " + OrderNumber + "; ");
            builder.Append("UserID: " + UserID + "; ");
            builder.Append("UserName: " + UserName + "; ");
            builder.Append("Price: " + BuyUnit.Price + "; ");
            builder.Append("TradeCount: " + BuyUnit.TradeStoneHandCount + "; ");
            builder.Append("PayType: " + PayType + "; ");
            builder.Append("FinishedStoneTradeHandCount: " + FinishedStoneTradeHandCount + "; ");
            builder.Append("BuyState: " + BuyState + "; ");
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
            builder.Append("AwardGoldCoin: " + AwardGoldCoin + "; ");
            if (AlipayLink != null)
            {
                builder.Append("AlipayLink: " + AlipayLink + "; ");
            }

            return builder.ToString();
        }
    }

    public enum StoneDelegateBuyState
    {
        Waiting,
        Succeed,
        Splited,
        Rejected,
        /// <summary>
        /// 撤消
        /// </summary>
        Cancel,
        Exception,
        /// <summary>
        /// 主要给支付宝订单使用，指提交了订单，并没有支付宝付款
        /// </summary>
        NotPayed
    }
}
