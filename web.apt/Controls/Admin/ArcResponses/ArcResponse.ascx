<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ArcResponse.ascx.vb" Inherits="Controls_Admin_ArcResponses_ArcResponse" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<fieldset> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtResponseText" runat="server" LabelText="Item" ValidationGroup="vldSave" MaxLength="256" /></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 