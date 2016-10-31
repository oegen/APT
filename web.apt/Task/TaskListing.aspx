<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TaskListing.aspx.vb" Inherits="Task_TaskListing" Title="Your Task Listing" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Navigation/SubNavEmpty.ascx" TagName="SubNav" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Tasks/TaskListing.ascx" TagName="TaskListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Generic/Paging.ascx" TagPrefix="oegen" TagName="Paging" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="maincontent" class="group menu-closed">
    
    <!-- Sub Nav -->
    <oegen:SubNav ID="ctrlSubNav" runat="server" />
     
    <!--CONTENT STARTS-->
    <div id="content" class="group">
                    
        <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="TASK_LIST" />
                          
        <!--LIST STARTS-->
        <div id="main-content-area" class="group">

            <div id="section-wrap">

                <!-- Filter -->
                <nav id="filter-projects" class="group">
                    <ul>
                        <li><span class="filter-titles">Filter by:</span></li>
                        <li><asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true"></asp:DropDownList></li>
                        <li>
                            <asp:TextBox ID="txtOwner" runat="server" onclick="this.value='';">Owner</asp:TextBox>
                            <asp:LinkButton ID="lnkGoOwner" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                        </li>
                        <li><span class="filter-titles">Sort by:</span></li>
                        <li>
                            <asp:DropDownList ID="ddlSortBy" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkClear" runat="server" CssClass="input_btn">Clear</asp:LinkButton>
                        </li>
                    </ul>
                </nav>

               

                <div id="gen-inner">

                    <oegen:Paging ID="ctrlPagingTop" runat="server" />

                    <div id="apt-list">

                        <asp:Repeater ID="rptrProjectListing" runat="server">
                            <ItemTemplate>
                                <h2>
                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                    <span class="ain"><asp:Label ID="lblProjectAIN" runat="server"></asp:Label></span>
                                </h2>
                                <ul class="<%# GetItemClass(Container.ItemIndex) %>">
                                    <oegen:TaskListing ID="ctrlTaskListing" runat="server" OnEmptyTaskListParentVisibleAlteration="true"></oegen:TaskListing>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>

                        <h2><asp:Label ID="lblNoResults" runat="server" Visible="false">You currently have no tasks assigned to you.</asp:Label></h2>

                    </div>

                    <oegen:Paging ID="ctrlPagingBottom" runat="server" />

                </div>

            </div>
                            
        </div>
        <!--LIST ENDS-->
                    
    </div>
    <!--CONTENT ENDS-->
                    
</div>

</asp:Content>