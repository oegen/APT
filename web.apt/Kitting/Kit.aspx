<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Kit.aspx.vb" Inherits="Kitting_Kit" 
MasterPageFile="~/MasterPages/MasterPage.master" Title="Kitting" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Kitting/Kit.ascx" TagName="Kit" TagPrefix="oegen" %>
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

                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Kitting" Description="Kit..." />
                 
            <!--  GENERIC FORM -->
                <div class="aptform gen-form">
                    <oegen:Kit ID="ctrlKit" runat="server" />
                </div>                     
            <!--  END GENERIC FORM -->

            </div> 
            
                          
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
