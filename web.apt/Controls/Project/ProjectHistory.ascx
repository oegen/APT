<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectHistory.ascx.vb" Inherits="Controls_Generic_History" %>

<h1 id="headerTitle" runat="server">History</h1>

<asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="2">
    <Columns>
        <asp:TemplateField HeaderText="Date">
            <ItemTemplate><asp:Label ID="lblDate" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate><asp:Label ID="lblAction" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Performed By">
            <ItemTemplate><asp:Label ID="lblPerformedBy" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Comment">
            <ItemTemplate><asp:Label ID="lblComment" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Reject Response">
            <ItemTemplate><asp:Label ID="lblRejectResponse" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Document">
            <ItemTemplate><asp:Label ID="lblDocument" runat="server"></asp:Label></ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>