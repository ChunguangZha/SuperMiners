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

        [DataMember]
        public RouletteAwardItem AwardItem;

        [DataMember]
        public DateTime WinTime;

        /// <summary>
        /// 是否已领取
        /// </summary>
        [DataMember]
        public bool IsGot;

        /// <summary>
        /// 是否已支付
        /// </summary>
        [DataMember]
        public bool IsPay;

        [DataMember]
        public string GotInfo1;

        [DataMember]
        public string GotInfo2;
    }
}
