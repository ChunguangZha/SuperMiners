<%@ Page Title="玩家注册" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SuperMinersWeb.Register" %>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <div class="declaration">
                声明： 请填写真实的支付宝账户和支付宝真实姓名，否则平台可能将无法确认您充值成功，且无法提现。由此带来的损失将由您本人承担。
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var xmlRequest;
        
        function CreateHttpRequest() {
            try{
                xmlRequest = new XMLHttpRequest();
            } catch (err) {
                xmlRequest = new ActiveXObject("Microsoft.XMLHTTP");
            }
        }

        function CallServerForUpdate() {
            xmlRequest.open("GET", "AuthCodeImageCallbackHandler.ashx");
            xmlRequest.onreadystatechange = ApplyUpdate;
            xmlRequest.send();
        }

        function ApplyUpdate() {
            if (xmlRequest.readyState == 4) {
                if (xmlRequest.status == 200) {
                    var img = document.getElementById("imgAuthCode");
                    var response = xmlRequest.response;
                    img.src = response;
                }
            }
        }

        function RefreshImg() {
            var img = document.getElementById('imgAuthCode');
            img.src = null;
            img.src = "AuthCode.aspx";
        }
    </script>
    <div class="homepage" onload="CreateHttpRequest">
        <p class="validation-summary-errors">
             <asp:Literal runat="server" ID="ErrorMessage" />
        </p>

        <fieldset>
            <legend>玩家信息</legend>
            <ol>
                <li>
                    <label for="txtUserName" class="label">用户名： </label>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtUserName"
                                    CssClass="field-validation-error" ErrorMessage="请填写用户名." />
                </li>
                <li>
                    <label for="txtNickName" class="label">昵称： </label>
                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNickName"
                                    CssClass="field-validation-error" ErrorMessage="请填写昵称." />
                </li>
                <li>
                    <label for="txtPassword" class="label">密    码： </label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                    CssClass="field-validation-error" ErrorMessage="请填写密码." />
                </li>
                <li>
                    <label for="txtConfirmPassword" class="label">确认密码： </label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="请再输入一遍密码." />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="两次密码不一至." />
                </li>
                <li>
                    <label for="txtAlipayAccount" class="label">邮箱：</label>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" TextMode="Email" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                    CssClass="field-validation-error" ErrorMessage="请填写邮箱." />
                </li>
                <li>
                    <label for="txtAlipayRealName" class="label">QQ：</label>
                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" TextMode="Number" />
                </li>
                <li>
                    <div class="positionR">
                        <label for="txtAlipayRealName" class="label">验证码：</label>
                        <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" ToolTip="请输入验证码！" />
                        <img src="AuthCodeImageCallbackHandler.ashx" alt="" id="imgAuthCode" /> 
                        <a href="javascript:CallServerForUpdate()">更换验证码</a> 
                    </div>
                </li>
            </ol>
            <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="注  册" CausesValidation="true" OnClick="btnRegister_Click" />
        </fieldset>
        
    </div>
</asp:Content>
