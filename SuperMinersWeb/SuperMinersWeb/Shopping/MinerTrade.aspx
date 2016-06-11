<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MinerTrade.aspx.cs" Inherits="SuperMinersWeb.Shopping.MinerTrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="shoppageinfo">
        <div class="shoppageinfo-l">
            <img src="../Images/miner.jpg" />
        </div>
        <div class="shoppageinfo-r">
            <h2>矿工</h2>
            <ul>
                <li>
                    <span>价格</span>
                    <span class="shoppageinfoprice">
                        <span class="shoppageinfoprice">￥</span>
                        <span class="shoppageinfoprice">20</span>
                    </span>
                </li>
                <li>
                    <span>数量</span>
                    <asp:TextBox ID="txtCount" runat="server" TextMode="Number" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCount"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="请选择购买数量." />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCount"
                         MinimumValue="1" MaximumValue="99999999" ErrorMessage="数量需大于0"></asp:RangeValidator>
                </li>
            </ul>
            <asp:Button ID="btnPay" runat="server" Text="购买" OnClick="btnPay_Click"/>
            <dl>
                <dt></dt>
            </dl>
        </div>
    </div>
</asp:Content>
