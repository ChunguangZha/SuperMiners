using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.User
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
    /// </summary>
    [DataContract]
    public class PlayerStoneFactoryAccountInfo
    {
        /// <summary>
        /// 工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
        /// </summary>
        public bool FactoryIsOpening = false;
        

        [DataMember]
        public int UserID;

        /// <summary>
        /// 1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        [DataMember]
        public int StackCount;

        /// <summary>
        /// 凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
        /// </summary>
        [DataMember]
        public StoneFactoryOneGroupSlave[] Slaves;

        /// <summary>
        /// 食物，该值每天0时需按奴隶值检查减值，充值时相应增加。
        /// </summary>
        [DataMember]
        public int Food;

        /// <summary>
        /// 只保存没有被提取的记录
        /// </summary>
        [DataMember]
        public StoneFactoryOutputRMBRecord[] OutputRMBRecords;

        /// <summary>
        /// 总计生产出的灵币值
        /// 该值每天都在变化（有进有出）
        /// </summary>
        [DataMember]
        public int TotalSumTempRMB;

        /// <summary>
        /// 当前可提取的灵币值（18天前生产出的灵币）
        /// 该值每天都在变化（有进有出）
        /// </summary>
        [DataMember]
        public int CurrentWithdrawableTempRMB;

        /// <summary>
        /// 当前不可提取的灵币值（18天内（含）生产出的灵币）
        /// 该值每天都在变化（有进有出）
        /// </summary>
        [DataMember]
        public int CurrentCanotWithdrawableTempRMB;

    }

    /// <summary>
    /// 加工厂每天生产出灵币记录信息，每天一条记录
    /// </summary>
    [DataContract]
    public class StoneFactoryOutputRMBRecord
    {
        /// <summary>
        /// 加工厂生产出的灵币
        /// </summary>
        [DataMember]
        public int OutputRMB;

        /// <summary>
        /// 生产时间
        /// </summary>
        [DataMember]
        public MyDateTime OutputTime;

        /// <summary>
        /// 是否已经提现
        /// </summary>
        [DataMember]
        public bool IsWithdrawed;

    }

    /// <summary>
    /// 一组奴隶信息（一次兑换的奴隶）
    /// </summary>
    [DataContract]
    public class StoneFactoryOneGroupSlave
    {
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
