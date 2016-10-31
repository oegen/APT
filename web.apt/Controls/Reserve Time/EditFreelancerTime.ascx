<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditFreelancerTime.ascx.vb" Inherits="Controls_Reserve_Time_EditFreelancerTime" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>

<fieldset> 
    <legend>Freelancer Time</legend> 

    <ol>
       <li><oegen:NumericTextBox ID="txtStartWeek" runat="server" LabelText="Start Week : " MaxLength="2" ValidationGroup="vldFreelanceTime" /></li>
       <li><oegen:NumericTextBox ID="txtStartYear" runat="server" LabelText="Start Year : " ValidationGroup="vldFreelanceTime" /></li>
       <li><oegen:NumericTextBox ID="txtNumWeeks" runat="server" LabelText="Number of Weeks : " MaxLength="2" ValidationGroup="vldFreelanceTime" /></li>
       <li><oegen:NumericTextBox ID="txtDuration" runat="server" LabelText="Total Number of Hours : " ValidationGroup="vldFreelanceTime" /></li>
    </ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldFreelanceTime">Save</asp:LinkButton><span></span>
</div>