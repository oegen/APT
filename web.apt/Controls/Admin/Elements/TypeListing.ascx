<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TypeListing.ascx.vb" Inherits="Controls_Admin_Elements_TypeListing" %>
<asp:GridView ID="grdvTypes" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkSetActivity" runat="server" OnCommand="lnkSetActivity_OnCommand"></asp:LinkButton>
                <asp:HyperLink ID="hypSubTypes" runat="server">View Subtypes</asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>