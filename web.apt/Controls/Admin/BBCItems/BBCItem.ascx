<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BBCItem.ascx.vb" Inherits="Controls_Admin_BBCItems_BBCItem" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>

<fieldset> 
<ol> 
   <li>
        <oegen:SimpleTextBox ID="txtPartNumber" runat="server" LabelText="Part Number" 
            ValidationGroup="vldSave" MaxLength="256" />
   </li>
   <li><oegen:SimpleDropDownList ID="ddlBrands" runat="server" LabelText="Brand" /></li>
   <li><oegen:SimpleTextBox ID="txtDescription" runat="server" LabelText="Description" 
            ValidationGroup="vldSave" MaxLength="256" /></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 