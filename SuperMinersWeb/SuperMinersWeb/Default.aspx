<%@ Page Title="主页" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SuperMinersWeb.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>一个可以赚钱的游戏.</h1>
            </hgroup>
            <div id="softwareDownload">
                <a href="Software/publish.htm">迅灵矿场客户端下载</a>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="homepage">
        <span id="homepageMessage" runat="server" ></span>
    </div>
</asp:Content>
