<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Timesheet.ascx.vb" Inherits="Controls_Timesheets_Timesheet" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="MultiLineTextBox" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox"%>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagPrefix="oegen" TagName="DateTimePicker" %>
<%@ Register Src="~/Controls/Timesheets/ElementTimeSheetListing.ascx" TagPrefix="oegen" TagName="ElementTimeSheetListing" %>

<asp:PlaceHolder runat="server" ID="plcBindingFix">
<script type="text/javascript">

    function AutoFillTimeSpent() {
        $("#<%#hourID%>").val("<%#AutoEntryHour%>");
        $("#<%#MinuteID%>").val("<%#AutoEntryMinute%>");
    }
              
</script> 
</asp:PlaceHolder>
<asp:Panel ID="pnlElementDetails" runat="server">
    <fieldset> 
        <ol>
            <li><oegen:SimpleDropDownList ID="ddlProject" runat="server" LabelText="Project" ValidationGroup="saveTimesheet" AutoPostBack="true" /></li>
            <li id="liElement" runat="server" visible="false">
                <label>Element:</label>
                <label id="lblElement" runat="server"></label>
            </li>
            <li>
                <oegen:DateTimePicker ID="txtDateOfWork" runat="server" LabelText="Date of Work" AutoPostBack="true" ValidationGroup="saveTimesheet" />
                <asp:Label ID="lblTimeEntered" runat="server"></asp:Label>
            </li>
            <li id="liProjectLevel" runat="server">
                <asp:Label ID="lblIsProjectLevel" runat="server" AssociatedControlID="chkProjectLevel">Project level</asp:Label>
                <asp:CheckBox ID="chkProjectLevel" runat="server" Checked="true" AutoPostBack="true" />
            </li> 
            <asp:PlaceHolder ID="plcProjectLevelTimeInfo" runat="server">
                <li><oegen:NumericTextBox ID="txtHourSpent" runat="server" LabelText="Hours " /></li>
                <li><oegen:NumericTextBox ID="txtMinuteSpent" runat="server" LabelText="Minutes " /></li>
                <li><oegen:SimpleDropDownList ID="ddlReason" runat="server" LabelText="Reason" ValidationGroup="saveTimesheet"  /></li>
            </asp:PlaceHolder>
            <oegen:ElementTimeSheetListing ID="ctrlElementTimeSheetListing" runat="server" Visible="false" />
        </ol>
    </fieldset>
    
    <asp:PlaceHolder ID="plcSave" runat="server">
        <div id="submit_btn" class="btn btn_orange">
            <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="saveTimesheet">Save</asp:LinkButton><span></span>
        </div>    
    </asp:PlaceHolder>
    
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"><br /><br /><br /><br />There aren't any elements in this project</asp:Label> 
</asp:Panel>

<asp:Panel ID="pnlNoItems" runat="server" Visible="false">
    You are not assigned to any projects
</asp:Panel>



