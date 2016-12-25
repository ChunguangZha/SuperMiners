using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.RaideroftheLostArk
{
    /// <summary>
    /// 此结构为保存到数据库的结构
    /// </summary>
    public class RaiderRoundMetaDataInfo
    {
        public int ID;

        public MyDateTime StartTime;

        public int AwardPoolSumStones;

        public string WinnerUserName;

        public int WinStones;

        public MyDateTime EndTime;
    }

    public class PlayerBetInfo
    {
        public int ID;

        public int RaiderRoundID;

        public int UserID;

        public int BetStones;

        public MyDateTime Time;
    }
}
