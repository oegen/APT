<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GenericTitle.ascx.vb" Inherits="Controls_Generic_GenericTitle" %>

<div id="form-title-wrap" class="group">
    <div class="form-title"><h2><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2></div>
    <div class="form-info-btn"><asp:LinkButton ID="lnkHover" runat="server" CssClass="someClass"><asp:Image runat="server" ImageUrl="~/App_Themes/_default/images/form-info-btn.png" /></asp:LinkButton></div>
</div>
    