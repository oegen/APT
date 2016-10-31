<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AuditTrail.ascx.vb" Inherits="Controls_Project_AuditTrail" %>

<asp:GridView ID="gvAuditTrail" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20">

    <Columns>
        <asp:TemplateField HeaderText="Date / Time Submitted">
            <ItemTemplate><asp:Label ID="lblDateTime" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Section">
            <ItemTemplate><asp:Label ID="lblSection" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Change Type">
            <ItemTemplate><asp:Label ID="lblChangeType" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Message">
            <ItemTemplate><asp:Label ID="lblMessage" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="User">
            <ItemTemplate><asp:Label ID="lblUser" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>