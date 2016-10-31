<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PremiumElementDetails.aspx.vb" Inherits="Elements_PremiumElementDetails" MasterPageFile="~/MasterPages/MasterPage.master" Title="Premium Element Details" %>
<%@ Register Src="~/Controls/Element/PremiumElementDetails.ascx" TagName="PremiumElementDetails" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" /> 
        <!--PROJECT LIST STARTS-->           
            <hr />

                <div id="gen-inner">
                    <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" TipTitle="Premium Product Details" Description="Premium Element Details......" />
                <!--  GENERIC FORM -->      
                    <div class="aptform gen-form">
                        <oegen:PremiumElementDetails ID="ctrlPremiumElementDetails" runat="server" />
                    </div>
                </div>                       
                <!--  END GENERIC FORM -->           
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>

