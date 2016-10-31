<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SimpleTextBox.ascx.vb" Inherits="Controls_Generic_SimpleTextBox" %>

<asp:Label ID="lblAttribute" runat="server" AssociatedControlID="txtValue" EnableViewState="false"></asp:Label>
<asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqValue" runat="server" Display="Dynamic" ControlToValidate="txtValue" Enabled="false">*</asp:RequiredFieldValidator>
