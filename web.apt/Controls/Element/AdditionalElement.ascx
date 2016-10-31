<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdditionalElement.ascx.vb" Inherits="Controls_Project_AdditionalElement" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>

<fieldset> 
<legend>Details</legend> 
<ol> 
   <li><oegen:SimpleTextBox ID="txtName" runat="server" LabelText="Premium Item: " 
            ValidationGroup="vldAdditionalElement" MaxLength="50" /></li>
   <li><oegen:SimpleTextBox ID="txtDescription" runat="server" LabelText="Description: " MaxLength="1000" /></li>
   <li><oegen:NumericTextBox ID="txtCost" runat="server" LabelText="Unit Cost: " MaxLength="9" ValidationGroup="vldAdditionalElement" /></li>
</ol> 
</fieldset>

<div id="submit_btn" runat="server" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldAdditionalElement">Save</asp:LinkButton><span></span>
</div> 

