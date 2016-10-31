<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AptListListing.ascx.vb" Inherits="Controls_Admin_Lists_AptListListing" %>
<asp:GridView ID="grdvAptListing" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:HyperLink ID="hypEditListItem" runat="server">Edit List Items</asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>