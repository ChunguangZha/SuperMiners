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
        public static readonly int TimeoutSeconds = 600;
        public static readonly int KeepAliveTimeSeconds = 600;
        public static readonly int ServiceToWebPort = 4510;
        public static readonly int ServiceToClientPort = 33101;
        public static readonly int ServiceToAdministrator = 33123;

        public static readonly string SaveConfigPassword = "wangzhongyan";

        public static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
        public static readonly string ConfigFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
        public static readonly string ClientVersionFile = Path.Combine(ConfigFolder, "clientVersion.xml");
        public static readonly string UserActionFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserAction");
        public static readonly string UserActionLogFile = Path.Combine(UserActionFolder, "actionLogs.xml");
        //public static readonly string UserWithdrawRMBImagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WithdrawRMBImages");
        public static readonly string VirtualShoppingImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VirtualShopping");
        public static readonly string CreditShoppingImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CreditShopping");
        public static readonly string NoticeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notice");

        public const string TestInvitationCode = "1000";

    }
}
