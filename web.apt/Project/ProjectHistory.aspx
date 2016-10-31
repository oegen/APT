<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectHistory.aspx.vb" Inherits="Project_ProjectHistory" 
 MasterPageFile="~/MasterPages/MasterPage.master" Title="Project History"%>
 <%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentHeader.ascx" TagName="ContentHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/EntityHistory.ascx" TagName="EntityHistory" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="PROJECT_HISTORY" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->            
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Project History" Description="View all the project history" />
                 
                <!--  GENERIC FORM -->
                                                 
                <div class="aptform gen-form">
                    <oegen:EntityHistory ID="ctrlProjectHistory" runat="server" />
                </div> 
            </div>                        
            <!--  END GENERIC FORM -->               
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
