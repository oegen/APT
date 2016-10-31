<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdditionalElementsListing.ascx.vb" Inherits="Controls_Element_AdditionalElementsListing" %>

<asp:Panel ID="pnlAdditionalElementListing" runat="server">
    <asp:GridView ID="grdvAdditionalElementListing" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Premium Product">
            <ItemTemplate>
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cost">
            <ItemTemplate>
                <asp:Literal ID="ltlCost" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:HyperLink ID="hypAdditionalElements" runat="server">Edit</asp:HyperLink>
                <asp:HyperLink ID="hypPremiumBrief" runat="server">View Details</asp:HyperLink>
                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:Panel>

<asp:Panel ID="pnlNoItems" runat="server" Visible="false">
    No additional products have been added
</asp:Panel>


