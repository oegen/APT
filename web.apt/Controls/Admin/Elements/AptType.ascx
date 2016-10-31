<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AptType.ascx.vb" Inherits="Controls_Admin_Elements_AptType" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<fieldset> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtTypeName" runat="server" LabelText="Type" ValidationGroup="vldSave" MaxLength="50" /></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 