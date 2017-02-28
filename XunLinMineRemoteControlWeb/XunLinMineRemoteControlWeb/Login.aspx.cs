using MetaData;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;
using XunLinMineRemoteControlWeb.Wcf;

namespace XunLinMineRemoteControlWeb
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (GlobalData.GameConfig == null)
            {
                Response.Write("<script>alert('服务器连接失败，暂时无法注册，请稍后再试！')</script>");
                return;
            }

            string userLoginName = this.txtUserLoginName.Text.Trim();
            if (string.IsNullOrEmpty(userLoginName))
            {
                Response.Write("<script>alert('请输入用户名!')</script>");
                return;
            }
            string password = this.txtPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('请输入密码!')</script>");
                return;
            }

            string clientIP = System.Web.HttpContext.Current.Request.UserHostAddress;


            var resultObj = WcfClient.Instance.Login(clientIP, userLoginName, password);
            if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('登录失败, 原因为：" + OperResult.GetMsg(resultObj.OperResultCode) + "')</script>");
                return;
            }

            WebPlayerInfo userinfo = WcfClient.Instance.GetPlayerInfo(resultObj.Message, userLoginName, clientIP);
            if (userinfo == null)
            {
                Response.Write("<script>alert('登录失败。')</script>");
                return;
            }
            if (userinfo.IsLocked)
            {
                Response.Write("<script>alert('您的账户已经被锁定，无法登录，请联系管理员解决。')</script>");
                return;
            }

            WebLoginUserInfo webloginPlayer = WebLoginUserInfo.FromWebPlayerInfo(userinfo);

            // 登录状态100分钟内有效
            MyFormsPrincipal<WebLoginUserInfo>.SignIn(webloginPlayer.UserLoginName, webloginPlayer, 30);
            MyFormsPrincipal<WebLoginUserInfo>.TrySetUserInfo(Context);

            Response.Redirect("Index.aspx", false);
        }
    }
}