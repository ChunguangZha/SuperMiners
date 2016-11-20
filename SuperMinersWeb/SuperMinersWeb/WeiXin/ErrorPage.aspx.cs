using SuperMinersWeb.WeiXin.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (TokenController.ErrorObj == null)
            {
                this.msg.Text = "TokenController.ErrorObj == null";
            }
            else
            {
                this.msg.Text = TokenController.ErrorObj.errmsg;
            }
        }
    }
}