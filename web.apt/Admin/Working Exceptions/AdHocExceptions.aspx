<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdHocExceptions.aspx.vb" Inherits="Admin_Working_Exceptions_AdHocExceptions"
MasterPageFile="~/MasterPages/MasterPage.master" Title="Ad Hoc Exceptions" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/WorkingException/AdHocException.ascx" TagName="AdHocException" TagPrefix="oegen" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="AD_HOC_EXCEPTION"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Ad Hoc Exceptions" 
                    Description="Ad Hoc Exceptions" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:AdHocException ID="ctrlAdHocException" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>