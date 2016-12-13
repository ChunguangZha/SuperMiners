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
        public static readonly int[] LevelTable = new int[]{
            //0级，没有心
            1000,//1级，一心
            4000,//2级，两心
            9000,//3级，三心
            15000,//4级，四心
            25000,//5级，五心
            50000,//6级，一钻
            100000,//7级，二钻
            200000,//8级，三钻
            500000,//9级，四钻
            1000000,//10级，五钻
            2000000,//11级，一皇冠
            50000000,//12级，两皇冠
            100000000,//13级，三皇冠
            200000000,//14级，四皇冠
            500000000,//15级，五皇冠
        };

        public static int GetCreditLevel(int creditValue)
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
    }
}
