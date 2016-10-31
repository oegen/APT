<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NumericTextBox.ascx.vb" Inherits="Controls_Generic_NumericTextBox" %>
<asp:Label ID="lblAttribute" runat="server" AssociatedControlID="txtValue" EnableViewState="true"></asp:Label>

<asp:TextBox ID="txtValue" runat="server" MaxLength="10"></asp:TextBox>

<asp:RequiredFieldValidator ID="reqValue" runat="server" ControlToValidate="txtValue" Display="Dynamic" Enabled="false">*</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="regNumber" runat="server" ControlToValidate="txtValue" 
    ValidationExpression="(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)" Display="Dynamic">Enter a number</asp:RegularExpressionValidator>