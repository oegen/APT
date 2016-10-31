<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KitListing.aspx.vb" Inherits="Kitting_KitListing" MasterPageFile="~/MasterPages/MasterPage.master" Title="Kit Listing" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Kitting/KitListing.ascx" TagName="KitListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="KITTING" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
               
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
                  
        <!--PROJECT LIST STARTS-->             
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" 
                    TipTitle="Kit Listing" Description="Kit Listing..." />
                 
                <!--  GENERIC FORM -->      
                <div class="aptform gen-form">
                    <asp:HyperLink ID="hypKit" runat="server" CssClass="adminLink">Add New Kit</asp:HyperLink><br />
                    <asp:HyperLink ID="hypKitBrief" runat="server" CssClass="adminLink">Final Kitting Brief</asp:HyperLink>
                    <oegen:KitListing ID="ctrlKitListing" runat="server" />
                </div>                      
                <!--  END GENERIC FORM -->
                
            </div>                
        <!--PROJECT LIST ENDS--> 
                    
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
