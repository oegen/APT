<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubclassTypeListing.ascx.vb" Inherits="Controls_Admin_Elements_SubclassTypeListing" %>
<asp:GridView ID="grdvSubclassType" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkSetActivity" runat="server" OnCommand="lnkSetActivity_OnCommand"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>