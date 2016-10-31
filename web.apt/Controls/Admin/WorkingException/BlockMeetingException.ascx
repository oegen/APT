<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BlockMeetingException.ascx.vb" 
Inherits="Controls_Admin_WorkingException_BlockMeetingException" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagPrefix="oegen" TagName="DateTimePicker"%>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="SimpleMultiLineTextBox"%>

<script type="text/javascript">
    $(function () {
            $(".datepicker").datepicker("option", "minDate", new Date());
        });
</script>

<fieldset> 
<ol> 
    <li>
         <oegen:DateTimePicker ID="txtDate" runat="server" LabelText="Date" ValidationGroup="vldSave" />
    </li>
    <li>
        <oegen:NumericTextBox ID="txtHours" runat="server" LabelText="Hours" ValidationGroup="vldSave" MaxLength="50" Required="true" />
    </li>
     <li>
        <oegen:SimpleMultiLineTextBox ID="txtDescription" runat="server" LabelText="Description" ValidationGroup="vldSave" Required="true" MaxLength="400"/>
    </li>

</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 