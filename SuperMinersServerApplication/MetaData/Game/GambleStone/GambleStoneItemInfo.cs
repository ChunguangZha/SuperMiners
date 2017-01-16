using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.Game.GambleStone
{
    public class GambleStoneItemInfo
    {
        public int ID;

        public GambleStoneItemColor Color;

        public int WinedTimes;

    }

    public enum GambleStoneItemColor
    {
        Red,
        Green,
        Blue,
        Purple
    }
}
