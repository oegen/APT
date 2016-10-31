<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ArcResponseListing.ascx.vb" Inherits="Controls_Admin_ArcReponses_ArcResponseListing" %>
<asp:GridView ID="grdvArcResponses" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
    <Columns>
        <asp:BoundField DataField="ResponseText" HeaderText="Response Text" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="hypEdit" runat="server">Edit</asp:HyperLink>
                <asp:LinkButton ID="lnkSetActivity" runat="server" OnCommand="lnkSetActivity_OnCommand"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>