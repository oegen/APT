<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserDisplayAndSelect.ascx.vb" Inherits="Controls_Generic_UserDisplayAndSelect" %>
<%@ Register Src="~/Controls/Generic/UserSearchListing.ascx" TagName="UserSearch" TagPrefix="oegen"  %>

<asp:PlaceHolder ID="pnlUserViewer" runat="server">

        <asp:Label ID="lblUser" runat="server"></asp:Label>
        <asp:LinkButton ID="lnkChange" runat="server" CssClass="lnkChange">Change</asp:LinkButton>
        <asp:CustomValidator ID="vldUser" runat="server" Display="Dynamic" Enabled="false" ErrorMessage="*"></asp:CustomValidator>

</asp:PlaceHolder>

<oegen:UserSearch ID="ctrlSearchuser" runat="server" Visible="false" LabelText="" />