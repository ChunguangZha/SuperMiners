<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SuperMinersWeiXin.View.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="index">
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label" for="txtUserName">矿主</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="1"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label" for="txtExp">贡献值</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtExp" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="2"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtRMB" class="weui-label">灵币</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtRMB" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="3"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnRechargeRMB" class="weui-btn weui-btn_mini weui-btn_disabled weui-btn_primary" >充值</a>
                    </div>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtGoldCoin" class="weui-label">金币</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtGoldCoin" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="4"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnRechargeGoldCoin" class="weui-btn weui-btn_mini weui-btn_primary" >充值</a>
                    </div>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtWorkStonesReservers" class="weui-label">矿石储量</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtWorkStonesReservers" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="5"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnBuyStoneReservers" class="weui-btn weui-btn_mini weui-btn_primary" >勘探</a>
                    </div>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtMiners" class="weui-label">矿工</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtMiners" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="6"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnBuyMiner" class="weui-btn weui-btn_mini weui-btn_primary" >购买</a>
                    </div>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtStones" class="weui-label">矿石</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtStones" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="7"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnSellStone" class="weui-btn weui-btn_mini weui-btn_primary" >出售</a>
                    </div>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtTempOutputStones" class="weui-label">可收取矿石</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtTempOutputStones" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="8"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnGetTempStone" class="weui-btn weui-btn_mini weui-btn_primary" >收取</a>
                    </div>
                </div>
            </div>
        </div>
        
        <!--BEGIN toast-->
        <div id="toast" style="display: none;">
            <div class="weui-mask_transparent"></div>
            <div class="weui-toast">
                <i class="weui-icon-success-no-circle weui-icon_toast"></i>
                <p class="weui-toast__content">已完成</p>
            </div>
        </div>
        <!--end toast-->

        <!-- loading toast -->
        <div id="loadingToast" style="display:none;">
            <div class="weui-mask_transparent"></div>
            <div class="weui-toast">
                <i class="weui-loading weui-icon_toast"></i>
                <p class="weui-toast__content">数据加载中</p>
            </div>
        </div>
    </div>
<script type="text/javascript">

    $(function () {
        var $iosDialog1 = $('#iosDialog1'),
            $iosDialog2 = $('#iosDialog2');

        $('#dialogs').on('click', '.weui-dialog__btn', function () {
            $(this).parents('.js_dialog').fadeOut(200);
        });

        $('#showIOSDialog1').on('click', function () {
            $iosDialog1.fadeIn(200);
        });
        $('#showIOSDialog2').on('click', function () {
            $iosDialog2.fadeIn(200);
        });
    });


    // toast
    $(function () {
        var $toast = $('#toast');
        $('#showToast').on('click', function () {
            if ($toast.css('display') != 'none') return;

            $toast.fadeIn(100);
            setTimeout(function () {
                $toast.fadeOut(100);
            }, 2000);
        });
    });

    // loading
    $(function () {
        var $loadingToast = $('#loadingToast');
        $('#showLoadingToast').on('click', function () {
            if ($loadingToast.css('display') != 'none') return;

            $loadingToast.fadeIn(100);
            setTimeout(function () {
                $loadingToast.fadeOut(100);
            }, 2000);
        });
    });
</script>
</asp:Content>
