using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.WeiXinCore;
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
            ErrorModel errObj = Session[Config.SESSIONKEY_RESPONSEERROR] as ErrorModel;
            if (errObj != null)
            {
                this.lblMsg.Text = "ErrorCode: " + errObj.errcode + ". ErrorMsg: " + errObj.errmsg;
            }
            else
            {
                this.lblMsg.Text = "微信登录失败";
            }
        }
    }
}