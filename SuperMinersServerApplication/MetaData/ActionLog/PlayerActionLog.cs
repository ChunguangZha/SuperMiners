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
        public decimal OperNumber;

        [DataMember]
        public string Remark;

        public DateTime Time = Common.INVALIDTIME;
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
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (!DateTime.TryParse(value, out Time))
                        {
                            Time = Common.INVALIDTIME;
                        }
                    }
                }
                catch (Exception)
                {
                    Time = Common.INVALIDTIME;
                }
            }
        }

    }

    public enum ActionType
    {
        Register,
        Refer,
        RMBRecharge,
        GoldCoinRecharge,
        BuyMine,
        BuyMiner,
        BuyStone,
        SellStone,
        SellDiamond,
        GatherStone,
        Login,
        GameFunny,
        WithdrawRMB,
        DelegateBuyStone,
        DelegateSellStone,
        DelegateBuyStoneSucceed,
        DelegateSellStoneSucceed,
        GameRaiderJoinBet,
        GameRaiderWin
    }
}
