<%@ Page Title="玩家注册" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SuperMinersWeb.Register" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="head" runat="server">
    <
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <div class="declaration">

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CallServerForUpdate() {
            document.getElementById("imgAuthCode").src = "AuthCode.ashx?Num=" + Math.random();
        }

    </script>
    <div class="homepage" onload="CreateHttpRequest">
        <div class="mtw">
            <div>
                <div class="rfm">
                    <table>
                        <tbody>
                            <tr class="registerinfoline">
                                <th style="width:150px">
                                    <label for="txtUserName" class="label">用户名： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" TabIndex="1" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtUserName"
                                    CssClass="field-validation-error" ErrorMessage="请填写用户名." >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtNickName" class="label">昵称： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="textbox" TabIndex="2" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNickName"
                                    CssClass="field-validation-error" ErrorMessage="请填写昵称." >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtPassword" class="label">密码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" TabIndex="3" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                    CssClass="field-validation-error" ErrorMessage="请填写密码." >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtConfirmPassword" class="label">确认密码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" TabIndex="4" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="请再输入一遍密码." >*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="两次密码不一至." />
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtEmail" class="label">邮箱： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="textbox" TextMode="Email" TabIndex="5" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                    CssClass="field-validation-error" ErrorMessage="请填写邮箱." >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtQQ" class="label">QQ： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="textbox" TextMode="Number" TabIndex="6" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                    <label for="txtAuthCode" class="label">验证码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入验证码！" TabIndex="7" />
                                </td>
                                <td>
                                    <span class="rqstar">*</span>
                                </td>
                                <td>
                                    <img src="AuthCode.ashx" class="checkimg" alt="验证码" id="imgAuthCode" /> 
                                    <a href="javascript:CallServerForUpdate()" class="checkimg">更换验证码</a> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAuthCode"
                                    CssClass="field-validation-error" ErrorMessage="请输入验证码." >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="registerinfoline">
                                <th>
                                </th>
                                <td>
                                    <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="注  册" CausesValidation="true" OnClick="btnRegister_Click" TabIndex="8" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>        
    </div>
</asp:Content>
