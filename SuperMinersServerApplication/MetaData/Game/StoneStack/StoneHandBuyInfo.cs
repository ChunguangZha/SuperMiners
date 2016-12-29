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
    /// 矿石委托买入订单
    /// </summary>
    [DataContract]
    public class StoneDelegateBuyOrderInfo
    {
        [DataMember]
        public string OrderNumber;

        [DataMember]
        public int UserID;

        ///// <summary>
        ///// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //private long _playerCreditValue = 0;

        ///// <summary>
        ///// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //public long PlayerCreditValue
        //{
        //    get { return this._playerCreditValue; }
        //    set
        //    {
        //        this._playerCreditValue = value;
        //        PlayerCreditLevel = CreditLevelConfig.GetCreditLevel(value);
        //    }
        //}

        ///// <summary>
        ///// 只服务器端使用。只读字段，只能从PlayerCreditValue中赋值，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //public int PlayerCreditLevel = 0;

        ///// <summary>
        ///// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //private int _playerExpValue;

        ///// <summary>
        ///// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //public int PlayerExpValue
        //{
        //    get { return _playerExpValue; }
        //    set
        //    {
        //        _playerExpValue = value;
        //        PlayerExpLevel = value / CreditLevelConfig.UserExpLevelValue;
        //    }
        //}

        ///// <summary>
        ///// 只服务器端使用。只读字段，只能从SellerExpValue中赋值，从数据库加载时，从PlayerFortuneInfo表中获取
        ///// </summary>
        //public int PlayerExpLevel = 0;


        /// <summary>
        /// 一手矿石委托买入的价格。注：一手为1000块矿石
        /// </summary>
        [DataMember]
        public StackTradeUnit BuyUnit = null;

        /// <summary>
        /// 实际完成的矿石手数。由于订单可能被拆分处理，所以该值一定小于等于委托值。
        /// </summary>
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
    }

    public enum StoneDelegateBuyState
    {
        Waiting,
        Succeed,
        Splited,
        Rejected
    }
}
