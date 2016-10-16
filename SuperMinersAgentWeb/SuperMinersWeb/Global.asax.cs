using SuperMinersWeb.Utility;
using SuperMinersWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace SuperMinersWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LogHelper.Instance.Init();
            WcfClient.Init();
            GlobalData.GameConfig = Wcf.WcfClient.Instance.GetGameConfig();
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition { Path = "~/scripts/jquery-1.7.2.min.js", DebugPath = "~/scripts/jquery-1.7.2.js", CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.7.2.min.js", CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.7.2.js" });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}