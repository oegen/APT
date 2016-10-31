<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BBCItemSelectAndDisplay.ascx.vb" Inherits="Controls_Generic_BBCItemSelectAndDisplay" %>
<%@ Register Src="~/Controls/Generic/BBCItemSearch.ascx" TagName="BBCItemSearch" TagPrefix="oegen"  %>

<asp:PlaceHolder ID="plcBBCItemViewer" runat="server">

        <asp:Literal ID="ltlBBCItem" runat="server"></asp:Literal>
        <asp:LinkButton ID="lnkChange" runat="server" CssClass="lnkChange">Change</asp:LinkButton>

</asp:PlaceHolder>

<oegen:BBCItemSearch ID="ctrlBBCItemSearch" runat="server" Visible="false" LabelText="" />
