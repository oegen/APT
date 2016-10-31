<%@ Page Title="Project Role Global Change" Language="VB" AutoEventWireup="false" CodeFile="ProjectRoleGlobalChange.aspx.vb" Inherits="Admin_Project_Role_Global_Change_ProjectRoleGlobalChange" 
MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/ProjectRoleGlobalChange/ProjectRoleGlobalChange.ascx" TagName="ProjectRoleGlobalChange" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="PROJECT_ROLE_GLOBAL_CHANGE" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Project Role Global Change" Description="Type" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                   <oegen:ProjectRoleGlobalChange ID="ctrlProjectRoleGlobalChange" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>