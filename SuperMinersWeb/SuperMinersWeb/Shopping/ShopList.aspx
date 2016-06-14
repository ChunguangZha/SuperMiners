<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="SuperMinersWeb.Shopping.ShopList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="listShop" runat="server">
        <ItemTemplate>
            <div class="shopitemwrap">
                <a class="shopitem">
                    <div class="img-wrap">
                        <img src="../Images/stones.jpg" />
                    </div>
                    <div class="title">                    
                        <span>矿石</span>                 
                        <span class="price">￥30</span>
                    </div>
                </a>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server"></asp:LinqDataSource>
</asp:Content>
