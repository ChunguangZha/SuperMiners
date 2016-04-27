using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersServerApplication
{
    internal static class GlobalData
    {
        public static readonly int TimeoutSeconds = 120;
        public static readonly int KeepAliveTimeSeconds = 100;
        public static readonly int ServiceToWebPort = 4510;
        public static readonly int ServiceToClientPort = 33101;
        public static readonly int ServiceToAdministrator = 33123;

        //public static string User;

        public static readonly string SaveConfigPassword = "wangzhongyan";

        public static readonly string DBName = "rserver300";
        public static readonly int DBPort = 13300;

        public static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
        public static readonly string ConfigFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
        public static readonly string UserActionFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserAction");
        public static readonly string UserWithdrawRMBImagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WithdrawRMBImages");

    }
}
