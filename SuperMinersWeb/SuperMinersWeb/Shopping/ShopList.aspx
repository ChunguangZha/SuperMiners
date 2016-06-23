<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="SuperMinersWeb.Shopping.ShopList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="shoplistview">
    <asp:ListView ID="listShop" runat="server">
        <ItemTemplate>
            <div class="shopitemwrap">
                <a class="shopitem" href='<%# Eval("Href") %>'>
                    <div class="img-wrap">
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImgPath") %>' />
                    </div>
                    <div class="title">                    
                        <span><%# Eval("Name") %></span>                 
                        <span class="price">￥<%# Eval("Price") %></span></div>
                </a>
            </div>
        </ItemTemplate>
    </asp:ListView>
    </div>
</asp:Content>
