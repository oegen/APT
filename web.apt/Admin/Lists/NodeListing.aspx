<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NodeListing.aspx.vb" Inherits="Admin_Lists_NodeListing" 
MasterPageFile="~/MasterPages/Admin.master" Title="List Node Listing" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Lists/NodeListing.ascx" TagName="NodeListing" TagPrefix="oegen" %>

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
            <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="List Items" Description="List Items" />  
            <!--  GENERIC FORM -->                                
            <div class="aptform gen-form">
                <asp:HyperLink CssClass="adminLink" ID="hypAddNode" runat="server">Add Node</asp:HyperLink><br />
                <oegen:NodeListing ID="ctrlNodeListing" runat="server" />
            </div>                        
            <!--  END GENERIC FORM -->          
            <!--PROJECT LIST ENDS-->
        </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>

