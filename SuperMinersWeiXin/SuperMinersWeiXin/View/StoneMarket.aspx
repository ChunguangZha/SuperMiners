<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="StoneMarket.aspx.cs" Inherits="SuperMinersWeiXin.View.StoneMarket" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../App_Themes/Theme1/weui-form-preview.css" />
    <script src="../Scripts/stonemarket.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="stonesmarket">
        <asp:ListView ID="lvStoneOrders" DataSourceID="SellStoneOrderListSource" runat="server">
            <LayoutTemplate>
                <div class="listview">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="weui-form-preview">
                    <div class="weui-form-preview__hd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">付款灵币</label>
                            <em class="weui-form-preview__value"><%# Eval("ValueRMB") %></em>
                        </div>
                    </div>
                    <div class="weui-form-preview__bd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">订单号</label>
                            <span class="weui-form-preview__value"><%# Eval("OrderNumber") %></span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">矿石数</label>
                            <span class="weui-form-preview__value"><%# Eval("SellStonesCount") %></span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">卖家</label>
                            <span class="weui-form-preview__value"><%# Eval("SellerUserName") %></span>
                        </div>
                    </div>
                    <div class="weui-form-preview__ft">
                        <a class="btn_buystone weui-form-preview__btn weui-form-preview__btn_primary"
                             data-orderid="<%# Eval("OrderNumber") %>" data-rmb="<%# Eval("ValueRMB") %>">购买</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="SellStoneOrderListSource" runat="server" TypeName="SuperMinersWeiXin.Controller.SellStoneOrderController" SelectMethod="GetNotFinishedSellorders"></asp:ObjectDataSource>
    </div>
</asp:Content>
