<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="ViewMineBuyRecords.aspx.cs" Inherits="SuperMinersWeiXin.View.ViewMineBuyRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="minerecords">
        <div class="weui-cells__title">矿山勘探记录</div>
        <asp:ListView ID="listMineBuyRecords" runat="server">
            <ItemTemplate>
                <div class="weui-cells">
                    <a class="weui-cell weui-cell_access" href="javascript:;">
                        <div class="weui-cell__bd">
                            <p>cell standard</p>
                        </div>
                        <div class="weui-cell__ft">说明文字</div>
                    </a>
                    <a class="weui-cell weui-cell_access" href="javascript:;">
                        <div class="weui-cell__bd">
                            <p>cell standard</p>
                        </div>
                        <div class="weui-cell__ft">说明文字</div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <div class="weui-loadmore">
            <i class="weui-loading"></i>
            <span class="weui-loadmore__tips">正在加载</span>
        </div>
        <div class="weui-loadmore weui-loadmore_line">
            <span class="weui-loadmore__tips">暂无数据</span>
        </div>
    </div>
</asp:Content>
