<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectBBCItemListing.ascx.vb" Inherits="Controls_Element_ProjectBBCItemListing" %>

<asp:Panel ID="pnlBBCItem" runat="server">
    <asp:GridView ID="grdvBBCItem" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
     <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity">
            <ItemTemplate>
                <asp:Literal ID="ltlQuantity" runat="server"></asp:Literal> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pack Quantity">
            <ItemTemplate>
                <asp:Literal ID="ltlPackQuantity" runat="server"></asp:Literal> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Delivery Date">
            <ItemTemplate>
                <asp:Literal ID="ltlDeliveryDate" runat="server"></asp:Literal> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
               <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
               <asp:LinkButton ID="lnkDelete" runat="server" OnCommand="lnkDelete_Click">Delete</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:Panel>
<asp:Panel ID="pnlNoItems" runat="server" Visible="false">
    No BBC Items have been added yet to this project
</asp:Panel>