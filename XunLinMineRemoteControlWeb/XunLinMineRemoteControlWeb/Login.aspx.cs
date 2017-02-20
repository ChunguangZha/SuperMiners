using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XunLinMineRemoteControlWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
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
        }
    }
}