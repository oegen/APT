<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FreelancerTimeListing.ascx.vb" Inherits="Controls_Reserve_Time_FreelancerTimeListing" %>

<asp:PlaceHolder ID="plcFreeLancerTime" runat="server">
    <asp:GridView ID="gvFreelanceTimes" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Start Week Number">
                <ItemTemplate>
                    <asp:Label ID="lblWeekNum" runat="server" CssClass="nameLink"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Weeks">
                <ItemTemplate>
                    <asp:Literal ID="ltlTotalWeeks" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Overall Duration">
                <ItemTemplate>
                    <asp:Literal ID="ltlDuration" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Average Hours Per Week">
                <ItemTemplate>
                    <asp:Literal ID="ltlAvgHoursPerWeek" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reserved By">
                <ItemTemplate>
                    <asp:Literal ID="ltlReservedBy" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:PlaceHolder>


<asp:Label ID="lblErrorMessage" runat="server" Visible="false">
    There is no time reserved for this project.
</asp:Label>