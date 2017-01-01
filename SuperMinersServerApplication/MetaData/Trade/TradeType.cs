using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Trade
{
    /// <summary>
    /// 值为2位Int
    /// </summary>
    public enum AlipayTradeInType
    {
        /// <summary>
        /// 矿石交易
        /// </summary>
        BuyStone = 11,

        /// <summary>
        /// 矿山交易
        /// </summary>
        BuyMine = 12,

        /// <summary>
        /// 购买矿工
        /// </summary>
        BuyMiner = 13,

        /// <summary>
        /// 充值灵币
        /// </summary>
        BuyRMB = 21,

        /// <summary>
        /// 充值金币
        /// </summary>
        BuyGoldCoin = 22,

        StackStoneBuy = 30,

        StackStoneSell=31,

    }

    /// <summary>
    /// 订单支付方式
    /// </summary>
    public enum PayType
    {
        Alipay = 0,
        RMB = 1,
        GoldCoin = 2,
        Diamand = 3
    }
}
