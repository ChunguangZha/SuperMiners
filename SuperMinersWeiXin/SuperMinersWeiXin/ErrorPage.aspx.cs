using SuperMinersWeiXin.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (TokenController.ErrorObj != null)
                {
                    this.lblMsg.Text = "ErrorCode: " + TokenController.ErrorObj.errcode + ". ErrorMsg: " + TokenController.ErrorObj.errmsg;
                }
                else
                {
                    this.lblMsg.Text = "微信登录失败";
                }
            }
        }
    }
}