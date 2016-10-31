<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EntityHistory.ascx.vb" Inherits="Controls_Generic_EntityHistory" %>
<asp:PlaceHolder ID="plcHistory" runat="server">
    <asp:GridView ID="grdvHistory" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Date">
            <ItemTemplate>
                <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Literal ID="ltlAction" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Performed By">
            <ItemTemplate>
                <asp:Literal ID="ltlPerformedBy" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Comment">
            <ItemTemplate>
                <asp:Literal ID="ltlComment" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Documents">
            <ItemTemplate>
               <asp:PlaceHolder ID="plcDocuments" runat="server"></asp:PlaceHolder>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcNoHistory" runat="server">
    <label>No history available</label>
</asp:PlaceHolder>
