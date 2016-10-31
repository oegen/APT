<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserSearchListing.ascx.vb" Inherits="Controls_User_UserSearchListing" %>

<asp:TextBox ID="txtSearch" CssClass="brief-txt-box-last" runat="server"></asp:TextBox>
<asp:LinkButton ID="lnkSearch" CssClass="lnkSearch" runat="server">Search</asp:LinkButton>

<asp:Label ID="lblNoResults" runat="server" ForeColor="Red" Visible="false">There were no results available for the search specified.</asp:Label>

<asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Username">
            <ItemTemplate><asp:Label ID="lblUsername" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Full Name">
            <ItemTemplate><asp:Label ID="lblFullName" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate><asp:LinkButton ID="lnkSelect" runat="server">Select</asp:LinkButton></ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>