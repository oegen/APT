<%@ Page Language="VB" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="false" CodeFile="FullTimesheetListing.aspx.vb" 
Inherits="Admin_Timesheets_FullTimesheetListing" Title="Full Timesheet Listing" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Timesheets/FullTimesheetsListing.ascx" TagName="FullTimesheetListing" TagPrefix="oegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="FULL_TIMESHEET_LISTING"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">                   
            <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />

            <nav id="filter-projects" class="group">
                <ul>
                    <li><span class="filter-titles">Surname:</span></li>
                    <li>
                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lnkSurname" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                    </li>

                    <li><span class="filter-titles">Project AIN</span></li>
                    <li>
                        <asp:TextBox ID="txtProject" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lnkProject" runat="server" CssClass="input_btn" ValidationGroup="vldSearchProject">Go</asp:LinkButton>
                    </li>

                    <li><span class="filter-titles">Date</span></li>
                    <li>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="datepicker"></asp:TextBox>
                        <asp:LinkButton ID="lnkDate" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                    </li> 
                </ul>
            </nav>
        
            <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Timesheet Listing" Description="Timesheet Management" />  

                <asp:RegularExpressionValidator ID="regProject" runat="server" ValidationExpression="(^\d*\.?\d*[0-9]+\d*$)|(^[0-9]+\d*\.\d*$)" 
                            ControlToValidate="txtProject" ValidationGroup="vldSearchProject" Display="Dynamic">Please the project AIN</asp:RegularExpressionValidator>
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:FullTimesheetListing ID="ctrlFullTimesheetListing" runat="server" />
                </div>                        
                <!--  END GENERIC FORM -->          
            <!--PROJECT LIST ENDS-->
            </div>           
    </div>
    <!--CONTENT ENDS-->
                    
</div>
</asp:Content>

