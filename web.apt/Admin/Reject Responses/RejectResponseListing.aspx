<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RejectResponseListing.aspx.vb" Inherits="Admin_Reject_Responses_Reject_ResponseListing" 
    MasterPageFile="~/MasterPages/Admin.master" Title="Reject Response Listings" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/ArcResponses/ArcResponseListing.ascx" TagName="ArcResponseListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="ARC_RESPONSE_MANAGER" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Reject Response Listing" Description="Reject Response Manager" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypAddRejectResponse" CssClass="adminLink" runat="server" NavigateUrl="~/Admin/Reject Responses/RejectResponse.aspx">Add Reject Response</asp:HyperLink>
                    <oegen:ArcResponseListing ID="ctrlRejectResponse" runat="server"></oegen:ArcResponseListing>
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>              
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
