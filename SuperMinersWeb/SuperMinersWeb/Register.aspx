<%@ Page Title="玩家注册" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SuperMinersWeb.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Default.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="registerpage">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    <label for="txtUserName"><span>*</span>用户名： </label>
                                </th>
                                <td class="inputcol">
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户名！" TabIndex="1" />
                                </td>
                                <td>
                                    <span id="msgUserName"></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtNickName">昵称： </label>
                                </th>
                                <td class="inputcol">
                                    <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入昵称！" TabIndex="2" />
                                </td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtPassword"><span>*</span>密码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="3" />
                                </td>
                                <td>                                    
                                    <span></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtConfirmPassword"><span>*</span>确认密码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请再次输入密码！" TabIndex="4" />
                                </td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtEmail"><span>*</span>邮箱： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="20" CssClass="textbox" TextMode="Email" ToolTip="请输入邮箱！" TabIndex="5" />
                                </td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtQQ">QQ： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="textbox" TabIndex="6" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtAuthCode"><span>*</span>验证码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入验证码！" TabIndex="7" />
                                </td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    请输入此验证码
                                    <img src="AuthCode" class="checkimg" alt="验证码" id="imgAuthCode" /> 
                                    <a href="javascript:CallServerForUpdate()" class="checkimg">换下一张</a> 
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                                    <input id="submit" type="submit" value="Submit" />
              <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="注  册" OnClick="btnRegister_Click" TabIndex="8" />
        </div>
</asp:Content>