<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementArtworkDetails.ascx.vb" Inherits="Controls_Project_ElementArtworkDetails" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagName="NumericTextBox" TagPrefix="oegen"%>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagName="SimpleTextBox" TagPrefix="oegen" %>

 <div class="aptform group">

     <fieldset> 
        <legend>Element Additional Print Details</legend>
        <ol>
            <li><oegen:NumericTextBox ID="txtNoOfColour" runat="server" LabelText="No of Colours" /></li>
            <li><oegen:SimpleTextBox ID="txtFinishedSize" runat="server" LabelText="Finished Size" MaxLength="256" /></li>
            <li><oegen:SimpleTextBox ID="txtMaterial" runat="server" LabelText="Material" MaxLength="256" /></li>
            <li><oegen:SimpleTextBox ID="txtFinishing" runat="server" LabelText="Finishing" MaxLength="256" /></li>
            <li><oegen:NumericTextBox ID="txtNoOfDelAdds" runat="server" LabelText="No of Del adds" /></li>
            <li><oegen:SimpleTextBox ID="txtDeliveryDetails" runat="server" LabelText="Delivery Details" MaxLength="256" /></li>
            <li><oegen:NumericTextBox ID="txtPackSize" runat="server" LabelText="Pack Size" /></li>
        </ol>
    </fieldset>

    <div id="submit_btn" class="btn btn_orange" runat="server">
        <asp:LinkButton ID="lnkSave" runat="server">Save Artwork Details</asp:LinkButton><span></span>
    </div>

 </div>



        
