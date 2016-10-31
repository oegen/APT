<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DocumentHistoryListing.ascx.vb" Inherits="Controls_Project_DocumentHistoryListing" %>

<asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
There hasn't been another version of this document created yet
</asp:PlaceHolder>

<asp:GridView ID="gvDocVersions" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:TemplateField HeaderText="Modified Date Time">
            <ItemTemplate>
                <asp:Label ID="lblCreationTime" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="User">
            <ItemTemplate>
                <asp:Label ID="lblUser" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Hyperlink ID="hypView" runat="server" Target="_blank">View</asp:Hyperlink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>