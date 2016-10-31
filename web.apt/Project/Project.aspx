<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Project.aspx.vb" Inherits="Project_Project" MasterPageFile="~/MasterPages/MasterPage.master" %>
<%@ Register Src="~/Controls/Navigation/SubNavProject.ascx" TagPrefix="oegen" TagName="SubNavProject" %>
<%@ Register Src="~/Controls/Layout/ContentHeader.ascx" TagPrefix="oegen" TagName="ContentHeader" %>
<%@ Register Src="~/Controls/Project/ProjectTags.ascx" TagPrefix="oegen" TagName="ProjectTags" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagPrefix="oegen" TagName="DocumentOverview" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
    <asp:Literal ID="ltlTabJavascript" runat="server">
    
    </asp:Literal>

    <div id="maincontent" class="group">
        <oegen:SubNavProject ID="ctrlSubNavProject" runat="server" />

    <div id="content" class="group">
        <oegen:ContentHeader ID="ctrlContentHeader" runat="server" />

        <nav>
            <div id="tabs" class="group">                
                <div id="tab-nav-wrap" class="group">
                    <ul class="group">
                        <li><a href="../tab_info.htm">Dashboard</a></li>
                        <li><a href="../tab_info.htm">Info</a></li>
                        <li><a href="ProjectTaskList.aspx">Tasks</a></li>
                        <li><a href="ProjectElement.aspx">Elements</a></li>
                        <li><a href="ProjectDocuments.aspx">Documents</a></li>
                        <li><a href="ProjectTags.aspx">Tags</a></li>
                    </ul>
                </div>
                        
                <div id="tabs-1" class="tabswrap">                       
                    <!-- Insert dashboard control -->
                    
                </div>
            </div>
        </nav>

    </div>
 </div>


</asp:Content>