<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewReserveTime.aspx.vb" Inherits="NewReserveTime"  MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Reserve Time/NewReserveTime.ascx" TagName="ReserveTime" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagName="SubNavProject" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ProjectHeader.ascx" TagName="ProjectHeader" TagPrefix="oegen" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" SelectedItem="RESERVE_PROJECT_TIME" />
    <!--SECONDARY NAV ENDS-->

    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:ProjectHeader ID="ctrlProjectHeader" runat="server" />
        <!--PROJECT LIST STARTS-->           
        <hr />

        <div id="gen-inner">
            <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Reserve Time" ToolTipText="Form Information to make it easier for the user. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source."></oegen:ContentTitle>

            <!--  GENERIC FORM -->     
            <div class="aptform gen-form">
                <oegen:ReserveTime ID="ctrlReserveTime" runat="server" Editable="true" />
            </div>                    
            <!--  END GENERIC FORM -->      
                    
        <!--PROJECT LIST ENDS--> 
        </div>
                      
    </div>
    <!--CONTENT ENDS-->              
</div>

</asp:Content>