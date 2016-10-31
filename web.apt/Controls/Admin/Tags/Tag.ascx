<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tag.ascx.vb" Inherits="Controls_Admin_Tags_Tag" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<fieldset> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtName" runat="server" LabelText="Name" ValidationGroup="vldSave" MaxLength="50" /></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 

<asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"><br /><br /><br /><br /></asp:Label>

