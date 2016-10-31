<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BlockMeetingExceptionListing.aspx.vb" Title="Block Meeting Exceptions Listing" 
Inherits="Admin_Working_Exceptions_BlockMeetingExceptionListing" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/WorkingException/BlockMeetingExceptionListing.ascx" TagName="BlockMeetingExceptionListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="BLOCK_MEETING_EXCEPTION" /> 
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Block Meeting Exception Listing" 
                    Description="Block Meeting Exception Listing" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypAddBlockMeetingEx" runat="server" CssClass="adminLink" NavigateUrl="~/Admin/Working Exceptions/BlockMeetingException.aspx">Add Block Meeting Exception</asp:HyperLink>
                    <oegen:BlockMeetingExceptionListing ID="ctrlBlockMeetingExceptionListing" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
