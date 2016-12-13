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
            10,//1级，一心
            1000,//2级，两心
            100000,//3级，一钻
            10000000,//4级，两钻
            100000000,//5级，一皇冠
            1000000000,//6级，两皇冠
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
