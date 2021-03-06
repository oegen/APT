﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WorkingWeekException.aspx.vb" Inherits="Admin_Working_Week_Exceptions_WorkingWeekException"
 MasterPageFile="~/MasterPages/MasterPage.master" Title="Working Week Exception" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/WorkingException/WorkingWeekException.ascx" TagName="WorkingWeekException" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="WORKING_WEEK_EXCEPTION"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Working Week Exceptions" 
                    Description="Working Week Exceptions" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:WorkingWeekException ID="ctrlWorkingWeekException" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>