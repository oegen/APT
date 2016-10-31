<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AuditTrail.aspx.vb" Inherits="Project_AuditTrail" MasterPageFile="~/MasterPages/MasterPage.master" Title="Audit Trail" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Project/AuditTrail.ascx" TagName="AuditTrail" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div id="maincontent" class="group">
        <!--SECONDARY NAV STARTS-->
         <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="AUDIT_TRAIL" />
        <!--SECONDARY NAV ENDS-->

        <!--CONTENT STARTS-->
        <div id="content" class="group">
               
            <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
                  
            <!--PROJECT LIST STARTS-->              
                <hr />

                <div id="gen-inner">

                    <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" TipTitle="Audit Trail" Description="Audit trail..." />
                 
                <!--  GENERIC FORM -->
                    <div class="aptform gen-form">
                        <oegen:AuditTrail ID="ctrlAuditTrail" runat="server" />
                    </div>                    
                <!--  END GENERIC FORM -->
                        
                </div>  

            <!--PROJECT LIST ENDS-->               
        </div>
        <!--CONTENT ENDS-->
                    
    </div>

</asp:Content>
