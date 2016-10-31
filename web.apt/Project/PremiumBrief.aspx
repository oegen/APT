<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PremiumBrief.aspx.vb" Inherits="Project_PremiumBrief" MasterPageFile="~/MasterPages/MasterPage.master" Title="Premium Brief" %>
<%@ Register Src="~/Controls/Element/PremiumBrief.ascx" TagName="PremiumBrief" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="PREMIUM_BRIEF" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->                
            <hr />

            <div id="gen-inner">

                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Outline Premium’s Brief" Description="Outline Premium’s Brief..." />
                 
                <!--  GENERIC FORM -->                           
                <div class="aptform gen-form">
                    <oegen:PremiumBrief ID="ctrlPremiumBrief" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->

           </div>

        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
