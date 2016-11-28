using MetaData;
using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBind_Click(object sender, EventArgs e)
        {
            try
            {
                WeiXinUserInfoModel wxuserinfo = Session["wxuserinfo"] as WeiXinUserInfoModel;
                if (wxuserinfo == null)
                {
                    Response.Write("<script>alert('微信登录失败，无法绑定')</script>");
                    return;
                }

                string userName = this.txtUserName.Text.Trim();
                string password = this.txtPassword.Text;
                if (string.IsNullOrEmpty(wxuserinfo.openid))
                {
                    Response.Write("<script>alert('微信登录失败，无法绑定')</script>");
                    return;
                }
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

                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                int result = WcfClient.Instance.BindWeiXinUser(wxuserinfo.openid, wxuserinfo.nickname, userName, password, ip);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    var player = WcfClient.Instance.GetPlayerByWeiXinOpenID(wxuserinfo.openid);
                    if (player != null)
                    {
                        MyUserInfo userinfo = new MyUserInfo();
                        userinfo.xlUserID = player.SimpleInfo.UserID;
                        userinfo.xlUserName = player.SimpleInfo.UserName;
                        userinfo.wxOpenID = wxuserinfo.openid;
                        // 登录状态100分钟内有效
                        MyFormsPrincipal<MyUserInfo>.SignIn(userinfo.xlUserName, userinfo, 100);

                    }

                    Response.Redirect("Index.aspx");
                }
                else
                {
                    Response.Write("<script>alert('绑定失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Bind User Exception", exc);
            }
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}