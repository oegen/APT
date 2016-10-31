<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubNavProject.ascx.vb" Inherits="Controls_Navigation_SubNavProject" %>
<nav id="secondary-nav" class="group">               
    <div id="control"><a id="controlbtn" class="open" title="Show/View your nav">&nbsp;</a></div>
    <div id="control-panel">

        <h2>Project</h2>
        <h3>Control Panel</h3>

        <ul id="secondary-nav-items">
            <li id="liEditProject" runat="server"><asp:LinkButton ID="lnkEditProject" runat="server">View Project</asp:LinkButton></li>
            <li id="liReserveProjectTime" runat="server"><asp:LinkButton ID="lnkReserveProjectTime" runat="server">Reserve Additional Time</asp:LinkButton></li>
            <li id="liViewReservedTime" runat="server"><asp:LinkButton ID="lnkViewReservedTime" runat="server">View Reserved Time</asp:LinkButton></li>
            <li id="liAdditionalElements" runat="server"><asp:LinkButton ID="lnkAdditionalElements" runat="server">Premium Products</asp:LinkButton></li>
            <li id="liBBCItems" runat="server"><asp:LinkButton ID="lnkBBCList" runat="server">BBC Items</asp:LinkButton></li>
            <li id="liPremiumBrief" runat="server"><asp:LinkButton ID="lnkPremiumBrief" runat="server">Outline Premium’s Brief</asp:LinkButton></li>
            <li id="liKitting" runat="server"><asp:LinkButton ID="lnkKitting" runat="server">Kitting</asp:LinkButton></li>
            <li id="liCostings" runat="server"><asp:LinkButton ID="lnkCostings" runat="server">Costings</asp:LinkButton></li>
            <li id="liProjectHistory" runat="server"><asp:LinkButton ID="lnkProjectHistory" runat="server">Project History</asp:LinkButton></li>
            <li id="liAuditTrail" runat="server"><asp:LinkButton ID="lnkAuditTrail" runat="server">Audit Trail</asp:LinkButton></li>
        </ul>

    </div>                    
</nav>