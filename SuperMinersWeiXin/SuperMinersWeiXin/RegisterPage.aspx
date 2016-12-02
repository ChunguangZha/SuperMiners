<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="SuperMinersWeiXin.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-cell_global.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-flex.css" />
    <link type="text/css" rel="stylesheet" href="App_Themes/Theme1/weui-button.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-cells__title"><h1>注册迅灵账户</h1></div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">用户名</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入用户名"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell weui-cell_vcode">
                <div class="weui-cell__hd">
                    <label class="weui-label">昵称</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入昵称"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">密码</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" CssClass="weui-input" TextMode="Password" placeholder="请输入密码"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">确认密码</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="15" CssClass="weui-input" TextMode="Password" placeholder="请确认密码"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">支付宝账户</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAlipayAccount" runat="server" MaxLength="30" CssClass="weui-input" placeholder="请输入支付宝账户"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">支付宝实名</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAlipayRealName" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入支付宝实名"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">身份证号</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtIDCardNo" runat="server" MaxLength="18" CssClass="weui-input" placeholder="请输入身份证号"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">邮箱</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="weui-input" type="email" placeholder="请输入邮箱"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">QQ</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="weui-input" type="number" pattern="[0-9]*" placeholder="请输入QQ号"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
            </div>
            <div class="weui-cell weui-cell_vcode">
                <div class="weui-cell__hd"><label class="weui-label">验证码</label></div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="weui-input" placeholder="请输入验证码"/>
                </div>
                <div class="weui-cell__ft">
                    <i class="weui-icon-warn"></i>
                </div>
                <div class="weui-cell__ft">
                    <img id="imgAuthCode" src="http://www.xlore.net/AuthCode" class="checkimg weui-vcode-img" />
                </div>
            </div>
        </div>
        <div class="weui-cells__tips"></div>
        <label for="weuiAgree" class="weui-agree">
            <input id="weuiAgree" type="checkbox" class="weui-agree__checkbox">
            <span class="weui-agree__text">
                阅读并同意<a href="javascript:void(0);">《相关条款》</a>
            </span>
        </label>

        <div class="weui-btn-area">
            <asp:Button ID="btnRegister" CssClass="weui-btn weui-btn_primary" runat="server" Text="注册" OnClick="btnRegister_Click" TabIndex="10" />
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
