using SuperMinersWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb
{
    public partial class Register : System.Web.UI.Page
    {
        string invitationCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            invitationCode = Request["ic"];
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "" ||
                this.txtPassword.Text == "" ||
                this.txtConfirmPassword.Text == "" ||
                this.txtEmail.Text == ""
                )
            {
                return;
            }

            if (this.txtPassword.Text != this.txtConfirmPassword.Text)
            {
                return;
            }

            string userName = this.txtUserName.Text;
            string nickName = this.txtNickName.Text;
            string email = this.txtEmail.Text;
            string qq = this.txtQQ.Text;
            string password = this.txtPassword.Text;
            string confirmpwd = this.txtConfirmPassword.Text;
            if (userName.Length < 3)
            {
                Response.Write("<script>alert('用户名长度不能少于3个字符!')</script>");
                return;
            }
            if (userName.Length > 15)
            {
                Response.Write("<script>alert('用户名长度不能超过15个字符!')</script>");
                return;
            }
            if (nickName.Length > 15)
            {
                Response.Write("<script>alert('昵称长度不能超过15个字符!')</script>");
                return;
            }
            if (email.Length > 20)
            {
                Response.Write("<script>alert('电子邮箱长度不能超过20个字符!')</script>");
                return;
            }
            if (qq.Length > 15)
            {
                Response.Write("<script>alert('QQ长度不能超过15个字符!')</script>");
                return;
            }
            if (password.Length < 6)
            {
                Response.Write("<script>alert('密码长度不能小于6位!')</script>");
                return;
            }
            if (password.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过15位!')</script>");
                return;
            }
            if (confirmpwd.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过15位!')</script>");
                return;
            }

            HttpCookie cookie = Request.Cookies["CheckCode"];
            if (cookie.Value.ToLower() != this.txtAuthCode.Text.Trim().ToLower())
            {
                Response.Write("<script>alert('验证码错误！')</script>");
                return;
            }

            int result;
            if (!WcfClient.IsReady)
            {
                Response.Write("<script>alert('服务器繁忙，请稍候!')</script>");
                return;
            }
            
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            result = WcfClient.Instance.CheckRegisterIP(ip);
            if (result < 0)
            {
                Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                return;
            }
            if (result > 0)
            {
                Response.Write("<script>alert('此IP注册玩家数，已经超出限制，无法注册!')</script>");
                return;
            }

            result = WcfClient.Instance.CheckUserNameExist(userName);
            if (result < 0)
            {
                Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                return;
            }
            if (result > 0)
            {
                Response.Write("<script>alert('该用户名已经存在，请选择其它用户名!')</script>");
                return;
            }

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(qq))
            {
                result = WcfClient.Instance.CheckEmailExist(email);
                if (result < 0)
                {
                    Response.Write("<script>alert('服务器连接失败, 请刷新页面重试!')</script>");
                    return;
                }
                if (result > 0)
                {
                    Response.Write("<script>alert('该支付宝已被其它用户使用，请选择其它支付宝账户!')</script>");
                    return;
                }
            }

            result = WcfClient.Instance.RegisterUser(ip, userName, nickName, this.txtPassword.Text, email, qq, invitationCode);
            if (result == 0)
            {
                Response.Write("<script>alert('注册成功!');window.location.href =''</script>");
                Response.Redirect("~/");
            }
            else if (result == 1)
            {
                Response.Write("<script>alert('用户名已经存在!')</script>");
            }
            else if (result == 2)
            {
                Response.Write("<script>alert('同一IP注册用户数超限!')</script>");
            }
            else
            {
                Response.Write("<script>alert('注册失败!')</script>");
            }
        }
        
    }
}