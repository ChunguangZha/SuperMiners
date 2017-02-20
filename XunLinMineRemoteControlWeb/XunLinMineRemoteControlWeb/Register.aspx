<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="XunLinMineRemoteControlWeb.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册迅灵账户</title>
    <link href="css/web.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-3.1.1.js"></script>
    <script src="Scripts/register.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class=" content-wrapper registerpage">
         <table>
             <tbody>
                 <tr>
                     <th>
                         <label for="txtUserName"><span>*</span>用户登录名： </label>
                     </th>
                     <td class="inputcol">
                         <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户登录名！" TabIndex="1" />
                     </td>
                     <td>
                         <span id="msgUserName" class="message"></span>
                         <img id="imgUserNameOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtNickName"><span>*</span>昵称： </label>
                     </th>
                     <td class="inputcol">
                         <asp:TextBox ID="txtNickName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入昵称！" TabIndex="2" />
                     </td>
                     <td>
                         <span id="msgNickName" class="message"></span>
                         <img id="imgNickNameOK" src="Images/yes.png" class="message" style="display:none"/>
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
                         <label for="txtConfirmPassword"><span>*</span>确认密码： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请再次输入密码！" TabIndex="4" />
                     </td>
                     <td>
                         <span id="msgConfirmPassword" class="message"></span>
                         <img id="imgConfirmPasswordOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtAlipayAccount"><span>*</span>支付宝账户： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtAlipayAccount" runat="server" MaxLength="30" CssClass="textbox" ToolTip="请输入支付宝账户！" TabIndex="5" />
                     </td>
                     <td>
                         <span id="msgAlipayAccount" class="message"></span>
                         <img id="imgAlipayAccountOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtAlipayRealName"><span>*</span>支付宝实名： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtAlipayRealName" runat="server" MaxLength="15" CssClass="textbox" TabIndex="6" />
                     </td>
                     <td>
                         <span id="msgAlipayRealName" class="message"></span>
                         <img id="imgAlipayRealNameOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtIDCardNo"><span>*</span>身份证号： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtIDCardNo" runat="server" MaxLength="18" CssClass="textbox" TabIndex="6" />
                     </td>
                     <td>
                         <span id="msgIDCardNo" class="message"></span>
                         <img id="imgIDCardNoOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtEmail"><span>*</span>邮箱： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="textbox" ToolTip="请输入邮箱！" TabIndex="7" />
                     </td>
                     <td>
                         <span id="msgEmail" class="message"></span>
                         <img id="imgEmailOK" src="Images/yes.png" class="message" style="display:none"/>
                     </td>
                 </tr>
                 <tr>
                     <th>
                         <label for="txtQQ"><span>*</span>QQ： </label>
                     </th>
                     <td>
                         <asp:TextBox ID="txtQQ" runat="server" MaxLength="15" CssClass="textbox" TabIndex="8" />
                     </td>
                     <td>
                         <span id="msgQQ" class="message"></span>
                         <img id="imgQQOK" src="Images/yes.png" class="message" style="display:none"/>
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
         <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="注  册" OnClick="btnRegister_Click" TabIndex="10" />
    </div>
    </form>
</body>
</html>
