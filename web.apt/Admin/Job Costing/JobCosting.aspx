<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobCosting.aspx.vb" Inherits="Admin_Job_Costing_JobCosting" MasterPageFile="~/MasterPages/Admin.master" Title="Job Costings" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavAdmin.ascx" TagName="SubNavAdmin" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/Lists/AptListListing.ascx" TagName="AptListListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Admin/JobCostings/JobCosting.ascx" TagName="JobCostings" TagPrefix="oegen" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavAdmin ID="ctrlSubNavAdmin" runat="server" SelectedItem="JOB_COSTING" />  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="ADMIN" />

        <nav id="filter-projects" class="group">
            <ul>
                <li><span class="filter-titles">Filter by:</span></li>
                <li><asp:DropDownList ID="ddlAptType" runat="server" AutoPostBack="true"></asp:DropDownList></li>
            </ul>
        </nav>
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
                <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="Job Listing" Description="Job Listing" />  
                <!--  GENERIC FORM -->                                
                <div class="aptform gen-form">
                    <oegen:JobCostings ID="ctrlJobCostings" runat="server" />        
                </div>                        
                <!--  END GENERIC FORM -->          
                <!--PROJECT LIST ENDS-->
            </div>               
    </div>
    <!--CONTENT ENDS-->                  
</div>

</asp:Content>


