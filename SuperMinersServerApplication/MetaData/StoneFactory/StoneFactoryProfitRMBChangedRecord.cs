using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.StoneFactory
{
    /// <summary>
    /// 加工厂每天生产出收益（灵币）记录信息，每天一条记录
    /// </summary>
    [DataContract]
    public class StoneFactoryProfitRMBChangedRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        /// <summary>
        /// 操作灵币，即包括加工厂生产出的收益入账、推荐注册的三级内玩家收获奖励入账，也包括提现出账（出账时值为负数）
        /// </summary>
        [DataMember]
        public decimal OperRMB;

        [DataMember]
        public FactoryProfitOperType ProfitType;

        /// <summary>
        /// 操作时间
        /// </summary>
        [DataMember]
        public MyDateTime OperTime;

    }

    public enum FactoryProfitOperType
    {
        /// <summary>
        /// 加工厂自行产出收益
        /// </summary>
        FactoryOutput,

        /// <summary>
        /// 推荐奖励收益
        /// </summary>
        ReferenceAward,

        /// <summary>
        /// 灵币提现
        /// </summary>
        WithdrawRMB,
    }

}
