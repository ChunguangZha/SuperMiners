<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Register1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/register.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="registerpanel" class="container registerpanel" style="height:700px">
		<div class="row">
            <div class="col-md-4">
                <label for="txtUserLoginName"><span>*</span>用户登录名： </label>
            </div>
			<div class="col-md-4">
				<asp:TextBox ID="txtUserLoginName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户登录名！" TabIndex="1" />
			</div>
			<div class="col-md-2">
                <span id="msgUserName" class="message"></span>
                <img id="imgUserNameOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtNickName"><span>*</span>昵称： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入昵称！" TabIndex="2" />
			</div>
			<div class="col-md-2">
                         <span id="msgNickName" class="message"></span>
                         <img id="imgNickNameOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                                    <label for="txtPassword"><span>*</span>密码： </label>
            </div>
			<div class="col-md-4">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="3" />
			</div>
			<div class="col-md-2">
                                    <span id="msgPassword" class="message"></span>
                                    <img id="imgPasswordOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtConfirmPassword"><span>*</span>确认密码： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请再次输入密码！" TabIndex="4" />
			</div>
			<div class="col-md-2">
                         <span id="msgConfirmPassword" class="message"></span>
                         <img id="imgConfirmPasswordOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<%--<div class="row">
            <div class="col-md-4">
                         <label for="txtAlipayAccount"><span>*</span>支付宝账户： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtAlipayAccount" runat="server" MaxLength="30" CssClass="textbox" ToolTip="请输入支付宝账户！" TabIndex="5" />
			</div>
			<div class="col-md-2">
                         <span id="msgAlipayAccount" class="message"></span>
                         <img id="imgAlipayAccountOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtAlipayRealName"><span>*</span>支付宝实名： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtAlipayRealName" runat="server" MaxLength="15" CssClass="textbox" TabIndex="6" />
			</div>
			<div class="col-md-2">
                         <span id="msgAlipayRealName" class="message"></span>
                         <img id="imgAlipayRealNameOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>--%>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtIDCardNo"><span>*</span>身份证号： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtIDCardNo" runat="server" MaxLength="18" CssClass="textbox" TabIndex="7" />
			</div>
			<div class="col-md-2">
                         <span id="msgIDCardNo" class="message"></span>
                         <img id="imgIDCardNoOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtEmail"><span>*</span>邮箱： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="textbox" ToolTip="请输入邮箱！" TabIndex="8" />
			</div>
			<div class="col-md-2">
                         <span id="msgEmail" class="message"></span>
                         <img id="imgEmailOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                         <label for="txtQQ"><span>*</span>QQ： </label>
            </div>
			<div class="col-md-4">
                         <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="textbox" TabIndex="9" />
			</div>
			<div class="col-md-2">
                         <span id="msgQQ" class="message"></span>
                         <img id="imgQQOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                                    <label for="txtAuthCode"><span>*</span>验证码： </label>
            </div>
			<div class="col-md-4">
                                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入验证码！" TabIndex="10" />
			</div>
			<div class="col-md-2">
                                    <span id="msgAuthCode" class="message"></span>
                                    <img id="imgAuthCodeOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
            </div>
			<div class="col-md-4">
                                    请输入此验证码
                                    <img id="imgAuthCode" src="AuthCode" class="checkimg" alt="验证码" /> 
                                    <a href="javascript:CallServerForUpdate()" class="checkimg">换下一张</a> 
			</div>
			<div class="col-md-2">
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
            </div>
			<div class="col-md-4">
			</div>
			<div class="col-md-2">
			</div>
        </div>
		<div class="row">
            <div class="col-md-4">
            </div>
			<div class="col-md-4">
                <asp:Button ID="btnRegister" CssClass="btn btn-primary btn-lg" runat="server" Text="注  册" OnClick="btnRegister_Click" TabIndex="11" />
			</div>
			<div class="col-md-2">
			</div>
		</div>
	</div>
</asp:Content>
