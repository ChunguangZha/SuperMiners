using MetaData;
using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using SuperMinersWeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBind_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = this.txtUserName.Text.Trim();
                string password = this.txtPassword.Text;
                if (userName == "")
                {
                    Response.Write("<script>alert('请输入用户名')</script>");
                    return;
                }
                if (password == "")
                {
                    Response.Write("<script>alert('请输入密码')</script>");
                    return;
                }
#if Test
                WeiXinUserInfoModel userObj = null;

                if (userName == "小开心")
                {
                    userObj = new WeiXinUserInfoModel()
                    {
                        openid = Config.TestUserOpenId,
                        nickname = "小查",
                    };
                }
                else if (userName == "nero")
                {
                    userObj = new WeiXinUserInfoModel()
                    {
                        openid = Config.TestLVSU_UserOpenID,
                        nickname = "wgflicker",
                    };
                }

                Session[Config.SESSIONKEY_WXUSERINFO] = userObj;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                int result = WcfClient.Instance.WeiXinLogin(userObj.openid, userObj.nickname, ip);

                if (result == OperResult.RESULTCODE_TRUE)
                {
                    var player = WcfClient.Instance.GetPlayerByWeiXinOpenID(userObj.openid);

                    WebUserInfo userinfo = new WebUserInfo();
                    userinfo.xlUserID = player.SimpleInfo.UserID;
                    userinfo.xlUserName = player.SimpleInfo.UserName;
                    userinfo.wxOpenID = userObj.openid;

                    // 登录状态100分钟内有效
                    MyFormsPrincipal<WebUserInfo>.SignIn(userinfo.xlUserName, userinfo, 100);
                    Session[userinfo.xlUserName] = player;

                    Server.Transfer("View/Index.aspx");
                }
                else if (result == OperResult.RESULTCODE_EXCEPTION)
                {
                    Response.Write("<script>alert('服务器连接失败，请稍候再试')</script>");
                }
                else
                {
                    Response.Write("<script>alert('测试登录失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                }

#else
                
                WeiXinUserInfoModel wxuserinfo = Session["wxuserinfo"] as WeiXinUserInfoModel;
                if (wxuserinfo == null)
                {
                    Response.Write("<script>alert('只能从微信客户端打开')</script>");
                    return;
                }

                if (string.IsNullOrEmpty(wxuserinfo.openid))
                {
                    Response.Write("<script>alert('微信登录失败，无法绑定')</script>");
                    return;
                }


                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                int result = WcfClient.Instance.BindWeiXinUser(wxuserinfo.openid, wxuserinfo.nickname, userName, password, ip);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    var player = WcfClient.Instance.GetPlayerByWeiXinOpenID(wxuserinfo.openid);
                    if (player != null)
                    {
                        WebUserInfo userinfo = new WebUserInfo();
                        userinfo.xlUserID = player.SimpleInfo.UserID;
                        userinfo.xlUserName = player.SimpleInfo.UserName;
                        userinfo.wxOpenID = wxuserinfo.openid;
                        // 登录状态100分钟内有效
                        MyFormsPrincipal<WebUserInfo>.SignIn(userinfo.xlUserName, userinfo, 100);
                        Session[userinfo.xlUserName] = player;

                        Response.Redirect("View/Index.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('绑定失败, 原因为：没有找到迅灵账户')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('绑定失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                }

#endif
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Bind User Exception", exc);
            }
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
    }
}