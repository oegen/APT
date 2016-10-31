<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdditionalElementListing.aspx.vb" Inherits="Elements_AdditionalElementListing" MasterPageFile="~/MasterPages/MasterPage.master" Title="Additional Element Listing" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Element/AdditionalElementsListing.ascx" TagName="AdditionalElementListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="ADDITIONAL_ELEMENTS" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Premium Product" Description="See the premium products for this project" />
                 
            <!--  GENERIC FORM -->     
                <div class="aptform gen-form">
                    <asp:HyperLink CssClass="adminLink" ID="hypAddNew" runat="server">Add New Product</asp:HyperLink>

                    <oegen:AdditionalElementListing ID="ctrlAdditionalElementListing" runat="server" />   
                </div>                    
            <!--  END GENERIC FORM -->              
        <!--PROJECT LIST ENDS--> 
            </div>
                      
    </div>
    <!--CONTENT ENDS-->              
</div>

</asp:Content>

