<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Node.ascx.vb" Inherits="Controls_Admin_Lists_Node" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<fieldset> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtName" runat="server" LabelText="List Item" ValidationGroup="vldSave" MaxLength="100" /></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 

<asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>