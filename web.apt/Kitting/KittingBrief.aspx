<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KittingBrief.aspx.vb" Inherits="Kitting_KittingBrief" MasterPageFile="~/MasterPages/MasterPage.master" Title="Kitting Brief" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Project/ProjectKittingBrief.ascx" TagName="KittingBrief" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="KITTING" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
               
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
                  
        <!--PROJECT LIST STARTS-->             
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Final Kitting Brief" Description="Final Kitting Brief..." />
                 
                <!--  GENERIC FORM -->                                 
                <div class="aptform gen-form">
                    <oegen:KittingBrief ID="ctrlKittingBrief" runat="server" />  
                </div>                         
                <!--  END GENERIC FORM -->               
                <!--PROJECT LIST ENDS-->
            </div>     
                 
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
