<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User.aspx.vb" Inherits="Admin_Users_User" MasterPageFile="~/MasterPages/Admin.master" Title="Add / Edit User" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Users/AddEditUser.ascx" TagName="AddEditUser" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Users/UserRoles.ascx" TagPrefix="oegen" TagName="UserRoles"%>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="USER_MANAGER"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" /> 
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Add / Edit User" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:AddEditUser ID="ctrlAddEditUser" runat="server" />
                    <oegen:UserRoles ID="ctrlUserRoles" runat="server" Visible="false" />
                </div>                        
                <!--  END GENERIC FORM -->
            </div>              
    </div>
    <!--CONTENT ENDS-->                   
</div>

</asp:Content>

