﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.Roulette
{
    [DataContract]
    public class RouletteAwardItem
    {
        [DataMember]
        public int ID;

        [DataMember]
        public string AwardName;

        [DataMember]
        public int AwardNumber;

        [DataMember]
        public RouletteAwardType RouletteAwardType;

        /// <summary>
        /// 价值人民币
        /// </summary>
        [DataMember]
        public float ValueMoneyYuan;

        [DataMember]
        public bool IsLargeAward;

        /// <summary>
        /// 是否为实物奖品，除了系统内部的都称为实物
        /// </summary>
        [DataMember]
        public bool IsRealAward;

        /// <summary>
        /// 中奖概率，所有奖项概率加一块为1
        /// </summary>
        [DataMember]
        public float WinProbability;
    }

    public enum RouletteAwardType
    {
        None,
        Stone,
        GoldCoin,
        Exp,
        StoneReserve,
        Huafei,
        IQiyiOneMonth,
        LeTV,
        Xunlei,
        Junnet
    }

    //500、300、200、100矿石、500金币、1000金币、5000金币、1、2、3贡献值、矿石储量、10、50话费、爱奇艺会员一个月、乐视会员、迅雷会员、骏网一卡通
}
