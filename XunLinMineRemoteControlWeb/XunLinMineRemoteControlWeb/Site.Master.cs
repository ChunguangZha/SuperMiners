using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;

namespace XunLinMineRemoteControlWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
                    if (principal == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo>.SignOut();
            Response.Redirect("Index.aspx");
        }
    }
}