<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SimpleDropDownList.ascx.vb" Inherits="Controls_Generic_SimpleDropDownList" %>
<asp:Label ID="lblAttribute" runat="server" AssociatedControlID="ddlValue" EnableViewState="false"></asp:Label>
<asp:DropDownList ID="ddlValue" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="reqValue" runat="server" Display="Dynamic" ControlToValidate="ddlValue" Enabled="false" InitialValue="0">*</asp:RequiredFieldValidator>