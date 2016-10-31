<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftNav.ascx.vb" Inherits="Controls_Navigation_LeftNav" %>

<div id="primary-nav">
    <asp:Image ImageUrl="~/App_Themes/_default/images/apt-logo.gif" CssClass="apt-logo" runat="server" />
    <ul class="primary-nav">
        <li id="liViewProjects" runat="server" class="view-projects-selected">
            <asp:LinkButton ID="lnkViewProjects" runat="server">View Projects</asp:LinkButton>
        </li>
        <li id="liViewTasks" runat="server" class="view-tasks">
            <asp:LinkButton ID="lnkViewTasks" runat="server">View Tasks</asp:LinkButton>
        </li>
        <li id="liReport" runat="server" class="reports">
            <asp:LinkButton ID="lnkReport" runat="server">Reports</asp:LinkButton>
         </li>
        <li id="liAdmin" runat="server" class="admin"> 
            <asp:LinkButton ID="lnkAdmin" runat="server">Admin</asp:LinkButton>
        </li>
        <li id="liSchedule" runat="server" class="calendar">
            <asp:LinkButton ID="lnkSchedule" runat="server">Schedule</asp:LinkButton>
        </li>
        <li id="liTimesheet" runat="server" class="time-sheet">
            <asp:LinkButton ID="lnkTimesheet" runat="server">Timesheet</asp:LinkButton>
        </li>
    </ul>
</div>
