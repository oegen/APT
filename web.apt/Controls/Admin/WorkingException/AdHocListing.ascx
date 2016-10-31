<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdHocListing.ascx.vb" Inherits="Controls_Admin_WorkingException_AdHocListing" %>

<asp:PlaceHolder ID="plcWorkingWeekException" runat="server">
    <asp:GridView ID="grdvWorkingWeekException" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Username">
            <ItemTemplate>
                <asp:Label ID="lblUsername" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Date">
            <ItemTemplate>
                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Date">
            <ItemTemplate>
                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hours">
            <ItemTemplate>
                <asp:Label ID="lblhours" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:hyperlink ID="hypEdit" runat="server">Edit</asp:hyperlink>
                <asp:LinkButton ID="lnkDelete" runat="server" OnCommand="lnkSetActivity_OnCommand">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server">
<br /><br />
    No working week exceptions have been added to the system yet
</asp:PlaceHolder>
