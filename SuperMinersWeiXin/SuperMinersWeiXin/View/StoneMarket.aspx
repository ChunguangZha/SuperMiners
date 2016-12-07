﻿<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="StoneMarket.aspx.cs" Inherits="SuperMinersWeiXin.View.StoneMarket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../App_Themes/Theme1/weui-form-preview.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="stonesmarket">
        <asp:ListView ID="listStoneOrders" DataSourceID="SellStoneOrderListSource" runat="server">
            <ItemTemplate>
                <div class="weui-form-preview">
                    <div class="weui-form-preview__hd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">付款灵币</label>
                            <em class="weui-form-preview__value"><% Eval("ValueRMB"); %></em>
                        </div>
                    </div>
                    <div class="weui-form-preview__bd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">订单号</label>
                            <span class="weui-form-preview__value"><% Eval("OrderNumber"); %></span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">出售矿石数</label>
                            <span class="weui-form-preview__value"><% Eval("SellStonesCount"); %></span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">卖家</label>
                            <span class="weui-form-preview__value"><% Eval("SellerUserName"); %></span>
                        </div>
                    </div>
                    <div class="weui-form-preview__ft">
                        <a class="weui-form-preview__btn weui-form-preview__btn_primary" href="javascript:">购买</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="SellStoneOrderListSource" runat="server" TypeName="MetaData.Trade.SellStonesOrder" SelectMethod="GetNotFinishedSellorders"></asp:ObjectDataSource>
    </div>
</asp:Content>
