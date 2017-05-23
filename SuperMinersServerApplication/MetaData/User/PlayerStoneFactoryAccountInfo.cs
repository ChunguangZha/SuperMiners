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
        [DataMember]
        public int UserID;

        /// <summary>
        /// 工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
        /// </summary>
        [DataMember]
        public bool FactoryIsOpening = false;

        /// <summary>
        /// 工厂剩余可用天数，默认值为3天，当0点检查时，如果工厂状态开启，又没有可用的矿石或奴隶，则该值减一天。直到减为0，工厂关闭。
        /// 
        /// </summary>
        [DataMember]
        public int FactoryLiveDays = 3;

        /// <summary>
        /// 总股数。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        [DataMember]
        public int TotalStackCount;

        /// <summary>
        /// 可以提取的股数。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        [DataMember]
        public int WithdrawableStackCount;

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
        /// 前一天有效矿石，每天0点计算前一天有效矿石（即前一天之前存入的矿石和前一天前存入的存活的奴隶），每天14点，会开出前一天盈利点，按盈利点*LastDayValidStone，得出前一天收益。
        /// </summary>
        [DataMember]
        public float LastDayValidStone;

        /// <summary>
        /// 只保存没有被提取的记录
        /// </summary>
        [DataMember]
        public StoneFactoryOutputRMBRecord[] OutputRMBRecords;

        /// <summary>
        /// 总计生产出的灵币值
        /// 该值每天都在变化（每天14点进账，只增不减）
        /// </summary>
        [DataMember]
        public int TotalSumTempRMB;

        /// <summary>
        /// 当前可提取的灵币值（18天前生产出的灵币）
        /// （每天14点进账，玩家提取时减值）
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
    /// 加工厂股权变更记录，入出都记录（正数为投入；负数为提取）
    /// </summary>
    public class StoneFactoryStackChangeRecord
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public int JoinStoneStackCount;

        [DataMember]
        public MyDateTime Time;
    }

    /// <summary>
    /// 加工厂每天生产出收益（灵币）记录信息，每天一条记录
    /// </summary>
    [DataContract]
    public class StoneFactoryOutputRMBRecord
    {
        [DataMember]
        public int ID;

        /// <summary>
        /// 加工厂生产出的收益（灵币）
        /// </summary>
        [DataMember]
        public int OutputRMB;

        [DataMember]
        public FactoryProfitType ProfitType;

        /// <summary>
        /// 产出时间（年）
        /// </summary>
        [DataMember]
        public int Year;

        /// <summary>
        /// 产出时间（月）
        /// </summary>
        [DataMember]
        public int Month;

        /// <summary>
        /// 产出时间（日）
        /// </summary>
        [DataMember]
        public int Day;

    }

    public enum FactoryProfitType
    {
        /// <summary>
        /// 加工厂自行产出收益
        /// </summary>
        FactoryOutput,

        /// <summary>
        /// 推荐奖励收益
        /// </summary>
        ReferenceAward
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
