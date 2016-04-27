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
            if (!IsPostBack)
            {
                invitationCode = Request["invitationcode"];
                if (!string.IsNullOrEmpty(invitationCode))
                {
                    this.txtInvitationCode.Visible = false;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "" ||
                this.txtPassword.Text == "" ||
                this.txtConfirmPassword.Text == ""// ||
                //this.txtAlipayRealName.Text == "" ||
                //this.txtAlipayAccount.Text == ""
                )
            {
                return;
            }

            if (this.txtPassword.Text != this.txtConfirmPassword.Text)
            {
                return;
            }

            string userName = this.txtUserName.Text;
            string alipay = this.txtAlipayAccount.Text;
            string alipayRealName = this.txtAlipayRealName.Text;
            string password = this.txtPassword.Text;
            string confirmpwd = this.txtConfirmPassword.Text;
            if (userName.Length > 15)
            {
                Response.Write("<script>alert('用户名长度不能超过30个字符!')</script>");
                return;
            }
            if (alipay.Length > 30)
            {
                Response.Write("<script>alert('支付宝账户长度不能超过30个字符!')</script>");
                return;
            }
            if (alipayRealName.Length > 15)
            {
                Response.Write("<script>alert('支付宝真实姓名长度不能超过30个字符!')</script>");
                return;
            }
            if (password.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过30个字符!')</script>");
                return;
            }
            if (confirmpwd.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过30个字符!')</script>");
                return;
            }

            int result;
            if (!WcfClient.IsReady)
            {
                Response.Write("<script>alert('服务器繁忙，请稍候!')</script>");
                return;
            }

            result = WcfClient.Instance.CheckUserNameExist(userName);
            if (result < 0)
            {
                Response.Write("<script>alert('服务器连接失败, 请刷新页面重试!')</script>");
                return;
            }
            if (result > 0)
            {
                Response.Write("<script>alert('该用户名已经存在，请选择其它用户名!')</script>");
                return;
            }

            if (!string.IsNullOrEmpty(alipay) && !string.IsNullOrEmpty(alipayRealName))
            {
                result = WcfClient.Instance.CheckUserAlipayExist(alipay, alipayRealName);
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

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            result = WcfClient.Instance.RegisterUser(ip, userName, this.txtPassword.Text, alipay, alipayRealName, this.txtInvitationCode.Text);
            if (result == 0)
            {
                Response.Write("<script>alert('注册成功!');window.location.href =''</script>");
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