<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserListing.aspx.vb" Inherits="Admin_Users_UserListing" MasterPageFile="~/MasterPages/Admin.master" Title="User Listing" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Users/UserListing.ascx" TagName="UserListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="USER_MANAGER"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />

           <nav id="filter-projects" class="group">
            <ul>
                <li><span class="filter-titles">Username:</span></li>
                <li>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkUsername" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                </li>
                <li><span class="filter-titles">Surname</span></li>
                <li>
                    <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkSurname" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                </li>
            </ul>
        </nav>
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
            <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="User Listing" Description="User Management" />  
            <!--  GENERIC FORM -->                                
            <div class="aptform gen-form">
                <asp:HyperLink ID="hypAddUser" runat="server" CssClass="adminLink" NavigateUrl="~/Admin/Users/User.aspx">Add User</asp:HyperLink>
                <oegen:UserListing ID="ctrlUserListing" runat="server" />
            </div>                        
            <!--  END GENERIC FORM -->          
        <!--PROJECT LIST ENDS-->
        </div>           
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
