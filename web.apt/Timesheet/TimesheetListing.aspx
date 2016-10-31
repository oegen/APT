<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TimesheetListing.aspx.vb" Inherits="Timesheet_TimesheetListing" MasterPageFile="~/MasterPages/MasterPage.master" Title="Timesheets Listing" %>
<%@ Register Src="~/Controls/Timesheets/UserTimesheetListing.ascx" TagName="UserTimeSheetListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavTimesheet.ascx" TagPrefix="oegen" TagName="SubNavTimesheet" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
        <oegen:subnavtimesheet ID="SubNavTimesheet" runat="server" SelectedItem="TIMESHEET_LISTING" />
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="TIMESHEET"/>
        
        <nav id="filter-projects" class="group">
            <ul>
                <li><span class="filter-titles">Date:</span></li>
                <li>
                    <asp:TextBox ID="txtDateOfWork" runat="server" CssClass="datepicker"></asp:TextBox>
                    <asp:LinkButton ID="lnkDateOfWork" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                </li>
            </ul>
        </nav>

        <!--PROJECT LIST STARTS-->
        <hr />
        <div id="gen-inner">
            <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Timesheet Listing" Description="View timesheet entries here." />  
            <!--  GENERIC FORM -->                                
            <div class="aptform gen-form">
                <oegen:UserTimeSheetListing ID="ctrlTimesheetListing" runat="server" />
            </div>                        
            <!--  END GENERIC FORM -->
        </div>         
        <!--PROJECT LIST ENDS-->               
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>
