<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RejectResponse.aspx.vb" Inherits="Admin_Reject_Responses_RejectResponse" Title="Add / Edit Reject Response" 
MasterPageFile="~/MasterPages/Admin.master" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/ArcResponses/ArcResponse.ascx" TagName="ArcResponse" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:subnavadmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="ARC_RESPONSE_MANAGER" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:generictitle ID="ctrlGenericTitle" runat="server" 
                TipTitle="Add Reject Response" Description="Reject Response Manager" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:ArcResponse ID="ctrlArcResponse" runat="server"></oegen:ArcResponse>
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>              
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
