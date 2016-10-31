<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TagListing.ascx.vb" Inherits="Controls_Admin_Tags_TagListing" %>
<asp:GridView ID="grdvTags" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Tag Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypTag" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkSetActivity" runat="server" OnCommand="lnkSetActivity_OnCommand"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>