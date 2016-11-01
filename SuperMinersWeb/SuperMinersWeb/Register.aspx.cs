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
    public partial class Register : System.Web.UI.Page
    {
        string invitationCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                invitationCode = Request["ic"];

                if (!string.IsNullOrEmpty(invitationCode))
                {
                    Session["ic"] = invitationCode;
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
            string nickName = this.txtNickName.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string qq = this.txtQQ.Text.Trim();
            string password = this.txtPassword.Text;
            string confirmpwd = this.txtConfirmPassword.Text;
            string alipayAccount = this.txtAlipayAccount.Text.Trim();
            string alipayRealName = this.txtAlipayRealName.Text.Trim();

            if (userName == "")
            {
                Response.Write("<script>alert('请输入用户名!')</script>");
                return;
            }
            if (nickName == "")
            {
                Response.Write("<script>alert('请输入昵称!')</script>");
                return;
            }
            if (password == "")
            {
                Response.Write("<script>alert('请输入密码!')</script>");
                return;
            }
            if (password != confirmpwd)
            {
                Response.Write("<script>alert('两次密码不一致!')</script>");
                return;
            }
            if (alipayAccount == "")
            {
                Response.Write("<script>alert('请输入支付宝账户!')</script>");
                return;
            }
            if (alipayRealName == "")
            {
                Response.Write("<script>alert('请输入支付宝实名!')</script>");
                return;
            }
            if (email == "")
            {
                Response.Write("<script>alert('请输入邮箱!')</script>");
                return;
            }
            if (qq == "")
            {
                Response.Write("<script>alert('请输入QQ号!')</script>");
                return;
            }

            invitationCode = Session["ic"] as string;

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
            
            result = WcfClient.Instance.CheckUserNameExist(userName);
            if (result == OperResult.RESULTCODE_PARAM_INVALID)
            {
                Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                return;
            }
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该用户名已经存在，请选择其它用户名!')</script>");
                return;
            }
            if (result != OperResult.RESULTCODE_FALSE)
            {
                Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                return;
            }

            if (nickName != "")
            {
                result = WcfClient.Instance.CheckNickNameExist(nickName);
                if (result == OperResult.RESULTCODE_PARAM_INVALID)
                {
                    Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                    return;
                }
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    Response.Write("<script>alert('该昵称已经存在，请选择其它昵称!')</script>");
                    return;
                }
                if (result != OperResult.RESULTCODE_FALSE)
                {
                    Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                    return;
                }
            }

            bool matchValue = Regex.IsMatch(alipayAccount, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");

            if (!matchValue)
            {
                matchValue = Regex.IsMatch(alipayAccount, @"^([1-9][0-9]*)$");
                if (!matchValue)
                {
                    Response.Write("<script>alert('请输入正确的支付宝账户')</script>");
                    return;
                }
            }
            result = WcfClient.Instance.CheckUserAlipayAccountExist(alipayAccount);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该支付宝账户已经被使用，无法再注册')</script>");
                return;
            }

            matchValue = Regex.IsMatch(alipayRealName, @"^[\u4E00-\u9FA5\uF900-\uFA2D]");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的支付宝实名')</script>");
                return;
            }

            result = WcfClient.Instance.CheckUserAlipayRealNameExist(alipayRealName);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该实名已经被使用，无法再注册')</script>");
                return;
            }


            matchValue = Regex.IsMatch(email, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的邮箱')</script>");
                return;
            }

            result = WcfClient.Instance.CheckEmailExist(email);
            if (result == OperResult.RESULTCODE_EXCEPTION)
            {
                Response.Write("<script>alert('服务器连接失败, 请刷新页面重试!')</script>");
                return;
            }
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该邮箱已被其它用户使用，请选择其它邮箱!')</script>");
                return;
            }
            if (result != OperResult.RESULTCODE_FALSE)
            {
                Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                return;
            }

            matchValue = Regex.IsMatch(qq, @"^([1-9][0-9]*)$");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的QQ号')</script>");
                return;
            }

            result = WcfClient.Instance.RegisterUser(ip, userName, nickName, password, alipayAccount, alipayRealName, email, qq, invitationCode);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('恭喜您成功加入灵币矿场!');this.location.href='Default.aspx';</script>");
            }

            Response.Write("<script>alert('注册失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
        }
        
    }
}