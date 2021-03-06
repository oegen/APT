﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectBBCItemsListing.aspx.vb" Inherits="Elements_ProjectBBCItemsListing" 
MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Element/ProjectBBCItemListing.ascx" TagName="BBCItemListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="BBC_ITEMS" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
         
        <!--PROJECT LIST STARTS-->
               
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="BBC Items" Description="BBC Items......" />
                 
                <!--  GENERIC FORM -->
                                                 
                    <div class="aptform gen-form">
                        <asp:HyperLink ID="hypAddBBCItem" runat="server" CssClass="adminLink">Add BBC Item</asp:HyperLink>
                        <oegen:BBCItemListing ID="ctrlBBCItemListing" runat="server" />
                    </div>                         
                <!--  END GENERIC FORM --> 
              </div>
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>