<%@ Page Title="" Language="C#" MasterPageFile="~/View/MyRecords.master" AutoEventWireup="true" CodeBehind="ViewMinerBuyRecords.aspx.cs" Inherits="SuperMinersWeiXin.View.ViewMinerBuyRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/viewminerbuyrecords.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="sub_subpage" data-id="minerrecords">      
        <div id="listview" class="listview weui-cells">
            <div id="listitems">

            </div>
            <div id="loading" class="weui-loadmore" style="display:none">
                <i class="weui-loading"></i>
                <span class="weui-loadmore__tips">正在加载</span>
            </div>
            <a id="loadmore" class="weui-loadmore weui-loadmore_line">
                <span class="weui-loadmore__tips">加载更多</span>
            </a>
        </div>
    </div>
</asp:Content>
