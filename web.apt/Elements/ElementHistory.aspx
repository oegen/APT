<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElementHistory.aspx.vb" Inherits="Elements_ElementHistory" 
 MasterPageFile="~/MasterPages/MasterPage.master" Title="Element History" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentHeader.ascx" TagName="ContentHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/EntityHistory.ascx" TagName="EntityHistory" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="EDIT_PROJECT" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->
               
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Element History" Description="Element History" />
                 
                <!--  GENERIC FORM -->                           
                <div class="aptform gen-form">
                    <oegen:EntityHistory ID="ctrlEntityHistory" runat="server" />
                </div> 
            </div>                        
            <!--  END GENERIC FORM --> 
              
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
