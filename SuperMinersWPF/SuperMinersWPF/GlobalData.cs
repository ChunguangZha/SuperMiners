using MetaData;
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

        public static readonly ServerClient Client = new ServerClient();

        public static UserUIModel CurrentUser
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

        public static void InitUser(PlayerInfo user)
        {
            if (CurrentUser == null)
            {
                CurrentUser = new UserUIModel(user);
            }
            else
            {
                CurrentUser.ParentObject = user;
            }
        }

        public static GameConfig GameConfig;

        public static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
    }
}
