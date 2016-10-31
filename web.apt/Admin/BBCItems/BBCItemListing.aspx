<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BBCItemListing.aspx.vb" Inherits="Admin_BBCItems_BBCItemListing" MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/BBCItems/BBCItemListing.ascx" TagName="BBCItemListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="BBC_ITEM_LISTING" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
        <hr />
        <div id="gen-inner">
            <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Type Listing" Description="BBC Item Listing" />  
            <!--  GENERIC FORM -->                                
            <div class="aptform gen-form"> 
            <asp:HyperLink ID="hypAddType" CssClass="adminLink" runat="server" NavigateUrl="~/Admin/BBCItems/BBCItem.aspx">Add a new BBC Item</asp:HyperLink>
                <oegen:BBCItemListing ID="ctrlBBCItemListing" runat="server" />
            </div>                        
            <!--  END GENERIC FORM -->          
            <!--PROJECT LIST ENDS-->
        </div>
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>