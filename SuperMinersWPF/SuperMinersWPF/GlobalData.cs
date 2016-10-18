using MetaData;
using MetaData.AgentUser;
using MetaData.SystemConfig;
using MetaData.User;
using SuperMinersWPF.Models;
using SuperMinersWPF.Wcf.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF
{
    public class GlobalData
    {
        public static readonly string DebugServer = "localhost";

        public const int PageItemsCount = 30;

        public static readonly ServerClient Client = new ServerClient();

        public static UserUIModel CurrentUser
        {
            get;
            private set;
        }

        public static AgentUserInfo AgentUserInfo;
        
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

        public static void InitUser(PlayerInfo user)
        {
            if (CurrentUser == null)
            {
                CurrentUser = new UserUIModel(user);
            }
            else
            {
                //decimal tempOutputStone = CurrentUser.TempOutputStones;
                CurrentUser.ParentObject = user;
                //CurrentUser.TempOutputStones = tempOutputStone;
            }
        }

        public static GameConfig GameConfig;
        public static RegisterUserConfig RegisterUserConfig;
        public static AwardReferrerLevelConfig AwardReferrerLevelConfig;

        public static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
    }
}
