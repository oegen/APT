<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Kit.ascx.vb" Inherits="Controls_Kit_Kit" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagName="SimpleMultiLineTextBox" TagPrefix="oegen" %>

<fieldset>
<legend>Add New Kit</legend>
    <ol>
        <li><oegen:SimpleTextBox ID="txtKitName" runat="server" LabelText="Kit Name" 
                ValidationGroup="vldSaveKit" MaxLength="256" /></li>
        <li><oegen:NumericTextBox ID="txtQuantity" runat="server" LabelText="Total Quantity of Kits" 
                ValidationGroup="vldSaveKit" /></li>
        <li runat="server" visible="false"><oegen:NumericTextBox ID="txtCost" runat="server" LabelText="Cost" MaxLength="9" 
                ValidationGroup="vldSaveKit" /></li>
        <li>
            <oegen:SimpleTextBox ID="txtKitPacking" runat="server" LabelText="Kit Packing" MaxLength="256" 
                ValidationGroup="vldSaveKit" />
        </li> 
        <li>
            <oegen:SimpleTextBox ID="txtDistribution" runat="server" LabelText="Distribution" MaxLength="256" 
                ValidationGroup="vldSaveKit" />
        </li>
        <li>
            <oegen:SimpleTextBox ID="txtPicking" runat="server" LabelText="Picking" MaxLength="256" 
                ValidationGroup="vldSaveKit" />
        </li>         
        <li>
            <oegen:SimpleTextBox ID="txtManualOrderEntry" runat="server" LabelText="Manual Order Entry" MaxLength="256" 
                ValidationGroup="vldSaveKit" />
        </li>
        <li>
            <oegen:SimpleMultiLineTextBox ID="txtComments" runat="server" LabelText="Comments" MaxLength="1000" 
                ValidationGroup="vldSaveKit" />
        </li>     
         <li>
            <oegen:SimpleTextBox ID="txtMultipleOrderEntry" runat="server" LabelText="Multiple Order Entry" MaxLength="256" 
                ValidationGroup="vldSaveKit" />
        </li>     


        <li>
            <label>Funding:</label>
            <asp:DropDownList ID="ddlBrandFunded" runat="server" AutoPostBack="true">
                <asp:ListItem Value="0">- Select an option -</asp:ListItem>
                <asp:ListItem Value="1">Brand Funded</asp:ListItem>
                <asp:ListItem Value="2">Sales Funded</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqBrandFunded" runat="server" InitialValue="0" ControlToValidate="ddlBrandFunded" 
                ErrorMessage="*" ValidationGroup="vldSaveKit"></asp:RequiredFieldValidator>
        </li>
      
        <li id="liSalesFunded" runat="server" visible="false">
            <oegen:NumericTextBox ID="txtSalesFunded" runat="server" LabelText="Sales Funded : Quantity" ValidationGroup="vldSaveKit" />
        </li>

        <asp:PlaceHolder ID="plcBrandFundInfo" runat="server"  Visible="false">
            <li>
                <oegen:NumericTextBox ID="txtBrandFunded" runat="server" LabelText="Brand Funded : Quantity" ValidationGroup="vldSaveKit" />
            </li>          
            <li>
                <oegen:SimpleTextBox ID="txtBudgetCode" runat="server" LabelText="Budget Code" MaxLength="256" />
            </li>
            <li>
                <oegen:SimpleTextBox ID="txtCostCentre" runat="server" LabelText="Cost Centre" MaxLength="256"  />
            </li>
            <li>
                <oegen:SimpleTextBox ID="txtProjectCode" runat="server" LabelText="Project Code" MaxLength="256"  />
            </li>    
        </asp:PlaceHolder>                    
    </ol>
</fieldset>

<fieldset runat="server" visible="false"> 
<legend>Kitting Quotes</legend> 
<ol> 

    <li>
        <label for="radListFragileItems">
            Contain fragile items:
        </label>
        <asp:RadioButtonList ID="radListFragileItems" runat="server"  CssClass="radio-table-alt">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>
    <li>
        <label for="radListHighValueItems">
            Contain high value items:
        </label>
        <asp:RadioButtonList ID="radListHighValueItems" runat="server" CssClass="radio-table-alt">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>
    <li>
        <label for="radListDespatchListToBeProvided">
            Get Despatched to a list to be provided?
        </label>
        <asp:RadioButtonList ID="radListDespatchListToBeProvided" runat="server" CssClass="radio-table-alt">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>
    <li>
        <label for="radListCalledOff">
             Get called off by Molson Coors:
        </label>
        <asp:RadioButtonList ID="radListCalledOff" runat="server" CssClass="radio-table-alt">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>
    <li>
        <label for="radListHighValueItems">
            Pre-collated kit - distribution only:
        </label>
        <asp:RadioButtonList ID="radListPreCollatedKit" runat="server" CssClass="radio-table-alt">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
    </li>
</ol> 
</fieldset>

<asp:PlaceHolder ID="plcSaveButton" runat="server">
    <div id="submit_btn" class="btn btn_orange">
        <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSaveKit">Save</asp:LinkButton><span></span>
    </div> 
</asp:PlaceHolder>

<asp:PlaceHolder ID="plcNewQuote" runat="server" Visible="false">
    <div id="submit_btn" class="btn btn_orange">
        <asp:LinkButton ID="lnkNewQuote" runat="server">Generate New Quote</asp:LinkButton><span></span>
    </div> 
</asp:PlaceHolder>
