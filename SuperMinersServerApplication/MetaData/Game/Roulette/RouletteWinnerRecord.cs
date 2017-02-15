using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.Roulette
{
    /// <summary>
    /// 幸运转盘中奖记录
    /// </summary>
    [DataContract]
    public class RouletteWinnerRecord
    {
        [DataMember]
        public int RecordID;

        [DataMember]
        public int UserID;

        [DataMember]
        public string UserName;

        //[DataMember]
        //public string UserNickName;

        public int RouletteAwardItemID;

        [DataMember]
        public RouletteAwardItem AwardItem;

        [DataMember]
        public MyDateTime WinTime;

        /// <summary>
        /// 是否已领取
        /// </summary>
        [DataMember]
        public bool IsGot;

        /// <summary>
        /// 允许为null
        /// </summary>
        [DataMember]
        public MyDateTime GotTime;

        /// <summary>
        /// 是否已支付
        /// </summary>
        [DataMember]
        public bool IsPay;

        /// <summary>
        /// 允许为null
        /// </summary>
        [DataMember]
        public MyDateTime PayTime;

        [DataMember]
        public string GotInfo1;

        [DataMember]
        public string GotInfo2;

        public override string ToString()
        {
            return "玩家[" + UserName + "]   幸运抽中" + AwardItem.AwardName;
        }
    }
}
