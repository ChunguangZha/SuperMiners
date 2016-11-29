using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SuperMinersWeiXin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LogHelper.Instance.Init();
            WcfClient.Init();
            //GlobalData.GameConfig = Wcf.WcfClient.Instance.GetGameConfig();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            MyFormsPrincipal<WebUserInfo>.TrySetUserInfo(app.Context);
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