using MetaData.SystemConfig;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XunLinMineRemoteControlWeb
{
    public class GlobalData
    {
        public static GameConfig GameConfig;

        public static UserRemoteServerItem[] ServerItems = null;

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
                    return (float)Math.Round(1M / GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB, 2);
                }

                return 100000;
            }
        }

        public static string[] InvalidName = new string[]{
            "傻逼",
            "骗子",
            "上当",
            "封号",
            "操你妈",
            "管理员",
            "骗",
            "shabi",
            "caonima",
            "pianzi",
            "{",
            "}",
            "<",
            ">",
            ".",
            ";",
            "(",
            ")",

        };
    }
}