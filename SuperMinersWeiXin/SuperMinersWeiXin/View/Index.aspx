<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SuperMinersWeiXin.View.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/index.js" ></script>
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
                    <label for="txtSellStoneQuan" class="weui-label">矿石出售券</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtSellStoneQuan" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="7"/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="txtLastGatherTime" class="weui-label">上次收取<br/>矿石时间</label>
                </div>
                <div class="weui-cell__bd">
                    <asp:TextBox ID="txtLastGatherTime" runat="server" CssClass="weui-input" ReadOnly="true" TabIndex="8"/>
                </div>
                <div class="weui-cell__ft">
                    <div>
                        <a id="abtnGetTempStone" class="weui-btn weui-btn_mini weui-btn_primary" >收取</a>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>
