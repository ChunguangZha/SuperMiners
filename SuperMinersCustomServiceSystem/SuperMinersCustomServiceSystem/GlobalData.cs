using MetaData.SystemConfig;
using MetaData.User;
using SuperMinersCustomServiceSystem.Wcf.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem
{
    public class GlobalData
    {
        public static readonly string DebugServer = "localhost";

        public static readonly ServerClient Client = new ServerClient();

        public static AdminInfo CurrentAdmin
        {
            get;
            private set;
        }

        public static string Token
        {
            get;
            private set;
        }

        public static bool IsLogined
        {
            get
            {
                return !String.IsNullOrEmpty(Token);
            }
        }

        public static void InitToken(string token)
        {
            Token = token;
        }

        public static void InitUser(AdminInfo user)
        {
            CurrentAdmin = user;
        }

        public static GameConfig GameConfig;
        public static RegisterUserConfig RegisterUserConfig;
        public static AwardReferrerLevelConfig AwardReferrerLevelConfig;

        public static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

        public const int PageItemsCount = 30;
    }
}
