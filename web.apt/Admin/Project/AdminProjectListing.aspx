<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminProjectListing.aspx.vb" Inherits="Admin_Project_AdminProjectListing" 
MasterPageFile="~/MasterPages/Admin.master" Title="Full Project Listing" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Project/AdminProjectListing.ascx" TagName="AdminProjectListing" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="FULL_PROJECT_LISTING"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">                   
            <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />

            <nav id="filter-projects" class="group">
                <ul>
                    <li><span class="filter-titles">Filter By:</span></li>
                    <li>
                        <asp:DropDownList ID="ddlFilterMode" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Archived" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Stopped" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </ul>
            </nav>
        
            <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Project Listing" Description="Project Management" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:AdminProjectListing ID="ctrlAdminProjectListing" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
            <!--PROJECT LIST ENDS-->
            </div>           
    </div>
    <!--CONTENT ENDS-->
                    
</div>
</asp:Content>
