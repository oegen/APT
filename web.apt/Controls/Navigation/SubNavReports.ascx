<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubNavReports.ascx.vb" Inherits="Controls_Navigation_SubNavReports" %>
<nav id="secondary-nav" class="group">               
    <div id="control"><a id="controlbtn" class="open" title="Show/View your nav">&nbsp;</a></div>
    <div id="control-panel">

        <h2>Reports</h2>
        <h3>Control Panel</h3>
                              
        <ul id="secondary-nav-items">
            <li id="liKPI" runat="server">
                <asp:HyperLink ID="hypKPI" runat="server" NavigateUrl="~/Reports/KPI-Reports.aspx">
                    KPI-Report
                </asp:HyperLink>
            </li>
            <li id="liClient" runat="server">
                <asp:HyperLink ID="hypClient" runat="server" NavigateUrl="~/Reports/Client Report.aspx">
                    Client Report
                </asp:HyperLink>
            </li>
            <li id="liCoordinator" runat="server">
                <asp:HyperLink ID="hypCoordinator" runat="server" NavigateUrl="~/Reports/Co-ordinator Report.aspx">
                    Co-ordinator Report
                </asp:HyperLink>
            </li>            
        </ul>
    </div>
</nav>