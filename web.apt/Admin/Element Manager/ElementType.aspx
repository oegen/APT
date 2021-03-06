﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElementType.aspx.vb" Inherits="Admin_Element_Manager_ElementType" Title="Add / Edit Element Type" 
MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Elements/AptType.ascx" TagName="AptType" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="ELEMENT_MANAGER" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Add/Edit Element Type" Description="Type" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:AptType ID="ctrlAptType" runat="server"></oegen:AptType>
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
