<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectTaskList.aspx.vb" Inherits="Project_ProjectTaskList" Title="Project Task Lists" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Tasks/TaskListing.ascx" TagName="TaskListing" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="tabswrap">

    <div id="tabs-inner" class="group">  

        <span class="tabs-titles">
            <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Your Tasks" ToolTipText="All tasks in which require you're completion appear in the task listing page."></oegen:ContentTitle>
        </span>

        <div id="apt-list" class="tasks group">
            <ul>
                <oegen:TaskListing ID="ctrlTaskListing" runat="server" />
            </ul>
        </div>
    
    </div>
</div>

</asp:Content>