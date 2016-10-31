<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TagListing.aspx.vb" Inherits="Admin_Tags_TagListing" Title="Tag Listing" MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Tags/TagListing.ascx" TagName="TagListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="TAG_MANAGER" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" /> 
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Tag Listing" Description="Tag Management" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypTag" CssClass="adminLink" runat="server" NavigateUrl="~/Admin/Tags/Tag.aspx">Add New Tag</asp:HyperLink> 
                    <oegen:TagListing ID="ctrlTagListing" runat="server"></oegen:TagListing>
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
