<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="SuperMinersWeiXin.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-cell_global.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-flex.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-button.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-icon.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-agree.css" />
    <script type="text/javascript" src="Scripts/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="Scripts/registeruser.js"></script>
</head>
<body>
    <div class="weui-toptips weui-toptips_warn js_tooltips">错误提示</div>

    <form id="form1" runat="server">
        <div class="weui-cells__title"><h1>注册迅灵账户</h1></div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label" for="txtUserName">用户名</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入用户名" TabIndex="1"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgUserNameOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label" for="txtNickName">昵称</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入昵称" TabIndex="2"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtPassword" class="weui-label">密码</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" CssClass="weui-input" TextMode="Password" placeholder="请输入密码" TabIndex="3"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgPasswordOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtConfirmPassword" class="weui-label">确认密码</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="15" CssClass="weui-input" TextMode="Password" placeholder="请确认密码" TabIndex="4"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgCpasswordOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtAlipayAccount" class="weui-label">支付宝账户</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAlipayAccount" runat="server" MaxLength="30" CssClass="weui-input" placeholder="请输入支付宝账户" TabIndex="5"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgAlipayAccountOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtAlipayRealName" class="weui-label">支付宝实名</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAlipayRealName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入支付宝实名" TabIndex="6"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgAlipayRealNameOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtIDCardNo" class="weui-label">身份证号</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtIDCardNo" runat="server" MaxLength="18" CssClass="weui-input" placeholder="请输入身份证号" TabIndex="7"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgIDCardNoOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtEmail" class="weui-label">邮箱</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="weui-input" type="email" placeholder="请输入邮箱" TabIndex="8"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgEmailOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtQQ" class="weui-label">QQ</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="weui-input" type="number" pattern="[0-9]*" placeholder="请输入QQ号" TabIndex="9"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgQQOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="txtAuthCode" class="weui-label">验证码</label></div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入验证码" TabIndex="10"/>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgAuthCodeOK" class="weiui-icon"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"></div>
                <div class="weui-cell__bd">
                    <img id="imgAuthCode" src="http://www.xlore.net/AuthCode" class="checkimg weui-vcode-img" />
                </div>
            </div>
        </div>
        <div class="weui-cells__tips"></div>
        <label for="weuiAgree" class="weui-agree">
            <input id="weuiAgree" type="checkbox" class="weui-agree__checkbox"/>
            <span class="weui-agree__text">
                阅读并同意<a href="javascript:void(0);">《相关条款》</a>
            </span>
        </label>

        <div class="weui-btn-area">
            <asp:Button ID="btnRegister" CssClass="weui-btn weui-btn_primary" runat="server" Text="注册" OnClick="btnRegister_Click" TabIndex="11" />
        </div>

    </form>
<script type="text/javascript">
    //$(function () {
    //    var $tooltips = $('.js_tooltips');

    //    $('#showTooltips').on('click', function () {
    //        if ($tooltips.css('display') != 'none') return;

    //        // toptips的fixed, 如果有`animation`, `position: fixed`不生效
    //        $('.page.cell').removeClass('slideIn');

    //        $tooltips.css('display', 'block');
    //        setTimeout(function () {
    //            $tooltips.css('display', 'none');
    //        }, 2000);
    //    });
    //});
</script>
</body>
</html>
