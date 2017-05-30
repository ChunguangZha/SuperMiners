using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.StoneFactory
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），无论多少苦力只需投喂一次，超过48小时苦力将会死亡。。。
    /// </summary>
    [DataContract]
    public class PlayerStoneFactoryAccountInfo
    {
        [DataMember]
        public int ID;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

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
        /// 冻结奴隶组数（100个奴隶为一组）。当天存入的奴隶先保存到冻结奴隶中。第二天0时结算时再加入到可用奴隶中。
        /// </summary>
        [DataMember]
        public int FreezingSlaveGroupCount;

        /// <summary>
        /// 可用奴隶组数（100个奴隶为一组）。凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
        /// </summary>
        [DataMember]
        public int EnableSlavesGroupCount;

        /// <summary>
        /// 食物（以所以奴隶（100矿工）一天口粮为单位），该值每天0时需按奴隶值检查减值，充值时相应增加，每次投入奴隶时，会自动增加本次奴隶两天的食物。
        /// </summary>
        [DataMember]
        public int Food;

        /// <summary>
        /// 上一次投喂奴隶时间（新加入的苦力以零时转换为可用苦力开始计时）
        /// </summary>
        [DataMember]
        public MyDateTime LastFeedSlaveTime;

        /// <summary>
        /// 奴隶寿命倒计时（秒）
        /// </summary>
        [DataMember]
        public int SlaveLiveDiscountms;

        /// <summary>
        /// 前一天有效矿石，每天0点计算前一天有效矿石（即前一天之前存入的矿石和前一天前存入的存活的奴隶），每天14点，会开出前一天盈利点，按盈利点*LastDayValidStone，得出前一天收益。
        /// </summary>
        [DataMember]
        public int LastDayValidStoneStack;

        /// <summary>
        /// 总可用股数，不包含冻结矿石（该值不存入数据库，每次查询数据库中所有股权变更记录累计得出）。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        [DataMember]
        public int TotalStackCount;

        /// <summary>
        /// 冻结的矿石股数（即当天存入的矿石为冻结状态，第二天0时检查时将期算为可用）
        /// </summary>
        [DataMember]
        public int FreezingStackCount;

        /// <summary>
        /// 可以提取的股数（该值不存入数据库，每次查询数据库中所有股权变更记录累计得出）。1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
        /// </summary>
        [DataMember]
        public int WithdrawableStackCount;

        /// <summary>
        /// 总计生产出的灵币值（该值不存入数据库，每次查询数据库中所有灵币收益变更记录累计得出）
        /// </summary>
        [DataMember]
        public decimal TotalProfitRMB;

        /// <summary>
        /// 当前可提取的灵币值（18天前生产出的灵币）（该值不存入数据库，每次查询数据库中所有灵币收益变更记录累计得出）
        /// </summary>
        [DataMember]
        public decimal WithdrawableProfitRMB;

        /// <summary>
        /// 昨日总收益
        /// </summary>
        [DataMember]
        public decimal YesterdayTotalProfitRMB;
        
        ///// <summary>
        ///// 当前不可提取的灵币值（18天内（含）生产出的灵币）（该值不存入数据库，每次查询数据库中所有灵币收益变更记录累计得出）
        ///// </summary>
        //[DataMember]
        //public int CurrentCanotWithdrawableTempRMB;

    }

}
