<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinResponse.aspx.cs" Inherits="SuperMinersWeb.WeiXin.WeiXinResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMsg" runat="server" Text="请稍等..." />
    </div>
        <div></div>
        <div>
            <asp:Button ID="btnRedirect" runat="server" Text="Redirect" OnClick="btnRedirect_Click"/>
        </div>
    </form>
</body>
</html>
