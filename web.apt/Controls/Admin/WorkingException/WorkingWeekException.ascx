<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WorkingWeekException.ascx.vb" Inherits="Controls_Admin_WorkingWeekException_WorkingWeekException" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="MultilineTextBox" %>

<fieldset> 
<ol> 
   <li>
        <oegen:SimpleDropDownList ID="ddlUser" runat="server" LabelText="User" ValidationGroup="vldSave" />
   </li>
    <li>
        <oegen:NumericTextBox ID="txtHours" runat="server" LabelText="Hours" ValidationGroup="vldSave" MaxLength="50" Required="true" />
    </li>
        <li>
        <oegen:MultilineTextBox ID="txtDescription" runat="server" LabelText="Description" ValidationGroup="vldSave" MaxLength="400" />
    </li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 