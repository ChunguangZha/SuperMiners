using MetaData.SystemConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeb
{
    public class GlobalData
    {
        public static GameConfig GameConfig;

        public static float MinerPrice
        {
            get
            {
                if (GameConfig != null)
                {
                    return (float)Math.Round(GlobalData.GameConfig.GoldCoin_Miner / (GlobalData.GameConfig.RMB_GoldCoin * GlobalData.GameConfig.Yuan_RMB), 2);
                }
                return 10000;
            }
        }

        public static float MinePrice
        {
            get
            {
                if (GameConfig != null)
                {
                    return (float)Math.Round(GlobalData.GameConfig.RMB_Mine / GlobalData.GameConfig.Yuan_RMB, 2);
                }
                return 100000;
            }
        }

        public static float StonePrice
        {
            get
            {
                if (GameConfig != null)
                {
                    return (float)Math.Round((1 * 100) / GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB, 2);
                }

                return 100000;
            }
        }
    }
}