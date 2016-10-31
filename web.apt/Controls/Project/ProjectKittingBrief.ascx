<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectKittingBrief.ascx.vb" Inherits="Controls_Project_ProjectKittingBrief" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagName="MultilineTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagName="SimpleDropDown" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Kitting/KitContents.ascx" TagName="KitContents" TagPrefix="oegen" %>

<fieldset> 
<legend>Kitting Brief</legend> 

<ol class="generic-alt-list"> 
    <li>
        <label for="radListKitsBuiltMDA">
            Kits to be built by MDA:
        </label>
        <asp:RadioButtonList ID="radListKitsBuiltMDA" runat="server" CssClass="radio-table-alt">
            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>

    <li>
        <oegen:SimpleDropDown ID="ddlKits" runat="server" AutoPostBack="true" LabelText="Kit: " ValidationGroup="vldKitBrief" />
    </li>

    <li><oegen:NumericTextBox ID="txtTotalKitQuantity" runat="server" LabelText="Total Kit Quantity" Required="true" ValidationGroup="vldKitBrief" /></li>
    <li><oegen:NumericTextBox ID="txtKittingCosts" runat="server" LabelText="Kitting Costs" Required="true" ValidationGroup="vldKitBrief" /></li>
    <li><oegen:SimpleTextBox ID="txtBudgetCode" runat="server" LabelText="Funding" MaxLength="256" ValidationGroup="vldKitBrief" /></li>
    <li><oegen:SimpleTextBox ID="txtKitStockCode" runat="server" LabelText="Kit Stock Code" MaxLength="256" ValidationGroup="vldKitBrief" /></li>
    <li><oegen:NumericTextBox ID="txtTotalKits" runat="server" LabelText="Total No of kits " ValidationGroup="vldKitBrief" Required="true" /></li>
    <li><oegen:MultilineTextBox ID="txtInstructions" runat="server" LabelText="Instructions" ValidationGroup="vldKitBrief" Required="true" /></li>
</ol> 
</fieldset>

<fieldset> 
<legend>Dates</legend> 
<ol class="generic-alt-list"> 
    <li><oegen:DateTimePicker ID="BriefSubmitDate" runat="server" LabelText="Kits on Stock Date" /></li>
    
    <asp:PlaceHolder ID="plcProposal" runat="server" Visible="false">
        <li><oegen:DateTimePicker ID="ProposalReqDate" runat="server" LabelText="Proposal Requirement Date" /></li>
        <li><oegen:DateTimePicker ID="QuoteProvidedDate" runat="server" LabelText="Quote Provided Date" /></li>
        <li><oegen:DateTimePicker ID="KitToBeBuiltDate" runat="server" LabelText="Kit To Be Built Date" /></li>
        <li><oegen:DateTimePicker ID="DeliveryDate" runat="server" LabelText="Delivery Date" /></li>
    </asp:PlaceHolder>
 
    <li><oegen:DateTimePicker ID="InHouseDate" runat="server"  LabelText="In-trade date" /></li>
    <li><oegen:DateTimePicker ID="ExpiryDate" runat="server"  LabelText="Expiry Date" /></li>
</ol> 
</fieldset>

<div id="kit-contents-wrap">
    <asp:PlaceHolder ID="plcKitContents" runat="server" Visible="false">
        <fieldset>
            <legend>Kit Contents</legend> 
        </fieldset>
        <br />
        <oegen:KitContents ID="ctrlKitContents" runat="server" />
        <asp:Literal ID="ltlKitContents" runat="server" EnableViewState="false" Visible="false">This kit does not have a quote added to it</asp:Literal> 
    </asp:PlaceHolder>
</div>

<asp:PlaceHolder ID="plcSave" runat="server">
    <div id="submit_btn" class="widebtn btn btn_orange">
        <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldKitBrief">Save</asp:LinkButton><span></span>
    </div> 

    <div id="right-btn">
        <div class="btn btn_grey">
            <asp:LinkButton ID="lnkPrintable" runat="server" Text="Printable Version" /><span></span>
        </div>
    </div>
</asp:PlaceHolder>
<br /><br /><br /><br />
<asp:HyperLink ID="hypPrintable" runat="server" Visible="false">Printable version</asp:HyperLink>

