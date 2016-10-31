<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdminProjectListing.ascx.vb" Inherits="Controls_Admin_Project_AdminProjectListing" %>

<asp:PlaceHolder ID="plcProjectListing" runat="server">
    <asp:GridView ID="grdvProjectListing" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderText="Project">
                <ItemTemplate>
                    <asp:Literal ID="ltlProject" runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkStartStop" runat="server" OnCommand="lnkStartStop_Command"></asp:LinkButton>
                    <asp:LinkButton ID="lnkArchive" runat="server" OnCommand="lnkArchive_Command"></asp:LinkButton>
                    <asp:HyperLink ID="hypView" runat="server">View</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
    No have projects have been added yet - WHY are you not using the system?
</asp:PlaceHolder>


