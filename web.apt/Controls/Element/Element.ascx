<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Element.ascx.vb" Inherits="Controls_Project_Element" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/SimpleMultiLineTextBox.ascx" TagPrefix="oegen" TagName="MultiLineTextBox" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox"%>
<%@ Register Src="~/Controls/Element/ElementSchemaBuilder.ascx" TagPrefix="oegen" TagName="ElementSchemaBuilder" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropDownList" %>
<%@ Register Src="~/Controls/Generic/DateTimePicker.ascx" TagName="DatePicker" TagPrefix="oegen" %>

 <div class="aptform group">
    
    <!--Page subnav-->                                           
    <ul id="ulElementActions" runat="server" class="page-subnav" visible="false">
        <li>
            <asp:HyperLink ID="hypViewHistory" runat="server" CssClass="round">
                <ins><span class="system-buttons view-history">
                    view-history
                </span></ins>
            </asp:HyperLink>
        </li>
        <li>
            <asp:LinkButton ID="lnkStartStop" runat="server" CssClass="round">
                <ins>
                    <span id="spanStartStopButton" runat="server" class="system-buttons delete">
                        <asp:Literal ID="ltlStopStartText" runat="server"></asp:Literal>
                    </span>
                </ins>
            </asp:LinkButton>
        </li>
    </ul>

    <fieldset> 
	    <legend>Element Details</legend>
        <ol>
            <li><oegen:SimpleDropDownList ID="ddlType" runat="server" LabelText="Type:" AutoPostBack="true" ValidationGroup="saveElement" /></li>
            <li id="liSubclass" runat="server" visible="false"><oegen:SimpleDropDownList ID="ddlSubclassType" runat="server" LabelText="Subclass Type:" ValidationGroup="saveElement" AutoPostBack="true" /></li>
            <li><oegen:SimpleTextBox ID="txtElementName" runat="server" LabelText="Element Name:" MaxLength="50" /></li>
            <li><oegen:MultiLineTextBox ID="txtDescription" runat="server" LabelText="Element Description:" MaxLength="1000" 
                ValidationGroup="saveElement" Required="true" /></li>
            <li><oegen:SimpleDropDownList ID="ddlTrade" runat="server" LabelText="Trade:" ValidationGroup="saveElement" /></li>
            <li><oegen:SimpleDropDownList ID="ddlTermsCondition" runat="server" LabelText="T&Cs" ValidationGroup="saveElement" /></li>
            <li><oegen:SimpleDropDownList ID="ddlBrands" runat="server" LabelText="Brands" ValidationGroup="saveElement" /></li>
            <li><oegen:SimpleDropDownList ID="ddlPage" runat="server" LabelText="Page" ValidationGroup="saveElement" /></li>
            <li><oegen:NumericTextBox ID="txtQuantity" runat="server" LabelText="Quantity:" Required="true" ValidationGroup="saveElement" /></li>
            <li><oegen:NumericTextBox ID="txtNoOfDelAddress" runat="server" LabelText="No of Del Addresses" ValidationGroup="saveElement" /></li>
            <li><oegen:MultiLineTextBox ID="txtDeliveryDetails" runat="server" LabelText="Delivery Details" ValidationGroup="saveElement" /></li>
            <li><oegen:DatePicker ID="txtArtworkDeliveryDate" runat="server" LabelText="Artwork Req'd Date" /></li>
        </ol>
    </fieldset>
    
    <oegen:ElementSchemaBuilder ID="ctlElementInfo" runat="server" />

    <div id="divSubmit" runat="server">
        <div id="submit_btn" class="btn btn_orange">
            <asp:LinkButton ID="lnkSaveElement" runat="server" ValidationGroup="saveElement">Save Element</asp:LinkButton><span></span>
        </div> 
    </div>

    <asp:PlaceHolder ID="plcPrintable" runat="server" Visible="false">
        <div id="right-btn">
            <div class="btn btn_grey">
                <asp:LinkButton ID="btnPrintable" runat="server" Text="Printable Version" /><span></span>
            </div>
        </div>       
    </asp:PlaceHolder>

    <asp:Panel ID="pnlAdditionalElementInfo" runat="server">
        <fieldset>
            <legend>Element Estimated Costings (For studio use only)</legend>
            <ol>
                <li><oegen:SimpleTextBox ID="txtArtworkTime" runat="server" LabelText="Artwork Time (hours):" MaxLength="256" /></li>
                <li><oegen:NumericTextBox ID="txtCostArtwork" runat="server" LabelText="Artwork Cost (£):" MaxLength="9" ValidationGroup="saveElement" /></li>
                <li><oegen:SimpleTextBox ID="txtPrintLeadTimes" runat="server" LabelText="Print Lead Times (days):" MaxLength="256" /></li>
                <li><oegen:NumericTextBox ID="txtCostPrint" runat="server" LabelText="Print Cost (£):" MaxLength="9" ValidationGroup="saveElement" /></li>
                <li><oegen:NumericTextBox ID="txtCostPerItem" runat="server" LabelText="Cost per Item (£):" MaxLength="9" ValidationGroup="saveElement" /></li>
            </ol>
        </fieldset>
    </asp:Panel>

 </div>






