using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Model
{
    public class SellStoneOrderUIModel
    {
        public SellStonesOrder _parentObject = null;

        public SellStonesOrder ParentObject
        {
            get { return _parentObject; }
            set
            {
                this._parentObject = value;
            }
        }

        public SellStoneOrderUIModel(SellStonesOrder parent)
        {
            this.ParentObject = parent;
        }

        /// <summary>
        /// 订单编号，以日期+卖方用户名HashCode+4位随机数
        /// </summary>
        public string OrderNumber
        {
            get { return this.ParentObject.OrderNumber; }
        }

        /// <summary>
        /// 卖方用户名
        /// </summary>
        public string SellerUserName
        {
            get { return this.ParentObject.SellerUserName; }
        }

        /// <summary>
        /// 出售矿石数
        /// </summary>
        public int SellStonesCount
        {
            get { return this.ParentObject.SellStonesCount; }
        }

        /// <summary>
        /// 买方需要支付的RMB数（即总价值），卖方可收获的RMB=ValueRMB-Expense;
        /// </summary>
        public decimal ValueRMB
        {
            get { return this.ParentObject.ValueRMB; }
        }

        public DateTime SellTime
        {
            get { return this.ParentObject.SellTime; }
        }

        public SellOrderState OrderState
        {
            get { return this.ParentObject.OrderState; }
        }


    }
}