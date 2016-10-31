<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TaskListing.ascx.vb" Inherits="Controls_Generic_TaskListing" %>

<asp:Repeater ID="rptrTasks" runat="server">

    <ItemTemplate>
        <li>
            <span class="apt-list-title"><asp:Label ID="lblName" runat="server"></asp:Label></span>
            <span class="apt-list-description"><asp:Label ID="lblDescription" runat="server"></asp:Label></span>
            <asp:Panel ID="pnlElement" runat="server" Visible="false"><span>Element : <asp:Label ID="lblElement" runat="server"></asp:Label></span></asp:Panel>
            <span>Owner: <asp:Label ID="lblOwner" runat="server"></asp:Label></span>
            <span class="apt-list-link"><asp:LinkButton ID="lnkCompleteTask" runat="server">[ View Task <span class="apt-list-arra">&nbsp;</span> ]</asp:LinkButton></span>
        </li>
    </ItemTemplate>

</asp:Repeater>


<asp:Label ID="lblNoResults" runat="server">You have no tasks that require completion for this project.</asp:Label>
