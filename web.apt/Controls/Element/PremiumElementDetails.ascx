<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PremiumElementDetails.ascx.vb" Inherits="Controls_Element_PremiumElementDetails" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagName="MultilineTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DatePicker" TagPrefix="oegen" %>

<fieldset> 
    <legend>Details</legend> 
    <ol class="generic-alt-list">
        <li><oegen:SimpleTextBox ID="txtStockCode" runat="server" LabelText="Premium Item Stock Code: " MaxLength="256"/></li>
        <li><oegen:SimpleTextBox ID="txtDescription" runat="server" LabelText="Outline Description: " /></li>
        <li><oegen:NumericTextBox ID="txtBudgetPerItem" runat="server" LabelText="Budget per item / Target pricing: " /></li>
        <li><oegen:NumericTextBox ID="txtQuantity" runat="server" LabelText="Quantity: " /></li>
        <li><oegen:SimpleTextBox ID="txtQuantityBreak" runat="server" LabelText="Quantity Break: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtColours" runat="server" LabelText="Colours: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtBrand" runat="server" LabelText="Brand: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtBrandPosition" runat="server" LabelText="Brand Position: " MaxLength="256" /></li>
        <li><oegen:NumericTextBox ID="txtNumberOfBranding" runat="server" LabelText="Number of Branding: " /></li>
        <li><oegen:SimpleTextBox ID="txtSizeOfItem" runat="server" LabelText="Size of item: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtTextile" runat="server" LabelText="Textile: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtPurposeOfItem" runat="server" LabelText="Purpose of Items: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtItemPackaging" runat="server" LabelText="How are they packed: " MaxLength="256" /></li>
        <li>
            <oegen:MultilineTextBox ID="txtFinalRequirements" runat="server" LabelText="Final Requirements" />
        </li>
        <li>
            <oegen:MultilineTextBox ID="txtWeightRestrictions" runat="server" LabelText="Weight Restrictions" MaxLength="1000" />
        </li>
        <li><oegen:SimpleTextBox ID="txtSampleRequired" runat="server" LabelText="Sample Required: " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtHasBDDesign" runat="server" LabelText="If no design required, has BD designed the item? " MaxLength="256" /></li>
        <li><oegen:SimpleTextBox ID="txtSimilarProducts" runat="server" LabelText="Similar Products: " MaxLength="256" /></li>
        <li>
            <label for="radExistOnBBC">Image uploaded:</label>
            <asp:RadioButtonList ID="radImageUpload" runat="server" CssClass="radio-table-alt">
                <asp:ListItem Value="True">Yes</asp:ListItem>
                <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
            </asp:RadioButtonList>
        </li>
        <li><oegen:NumericTextBox ID="txtQuoteCosts" runat="server" LabelText="Quote Costs" /></li>
        <li><oegen:SimpleTextBox ID="txtSupplier" runat="server" LabelText="Supplier" MaxLength="256" /></li>
        <li><oegen:DatePicker ID="dtEtaDate" runat="server" LabelText="ETA Date" /></li>
        <li><oegen:DatePicker ID="dtExpiryDate" runat="server" LabelText="Expiry Date" /></li>

        <li><oegen:DatePicker ID="txtBriefSubmittedDate" runat="server" LabelText="Date Brief Submitted" /></li>
        <li><oegen:DatePicker ID="txtProposalRequiredDate" runat="server" LabelText="Proposal Required Date" /></li>
        <li><oegen:DatePicker ID="txtQuoteProvidedDate" runat="server" LabelText="Quote Provided Date" /></li>
        <li><oegen:DatePicker ID="txtArtworkAvailableDate" runat="server" LabelText="Artwork Available From" /></li>
        <li><oegen:DatePicker ID="txtPODate" runat="server" LabelText="PO Date" /></li>
        <li><oegen:DatePicker ID="txtDeliveryDate" runat="server" LabelText="Delivery Date" /></li>
        <li><oegen:SimpleTextBox ID="txtDeliveryAddress" runat="server" LabelText="Delivery Address" /></li>

        <li style="visibility:hidden"><oegen:SimpleTextBox ID="txtImage" runat="server" LabelText="Image" /></li>

    

        </ol>
</fieldset> 

<div id="divSave" runat="server" class="widebtn btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server">Save</asp:LinkButton><span></span>
</div> 