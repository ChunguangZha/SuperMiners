<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Records.aspx.cs" Inherits="SuperMinersWeiXin.View.Records" %>
<%@ OutputCache Duration="20" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/viewrecords.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="records">
        <div class="page__bd" style="height: 100%;">
            <div id="wrapper" class="weui-tab">
                <div class="scroll weui-navbar">
                    <div id="topnav" class="scrollpanel">
                        <div id="navgoldcoinrecords" class="weui-navbar__item weui-bar__item_on">
                            金币充值记录
                        </div>
                        <div id="navminerecords" class="weui-navbar__item">
                            矿山勘探记录
                        </div>
                        <div id="navminerrecords" class="weui-navbar__item">
                            矿工购买记录
                        </div>
                        <%--<div id="navrmbrechargerecords" class="weui-navbar__item">
                            灵币充值记录
                        </div>--%>
                        <div id="navrmbwithdrawrecords" class="weui-navbar__item">
                            灵币提现记录
                        </div>
                        <div id="navstonebuyrecords" class="weui-navbar__item">
                            矿石买入记录
                        </div>
                        <div id="navstonesellrecords" class="weui-navbar__item">
                            矿石出售记录
                        </div>
                    </div>
                </div>
                <div class="weui-tab__panel">
                    <div class="listview weui-cells">
                        <div data-id="navgoldcoinrecords" class="listrecords">
                        </div>
                        <div data-id="navminerecords" class="listrecords" style="display:none">
                        </div>
                        <div data-id="navminerrecords" class="listrecords" style="display:none">
                        </div>
                        <div data-id="navrmbrechargerecords" class="listrecords" style="display:none">
                        </div>
                        <div data-id="navrmbwithdrawrecords" class="listrecords" style="display:none">
                        </div>
                        <div data-id="navstonebuyrecords" class="listrecords" style="display:none">
                        </div>
                        <div data-id="navstonesellrecords" class="listrecords" style="display:none">
                        </div>
                        <div id="divloading" class="weui-loadmore" style="display:none">
                            <i class="weui-loading"></i>
                            <span class="weui-loadmore__tips">正在加载</span>
                        </div>
                        <a id="btnloadmore" class="weui-loadmore weui-loadmore_line">
                            <span class="weui-loadmore__tips">加载更多</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
