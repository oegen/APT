<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdHocListing.aspx.vb" MasterPageFile="~/MasterPages/MasterPage.master"
Inherits="Admin_Working_Exceptions_AdHocListing" Title="Ad Hoc Exceptions Listing" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/WorkingException/AdHocListing.ascx" TagName="AdHocListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="AD_HOC_EXCEPTION"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Ad Hoc Exceptions Listing" 
                    Description="Ad Hoc Exceptions Listing" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypAddAdHoc" runat="server" CssClass="adminLink" NavigateUrl="~/Admin/Working Exceptions/AdHocExceptions.aspx">Add Ad Hocs</asp:HyperLink>
                    <oegen:AdHocListing ID="ctrlAdHocListing" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>