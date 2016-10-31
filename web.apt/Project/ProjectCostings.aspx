<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectCostings.aspx.vb" Inherits="Project_ProjectCostings"
MasterPageFile="~/MasterPages/MasterPage.master" Title="Project Costings" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Project/ProjectCostings.ascx" TagName="ProjectCostings" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="COSTINGS" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
               
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
                  
        <!--PROJECT LIST STARTS-->           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Project Costings" Description="Project Costings..." />
                 
            <!--  GENERIC FORM -->                               
                <div class="aptform gen-form">
                    <oegen:ProjectCostings ID="ctrlProjectCostings" runat="server" />
                </div>                        
            <!--  END GENERIC FORM -->            
        <!--PROJECT LIST ENDS-->
        </div>             
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>

