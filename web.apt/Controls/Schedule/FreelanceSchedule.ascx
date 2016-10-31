<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FreelanceSchedule.ascx.vb" Inherits="Controls_Schedule_FreelanceScheduleItem" %>

<asp:PlaceHolder ID="plcFreelanceTime" runat="server">
    <asp:GridView ID="gvFreelanceTimes" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Project">
                <ItemTemplate>
                    <asp:Label ID="lblAIN" runat="server"></asp:Label> - 
                    <asp:HyperLink ID="lnkProject" runat="server" CssClass="nameLink"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Week Number">
                <ItemTemplate>
                    <asp:Label ID="lblStartWeekNum" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Weeks">
                <ItemTemplate>
                    <asp:Label ID="lblTotalWeeks" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Duration<br />(for all weeks)">
                <ItemTemplate>
                    <asp:Label ID="lblDuration" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Avg. Hours Per Week">
                <ItemTemplate>
                    <asp:Label ID="lblAvgHoursPerWeek" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reserved By">
                <ItemTemplate>
                    <asp:Label ID="lblReservedBy" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server">
     No Freelancers are required this week.
</asp:PlaceHolder>
