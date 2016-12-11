<%@ Page Title="" Language="C#" MasterPageFile="~/View/MyRecords.master" AutoEventWireup="true" CodeBehind="ViewMineBuyRecords.aspx.cs" Inherits="SuperMinersWeiXin.View.ViewMineBuyRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/viewminebuyrecords.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="sub_subpage" data-id="minerecords">
        <div class="weui-cells__title">矿山勘探记录</div>
        <div id="listview" class="listview weui-cells">
        </div>
        <div id="loading" class="weui-loadmore" style="display:none">
            <i class="weui-loading"></i>
            <span class="weui-loadmore__tips">正在加载</span>
        </div>
        <div id="loadmore" class="weui-loadmore weui-loadmore_line">
            <span class="weui-loadmore__tips"><a href="javascript:getMineBuyRecordList()">加载更多</a></span>
        </div>
    </div>
</asp:Content>
