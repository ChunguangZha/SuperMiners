<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="StoneMarket.aspx.cs" Inherits="SuperMinersWeiXin.View.StoneMarket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../App_Themes/Theme1/weui-form-preview.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="stonesmarket">
        <asp:ListView ID="listStoneOrders" runat="server">
            <ItemTemplate>
                <div class="weui-form-preview">
                    <div class="weui-form-preview__hd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">付款金额</label>
                            <em class="weui-form-preview__value">¥2400.00</em>
                        </div>
                    </div>
                    <div class="weui-form-preview__bd">
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">商品</label>
                            <span class="weui-form-preview__value">电动打蛋机</span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">标题标题</label>
                            <span class="weui-form-preview__value">名字名字名字</span>
                        </div>
                        <div class="weui-form-preview__item">
                            <label class="weui-form-preview__label">标题标题</label>
                            <span class="weui-form-preview__value">很长很长的名字很长很长的名字很长很长的名字很长很长的名字很长很长的名字</span>
                        </div>
                    </div>
                    <div class="weui-form-preview__ft">
                        <a class="weui-form-preview__btn weui-form-preview__btn_primary" href="javascript:">操作</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
