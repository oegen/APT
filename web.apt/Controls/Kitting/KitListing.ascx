<%@ Control Language="VB" AutoEventWireup="false" CodeFile="KitListing.ascx.vb" Inherits="Controls_Kitting_KitListing" %>

<asp:GridView ID="grdvKit" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true">
    <Columns>
        <asp:BoundField HeaderText="Kit" DataField="Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypLatestQuote" runat="server">Latest Quote</asp:HyperLink>
                <asp:HyperLink ID="hypViewQuotes" runat="server">View Quotes</asp:HyperLink>
                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click">Remove</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>