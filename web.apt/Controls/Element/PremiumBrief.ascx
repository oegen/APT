<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PremiumBrief.ascx.vb" Inherits="Controls_Project_PremiumBrief" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagName="MultilineTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/UserDisplayAndSelect.ascx" TagName="UserBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>

<fieldset> 
    <legend>Quotes Total Costing:</legend>
    <ol>
        <li><oegen:NumericTextBox ID="txtTotalCostingEstimate" runat="server" LabelText="Estimate" MaxLength="7" /></li>
        <li><oegen:NumericTextBox ID="txtTotalCostingFinal" runat="server" LabelText="Final" MaxLength="7" /></li>
    </ol>

    <legend>Production Time:</legend>
    <ol>
        <li><oegen:NumericTextBox ID="txtProductionTimeCostEstimate" runat="server" LabelText="Estimate" MaxLength="7" /></li>
        <li><oegen:NumericTextBox ID="txtProductionTimeCostFinal" runat="server" LabelText="Final" MaxLength="7" /></li>
    </ol>

    <legend>Previous Cost:</legend>
    <ol>
        <li><oegen:NumericTextBox ID="txtPreviousCostEstimate" runat="server" LabelText="Estimate" MaxLength="7" /></li>
        <li><oegen:NumericTextBox ID="txtPreviousCostFinal" runat="server" LabelText="Final" MaxLength="7" /></li>
        <li><oegen:MultilineTextBox ID="txtPreviousSupplier" runat="server" LabelText="Previous Supplier" MaxLength="256" /></li>
    </ol>

    <legend>New MDA Fields</legend>
    <ol>
        <li>
            <oegen:SimpleDropDownList ID="ctrlMDAManager" runat="server" LabelText="MDA Manager" />
        </li>
        <li>
            <oegen:MultilineTextBox ID="txtBackground" runat="server" LabelText="Background" MaxLength="1000" />
        </li>
        <li>
            <oegen:MultilineTextBox ID="txtPurposeOfProject" runat="server" LabelText="Purpose of Project" MaxLength="1000" />
        </li>
        <li>
            <oegen:MultilineTextBox ID="txtTargetMarket" runat="server" LabelText="Target Market" MaxLength="1000" />
        </li>
        <li>
            <label">On / Off Trade</label>
            <asp:RadioButtonList ID="radOnTrade" runat="server" CssClass="radio-table-alt">
                <asp:ListItem Value="True" Selected="True">Yes</asp:ListItem>
                <asp:ListItem Value="False">No</asp:ListItem>
            </asp:RadioButtonList>
        </li>
        <li>
            <oegen:MultilineTextBox ID="txtFurtherInfo" runat="server" LabelText="Further Info" MaxLength="1000" />
        </li>
    </ol>

</fieldset>

<div id="submit_btn" runat="server" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSaveKitBrief">Save</asp:LinkButton><span></span>
</div> 

<asp:PlaceHolder ID="plcPrintBtn" runat="server" Visible="false">
    <div id="right-btn">
        <div class="btn btn_grey">
            <asp:LinkButton ID="lnkPrint" runat="server">Printable Version</asp:LinkButton><span></span>
        </div>
    </div>
</asp:PlaceHolder>

