<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KitQuoteListing.ascx.vb" Inherits="Controls_Kitting_KitQuoteListing" %>

<asp:GridView ID="grdvKitQuote" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true">
    <Columns>
        <asp:BoundField HeaderText="Quote Number" DataField="Number" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypView" runat="server">View</asp:HyperLink>
                <asp:HyperLink ID="hypViewContents" runat="server">View Contents</asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


