<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SuperMinersWeiXin.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <h2>请绑定迅灵矿场账户</h2>
        </div>
            <div>
                <div>
                    <span>用户名：</span>
                </div>
                <div>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户名！" TabIndex="1" />
                </div>
            </div>
            <div>
                <div>
                    <span>密码：</span>
                </div>
                <div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="2" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Button ID="btnBind" runat="server" Text="绑定" OnClick="btnBind_Click"/>
                </div>
                <div>
                    <asp:Button ID="btnRegist" runat="server" Text="注册" OnClick="btnRegist_Click" />
                </div>
            </div>
    </div>
    </form>
</body>
</html>
