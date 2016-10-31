<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListListing.aspx.vb" Inherits="Admin_Lists_ListListing" Title="List Listing" 
MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Lists/AptListListing.ascx" TagName="AptListListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="LIST_MANAGER"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="List Listing" Description="List Management" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypAddList" CssClass="adminLink" runat="server" NavigateUrl="~/Admin/Lists/List.aspx">Add List</asp:HyperLink>
                    <oegen:AptListListing ID="ctrlAptListListing" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
