using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    /// <summary>
    /// 玩家下注记录。数据库中，每月一张表
    /// </summary>
    public class GambleStonePlayerBetRecord
    {
        public int UserID;

        public MyDateTime Time;

        /// <summary>
        /// 
        /// </summary>
        public int InningID;

        public int BetRedStone;

        public int BetGreenStone;

        public int BetBlueStone;

        public int BetPurpleStone;


    }
}
