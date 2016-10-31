<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DateTimePicker.ascx.vb" Inherits="Controls_Generic_DateTimePicker" %>

<asp:Label ID="lblAttribute" runat="server" EnableViewState="false" AssociatedControlID="txtDate"></asp:Label>

<asp:TextBox ID="txtDate" runat="server" CssClass="datepicker"></asp:TextBox>

<asp:RequiredFieldValidator ID="reqValue" runat="server" ControlToValidate="txtDate" Display="Dynamic" Enabled="false">*</asp:RequiredFieldValidator>

