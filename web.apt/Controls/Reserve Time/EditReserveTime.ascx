<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditReserveTime.ascx.vb" Inherits="Controls_Reserve_Time_EditReserveTime" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>

<fieldset> 
    <legend>Reserve Time</legend> 

    <ol>
       <li><oegen:NumericTextBox ID="txtWeekNumber" runat="server" LabelText="Week Number : " MaxLength="2" ValidationGroup="vldReserveTime" /></li>
       <li><oegen:NumericTextBox ID="txtYearNumber" runat="server" LabelText="Year Number : " ValidationGroup="vldReserveTime" /></li>
       <li><oegen:NumericTextBox ID="txtDuration" runat="server" LabelText="Number of Hours : " ValidationGroup="vldReserveTime" /></li>
       <li><oegen:NumericTextBox ID="txtNumberArtworkers" runat="server" LabelText="Number of Artworkers : " ValidationGroup="vldReserveTime" /></li>
    </ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldReserveTime">Save</asp:LinkButton><span></span>
</div>