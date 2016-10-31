<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubNavTimeSheet.ascx.vb" Inherits="Controls_Navigation_SubNavTimeSheet" %>
<nav id="secondary-nav" class="group">               
    <div id="control"><a id="controlbtn" class="open" title="Show/View your nav">&nbsp;</a></div>
    <div id="control-panel">

        <h2>Timesheets</h2>
        <h3>Control Panel</h3>
                              
        <ul id="secondary-nav-items">
            <li id="liTimeSheetListing" runat="server">
                <asp:HyperLink ID="hypTimesheetList" runat="server" NavigateUrl="~/Timesheet/TimesheetListing.aspx">
                    Timesheet Listing
                </asp:HyperLink>
            </li>
            <li id="liTimeEntry" runat="server">
                <asp:HyperLink ID="hypTimesheetEntry" runat="server" NavigateUrl="~/Timesheet/Timesheet.aspx">
                    Timesheet Entry
                </asp:HyperLink>
            </li>
        </ul>

    </div>                    
</nav>