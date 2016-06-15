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

        public float MinerPrice
        {
            get
            {
                if (GameConfig == null)
                {

                }
                return (float)Math.Round(GlobalData.GameConfig.GoldCoin_Miner / (GlobalData.GameConfig.RMB_GoldCoin * GlobalData.GameConfig.Yuan_RMB), 2);
            }
        }
    }
}