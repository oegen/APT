<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubNavAdmin.ascx.vb" Inherits="Controls_Navigation_SubNavAdmin" %>
<nav id="secondary-nav" class="group">               
    <div id="control"><a id="controlbtn" class="open" title="Show/View your nav">&nbsp;</a></div>
    <div id="control-panel">

        <h2>Admin</h2>
        <h3>Control Panel</h3>
                              
        <ul id="secondary-nav-items">
            <li id="liUserManager" runat="server">
                <asp:HyperLink ID="hypUserManager" runat="server" NavigateUrl="~/Admin/Users/UserListing.aspx">
                    User Manager
                </asp:HyperLink>
            </li>
            <li id="liTagManager" runat="server">
                <asp:HyperLink ID="hypTagManager" runat="server" NavigateUrl="~/Admin/Tags/TagListing.aspx">
                    Tag Manager
                </asp:HyperLink>
            </li>
            <li id="liArcResponse" runat="server" visible="false">
                <asp:HyperLink ID="hypArcResponses" runat="server" NavigateUrl="~/Admin/Reject Responses/RejectResponseListing.aspx">
                    Reject Responses
                </asp:HyperLink>
            </li>
            <li id="liList" runat="server">
                <asp:HyperLink ID="hypList" runat="server" NavigateUrl="~/Admin/Lists/ListListing.aspx">
                    List Manager
                </asp:HyperLink>
            </li>
            <li id="liBBCItem" runat="server">
                <asp:HyperLink ID="hypBBCItem" runat="server" NavigateUrl="~/Admin/BBCItems/BBCItemListing.aspx">
                    BBC Item Listing
                </asp:HyperLink>
            </li>
            <li id="liElement" runat="server">
                <asp:HyperLink ID="hypElement" runat="server" NavigateUrl="~/Admin/Element Manager/TypeListing.aspx">
                    Element Manager
                </asp:HyperLink>
            </li>
            <li id="liJobCosting" runat="server">
                <asp:HyperLink ID="hypJobCosting" runat="server" NavigateUrl="~/Admin/Job Costing/JobCosting.aspx">
                    Job Costing
                </asp:HyperLink>
            </li>
            <li id="liWorkingWeekException" runat="server">
                <asp:HyperLink ID="hypWorkingWeekException" runat="server" NavigateUrl="~/Admin/Working Exceptions/WorkWeekExceptionListing.aspx">
                    Working Week Exceptions
                </asp:HyperLink>
            </li>
            <li id="liAdHocException" runat="server">
                <asp:HyperLink ID="hypAdHocException" runat="server" NavigateUrl="~/Admin/Working Exceptions/AdHocListing.aspx">
                    Ad Hoc Exceptions
                </asp:HyperLink>
            </li>
            <li id="liBlockMeeting" runat="server">
                <asp:HyperLink ID="hypBlockMeeting" runat="server" NavigateUrl="~/Admin/Working Exceptions/BlockMeetingExceptionListing.aspx">
                    Block Meeting Exceptions
                </asp:HyperLink>
            </li>
            <li id="liProjectRoleGlobalChange" runat="server">
                <asp:HyperLink ID="hypProjRoleGlobalChange" runat="server" NavigateUrl="~/Admin/Project Role Global Change/ProjectRoleGlobalChange.aspx">
                    Project Role Global Change
                </asp:HyperLink>
            </li>
            <li id="liProjectListing" runat="server">
                <asp:HyperLink ID="hypProjectListing" runat="server" NavigateUrl="~/Admin/Project/AdminProjectListing.aspx">
                    Full Project Listing
                </asp:HyperLink>
            </li>
            <li id="liTimesheetListing" runat="server">
                <asp:HyperLink ID="hypTimesheetListing" runat="server" NavigateUrl="~/Admin/Timesheets/FullTimesheetListing.aspx">
                    Full Timesheet Listing
                </asp:HyperLink>
            </li>
        </ul>

    </div>                    
</nav>