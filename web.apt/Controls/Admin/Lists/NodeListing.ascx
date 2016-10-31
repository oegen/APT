<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NodeListing.ascx.vb" Inherits="Controls_Admin_Lists_NodeListing" %>
<asp:GridView ID="grdvNodeListing" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkDelete_Click">Remove</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>