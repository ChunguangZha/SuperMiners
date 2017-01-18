using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    /// <summary>
    /// 赌石一局信息，每月一张表（64局为一轮）
    /// </summary>
    public class GambleStoneInningInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string ID;

        public int RoundID;

        public MyDateTime StartTime;

        public int CountDownSeconds;

        public MyDateTime EndTime;

        public int BetRedStone;

        public int BetGreenStone;

        public int BetBlueStone;

        public int BetPurpleStone;

        public GambleStoneItemColor WinnedColor;

        public int WinnedTimes;

        public int WinnedOutStone;
    }

    public class GambleStoneRoundInfo
    {
        public int ID;

        public MyDateTime StartTime;

        public int FinishedInningCount;

        public MyDateTime EndTime;

        public int AllBetInStone;

        public int AllWinnedOutStone;

        public string TableName;
    }

    public class GambleStoneDailyScheme
    {
        public MyDateTime Date;

        public int ProfitStoneObjective;

        public int AllBetInStone;

        public int AllWinnedOutStone;
    }
}
