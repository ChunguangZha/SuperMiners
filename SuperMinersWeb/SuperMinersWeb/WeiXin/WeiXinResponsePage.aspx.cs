using SuperMinersWeb.Utility;
using SuperMinersWeb.WeiXin.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class WeiXinResponsePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
                string code = Request["code"];
                string state = Request["state"];

                LogHelper.Instance.AddInfoLog("code:" + code + "; state: " + state);

                if (state == Config.state)
                {
                    bool isOK = WeiXinHandler.SynGetUserAccessToken(code);
                    if (!isOK)
                    {
                        Server.Transfer("ErrorPage.aspx");
                    }
                    else
                    {
                        if (TokenController.WeiXinUserObj == null)
                        {
                            this.lblMsg.Text = "欢迎进入迅灵矿场";
                        }
                        else
                        {
                            this.lblMsg.Text = "欢迎 [" + TokenController.WeiXinUserObj.nickname + "] 进入迅灵矿场";
                        }
                    }
                }
                else
                {

                }
            }
        }
    }
}