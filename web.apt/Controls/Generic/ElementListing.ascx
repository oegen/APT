<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementListing.ascx.vb" Inherits="Controls_Generic_ElementListing" %>

<asp:Repeater ID="rptrElements" runat="server">
<HeaderTemplate>
<div id="right-nav">
    <div class="right-nav-inner">
        <h2>Added Elements</h2>
        <ul>
</HeaderTemplate>
<ItemTemplate>
    <li id="liElement" runat="server">
        <asp:Hyperlink ID="hypName" runat="server"></asp:Hyperlink>
        <asp:LinkButton ID="lnkStartStopElement" runat="server" Visible="false" OnCommand="LnkBtnStartStopElement">Stop Element</asp:LinkButton>
    </li>
</ItemTemplate>
</asp:Repeater>

        </ul>
         <div id="divAddNewElement" runat="server" class="right-btn btn_orange">
            <asp:HyperLink ID="hypAddNewElement" runat="server">Add New Element</asp:HyperLink>
            <span></span>
        </div>
    </div>
</div>
