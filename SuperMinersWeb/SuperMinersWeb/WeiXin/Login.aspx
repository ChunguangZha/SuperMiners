<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SuperMinersWeb.WeiXin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../Content/WeiXinStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
                    <table>
                        <tbody>
                            <tr>
                                <th>
                                    <label for="txtUserName"><span>*</span>用户名： </label>
                                </th>
                                <td class="inputcol">
                                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户名！" TabIndex="1" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <label for="txtPassword"><span>*</span>密码： </label>
                                </th>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="3" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
        <div>
            <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click"/>
        </div>
        </div>
    </form>
</body>
</html>
