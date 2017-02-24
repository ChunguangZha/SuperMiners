<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/login.js" type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div id="loginpanel" class="container registerpanel" style="height:700px">
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
                                    <label for="txtPassword"><span>*</span>密码： </label>
            </div>
			<div class="col-md-4">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="2" />
			</div>
			<div class="col-md-2">
                                    <span id="msgPassword" class="message"></span>
                                    <img id="imgPasswordOK" src="images/yes.png" class="message" style="display:none"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
                                    <label for="txtAuthCode"><span>*</span>验证码： </label>
            </div>
			<div class="col-md-4">
                                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入验证码！" TabIndex="3" />
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
                                    <asp:Label ID="lblAlert" runat="server" CssClass="alertmsg"/>
			</div>
		</div>
		<div class="row">
            <div class="col-md-4">
            </div>
			<div class="col-md-6">
			</div>
			<div class="col-md-2">
			</div>
        </div>
		<div class="row">
            <div class="col-md-4">
            </div>
			<div class="col-md-6">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-lg" Text="登录" OnClick="btnLogin_Click" TabIndex="4" />
                    <a id="btnRegister" runat="server" class="btn btn-primary btn-lg" href="Register.aspx" >注册</a>
			</div>
			<div class="col-md-2">
			</div>
		</div>
	</div>
</asp:Content>
