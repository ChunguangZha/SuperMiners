<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="StoneMarket.aspx.cs" Inherits="SuperMinersWeiXin.View.StoneMarket" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../App_Themes/Theme1/weui-form-preview.css" />
    <script src="../Scripts/stonemarket.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="stonesmarket">
        <div style="z-index:999">
            <a id="abtnRefreshSellStoneList" class="myui-toprefresh myui-toprefresh_line" >刷新</a>
        </div>
        <div class="listview">

        </div>
    </div>
</asp:Content>
