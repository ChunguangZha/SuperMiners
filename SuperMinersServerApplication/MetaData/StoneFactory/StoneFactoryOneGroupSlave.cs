using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.StoneFactory
{
    /// <summary>
    /// 一组奴隶信息（一次兑换的奴隶）
    /// </summary>
    [DataContract]
    public class StoneFactoryOneGroupSlave
    {
        [DataMember]
        public int UserID;

        /// <summary>
        /// 投入的奴隶数
        /// </summary>
        [DataMember]
        public int JoinInSlaveCount;

        /// <summary>
        /// 还生存的奴隶数，如果玩家购买的食物不足够所有奴隶使用，则没有食物的奴隶死亡。
        /// </summary>
        [DataMember]
        public int LiveSlaveCount;

        /// <summary>
        /// 兑换时间
        /// </summary>
        [DataMember]
        public MyDateTime ChargeTime;

        /// <summary>
        /// 奴隶是否活着
        /// </summary>
        [DataMember]
        public bool isLive;

        /// <summary>
        /// 剩余寿命（天数，最多2天）,该值每天0时需自减，为0时，先检查是否有食物，有，则相应数量的奴隶继续活着，该值恢复为2天；否则该组奴隶全部死亡
        /// </summary>
        [DataMember]
        public byte LifeDays;

    }
}
