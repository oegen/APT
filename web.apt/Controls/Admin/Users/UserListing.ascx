<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserListing.ascx.vb" Inherits="Controls_Admin_Users_UserListing" %>

<asp:GridView ID="grdvUsers" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true">
    <Columns>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Username">
            <ItemTemplate>
                <asp:Literal ID="ltlUsername" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:HyperLink ID="hypViewDetails" runat="server">View Details</asp:HyperLink>
                <asp:HyperLink ID="hypViewLogin" runat="server">View Login Details</asp:HyperLink>
                <asp:LinkButton ID="lnkSetUserActivity" runat="server" OnCommand="lnkSetUserActivity_Command"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<asp:Panel ID="pnlEmpty" runat="server">
    No users could be found
</asp:Panel>

