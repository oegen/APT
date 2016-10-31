<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DocumentListing.ascx.vb" Inherits="Controls_Generic_DocumentListing" %>

<asp:Panel ID="pnlDocuments" runat="server">
    <asp:GridView ID="grdvDocuments" runat="server" AllowPaging="true" AutoGenerateColumns="false" PageSize="20">
        <Columns>

            <asp:TemplateField HeaderText="DOCUMENT">
                <ItemTemplate>
                    <asp:HyperLink ID="hypDocumentName" runat="server" CssClass="nameLink" Target="_blank" Visible="false"></asp:HyperLink>
                    <asp:LinkButton ID="lnkDocumentName" runat="server" CssClass="nameLink" OnClick="lnkOpen_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="UPLOADED BY">
                <ItemTemplate>
                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="DATE UPLOADED">
                <ItemTemplate>
                    <asp:Label ID="lblDateUploaded" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CATEGORY">
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Size">
                <ItemTemplate>
                    <asp:Label ID="lblSize" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ACTIONS">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click">Delete</asp:LinkButton>
                    <asp:LinkButton ID="lnkHistory" runat="server" OnClick="lnkHistory_Click">History</asp:LinkButton>
                    <asp:HyperLink ID="hypAnnotate" runat="server" Visible="false" Target="_blank">Annotate</asp:HyperLink>
                    <asp:HyperLink ID="hypJavaAnnotate" runat="server" Visible="false">Old Annotate</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Panel>

<asp:Panel ID="pnlNoItems" runat="server" Visible="false">
    <asp:Literal ID="ltlNoItem" runat="server" EnableViewState="false">No Documents have been uploaded for this category</asp:Literal>
</asp:Panel>