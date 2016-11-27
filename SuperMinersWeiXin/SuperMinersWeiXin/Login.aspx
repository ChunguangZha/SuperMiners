<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SuperMinersWeiXin.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <h2>请绑定迅灵矿场账户</h2>
        </div>
        <div>
            <div>
                <span>用户名：</span>
            </div>
            <div>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="15" CssClass="textbox" ToolTip="请输入用户名！" TabIndex="1" />
            </div>
        </div>
        <div>
            <div>
                <span>密码：</span>
            </div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox" MaxLength="15" ToolTip="请输入密码！" TabIndex="2" />
            </div>
        </div>
        <div>
            <div>
                <asp:Button ID="btnBind" runat="server" Text="绑定" OnClick="btnBind_Click"/>
            </div>
            <div>
                <asp:Button ID="btnRegist" runat="server" Text="注册" OnClick="btnRegist_Click" />
            </div>
        </div>
    </div>
</asp:Content>
