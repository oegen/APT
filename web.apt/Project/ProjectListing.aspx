<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectListing.aspx.vb" Inherits="Task_ProjectListing" Title="Project Listing" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Navigation/SubNavEmpty.ascx" TagName="SubNav" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/GenericHeader.ascx" TagName="GenericHeader" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Project/ProjectListing.ascx" TagName="ProjectListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen"  %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="maincontent" class="group menu-closed">
                
        <!-- Sub Nav -->
        <!-- <oegen:SubNav ID="ctrlSubNav" runat="server" /> -->

        <!--CONTENT STARTS-->                  
        <div id="content" class="group">

            <oegen:GenericHeader ID="ctrlGenHeader" runat="server" CurrentSetting="PROJECT_LIST" />
                        
            <!--PROJECT LIST STARTS-->
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
                            <li><span class="filter-titles">In-Trade Date:</span></li>
                            <li>
                                <asp:TextBox ID="txtTradeInDate" runat="server" CssClass="datepicker"></asp:TextBox>
                                <asp:LinkButton ID="lnkTradeInDate" runat="server" CssClass="input_btn">Go</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkClear" runat="server" CssClass="input_btn">Clear</asp:LinkButton>
                            </li>
                        </ul>
                    </nav>

                    <div id="gen-inner">
                        <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Your Projects..." ToolTipText="Your Projects form description..."></oegen:ContentTitle>
                        <oegen:ProjectListing ID="ctrlProjectListing" runat="server" />
                    </div>

                </div>
          
            </div>  
            <!--PROJECT LIST ENDS-->
            
        </div>
        <!--CONTENT ENDS-->
        
    </div>


</asp:Content>
