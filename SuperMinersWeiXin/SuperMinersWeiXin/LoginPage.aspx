<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SuperMinersWeiXin.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-cell_global.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-flex.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-button.css" />
</head>
<body>
    <form id="form1" runat="server" enableviewstate="false">
    <div>
        <div class="weui-cells__title"><h1>请绑定迅灵矿场账户</h1></div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="txtUserName" class="weui-label">用户名</label></div>
                <div class="weui-cell__bd">
                    <asp:TextBox id="txtUserName" runat="server" class="weui-input" MaxLength="15" type="text" placeholder="请输入用户名"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="txtpassword" class="weui-label">密码</label></div>
                <div class="weui-cell__bd">
                    <asp:TextBox id="txtPassword" runat="server" class="weui-input" MaxLength="15" type="password" placeholder="请输入密码"/>
                </div>
            </div>
        </div>
        <div class="weui-cells__tips">账户绑定后不可更改</div>
        <div style="height:50px;"></div>
        <div class="weui-flex">
            <div class="weui-flex__item weui-btn-area">
                <asp:Button ID="btnBind" runat="server" CssClass=" weui-btn weui-btn_primary" Text="绑定" OnClick="btnBind_Click"/>
            </div>
            <div class="weui-flex__item weui-btn-area">
                <asp:Button ID="btnRegist" runat="server" CssClass=" weui-btn weui-btn_primary" Text="注册" OnClick="btnRegist_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
