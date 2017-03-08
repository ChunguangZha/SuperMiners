using MetaData;
using SuperMinersWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb
{
    public partial class TransferPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string password = this.txtPassword.Text;
            string alipayAccount = this.txtAlipayAccount.Text.Trim();
            string alipayRealName = this.txtAlipayRealName.Text.Trim();
            string newServerUserLoginName = this.txtNewServerUserLoginName.Text.Trim();
            string newServerPassword = this.txtNewServerPassword.Text.Trim();

            if (!WcfClient.IsReady)
            {
                Response.Write("<script>alert('服务器繁忙，请稍候!')</script>");
                return;
            }

            if (!CheckAuthCode())
            {
                return;
            }
            if (!CheckAlipayAccount(alipayAccount))
            {
                return;
            }
            if (!CheckAlipayRealName(alipayRealName))
            {
                return;
            }

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            int result = WcfClient.Instance.TransferOldUser(userName, password, alipayAccount, alipayRealName, email, newServerUserLoginName, newServerPassword);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('您的账户已经登记成功，等待管理员处理，处理结果将发送到您的邮箱中!');this.location.href='Default.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('您的账户登记失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
            }
        }

        private bool CheckAuthCode()
        {
            HttpCookie cookie = Request.Cookies["CheckCode"];
            if (cookie.Value.ToLower() != this.txtAuthCode.Text.Trim().ToLower())
            {
                Response.Write("<script>alert('验证码错误！')</script>");
                return false;
            }

            return true;
        }

        private bool CheckAlipayAccount(string alipayAccount)
        {
            if (alipayAccount == "")
            {
                Response.Write("<script>alert('请输入支付宝账户!')</script>");
                return false;
            }

            bool matchValue = Regex.IsMatch(alipayAccount, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
            if (!matchValue)
            {
                matchValue = Regex.IsMatch(alipayAccount, @"^([1-9][0-9]*)$");
                if (!matchValue)
                {
                    Response.Write("<script>alert('请输入正确的支付宝账户')</script>");
                    return false;
                }
                else
                {
                    if (alipayAccount.Length != 11)
                    {
                        Response.Write("<script>alert('请输入正确的支付宝账户')</script>");
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckAlipayRealName(string alipayRealName)
        {
            if (alipayRealName == "")
            {
                Response.Write("<script>alert('请输入支付宝实名!')</script>");
                return false;
            }

            bool matchValue = Regex.IsMatch(alipayRealName, @"^[\u4E00-\u9FA5\uF900-\uFA2D]");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的支付宝实名')</script>");
                return false;
            }
            return true;
        }

    }
}