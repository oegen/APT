<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectCostings.ascx.vb" Inherits="Controls_Project_ProjectCostings" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen" %>

<fieldset>
    <legend>Initial Costings</legend>
    <ol>
        <li>
           <asp:Label runat="server" AssociatedControlID="lblArtworkCosting" EnableViewState="false">Artwork</asp:Label>
           <asp:Label ID="lblArtworkCosting" runat="server"></asp:Label>
        </li>
        <li>
           <asp:Label runat="server" AssociatedControlID="lblPrint" EnableViewState="false">Print</asp:Label>
           <asp:Label ID="lblPrint" runat="server"></asp:Label>
        </li>
        <li> 
           <asp:Label runat="server" AssociatedControlID="lblKitting" EnableViewState="false">Kitting</asp:Label>
           <asp:Label ID="lblKitting" runat="server"></asp:Label>
        </li>
        <li>
           <asp:Label runat="server" AssociatedControlID="lblPremiums" EnableViewState="false">Premiums</asp:Label>
           <asp:Label ID="lblPremiums" runat="server"></asp:Label>
        </li>
    </ol>
</fieldset>

<fieldset>
    <legend>Other Costings</legend>
    <ol>
        <li>
            <oegen:NumericTextBox ID="txtAddStudioCosts" runat="server" LabelText="Add Studio Costs" 
                ValidationGroup="vldCostings" />
        </li>
        <li>
            <oegen:NumericTextBox ID="txtFinalStudioCosts" runat="server" LabelText="Final Studio Costs" 
                ValidationGroup="vldCostings" />
        </li>
        <li>
            <oegen:NumericTextBox ID="txtEstimatePrintCost" runat="server" LabelText="Estmate Print Costs" 
                ValidationGroup="vldCostings"/>
        </li>
        <li>
            <oegen:NumericTextBox ID="txtFinalPrintCosts" runat="server" LabelText="Final Print Costs" 
                ValidationGroup="vldCostings"/>
        </li>
    </ol>
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldCostings">Save Costings</asp:LinkButton>
    <span></span>
</div>