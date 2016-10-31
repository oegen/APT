<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BlockMeetingException.aspx.vb" Title="Block Meeting Exception" 
Inherits="Admin_Working_Exceptions_BlockMeetingException" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/WorkingException/BlockMeetingException.ascx" TagName="BlockMeetingException" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="BLOCK_MEETING_EXCEPTION"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Block Meeting Exceptions" 
                    Description="Block Meeting Exceptions" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:BlockMeetingException ID="ctrlBlockMeetingException" runat="server" />    
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
