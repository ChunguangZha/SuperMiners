using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.ActionLog
{
    [DataContract]
    public class PlayerActionLog
    {
        [DataMember]
        public string UserName;

        [DataMember]
        public ActionType ActionType;

        [DataMember]
        public int OperNumber;

        [DataMember]
        public string Remark;

        public DateTime Time;
        [DataMember]
        public string LastLoginTimeString
        {
            get
            {
                return this.Time.ToString();
            }
            set
            {
                try
                {
                    Time = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    Time = new DateTime(1990, 1, 1);
                }
            }
        }

        [DataMember]
        public int SystemAllPlayerCount;

        [DataMember]
        public int SystemAllMinerCount;

        [DataMember]
        public float SystemAllOutputStoneCount;

    }

    public enum ActionType
    {
        Register,
        Refer,
        RMBRecharge,
        GoldCoinRecharge,
        BuyMine,
        BuyMiner,
        SellStone,
        SellDiamond,
        GatherStone,
        Login,
    }
}
