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
        [DataMember]
        public int UserID;

        /// <summary>
        /// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        private long _playerCreditValue = 0;

        /// <summary>
        /// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        public long PlayerCreditValue
        {
            get { return this._playerCreditValue; }
            set
            {
                this._playerCreditValue = value;
                PlayerCreditLevel = CreditLevelConfig.GetCreditLevel(value);
            }
        }

        /// <summary>
        /// 只服务器端使用。只读字段，只能从PlayerCreditValue中赋值，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        public int PlayerCreditLevel = 0;

        /// <summary>
        /// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        private int _playerExpValue;

        /// <summary>
        /// 只服务器端使用。非数据库字段，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        public int PlayerExpValue
        {
            get { return _playerExpValue; }
            set
            {
                _playerExpValue = value;
                PlayerExpLevel = value / CreditLevelConfig.UserExpLevelValue;
            }
        }

        /// <summary>
        /// 只服务器端使用。只读字段，只能从SellerExpValue中赋值，从数据库加载时，从PlayerFortuneInfo表中获取
        /// </summary>
        public int PlayerExpLevel = 0;

        
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
