<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferPage.aspx.cs" Inherits="SuperMinersWeb.TransferPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/transferOldPlayer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
              <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="登   记" OnClick="btnRegister_Click" TabIndex="10" />
        </div>
</asp:Content>
