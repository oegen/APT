<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AdHocException.ascx.vb" Inherits="Controls_Admin_WorkingException_AdHocException" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList"%>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox"%>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagPrefix="oegen" TagName="DateTimePicker"%>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="MultilineTextBox"%>

<script type="text/javascript">
    $(function () {
        $(".datepicker").datepicker("option", "minDate", new Date());
    });
</script>

<fieldset> 
<ol> 
    <li>
        <oegen:SimpleDropDownList ID="ddlUser" runat="server" LabelText="User" ValidationGroup="vldSave" />
    </li>
    <li>
        <asp:Label ID="lblAttribute" runat="server" AssociatedControlID="ddlAdHocOptions" EnableViewState="true">Ad Hoc Type</asp:Label>
            <asp:DropDownList ID="ddlAdHocOptions" runat="server" AutoPostBack="true">
                <asp:ListItem Text="Single Day" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Multiple Days" Value="1" Selected="False"></asp:ListItem>
            </asp:DropDownList>
    </li>
    <li>
        <oegen:DateTimePicker ID="dpStartDate" runat="server" LabelText="Start Date" ValidationGroup="vldSave" />
    </li>
    <li id="liEndDate" runat="server">
        <oegen:DateTimePicker ID="dpEndDate" runat="server" LabelText="End Date" />
        <label>
            <asp:label ID="ltlEndDateError" runat="server" 
                ForeColor="Red" Visible="false" EnableViewState="false">
            </asp:label>
        </label>
    </li>
    <li id="liHours" runat="server">
        <oegen:NumericTextBox ID="txtHours" runat="server" LabelText="Hours" Required="true" ValidationGroup="vldSave" />
    </li>
    <li>
        <oegen:MultilineTextBox ID="txtDescription" runat="server" LabelText="Description" ValidationGroup="vldSave" MaxLength="400" />
    </li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 

