<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BBCItemListing.ascx.vb" Inherits="Controls_Admin_BBCItems_BBCItemListing" %>
<asp:GridView ID="grdvBBCItem" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="PartNumber" HeaderText="Part Number" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Literal ID="ltlBrand" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkSetActivity" runat="server" OnCommand="lnkSetActivity_OnCommand">Remove</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
