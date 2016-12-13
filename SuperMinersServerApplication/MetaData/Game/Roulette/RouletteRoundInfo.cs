using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.Roulette
{
    /// <summary>
    /// 每一轮幸运大转盘累计信息
    /// </summary>
    [DataContract]
    public class RouletteRoundInfo
    {
        [DataMember]
        public int ID;

        /// <summary>
        /// 奖池累计矿石
        /// </summary>
        [DataMember]
        public int AwardPoolSumStone;

        /// <summary>
        /// 本轮累计中出价值人民币元
        /// </summary>
        [DataMember]
        public decimal WinAwardSumYuan;

        public DateTime StartTime = Common.INVALIDTIME;

        [DataMember]
        public string StartTimeString
        {
            get
            {
                if (this.StartTime == null)
                {
                    return "";
                }
                return this.StartTime.ToString();
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!DateTime.TryParse(value, out StartTime))
                        {
                            StartTime = Common.INVALIDTIME;
                        }
                    }
                }
                catch (Exception)
                {
                    StartTime = Common.INVALIDTIME;
                }
            }
        }

        [DataMember]
        public int MustWinAwardItemID;

        [DataMember]
        public bool Finished;
    }
}
