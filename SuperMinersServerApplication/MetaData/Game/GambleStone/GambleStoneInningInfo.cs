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

        /// <summary>
        /// 第N局（1-64）
        /// </summary>
        public int InningIndex;

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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("第{0}轮;", RoundID);
            builder.AppendFormat("第{0}局;", InningIndex);
            builder.AppendFormat("GUID:{0};", ID);
            builder.AppendFormat("StartTime:{0};", StartTime);
            builder.AppendFormat("CountDownSeconds:{0};", CountDownSeconds);
            builder.AppendFormat("EndTime:{0};", EndTime == null ? "" : EndTime.ToString());
            builder.AppendFormat("BetRedStone:{0};", BetRedStone);
            builder.AppendFormat("BetGreenStone:{0};", BetGreenStone);
            builder.AppendFormat("BetBlueStone:{0};", BetBlueStone);
            builder.AppendFormat("BetPurpleStone:{0};", BetPurpleStone);
            builder.AppendFormat("WinnedColor:{0};", WinnedColor.ToString());
            builder.AppendFormat("WinnedTimes:{0};", WinnedTimes);
            builder.AppendFormat("WinnedOutStone:{0};", WinnedOutStone);

            return builder.ToString();
        }
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("ID:{0};", ID);
            builder.AppendFormat("StartTime:{0};", StartTime);
            builder.AppendFormat("FinishedInningCount:{0};", FinishedInningCount);
            builder.AppendFormat("EndTime:{0};", EndTime == null ? "" : EndTime.ToString());
            builder.AppendFormat("AllBetInStone:{0};", AllBetInStone);
            builder.AppendFormat("AllWinnedOutStone:{0};", AllWinnedOutStone);
            builder.AppendFormat("TableName:{0};", TableName);

            return builder.ToString();
        }
    }

    public class GambleStoneDailyScheme
    {
        public MyDateTime Date;

        public int ProfitStoneObjective;

        public int AllBetInStone;

        public int AllWinnedOutStone;
    }
}
