<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MessagePanel.ascx.vb" Inherits="Controls_Layout_MessagePanel" %>

<div id="description-panel" style="display: none; "> 

    <h3>Section:</h3>
    <p><asp:Label ID="lblUpdatedSection" runat="server"></asp:Label></p>

    <h3>Action:</h3>
    <p><asp:Label ID="lblAction" runat="server"></asp:Label></p>

    <h3>Updated by:</h3>
    <p><asp:Label ID="lblUpdatedBy" runat="server"></asp:Label></p>

    <h3>Update on:</h3>
    <p><asp:Label ID="lblUpdateDate" runat="server"></asp:Label></p>

    <h3>Details:</h3>
    <p><asp:Label ID="lblDetails" runat="server"></asp:Label></p>

</div>