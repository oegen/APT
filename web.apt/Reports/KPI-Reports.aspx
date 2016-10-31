<%@ Page Language="VB" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="false" CodeFile="KPI-Reports.aspx.vb" Inherits="Reports_KPI_Reports" %>
<%@ Register TagPrefix="oegen" TagName="DateTimePicker" Src="~/Controls/Generic/DateTimePicker.ascx" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericTitle.ascx" TagName="GenericTitle" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Navigation/SubNavReports.ascx" TagName="SubNavReports" TagPrefix="oegen" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="sm" runat="server" />
<div id="maincontent" class="group">
    <!--SECONDARY NAV STARTS-->
    <oegen:SubNavReports ID="ctrlSubNavReport" runat="server" SelectedItem="KPI"/>  
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
                        <th><asp:Literal runat="server" ID="ltlFilter" /></th>
                        <th>No. of Projects</th>
                        <th>% share</th>
                        <th>Acual Time</th>
                        <th>% share</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptrArtworkers" OnItemDataBound="rptrItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="ltlName" /></td>
                                <td><asp:Literal runat="server" ID="ltlNoOfProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlActualTime" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareTime" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater runat="server" ID="rptrDesigners" OnItemDataBound="rptrItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="ltlName" /></td>
                                <td><asp:Literal runat="server" ID="ltlNoOfProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlActualTime" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareTime" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater runat="server" ID="rptrFreeLancers" OnItemDataBound="rptrItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal runat="server" ID="ltlName" /></td>
                                <td><asp:Literal runat="server" ID="ltlNoOfProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareProjects" /></td>
                                <td><asp:Literal runat="server" ID="ltlActualTime" /></td>
                                <td><asp:Literal runat="server" ID="ltlPercentShareTime" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td></td>
                        <td><b>Total</b></td>
                        <td><b><asp:Literal runat="server" ID="ltlTotalProjects" /></b></td>
                        <td><b>Total</b></td>
                        <td><b><asp:Literal runat="server" ID="ltlTotalTime" /></b></td>
                    </tr>
            
                </table>

                <asp:RadioButtonList runat="server" ID="rlstDateFilter" CssClass="reports-RBtns" AutoPostBack="true">
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
      
