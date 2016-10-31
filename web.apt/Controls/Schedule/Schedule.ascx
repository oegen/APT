<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Schedule.ascx.vb" Inherits="Controls_Schedule_Schedule" %>
<%@ Register Src="~/Controls/Schedule/ProjectSchedule.ascx" TagName="ProjectSchedule" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Schedule/FreelanceSchedule.ascx" TagName="FreelanceSchedule" TagPrefix="oegen" %>

<div id="apt-list">
                
    <h2>Artworker Reserved Time</h2>

    <oegen:ProjectSchedule ID="ctrlProjectSchedule" runat="server" />

    <h2>Freelance Time</h2>

    <oegen:FreelanceSchedule ID="ctrlFreelanceShedule" runat="server" />

    <h2><asp:Label ID="lblNoResults" runat="server" Visible="false">There are no projects with reserved time for this week.</asp:Label></h2>

</div>