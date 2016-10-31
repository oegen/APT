<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FullTimesheetsListing.ascx.vb" Inherits="Controls_Admin_Timesheets_FullTimesheetsListing" %>

<asp:PlaceHolder ID="plcTimesheet" runat="server">
    <asp:GridView ID="grdvTimeSheets" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Project">
            <ItemTemplate>
                <asp:Literal ID="ltlProject" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Element">
            <ItemTemplate>
                <asp:Literal ID="ltlElement" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="User">
            <ItemTemplate>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Entry Date">
            <ItemTemplate>
                <asp:Literal ID="ltlEntryDate" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Time Spent">
            <ItemTemplate>
                <asp:Literal ID="ltlTimeSpent" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Reason">
            <ItemTemplate>
                <asp:Literal ID="ltlReason" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
    <asp:Literal ID="ltlNoResult" runat="server">There are no timesheet entries in the system yet</asp:Literal>
</asp:PlaceHolder>

