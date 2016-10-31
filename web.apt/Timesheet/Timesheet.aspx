<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Timesheet.aspx.vb" Inherits="Timesheet_Timesheet" MasterPageFile="~/MasterPages/MasterPage.master" Title="Timesheets" %>
<%@ Register Src="~/Controls/Timesheets/Timesheet.ascx" TagName="Timesheet" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavTimesheet.ascx" TagPrefix="oegen" TagName="SubNavTimesheet" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:SubNavTimesheet ID="SubNavTimesheet" runat="server" SelectedItem="TIMESHEET_ENTRY" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="TIMESHEET"/>
             
        <!--PROJECT LIST STARTS-->        
            <hr />
                <div id="gen-inner">
                    <oegen:GenericTitle ID="ctrlMagicalToolTip" runat="server" TipTitle="Timesheet Entry" Description="Enter your timesheets here" />
                    <!--  GENERIC FORM -->                             
                    <div class="aptform gen-form">
                        <oegen:Timesheet ID="ctrlTimesheet" runat="server" />
                    </div>                         
                    <!--  END GENERIC FORM -->
                </div>             
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>




