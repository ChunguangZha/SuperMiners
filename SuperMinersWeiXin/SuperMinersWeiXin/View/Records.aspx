<%@ Page Title="" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="Records.aspx.cs" Inherits="SuperMinersWeiXin.View.Records" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="records">
        <div class="page__bd" style="height: 100%;">
            <div id="wrapper" class="weui-tab">
                <div class="scroll weui-navbar">
                    <div class="scrollpanel">
                        <div class="weui-navbar__item weui-bar__item_on">
                            金币充值记录
                        </div>
                        <div class="weui-navbar__item">
                            矿山勘探记录
                        </div>
                        <div class="weui-navbar__item">
                            矿工购买记录
                        </div>
                        <div class="weui-navbar__item">
                            灵币充值记录
                        </div>
                        <div class="weui-navbar__item">
                            灵币提现记录
                        </div>
                        <div class="weui-navbar__item">
                            矿石买入记录
                        </div>
                        <div class="weui-navbar__item">
                            矿石出售记录
                        </div>
                    </div>
                </div>
                <div class="weui-tab__panel">

                </div>
            </div>
        </div>
    </div>
<script type="text/javascript">
    $(function () {
        $('.weui-navbar__item').on('click', function () {
            $(this).addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
        });

        var i = 0;
        $(".weui-navbar__item").each(function () {
            var left = (100 * i) + "px"
            i++;
            $(this).css("left", left);
        });

        var Scroll = new iScroll('wrapper', { hScrollbar: false, vScrollbar: false });
    });
</script>
</asp:Content>
