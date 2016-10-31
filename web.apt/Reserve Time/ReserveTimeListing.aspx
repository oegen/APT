<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReserveTimeListing.aspx.vb" Inherits="Reserve_Time_ReserveTimeListing" MasterPageFile="~/MasterPages/MasterPage.master" Title="Reserved Time Listing" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Reserve time/ReservedTimeListing.ascx" TagName="ReserveTimeListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Reserve time/FreelancerTimeListing.ascx" TagName="FreelancerListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="VIEW_RESERVED_TIME" />
    <!--SECONDARY NAV ENDS-->

    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->           
        <hr />

        <div id="gen-inner">
            <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                TipTitle="Reserve Time Listing" Description="All reserved times for the specified project." />
            
            <!--  GENERIC FORM -->     
            <div class="aptform gen-form">
                <asp:LinkButton CssClass="adminLink" ID="lnkAddNew" runat="server" Visible="false">Add New Reserve Time Item</asp:LinkButton>

                <oegen:ReserveTimeListing ID="ctrlReserveTimeListing" runat="server" />   
            </div>                    
            <!--  END GENERIC FORM -->   
            
            <oegen:GenericTitle ID="GenericTitle1" runat="server" 
                TipTitle="Freelancers Required" Description="All freelancer times for the specified project." /> 
                
            <!-- <h2>Freelancers Required</h2> -->

            <!--  GENERIC FORM -->     
            <div class="aptform gen-form">
                <asp:LinkButton CssClass="adminLink" ID="lnkAddFreelancer" runat="server" Visible="false">Add New Freelancer Time</asp:LinkButton>

                <oegen:FreelancerListing ID="ctrlFreelancerListing" runat="server" />
            </div>                    
            <!--  END GENERIC FORM -->    
                    
        <!--PROJECT LIST ENDS--> 
        </div>
                      
    </div>
    <!--CONTENT ENDS-->              
</div>

</asp:Content>