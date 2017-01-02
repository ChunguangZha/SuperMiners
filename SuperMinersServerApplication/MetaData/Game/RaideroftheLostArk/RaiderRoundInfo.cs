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
        [DatabaseField(Unique=true)]
        public int ID;

        [DatabaseField(NotNull = true)]
        public RaiderRoundState State = RaiderRoundState.Waiting;

        [DatabaseField(NotNull = false)]
        public MyDateTime StartTime;

        [DatabaseField(NotNull = false)]
        public int AwardPoolSumStones;

        [DatabaseField(NotNull = false)]
        public string WinnerUserName;

        [DatabaseField(NotNull = false)]
        public int WinnerUserID;

        [DatabaseField(NotNull = false)]
        public int WinStones;

        [DatabaseField(NotNull = false)]
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

    public enum RaiderRoundState
    {
        Waiting,
        Started,
        Finished
    }
}
