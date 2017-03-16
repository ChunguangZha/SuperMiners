using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;
using XunLinMineRemoteControlWeb.Utility;
using XunLinMineRemoteControlWeb.Wcf;

namespace XunLinMineRemoteControlWeb
{
    public partial class Register : System.Web.UI.Page
    {
        string invitationCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalData.GameConfig == null)
            {
                Response.Write("<script>alert('服务器连接失败，暂时无法注册，请稍后再试！')</script>");
                return;
            }

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
            try
            {
                string userName = this.txtUserLoginName.Text.Trim();
                string nickName = this.txtNickName.Text.Trim();
                string email = this.txtEmail.Text.Trim();
                string qq = this.txtQQ.Text.Trim();
                string password = this.txtPassword.Text;
                string confirmpwd = this.txtConfirmPassword.Text;
                //string alipayAccount = this.txtAlipayAccount.Text.Trim();
                //string alipayRealName = this.txtAlipayRealName.Text.Trim();
                string IDCardNo = this.txtIDCardNo.Text.Trim();

                foreach (var item in GlobalData.InvalidName)
                {
                    if (userName.Contains(item))
                    {
                        Response.Write("<script>alert('用户名无效，无法注册!')</script>");
                        return;
                    }
                    if (nickName.Contains(item))
                    {
                        Response.Write("<script>alert('昵称无效，无法注册!')</script>");
                        return;
                    }
                }

                if (!WcfClient.IsReady)
                {
                    Response.Write("<script>alert('服务器繁忙，请稍候!')</script>");
                    return;
                }

                if (!CheckAuthCode())
                {
                    return;
                }
                if (!CheckUserLoginName(userName))
                {
                    return;
                }
                if (!CheckUserName(nickName))
                {
                    return;
                }
                if (!CheckPassword(password, confirmpwd))
                {
                    return;
                }
                //if (!CheckAlipayAccount(alipayAccount))
                //{
                //    return;
                //}
                //if (!CheckAlipayRealName(alipayRealName))
                //{
                //    return;
                //}
                if (!CheckIDCardNo(IDCardNo))
                {
                    return;
                }
                if (!CheckEmail(email))
                {
                    return;
                }
                if (!CheckQQ(qq))
                {
                    return;
                }

                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                RegisterUser(ip, userName, nickName, password, null, null, IDCardNo, email, qq);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception ", exc);
            }
        }

        private bool CheckUserLoginName(string userName)
        {
            if (userName == "")
            {
                Response.Write("<script>alert('请输入用户名!')</script>");
                return false;
            }
            if (userName.Length < 3)
            {
                Response.Write("<script>alert('用户名长度不能少于3个字符!')</script>");
                return false;
            }
            if (userName.Length > 15)
            {
                Response.Write("<script>alert('用户名长度不能超过15个字符!')</script>");
                return false;
            }
            int result = WcfClient.Instance.CheckUserLoginNameExist(userName);
            if (result == OperResult.RESULTCODE_PARAM_INVALID)
            {
                Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                return false;
            }
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该用户名已经存在，请选择其它用户名!')</script>");
                return false;
            }
            if (result != OperResult.RESULTCODE_FALSE)
            {
                Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                return false;
            }

            return true;
        }

        private bool CheckUserName(string nickName)
        {
            if (nickName == "")
            {
                Response.Write("<script>alert('请输入昵称!')</script>");
                return false;
            }
            if (nickName.Length > 15)
            {
                Response.Write("<script>alert('昵称长度不能超过15个字符!')</script>");
                return false;
            }

            int result = WcfClient.Instance.CheckUserNameExist(nickName);
            if (result == OperResult.RESULTCODE_PARAM_INVALID)
            {
                Response.Write("<script>alert('注册失败, 可能是输入数据无效，请重新输入再试!')</script>");
                return false;
            }
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该昵称已经存在，请选择其它昵称!')</script>");
                return false;
            }
            if (result != OperResult.RESULTCODE_FALSE)
            {
                Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                return false;
            }

            return true;
        }

        private bool CheckPassword(string password, string confirmpwd)
        {
            if (password == "")
            {
                Response.Write("<script>alert('请输入密码!')</script>");
                return false; ;
            }
            if (password != confirmpwd)
            {
                Response.Write("<script>alert('两次密码不一致!')</script>");
                return false; ;
            }
            if (password.Length < 6)
            {
                Response.Write("<script>alert('密码长度不能小于6位!')</script>");
                return false; ;
            }
            if (password.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过15位!')</script>");
                return false; ;
            }
            if (confirmpwd.Length > 15)
            {
                Response.Write("<script>alert('密码长度不能超过15位!')</script>");
                return false; ;
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
            int result = WcfClient.Instance.CheckUserAlipayAccountExist(alipayAccount);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该支付宝账户已经被使用，无法再注册')</script>");
                return false;
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
            //result = WcfClient.Instance.CheckUserAlipayRealNameExist(alipayRealName);
            //if (result == OperResult.RESULTCODE_TRUE)
            //{
            //    Response.Write("<script>alert('该实名已经被使用，无法再注册')</script>");
            //    return;
            //}

            return true;
        }

        private bool CheckIDCardNo(string IDCardNo)
        {
            if (IDCardNo == "")
            {
                Response.Write("<script>alert('请输入身份证号!')</script>");
                return false;
            }

            if (!IDCardVerifyTools.VerifyIDCard(IDCardNo))
            {
                Response.Write("<script>alert('请输入正确的身份证号')</script>");
                return false;
            }

            int result = WcfClient.Instance.CheckUserIDCardNoExist(IDCardNo);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('身份证号已经被使用，无法再注册')</script>");
                return false;
            }

            return true;
        }

        private bool CheckEmail(string email)
        {
            if (email == "")
            {
                Response.Write("<script>alert('请输入邮箱!')</script>");
                return false;
            }
            if (email.Length > 30)
            {
                Response.Write("<script>alert('电子邮箱长度不能超过30个字符!')</script>");
                return false;
            }

            bool matchValue = Regex.IsMatch(email, @"^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的邮箱')</script>");
                return false;
            }

            int result = WcfClient.Instance.CheckEmailExist(email);
            if (result == OperResult.RESULTCODE_EXCEPTION)
            {
                Response.Write("<script>alert('服务器连接失败, 请刷新页面重试!')</script>");
                return false;
            }
            if (result == OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('该邮箱已被其它用户使用，请选择其它邮箱!')</script>");
                return false;
            }
            if (result != OperResult.RESULTCODE_FALSE)
            {
                Response.Write("<script>alert('注册失败, 请刷新页面重试!')</script>");
                return false;
            }

            return true;
        }

        private bool CheckQQ(string qq)
        {
            if (qq == "")
            {
                Response.Write("<script>alert('请输入QQ号!')</script>");
                return false;
            }

            if (qq.Length > 15)
            {
                Response.Write("<script>alert('QQ长度不能超过15个字符!')</script>");
                return false;
            }

            bool matchValue = Regex.IsMatch(qq, @"^([1-9][0-9]*)$");
            if (!matchValue)
            {
                Response.Write("<script>alert('请输入正确的QQ号')</script>");
                return false;
            }

            return true;
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

        private void RegisterUser(string clientIP, string userLoginName, string nickName, string password, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq)
        {
            invitationCode = Session["ic"] as string;

            int result = WcfClient.Instance.RegisterUser(clientIP, userLoginName, nickName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            if (result != OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('注册失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                return;
            }

            var resultObj = WcfClient.Instance.Login(clientIP, userLoginName, password);
            if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
            {
                Response.Write("<script>alert('注册成功，但登录失败, 原因为：" + OperResult.GetMsg(resultObj.OperResultCode) + "')</script>");
                return;
            }

            string message = "";
            bool isOK = Controller.GetPlayerInfo(resultObj.Message, userLoginName, clientIP, Context, out message);
            if (!isOK)
            {
                Response.Write("<script>alert('" + message + "')</script>");
            }
            else
            {
                Response.Redirect("Index.aspx", false);
            }
        }
    }
}