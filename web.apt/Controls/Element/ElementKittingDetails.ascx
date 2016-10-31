<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementKittingDetails.ascx.vb" Inherits="Controls_Project_ElementKittingDetails" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="oegen" %>


 <div class="aptform group">

     <fieldset> 
        <legend>Element Kitting Details</legend>
        <ol>
            <li><oegen:NumericTextBox ID="txtCostPerItem" runat="server" LabelText="Cost Per Item" /></li>
            <li><oegen:DateTimePicker ID="txtExpiryDate" runat="server" LabelText="Expiry Date" /></li>
            <li><oegen:SimpleTextBox ID="txtPONumber" runat="server" LabelText="PO Number" MaxLength="256" /></li>
            <li>
                <label for="radListExistNew">
                    Existing or New
                </label>
                
                <asp:RadioButtonList ID="radListExistNew" runat="server" CssClass="radio-table-alt">
                    <asp:ListItem Value="0">New</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">Existing</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li><oegen:SimpleTextBox ID="txtSupplier" runat="server" LabelText="Print Supplier" MaxLength="256" /></li>
            <li><oegen:DateTimePicker ID="txtDateIntoMDA" runat="server" LabelText="Due date into MDA" /></li>
        </ol>
    </fieldset>

    <div id="submit_btn" class="btn btn_orange" runat="server">
        <asp:LinkButton ID="lnkSave" runat="server">Save</asp:LinkButton><span></span>
    </div>

 </div>











