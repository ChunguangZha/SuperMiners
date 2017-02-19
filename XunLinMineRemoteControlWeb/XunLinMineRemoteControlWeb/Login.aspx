<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录迅灵账户</title>
    <link href="css/web.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="center">
            <div>
                <label>用户登录名: </label>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="150" />
            </div>
            <div>
                <label>密码：</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" />
            </div>
        </div>
        <div>

        </div>
        <div>
            <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="登录" />
            <asp:LinkButton runat="server" href="Register.aspx" class="button" >注册</asp:LinkButton>
        </div>
    </form>
</body>
</html>
