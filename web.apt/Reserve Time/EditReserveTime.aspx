<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditReserveTime.aspx.vb" Inherits="Reserve_Time_EditReserveTime" MasterPageFile="~/MasterPages/MasterPage.master" Title="Additional Element" %>
<%@ Register Src="~/Controls/Reserve Time/EditReserveTime.ascx" TagName="ReserveTime" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/MasterPage.master" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:subnavproject ID="ctrlSubNavProject" runat="server" SelectedItem="VIEW_RESERVED_TIME" />
    <!--SECONDARY NAV ENDS-->

    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->           
            <hr />

            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" 
                    TipTitle="Add/Edit Reserve Time" Description="Add or edit a reserve timing item." />
                 
                <!--  GENERIC FORM -->                               
                <div class="aptform gen-form">
                    <oegen:ReserveTime ID="ctrlReserveTime" runat="server" />
                </div>                       
                <!--  END GENERIC FORM -->
            
            </div> 
                       
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->              
</div>

</asp:Content>