<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="false" CodeFile="Co-ordinator Report.aspx.vb" Inherits="Reports_Co_ordinator_Report" %>
<%@ Register TagPrefix="oegen" TagName="DateTimePicker" Src="~/Controls/Generic/DateTimePicker.ascx" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavReports.ascx" TagName="SubNavReports" TagPrefix="oegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavReports ID="ctrlSubNavReports" runat="server" SelectedItem="COORDINATOR"/>  
    <!--SECONDARY NAV ENDS-->
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="REPORTS" />
        
        <!--PROJECT LIST STARTS-->
            <hr />
            <div id="gen-inner">
            <oegen:GenericTitle ID="ctrlGenericTitle" runat="server" TipTitle="User Listing" Description="User Management" />  
            <!--  GENERIC FORM -->                                
            <div class="aptform gen-form">
    <div>
        <table>
            <tr>
                <th><asp:Literal runat="server" ID="ltlFilterTable1" /></th>
                <th>No. of Projects</th>
                <th>% share</th>
                <th>Est. Time</th>
                <th>Acual Time</th>
                <th>% accuracy</th>
            </tr>
            <asp:Repeater runat="server" ID="rptrCoordinatorTable1" OnItemDataBound="rptrCoordinatorTable1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="ltlName" /></td>
                        <td><asp:Literal runat="server" ID="ltlNoOfProjects" /></td>
                        <td><asp:Literal runat="server" ID="ltlPercentShareProjects" /></td>
                        <td><asp:Literal runat="server" ID="ltlEstimatedTime" /></td>
                        <td><asp:Literal runat="server" ID="ltlActualTime" /></td>
                        <td><asp:Literal runat="server" ID="ltlAccuracy" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>      
            <tr>
                <td></td>                
                <td></td>
                <td>Total</td>
                <td><asp:Literal runat="server" ID="ltlTotalProjectsTable1" /></td>
                <td></td>
                <td></td>
            </tr>      
        </table>

        <br />

        <table>
            <tr>
                <th><asp:Literal runat="server" ID="ltlFilterTable2" /></th>
                <th>No. of Projects</th>
                <th>Overrun Projects</th>
                <th>% accuracy</th>
                <th>Overrun Time</th>
            </tr>
            <asp:Repeater runat="server" ID="rptrCoordinatorTable2" OnItemDataBound="rptrCoordinatorTable2_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><asp:Literal runat="server" ID="ltlName" /></td>
                        <td><asp:Literal runat="server" ID="ltlNoOfProjects" /></td>
                        <td><asp:Literal runat="server" ID="ltlOverrunProjects" /></td>
                        <td><asp:Literal runat="server" ID="ltlPercentAccuracy" /></td>
                        <td><asp:Literal runat="server" ID="ltlOverrunTime" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>      
            <tr>
                <td></td>
                <td>Total</td>
                <td><asp:Literal runat="server" ID="ltlTotalProjectsTable2" /></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>      
        </table>






        <asp:RadioButtonList runat="server" CssClass="reports-RBtns" ID="rlstDateFilter">
            <asp:ListItem Selected="True">All</asp:ListItem>
            <asp:ListItem>YTD</asp:ListItem>
            <asp:ListItem>MAT</asp:ListItem>
            <asp:ListItem>Date Range</asp:ListItem>
        </asp:RadioButtonList>      
        


        <fieldset class="reporting">
            <legend>Date Range</legend>
            <ol class="reporting-list">
                <li><oegen:DateTimePicker runat="server" ID="dtpBegin" LabelText="Start Date" /></li>
                <li><oegen:DateTimePicker runat="server" ID="dtpEnd" LabelText="End Date" /></li>
                <li><label></label>                
                <asp:DropDownList runat="server" ID="ddlProjectStatus">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Completed</asp:ListItem>
                    <asp:ListItem>Archived</asp:ListItem>
                    <asp:ListItem>In Progress</asp:ListItem>
                    <asp:ListItem>Stopped &amp; Cancelled</asp:ListItem>
                </asp:DropDownList></li>
            </ol>
        </fieldset>         
                             

        <div class="submit_btn btn btn_grey">
            <asp:Button runat="server" ID="Button1" OnClick="btnSubmit_Click" Text="Download" /><span></span>
        </div>

        <div id="right-btn" class="submit_btn btn btn_orange">
            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit Filter" /><span></span>
        </div>
                        
        


    </div>

    </div>                        
            <!--  END GENERIC FORM -->          
        <!--PROJECT LIST ENDS-->
        </div>           
    </div>
    <!--CONTENT ENDS-->
                    
</div>
</asp:Content>

