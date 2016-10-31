<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectBBCItem.ascx.vb" Inherits="Controls_Element_ProjectBBCItem" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="MultiLineTextBox" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox"%>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DatePicker" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/BBCItemSearch.ascx" TagName="BBCItemSearch" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/BBCItemSelectAndDisplay.ascx" TagName="BBCItemSelectAndDisplay" TagPrefix="oegen" %>

<fieldset> 
<ol> 
   <li>
        <asp:Label ID="lblBBCItem" runat="server" AssociatedControlID="ctrlBBCItemSelectAndDisplay" EnableViewState="false">BBC Item</asp:Label>
        <oegen:BBCItemSelectAndDisplay ID="ctrlBBCItemSelectAndDisplay" runat="server" />
   </li>
   
   <li><oegen:NumericTextBox ID="txtQuantity" runat="server" LabelText="Quantity" ValidationGroup="saveBBC" Required="true" /></li>
   <li><oegen:NumericTextBox ID="txtPackQuantity" runat="server" LabelText="Pack Quantity" ValidationGroup="saveBBC" Required="true" /></li>
   <li><oegen:DatePicker ID="txtDeliveryDate" runat="server" LabelText="Delivery Date" ValidationGroup="saveBBC" /></li>
</ol> 
</fieldset>

<asp:PlaceHolder ID="plcSave" runat="server">
    <div id="submit_btn" runat="server" class="btn btn_orange">
        <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="saveBBC">Save</asp:LinkButton><span></span>
    </div> 

    <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false" Visible="false">You must select a BBC Item before saving</asp:Label>
</asp:PlaceHolder>

