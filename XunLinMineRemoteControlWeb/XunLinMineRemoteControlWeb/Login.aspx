<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录迅灵账户</title>
    <link href="css/web.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-3.1.1.js"></script>
    <script src="Scripts/login.js" type="text/javascript" ></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registerpage content-wrapper">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    <label for="txtUserLoginName"><span>*</span>用户登录名： </label>
                                </th>
                                <td class="inputcol">
                                    <asp:TextBox ID="txtUserLoginName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户登录名！" TabIndex="1" />
                                </td>
                                <td>
                                    <span id="msgUserName" class="message"></span>
                                    <img id="imgUserNameOK" src="Images/yes.png" class="message" style="display:none"/>
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
                                    <span id="msgPassword" class="message"></span>
                                    <img id="imgPasswordOK" src="Images/yes.png" class="message" style="display:none"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtAuthCode"><span>*</span>验证码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtAuthCode" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入验证码！" TabIndex="9" />
                                </td>
                                <td>
                                    <span id="msgAuthCode" class="message"></span>
                                    <img id="imgAuthCodeOK" src="Images/yes.png" class="message" style="display:none"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    请输入此验证码
                                    <img id="imgAuthCode" src="AuthCode" class="checkimg" alt="验证码" /> 
                                    <a href="javascript:CallServerForUpdate()" class="checkimg">换下一张</a> 
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    <asp:Label ID="lblAlert" runat="server" CssClass="alertmsg"/>
                                </td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                <div>
                    <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="登录" OnClick="btnLogin_Click" />
                    <a id="btnRegister" runat="server" href="Register.aspx">注册</a>
                </div>
        </div>
    </form>
</body>
</html>
