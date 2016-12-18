using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaData.SystemConfig
{
    public static class CreditLevelConfig
    {
        /// <summary>
        /// 现设0-6，共7级：小于10的，为0，没有图标；10到1000之间为1级，一心；
        /// </summary>
        public static readonly long[] LevelTable = new long[]{
            //0级，没有心
            10000,//1级，一心
            40000,//2级，两心
            90000,//3级，三心
            150000,//4级，四心
            250000,//5级，五心
            500000,//6级，一钻
            1000000,//7级，二钻
            2000000,//8级，三钻
            5000000,//9级，四钻
            10000000,//10级，五钻
            20000000,//11级，一皇冠
            500000000,//12级，两皇冠
            1000000000,//13级，三皇冠
            2000000000,//14级，四皇冠
            5000000000,//15级，五皇冠
        };

        public static int GetCreditLevel(long creditValue)
        {
            int level = 0;
            for (int i = 0; i < LevelTable.Length; i++)
            {
                if (creditValue < LevelTable[i])
                {
                    level = i;
                    break;
                }
            }

            return level;
        }
        
        public static int UserExpLevelValue = 2000;

    }
}
