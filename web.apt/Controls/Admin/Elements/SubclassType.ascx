<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SubclassType.ascx.vb" Inherits="Controls_Admin_Elements_SubclassType" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>

<asp:HyperLink ID="hypSubclassListing" runat="server" CssClass="adminLink">Back to subclass listing</asp:HyperLink>

<fieldset> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtSubclassTypeName" runat="server" LabelText="Type" ValidationGroup="vldSave" MaxLength="100" /></li> 
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 