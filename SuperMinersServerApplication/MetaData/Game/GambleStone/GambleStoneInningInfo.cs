using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    /// <summary>
    /// 赌石一局信息，每月一张表（64局为一轮）一张表存约64800条记录
    /// </summary>
    [DataContract]
    public class GambleStoneInningInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
        [DataMember]
        public string ID;

        /// <summary>
        /// 第N局（1-64）
        /// </summary>
        [DataMember]
        public int InningIndex;

        [DataMember]
        public int RoundID;

        //[DataMember]
        //public MyDateTime StartTime;

        [DataMember]
        public int CountDownSeconds;

        //[DataMember]
        //public MyDateTime EndTime;

        [DataMember]
        public int BetRedStone;

        [DataMember]
        public int BetGreenStone;

        [DataMember]
        public int BetBlueStone;

        [DataMember]
        public int BetPurpleStone;

        [DataMember]
        public GambleStoneItemColor WinnedColor;

        [DataMember]
        public int WinnedTimes;

        [DataMember]
        public int WinnedOutStone;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("第{0}轮;", RoundID);
            builder.AppendFormat("第{0}局;", InningIndex);
            builder.AppendFormat("GUID:{0};", ID);
            //builder.AppendFormat("StartTime:{0};", StartTime);
            builder.AppendFormat("CountDownSeconds:{0};", CountDownSeconds);
            //builder.AppendFormat("EndTime:{0};", EndTime == null ? "" : EndTime.ToString());
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

    [DataContract]
    public class GambleStoneRoundInfo
    {
        [DataMember]
        public int ID;

        [DataMember]
        public MyDateTime StartTime;

        [DataMember]
        public int FinishedInningCount;

        [DataMember]
        public MyDateTime EndTime;

        [DataMember]
        public int CurrentWinRedCount;

        [DataMember]
        public int CurrentWinGreenCount;

        [DataMember]
        public int CurrentWinBlueCount;

        [DataMember]
        public int CurrentWinPurpleCount;

        [DataMember]
        public int LastWinRedCount;

        [DataMember]
        public int LastWinGreenCount;

        [DataMember]
        public int LastWinBlueCount;

        [DataMember]
        public int LastWinPurpleCount;

        [DataMember]
        public int AllBetInStone;

        [DataMember]
        public int AllWinnedOutStone;

        [DataMember]
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

    [DataContract]
    public class GambleStoneRound_InningInfo
    {
        [DataMember]
        public GambleStoneRoundInfo roundInfo;

        [DataMember]
        public GambleStoneInningInfo inningInfo;
    }

    [DataContract]
    public class GambleStoneDailyScheme
    {
        [DataMember]
        public int ID;

        [DataMember]
        public MyDateTime Date;

        [DataMember]
        public int ProfitStoneObjective;

        [DataMember]
        public int AllBetInStone;

        [DataMember]
        public int AllWinnedOutStone;
    }

    public enum GambleStoneItemColor
    {
        Red,
        Green,
        Blue,
        Purple
    }
}
