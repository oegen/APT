<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserTimesheetListing.ascx.vb" Inherits="Controls_TimeSheets_UserTimeSheetListing" %>

<asp:Panel ID="pnlTimesheet" runat="server">
    <asp:GridView ID="grdvTimeSheets" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Time Taken">
            <ItemTemplate>
                <asp:Label ID="lblTimeTaken" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date of Work">
            <ItemTemplate>
                <asp:Label ID="lblDateOfWork" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypView" runat="server">View</asp:HyperLink>
                <asp:LinkButton ID="lnkDelete" runat="server" OnCommand="lnkDelete_OnCommand">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:Panel>

<asp:Panel ID="pnlNoItems" runat="server" Visible="false">
    No timesheet entries found
</asp:Panel>

