<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingItem.aspx.cs" ViewStateMode="Disabled" Inherits="XunLinMineRemoteControlWeb.ShoppingItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/shopping.js" type="text/javascript" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="shoppageinfo">
        <div class="shoppageinfo-l">
            <img src="images/index_01.jpg" />
        </div>
        <div class="shoppageinfo-r">
            <h2>远程协助服务</h2>
            <ul>
                <li>
                    <span>请选择</span>
                    <select id="serverTypeSelect" runat="server">
                        <option value="once">一次</option>
                        <option value="onemonth">一月</option>
                        <option value="halfyear">半年</option>
                        <option value="oneyear">一年</option>
                    </select>
                </li>
                <li>
                    <span>价格</span>
                    <span class="shoppageinfoprice">
                        <span class="shoppageinfoprice">￥</span>
                        <span class="shoppageinfoprice" id="lblPrice" runat="server">50</span>
                    </span>
                </li>
                <li>
                </li>
            </ul>
            <p>
            <asp:Button ID="btnPay" runat="server" class="button" Text="购买" OnClick="btnPay_Click"/>
            </p>
            <dl>
                <dt></dt>
            </dl>
        </div>
    </div>
</asp:Content>
