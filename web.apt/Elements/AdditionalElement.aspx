<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdditionalElement.aspx.vb" Inherits="Elements_AdditionalElement" MasterPageFile="~/MasterPages/MasterPage.master" Title="Additional Element" %>
<%@ Register Src="~/Controls/Element/AdditionalElement.ascx" TagName="AdditionalElement" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="ADDITIONAL_ELEMENTS" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Premium Products" Description="See the premium products for this project" />
                 
                <!--  GENERIC FORM -->                               
                <div class="aptform gen-form">
                    <oegen:AdditionalElement ID="ctrlAdditionalElement" runat="server" />
                </div>                       
                <!--  END GENERIC FORM -->
            
            </div> 
                       
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->              
</div>

</asp:Content>
