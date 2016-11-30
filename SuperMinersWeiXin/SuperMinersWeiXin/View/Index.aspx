<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SuperMinersWeiXin.View.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="root_subpage" data-id="index">
        <div>
            <span>矿主：</span>
            <span>
                <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>贡献值：</span>
            <span>
                <asp:TextBox ID="txtExp" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>灵币：</span>
            <span>
                <asp:TextBox ID="txtRMB" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>金币：</span>
            <span>
                <asp:TextBox ID="txtGoldCoin" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>矿石储量：</span>
            <span>
                <asp:TextBox ID="txtWorkStonesReservers" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>矿工：</span>
            <span>
                <asp:TextBox ID="txtMiners" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <span>矿石：</span>
            <span>
                <asp:TextBox ID="txtStones" runat="server" ReadOnly="true" />
            </span>
        </div>
        <div>
            <div>可收取矿石：</div>
            <div>
                <asp:TextBox ID="txtTempOutputStones" runat="server" ReadOnly="true" />
            </div>
        </div>
    </div>
</asp:Content>
