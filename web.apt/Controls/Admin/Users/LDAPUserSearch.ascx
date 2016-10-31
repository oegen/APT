<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LDAPUserSearch.ascx.vb" Inherits="Controls_Admin_Users_LDAPUserSearch" %>

<span class="ldap-search"><asp:Label ID="lblAttribute" CssClass="search" runat="server">Search</asp:Label>
<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
<asp:LinkButton CssClass="lnkGo" ID="lnkSearch" runat="server">Go</asp:LinkButton></span>

<asp:Label ID="lblNoResults" runat="server" Visible="false">There were no results found for your search.</asp:Label>

<asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate><asp:Label ID="lblName" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Username">
            <ItemTemplate><asp:Label ID="lblUsername" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department">
            <ItemTemplate><asp:Label ID="lblDepartment" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Job Title">
            <ItemTemplate><asp:Label ID="lblJobTitle" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="E-mail">
            <ItemTemplate><asp:Label ID="lblEmail" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate><asp:LinkButton ID="lnkSelect" runat="server">Select</asp:LinkButton></ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
